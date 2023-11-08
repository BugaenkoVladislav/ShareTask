using System.Reflection.Metadata;
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

        [HttpPost("SignUp")]
        public IActionResult SignUp(User user)
        {
            try
            {
                _db.Users.Add(new User()
                {
                    Username = user.Username,
                    Firstname = user.Firstname,
                    Lastname = user.Lastname,
                    Midname = user.Midname,
                    IsAdmin = false
                });
                _db.SaveChanges();
                return Ok("user successfully added");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            
        }
        
        
        [HttpPut("ChangeUser")]
        [Authorize(Policy = "Authorized")]
        public async Task<IActionResult> ChangeUser([FromBody] User userNew)
        {
            try
            {
                var claimValue = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "username")?.Value;
                var user = _db.Users.First(x => x.Username == claimValue);
                user.Username = userNew.Username;
                user.Password = userNew.Password;
                user.Firstname = userNew.Firstname;
                user.Lastname = userNew.Lastname;
                user.Midname = userNew.Lastname;
                user.IsAdmin = false;
                _db.Update(user);
                _db.SaveChanges();
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet("Me")]
        [Authorize(Policy = "Authorized")]
        public IActionResult Me() //порешай с 404
        {
            try
            {
                var username = HttpContext.User.Claims.First(x => x.Type == "username").Value;
                var user = _db.Users.FirstOrDefault(x => x.Username == username);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        
        [HttpPost("SignOut")]
        [Authorize(Policy = "Authorized")]
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
