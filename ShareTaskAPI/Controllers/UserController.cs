using System.IdentityModel.Tokens.Jwt;
using System.Reflection.Metadata;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ShareTaskAPI.Context;
using ShareTaskAPI.Entities;
using ShareTaskAPI.Service;

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
                var dbUser = _db.Users.FirstOrDefault(x => x.Username == user.Username && x.Password == user.Password);
                if ( dbUser == null)
                {
                    return NotFound("Uncorrect login or password");
                }
                string userAgent = Request.Headers["User-Agent"].ToString();
                var role = dbUser.IsAdmin == true ? "1" : "0";
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, dbUser.Username.ToString()),
                    new Claim(ClaimTypes.Role,role)
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes("MySecretCooffeeProjKey121205");
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.UtcNow.AddDays(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = "Bearer " + tokenHandler.WriteToken(token);
                if (userAgent.Contains("Mobile"))
                {
                    return Ok(tokenString);
                }
                var claimsIdentity = new ClaimsIdentity(claims,JwtBearerDefaults.AuthenticationScheme);
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
                if (_db.Users.FirstOrDefault(x => x.Username == user.Username) == null)
                {
                    _db.Users.Add(new User()
                    {
                        Username = user.Username,
                        Password = user.Password,
                        Firstname = user.Firstname,
                        Lastname = user.Lastname,
                        Midname = user.Midname,
                        IsAdmin = false
                    });
                    _db.SaveChanges();
                    return Ok("user successfully added");
                }
                return BadRequest("This username all ready exist");
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
                var claimValue = AccountActions.ReturnUserFromCookie(HttpContext, _db).Username;
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
                var username = AccountActions.ReturnUserFromCookie(HttpContext, _db).Username;
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
                string userAgent = Request.Headers["User-Agent"].ToString();
                if (userAgent.Contains("Mobile"))
                {
                    await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                    return Ok();
                }
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
