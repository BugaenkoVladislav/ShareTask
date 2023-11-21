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
                var idUser = AccountActions.ReturnUserFromCookie(this.HttpContext, _db).IdUser;
                _db.Tasks.Add(new Task
                {
                    IdCreator = idUser,
                    IdList = task.IdList,
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
        [HttpGet("GetAllTask/{idList}")]
        public IActionResult GetAllTasks([FromRoute] long idList)
        {
            try
            {
                return Ok(_db.Tasks.Where(x => x.IdList == idList).ToList());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("DeleteTask/{idList}/{idTask}")]
        public IActionResult DeleteTask([FromRoute] long idTask, [FromRoute] long idList)
        {
            try
            {
                var task = _db.Tasks.FirstOrDefault(x => x.IdTask == idTask && x.IdList == idList);
                if (task == null)
                    return NotFound();
                _db.Remove(task);
                _db.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("EditTask/{idList}/{idTask}")]
        public IActionResult EditTask([FromRoute] long idTask, [FromRoute] long idList, [FromBody] Task newTask)
        {
            try
            {
                var task = _db.Tasks.FirstOrDefault(x => x.IdTask == idTask && x.IdList == idList);
                if (task == null)
                    return NotFound();
                task.Description = newTask.Description;
                task.NameTask = newTask.NameTask;
                task.IdRole = newTask.IdRole;
                _db.Update(task);
                _db.SaveChanges();
                return Ok(task);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
