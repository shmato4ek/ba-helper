﻿using BAHelper.API.Extensions;
using BAHelper.BLL.Services;
using BAHelper.Common.DTOs.ProjectTask;
using BAHelper.Common.DTOs.Subtask;
using BAHelper.Common.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;
using Spire.Pdf.Annotations;

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
        public async Task<ActionResult> AddTask([FromBody] NewProjectTaskDTO newTaskDto, int projectId)
        {
            return Ok(await _projectTaskService.AddProjectTask(newTaskDto, projectId, this.GetUserIdFromToken()));
        }

        [HttpPut("assign")]
        public async Task<ActionResult> AddUsersToTask(int taskId, string email)
        {
            return Ok(await _projectTaskService.AddUserToTask(taskId, email, 5));
        }

        [HttpPut]
        public async Task<ActionResult> UpdateTask([FromBody] UpdateProjectTaskDTO updatedTask)
        {
            return Ok(await _projectTaskService.UpdateTask(updatedTask, this.GetUserIdFromToken()));
        }

        [HttpGet("subtask")]
        public async Task<ActionResult> GetAllSubtasksByTaskId(int taskId)
        {
            return Ok(await _projectTaskService.GetAllSubtasks(taskId));
        }

        [HttpPut("subtask")]
        public async Task<ActionResult> UpdateSubtask([FromBody] UpdateSubtaskDTO updatedSubtask)
        {
            return Ok(await _projectTaskService.UpdateSubtask(updatedSubtask, this.GetUserIdFromToken()));
        }

        [HttpGet("project/user")]
        public async Task<ActionResult> GetUserTasks(int projectId)
        {
            return Ok(await _projectTaskService.GetAllUsersTasksByProject(this.GetUserIdFromToken(), projectId));
        }

        [HttpGet("project")]
        public async Task<ActionResult> GetProjectsTasks(int projectId)
        {
            return Ok(await _projectTaskService.GetAllTasksByProjectId(projectId));
        }

        [HttpPut("state")]
        public async Task<ActionResult> ChangeTaskState(int taskId, TaskState taskState)
        {
            return Ok(await _projectTaskService.ChangeTaskState(this.GetUserIdFromToken(), taskId, taskState));
        }

        [HttpPut("subtask/state")]
        public async Task<ActionResult> ChangeSubtaskState(int subtaskId, TaskState taskState)
        {
            return Ok(await _projectTaskService.ChangeSubtaskState(subtaskId, taskState, this.GetUserIdFromToken()));
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteTask(int taskId)
        {
            await _projectTaskService.DeleteTask(taskId, this.GetUserIdFromToken());
            return NoContent();
        }

        [HttpDelete("subtask")]
        public async Task<ActionResult> DeleteSubtask(int subtaskId)
        {
            await _projectTaskService.DeleteSubtask(subtaskId, this.GetUserIdFromToken());
            return NoContent();
        }

        [HttpPost("subtask")]
        public async Task<ActionResult> AddSubtask([FromBody] NewSubtaskDTO newSubtask)
        {
            return Ok(await _projectTaskService.AddSubtask(newSubtask, this.GetUserIdFromToken()));
        }

        [HttpPut("approve")]
        public async Task<ActionResult> ApproveTask(int taskId)
        {
            return Ok(await _projectTaskService.ApproveTask(taskId, this.GetUserIdFromToken()));
        }

        [HttpPut("subtask/approve")]
        public async Task<ActionResult> ApproveSubtask(int subtaskId)
        {
            return Ok(await _projectTaskService.ApproveSubtask(subtaskId, this.GetUserIdFromToken()));
        }
    }
}
