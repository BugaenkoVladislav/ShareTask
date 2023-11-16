using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShareTaskAPI.Context;
using ShareTaskAPI.Entities;
using ShareTaskAPI.Service;

namespace ShareTaskAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "Authorized")]
    public class ListController : ControllerBase
    {
        private MyDbContext _db;
        public ListController(MyDbContext db)
        {
            _db = db;
        }

        [HttpPost("AddList")]
        public IActionResult AddList([FromBody]List list)
        {
            try
            {
                var user = AccountActions.ReturnUserFromCookie(this.HttpContext, _db);
                _db.Lists.Add(new List()
                {
                    Name = list.Name,
                    IsPublic = list.IsPublic,
                    Description = list.Description,
                    IdCreator = user.IdUser,
                    Linq = list.Linq
                });
                _db.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("GetMyLists")]
        public IActionResult GetMyLists()
        {
            try
            {
                var user = AccountActions.ReturnUserFromCookie(this.HttpContext, _db).IdUser;
                var list = _db.Lists.Where(x => x.IdCreator == user).ToList();
                return Ok(list);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet("GetMyList/{name}")]
        public IActionResult GetMyList([FromRoute] string name)
        {
            try
            {
                var user = AccountActions.ReturnUserFromCookie(this.HttpContext, _db).IdUser;
                Entities.List? list = _db.Lists.FirstOrDefault(x => x.Name == name && x.IdCreator == user);
                if (list == null)
                    return NotFound("list not found");
                HttpContext.Response.Cookies.Append("idList", Convert.ToString(list.IdList));
                return Ok(list);
                
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("DeleteMyList/{name}")]
        public IActionResult DeleteMyList([FromRoute] string name)
        {
            try
            {
                var user = AccountActions.ReturnUserFromCookie(this.HttpContext, _db);
                var list = _db.Lists.FirstOrDefault(x => x.Name == name && x.IdCreator == user.IdUser);
                if (list == null)
                    return NotFound("list not found");
                _db.Lists.Remove(list);
                _db.SaveChanges();
                return Ok("Success");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
