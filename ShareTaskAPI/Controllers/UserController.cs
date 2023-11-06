using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using ShareTaskAPI.Authorization;
using ShareTaskAPI.Context;
using ShareTaskAPI.Entities;

namespace ShareTaskAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private MyDbContext _db;

        public UserController(MyDbContext db)
        {
            _db = db;
        }
        [HttpPost]
        public async Task<IActionResult> SignIn([FromBody]User user)//разберись
        {
            
            if (_db.Users.FirstOrDefault(x => x.Username == user.Username && x.Password == user.Password)==null)
            {
                return NotFound("Uncorrect login or password");
            }
            
            var role = user.IsAdmin == true ? "Admin" : "User";
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, role)
            };
            
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
            return Ok();
        }
        [Authorize(AuthenticationSchemes ="Cookies")]
        [HttpGet]
        public IActionResult GetAllUsers()
        {
            try
            {
                return Ok(_db.Users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
