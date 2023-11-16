using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ShareTaskAPI.Context;
using ShareTaskAPI.Entities;
using ShareTaskAPI.Service;
using Task = ShareTaskAPI.Entities.Task;

namespace ShareTaskAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "SelectedList")]
    [Authorize(Policy = "Authorized")]
    public class TaskController : ControllerBase
    {
        private MyDbContext _db;
        public TaskController(MyDbContext db)
        {
            _db = db;
        }

        [HttpPost("CreateTask")]
        public IActionResult CreateTask([FromBody] Entities.Task task)
        {
            try
            {
                var idList = AccountActions.ReturnListId(this.HttpContext);
                var idUser = AccountActions.ReturnUserFromCookie(this.HttpContext, _db).IdUser;
                _db.Tasks.Add(new Task
                {
                    IdCreator = idUser,
                    IdList = idList,
                    IdRole = task.IdRole,
                    NameTask = task.NameTask,
                    Description = task.Description
                });
                _db.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet("GetAllTask")]
        public IActionResult GetAllTasks()
        {
            try
            {
                var idList = AccountActions.ReturnListId(HttpContext);
                return Ok(_db.Tasks.Where(x => x.IdList == idList).ToList());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
