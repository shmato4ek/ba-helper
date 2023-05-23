using BAHelper.BLL.JWT;
using BAHelper.BLL.Services;
using BAHelper.Common.DTOs.ProjectTask;
using BAHelper.Common.DTOs.Subtask;
using BAHelper.Common.Enums;
using Microsoft.AspNetCore.Mvc;

namespace BAHelper.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ProjectTaskService _projectTaskService;
        private readonly JwtFactory _jwtFactory;
        public TaskController (ProjectTaskService projectTaskService, JwtFactory jwtFactory)
        {
            _jwtFactory = jwtFactory;
            _projectTaskService = projectTaskService;
        }

        [HttpPost]
        public async Task<ActionResult> AddTask([FromBody] NewProjectTaskDTO newTaskDto)
        {
            var token = Request.Headers["x-auth-token"].ToString();
            var userId = _jwtFactory.GetValueFromToken(token);
            return Ok(await _projectTaskService.AddProjectTask(newTaskDto, userId));
        }

        [HttpPut("assign")]
        public async Task<ActionResult> AddUsersToTask(int taskId, string email)
        {
            var token = Request.Headers["x-auth-token"].ToString();
            var userId = _jwtFactory.GetValueFromToken(token);
            return Ok(await _projectTaskService.AddUserToTask(taskId, email, userId));
        }

        [HttpPut]
        public async Task<ActionResult> UpdateTask([FromBody] UpdateProjectTaskDTO updatedTask)
        {
            var token = Request.Headers["x-auth-token"].ToString();
            var userId = _jwtFactory.GetValueFromToken(token);
            return Ok(await _projectTaskService.UpdateTask(updatedTask, userId));
        }

        [HttpPut("subtask")]
        public async Task<ActionResult> UpdateSubtask([FromBody] UpdateSubtaskDTO updatedSubtask)
        {
            var token = Request.Headers["x-auth-token"].ToString();
            var userId = _jwtFactory.GetValueFromToken(token);
            return Ok(await _projectTaskService.UpdateSubtask(updatedSubtask, userId));
        }

        [HttpPut("state")]
        public async Task<ActionResult> ChangeTaskState([FromBody] int taskId, TaskState taskState)
        {
            var token = Request.Headers["x-auth-token"].ToString();
            var userId = _jwtFactory.GetValueFromToken(token);
            return Ok(await _projectTaskService.ChangeTaskState(userId, taskId, taskState));
        }

        [HttpPut("subtask/state")]
        public async Task<ActionResult> ChangeSubtaskState(int subtaskId, TaskState taskState)
        {
            var token = Request.Headers["x-auth-token"].ToString();
            var userId = _jwtFactory.GetValueFromToken(token);
            return Ok(await _projectTaskService.ChangeSubtaskState(subtaskId, taskState, userId));
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteTask(int taskId)
        {
            var token = Request.Headers["x-auth-token"].ToString();
            var userId = _jwtFactory.GetValueFromToken(token);
            await _projectTaskService.DeleteTask(taskId, userId);
            return NoContent();
        }

        [HttpDelete("subtask")]
        public async Task<ActionResult> DeleteSubtask(int subtaskId)
        {
            var token = Request.Headers["x-auth-token"].ToString();
            var userId = _jwtFactory.GetValueFromToken(token);
            await _projectTaskService.DeleteSubtask(subtaskId, userId);
            return NoContent();
        }

        [HttpPost("subtask")]
        public async Task<ActionResult> AddSubtask([FromBody] NewSubtaskDTO newSubtask)
        {
            var token = Request.Headers["x-auth-token"].ToString();
            var userId = _jwtFactory.GetValueFromToken(token);
            return Ok(await _projectTaskService.AddSubtask(newSubtask, userId));
        }

        [HttpPut("approve")]
        public async Task<ActionResult> ApproveTask(int taskId)
        {
            var token = Request.Headers["x-auth-token"].ToString();
            var userId = _jwtFactory.GetValueFromToken(token);
            return Ok(await _projectTaskService.ApproveTask(taskId, userId));
        }

        [HttpPut("subtask/approve")]
        public async Task<ActionResult> ApproveSubtask(int subtaskId)
        {
            var token = Request.Headers["x-auth-token"].ToString();
            var userId = _jwtFactory.GetValueFromToken(token);
            return Ok(await _projectTaskService.ApproveSubtask(subtaskId, userId));
        }
    }
}
