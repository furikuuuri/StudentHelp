using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sheldy.DropBoxApi;
using Sheldy.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Sheldy.RequestedTasks;

namespace Sheldy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestedTaskController : ControllerBase
    {
        SheldyContext db;
        RequstedTaskList list;

        public RequestedTaskController()
        {
            db = new SheldyContext();
            list = new RequstedTaskList();
        }

        [HttpPost("addRequestTask")]
        public async Task<ActionResult> addRequestedTask([FromForm] IFormFile uploadedFile)
        {
            try
            {
                int userId = Convert.ToInt32(Request.Query.FirstOrDefault(p => p.Key == "userId").Value);
                int taskId = Convert.ToInt32(Request.Query.FirstOrDefault(p => p.Key == "taskId").Value);
                User user = db.Users.FirstOrDefault(p => p.Id == userId);

                Sheldy.Models.Task task = db.Tasks.Include(prop => prop.CategoryTask).FirstOrDefault(p => p.Id == taskId);
                string nameFile = $"{user.Username}-{task.Name}-{task.CategoryTask.Name}-{DateTime.Now}{uploadedFile.FileName}";
                DropBox drop = new DropBox();
                if (uploadedFile != null)//Добавление на дроп бокс
                {

                    string urlCheck = await drop.AddRequestedTask(uploadedFile, nameFile);
                    urlCheck = urlCheck.Replace("?dl=0", "?dl=1");
                    RequestedTask rq = new RequestedTask
                    {
                        TaskId = taskId,
                        UserId = userId,
                        Url = urlCheck,
                        Time = DateTime.Now
                    };
                    list.AddRequstedTask(rq);

                }
                return Ok();
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }





        }

        [HttpGet("getAll")]
        public IEnumerable<RequestedTask> getAll()
        {
            return db.RequestedTasks.Include(p=>p.Task).Include(p=>p.User).ToList();
        }
        [HttpPost("accept")]
        public IActionResult acceptTask(RequestedTask task)
        {
            AvailableTask taskA = new AvailableTask
            {
                TaskId = task.TaskId,
                Time = DateTime.Now,
                UserId = task.UserId,
            };
            db.AvailableTasks.Add(taskA);
            db.SaveChanges();
            task=db.RequestedTasks.FirstOrDefault(p=>p.Id==task.Id);
            db.RequestedTasks.Remove(task);
            db.SaveChanges();
            return Ok();
        }
        [HttpPost("decline")]
        public IActionResult declineTask(RequestedTask task)
        {
            task = db.RequestedTasks.FirstOrDefault(p => p.Id == task.Id);
            db.RequestedTasks.Remove(task);
            db.SaveChanges();
            return Ok();
        }

    }
}
