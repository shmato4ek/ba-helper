using BAHelper.API.Extensions;
using BAHelper.BLL.Services;
using BAHelper.Common.DTOs.Project;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

namespace BAHelper.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly ProjectService _projectService;
        private readonly ScheduleService _scheduleService;

        public ProjectController(ProjectService projectService, ScheduleService scheduleService)
        {
            _projectService = projectService;
            _scheduleService = scheduleService;
        }

        [HttpPost]
        public async Task<ActionResult> CreateProject([FromBody] NewProjectDTO newProject)
        {
            return Ok(await _projectService.CreateProject(newProject, this.GetUserIdFromToken()));
        }

        [HttpGet("user/own")]
        public async Task<ActionResult> GetAllUsersOwnProject()
        {
            var projects = await _projectService.GetAllUsersOwnProject(5);
            return Ok(projects);
        }

        [HttpGet("user")]
        public async Task<ActionResult> GetAllUsersProjects()
        {
            return Ok(await _projectService.GetAllUsersProjects(5));
        }

        [HttpGet("project/{projectId:int}")]
        public async Task<ActionResult> GetAllProjectTasks(int projectId)
        {
            var tasks = await _projectService.GetAllProjectTasks(projectId);
            return Ok(tasks);
        }

        [HttpPut("add-user/{userId:int}")]
        public async Task<ActionResult> AddUserToProject(int projectId, string email)
        {
            return Ok(await _projectService.AddUserToProject(projectId, email, this.GetUserIdFromToken()));
        }

        [HttpPut]
        public async Task<ActionResult> UpdateProject([FromBody] UpdateProjectDTO updatedProject)
        {
            var project = await _projectService.UpdateProject(updatedProject);
            return Ok(project);
        }

        [HttpPut("archive/{projectId:int}")]
        public async Task<ActionResult> MoveToArchive(int projectId)
        {
            await _projectService.MoveToArchive(projectId, this.GetUserIdFromToken());
            return NoContent();
        }

        [HttpDelete("{projectId:int}")]
        public async Task<ActionResult> DeleteProject(int projectId)
        {
            Request.Headers.TryGetValue(HeaderNames.Authorization, out Microsoft.Extensions.Primitives.StringValues value);
            var token = value.ToString();
            await _projectService.DeleteProject(projectId, token);
            return NoContent();
        }
    }
}
