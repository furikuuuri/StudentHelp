using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Sheldy.Tasks;
using Sheldy.Models;
using Microsoft.AspNetCore.Authorization;

namespace Sheldy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        TaskList taskList;

        public TaskController()
        {
            taskList = new TaskList();
        }

        [Authorize(Roles = "admin")]
        [HttpPost("addTask")]
        public IActionResult addTask(Task task)
        {
            taskList.AddTask(task);
            return Ok(new {
                name=task.Name
            });
        }

        [HttpGet("getTask")]
        public IEnumerable<Task> getTask(int id)
        {
            return taskList.getTasks(id);
        }
        [HttpGet("getMyTask")]
        public IEnumerable<AvailableTask> getMyTask(int id)
        {
            return taskList.getMyTasks(id);
        }
    }
}
