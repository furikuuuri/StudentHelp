using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sheldy.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.EntityFrameworkCore;
using Sheldy.Repositories;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Sheldy.Controllers
{   
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IOptions<Authentification.AuthOptions> authOption;
        IUserRepository<User> users;

        public UserController(IOptions<Authentification.AuthOptions> authOptions)
        {
            this.authOption = authOptions;
            users = new UserRepository();
        }
        
        [HttpPost("register")]
        public IActionResult RegisterNew(User user)
        {
            try
            {
                user = users.Create(user);
                if (user != null)
                {
                    var token = GenerateJWT(user);
                    return Ok(
                        new
                        {
                            username = user.Username,
                            access_token = token,
                            role = user.Role.RoleName,
                            id = user.Id
                        });
                }
            }
            catch(Exception e)
            {

            }   
            return Unauthorized("Логин уже занят");
        }
        [HttpPost("login")]
        public IActionResult login(User user)
        {
            user = users.GetUser(user);
            if(user!=null)
            {
                
                var token = GenerateJWT(user);
                return Ok(
                    new
                    {
                        username = user.Username,
                        access_token = token,
                        role = user.Role.RoleName,
                         id = user.Id
                    });
            }
            return Unauthorized("Неверный логин или пароль");
        }
        

            private string GenerateJWT(User user)
            {
                var authParams = authOption.Value;

                var securityKey = authParams.GetSymmetricSecurityKey();
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                var claims = new List<Claim>()
                {
                    new Claim(JwtRegisteredClaimNames.Sub,user.Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.UniqueName,user.Username)

                };
                claims.Add(new Claim("role", user.Role.RoleName));
                var token = new JwtSecurityToken(
                    authParams.Issuer,
                    authParams.Audience,
                    claims,
                    expires: DateTime.Now.AddSeconds(authParams.TokenLifeTime),
                    signingCredentials: credentials
                    );
                return new JwtSecurityTokenHandler().WriteToken(token);
            }
    }
}
