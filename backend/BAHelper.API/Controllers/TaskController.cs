using BAHelper.BLL.Services;
using BAHelper.Common.DTOs.ProjectTask;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BAHelper.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ProjectTaskService _projectTaskService;
        public TaskController (ProjectTaskService projectTaskService)
        {
            _projectTaskService = projectTaskService;
        }

        [HttpPost]
        public async Task<IActionResult> AddTask([FromBody] NewProjectTaskDTO newTaskDto)
        {
            var createdTask = await _projectTaskService.AddProjectTask(newTaskDto);
            return Ok(createdTask);
        }

        [HttpPut("add user")]
        public async Task<IActionResult> AddUsersToTask(int taskId, int userId)
        {
            return Ok(await _projectTaskService.AddUserToTask(taskId, userId));
        }

        [HttpGet("get tasks by project id")]
        public async Task<IActionResult> GetUserTasks(int userId, int projectId)
        {
            return Ok(await _projectTaskService.GetAllUsersTasksByProject(userId, projectId));
        }

        [HttpGet("All tasks by project")]
        public async Task<IActionResult> GetProjectsTasks(int projectId)
        {
            return Ok(await _projectTaskService.GetAllTasksByProjectId(projectId));
        }
    }
}
