using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        [HttpPost("SignIn")]
        public async Task<IActionResult> SignIn([FromBody]User user)//разберись
        {
            try
            {
                if (_db.Users.FirstOrDefault(x => x.Username == user.Username && x.Password == user.Password) == null)
                {
                    return NotFound("Uncorrect login or password");
                }

                var role = user.IsAdmin == true ? "Admin" : "User";
                var claims = new List<Claim>
                {
                    new Claim("username", user.Username.ToString()),
                    new Claim("role",role)
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            
        }
        
        
        [HttpGet("GetAllUsers")]
        [Authorize(Policy = "Authorized")]
        public IActionResult GetAllUsers() //порешай с 404
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
        [HttpPost("SignOut")]
        public async Task<IActionResult> SignOut()
        {
            try
            {
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
