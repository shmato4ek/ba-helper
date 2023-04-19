﻿using BAHelper.API.Extensions;
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
            var createdTask = await _projectTaskService.AddProjectTask(newTaskDto, this.GetUserIdFromToken());
            return Ok(createdTask);
        }

        [HttpPut("assign/{taskId:int}")]
        public async Task<IActionResult> AddUsersToTask(int taskId, int userId)
        {
            return Ok(await _projectTaskService.AddUserToTask(taskId, userId, this.GetUserIdFromToken()));
        }

        [HttpGet("project/{userId:int}")]
        public async Task<IActionResult> GetUserTasks(int projectId)
        {
            return Ok(await _projectTaskService.GetAllUsersTasksByProject(this.GetUserIdFromToken(), projectId));
        }

        [HttpGet("project/{projectId:int}")]
        public async Task<IActionResult> GetProjectsTasks(int projectId)
        {
            return Ok(await _projectTaskService.GetAllTasksByProjectId(projectId));
        }

        [HttpPut("change-state")]
        public async Task<IActionResult> ChangeTaskState(int taskId, TaskState taskState)
        {
            return Ok(await _projectTaskService.ChangeTaskState(this.GetUserIdFromToken(), taskId, taskState));
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteTask(int taskId)
        {
            await _projectTaskService.DeleteTask(taskId);
            return NoContent();
        }

        [HttpPut("subtask")]
        public async Task<IActionResult> AddSubtask([FromBody] NewSubtaskDTO newSubtask)
        {
            return Ok(await _projectTaskService.AddSubtask(newSubtask, this.GetUserIdFromToken()));
        }

        [HttpPut("Approve")]
        public async Task<IActionResult> ApproveTask(int taskId)
        {
            return Ok(await _projectTaskService.ApproveTask(taskId, this.GetUserIdFromToken()));
        }
    }
}
