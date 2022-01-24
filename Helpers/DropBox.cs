using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dropbox.Api;
using Dropbox.Api.Files;
using Dropbox.Api.Sharing;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Sheldy.DropBoxApi
{
    public class DropBox
    {
        public static string token= "XgiVcQyGI40AAAAAAAAAAS96fEJGP23Db-dieBKyc4WyYji0QUxs81J2-gLv9Nbm";

        public async Task<string> AddRequestedTask(IFormFile file,string fileName)
        {
            using (var dbx = new DropboxClient(token))
            {

                using (var fileStream = new MemoryStream())
                {
                    await file.CopyToAsync(fileStream);
                    fileStream.Position = 0;
                    
                    var metadata = await dbx.Files.UploadAsync(
                    "/checks/"+fileName,
                    WriteMode.Overwrite.Instance,
                    body: fileStream
                    );

                    var result = await dbx.Sharing.CreateSharedLinkWithSettingsAsync("/checks/" + fileName);
                    return result.Url;
                }                    
            }
        }


    }

}
