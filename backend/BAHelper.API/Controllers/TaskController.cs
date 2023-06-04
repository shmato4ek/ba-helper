using BAHelper.BLL.JWT;
using BAHelper.BLL.Services;
using BAHelper.Common.DTOs.ProjectTask;
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
        public async Task<ActionResult> AddUsersToTask([FromBody] TaskAddUser task)
        {
            var token = Request.Headers["x-auth-token"].ToString();
            var userId = _jwtFactory.GetValueFromToken(token);
            return Ok(await _projectTaskService.AddUserToTask(task.TaskId, task.Email, userId));
        }

        [HttpPut]
        public async Task<ActionResult> UpdateTask([FromBody] UpdateProjectTaskDTO updatedTask)
        {
            var token = Request.Headers["x-auth-token"].ToString();
            var userId = _jwtFactory.GetValueFromToken(token);
            return Ok(await _projectTaskService.UpdateTask(updatedTask, userId));
        }

        [HttpPut("state")]
        public async Task<ActionResult> ChangeTaskState([FromBody] ProjectTaskChangeState task)
        {
            var token = Request.Headers["x-auth-token"].ToString();
            var userId = _jwtFactory.GetValueFromToken(token);
            return Ok(await _projectTaskService.ChangeTaskState(userId, task.TaskId, task.TaskState));
        }

        [HttpDelete("{taskId:int}")]
        public async Task<ActionResult> DeleteTask(int taskId)
        {
            var token = Request.Headers["x-auth-token"].ToString();
            var userId = _jwtFactory.GetValueFromToken(token);
            await _projectTaskService.DeleteTask(taskId, userId);
            return NoContent();
        }

        [HttpPut("approve")]
        public async Task<ActionResult> ApproveTask([FromBody]ApproveTaskDTO task)
        {
            var token = Request.Headers["x-auth-token"].ToString();
            var userId = _jwtFactory.GetValueFromToken(token);
            await _projectTaskService.ApproveTask(task.TaskId, userId);
            return Ok();
        }

        [HttpGet("rnd-tasks")]
        public async Task<ActionResult> RandomTasks(int projectId, int count)
        {
            await _projectTaskService.AddRandomTasks(projectId, count);
            return Ok();
        }
    }
}
