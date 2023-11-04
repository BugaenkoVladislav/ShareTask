using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShareTaskAPI.Context;

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
