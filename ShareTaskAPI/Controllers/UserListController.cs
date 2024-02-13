using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ShareTaskAPI.Context;
using ShareTaskAPI.Entities;
using ShareTaskAPI.Service;

namespace ShareTaskAPI.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Policy = "Authorized")]
    [ApiController]
    public class UserListController : ControllerBase
    {
        private MyDbContext _db;
        public UserListController(MyDbContext db)
        {
            _db = db;
        }
        [HttpPost("PostUserList/{link}")]
        public IActionResult PostUserList([FromRoute] string link)
        {
            try
            {
                var user = AccountActions.ReturnUserFromCookie(HttpContext, _db);
                var list = _db.Lists.FirstOrDefault(x => x.Linq == link);
                if (list == null)
                    return NotFound();
                _db.Add(new UserList
                {
                    IdList = list.IdList,
                    IdUser = user.IdUser
                });
                _db.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("GetAllEnabledLists")]
        public IActionResult GetAllEnabledLists()
        {
            try
            {
                var user = AccountActions.ReturnUserFromCookie(HttpContext, _db);
                var list = _db.UsersLists.Where(x => x.IdUser == user.IdUser).ToList();
                return Ok(list);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet("GetUserList/{idList}")]
        public IActionResult GetUserList([FromRoute] int idList)
        {
            try
            {
                var user = AccountActions.ReturnUserFromCookie(this.HttpContext, _db).IdUser;
                var list = _db.UsersLists.FirstOrDefault(x => x.IdUser == user && x.IdList == idList);
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


        [HttpDelete("DeleteUserList/{idList}")]
        public IActionResult DeleteUserList([FromRoute] long idList)
        {
            try
            {
                var userId = AccountActions.ReturnUserFromCookie(this.HttpContext, _db).IdUser;
                var userlist = _db.UsersLists.FirstOrDefault(x => x.IdUser == userId && x.IdList == idList);
                if (userlist == null)
                    return NotFound();
                _db.Remove(userlist);
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
