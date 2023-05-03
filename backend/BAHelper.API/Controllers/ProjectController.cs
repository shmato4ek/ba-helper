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

        public ProjectController(ProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpPost]
        public async Task<ActionResult> CreateProject([FromBody] NewProjectDTO newProject)
        {
            return Ok(await _projectService.CreateProject(newProject, this.GetUserIdFromToken()));
        }

        [HttpGet]
        public async Task<ActionResult> GetProjectById(int projectId)
        {
            return Ok(await _projectService.GetProjectById(projectId, this.GetUserIdFromToken()));
        }

        [HttpGet("user/own")]
        public async Task<ActionResult> GetAllUsersOwnProject()
        {
            return Ok(await _projectService.GetAllUsersOwnProject(this.GetUserIdFromToken()));
        }

        [HttpGet("user")]
        public async Task<ActionResult> GetAllUsersProjects()
        {
            return Ok(await _projectService.GetAllUsersProjects(this.GetUserIdFromToken()));
        }

        //[HttpGet("tasks")]
        //public async Task<ActionResult> GetAllProjectTasks(int projectId)
        //{
        //    return Ok(await _projectService.GetAllProjectTasks(projectId, 5));
        //}

        //[HttpPut("add-user")]
        //public async Task<ActionResult> AddUserToProject(int projectId, string email)
        //{
        //    return Ok(await _projectService.AddUserToProject(projectId, email, 5));
        //}

        [HttpPut]
        public async Task<ActionResult> UpdateProject([FromBody] UpdateProjectDTO updatedProject)
        {
            return Ok(await _projectService.UpdateProject(updatedProject, this.GetUserIdFromToken()));
        }

        [HttpPut("archive")]
        public async Task<ActionResult> MoveToArchive(int projectId)
        {
            await _projectService.MoveToArchive(projectId, this.GetUserIdFromToken());
            return NoContent();
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteProject(int projectId)
        {
            await _projectService.DeleteProject(projectId, this.GetUserIdFromToken());
            return NoContent();
        }
    }
}
