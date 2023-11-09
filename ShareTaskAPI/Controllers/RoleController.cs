using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShareTaskAPI.Context;
using ShareTaskAPI.Entities;

namespace ShareTaskAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "Authorized")]
    public class RoleController : ControllerBase
    {
        private MyDbContext _db;
        public RoleController(MyDbContext db)
        {
            _db = db;
        }
        
        
        [HttpPost("AddRole/{role}")]
        [Authorize(Policy = "OnlyForAdmin")]
        public IActionResult AddRole([FromRoute] string role)
        {
            try
            {
                _db.Roles.Add(new Role
                {
                    Role1 = role
                });
                _db.SaveChanges();
                return Ok(role);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("GetAllRoles")]
        public IActionResult GetAllRoles()
        {
            try
            {
                return Ok(_db.Roles);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    
        
        [HttpDelete("DeleteRole/{role}")]
        [Authorize(Policy = "OnlyForAdmin")]
        public IActionResult DeleteRole([FromRoute] string role)
        {
            try
            {
                var r = _db.Roles.FirstOrDefault(x => x.Role1 == role);
                if (r == null)
                    return NotFound();
                _db.Roles.Remove(r);
                _db.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        
    }
}
