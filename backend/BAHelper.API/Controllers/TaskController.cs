using BAHelper.BLL.Services;
using BAHelper.Common.DTOs.ProjectTask;
using BAHelper.Common.DTOs.Subtask;
using BAHelper.Common.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;

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

        [HttpPut("Change state")]
        public async Task<IActionResult> ChangeTaskState(int userId, int taskId, TaskState taskState)
        {
            return Ok(await _projectTaskService.ChangeTaskState(userId, taskId, taskState));
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteTask(int taskId)
        {
            return Ok(await _projectTaskService.DeleteTask(taskId));
        }

        [HttpPut("Add subtask")]
        public async Task<IActionResult> AddSubtask([FromBody] NewSubtaskDTO newSubtask)
        {
            return Ok(await _projectTaskService.AddSubtask(newSubtask));
        }

        [HttpPut("Approve task")]
        public async Task<IActionResult> ApproveTask(int taskId, int userId)
        {
            return Ok(await _projectTaskService.ApproveTask(taskId, userId));
        }
    }
}
