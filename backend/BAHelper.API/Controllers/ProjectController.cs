using BAHelper.BLL.JWT;
using BAHelper.BLL.Services;
using BAHelper.Common.DTOs.Project;
using Microsoft.AspNetCore.Mvc;

namespace BAHelper.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly ProjectService _projectService;
        private readonly JwtFactory _jwtFactory;

        public ProjectController(ProjectService projectService, JwtFactory jwtFactory)
        {
            _projectService = projectService;
            _jwtFactory = jwtFactory;
        }

        [HttpPost]
        public async Task<ActionResult> CreateProject(NewProjectDTO newProject)
        {
            var token = Request.Headers["x-auth-token"].ToString();
            var userId = _jwtFactory.GetValueFromToken(token);
            return Ok(await _projectService.CreateProject(newProject, userId));
        }

        [HttpGet("{projectId:int}")]
        public async Task<ActionResult> GetProjectById(int projectId)
        {
            var token = Request.Headers["x-auth-token"].ToString();
            var userId = _jwtFactory.GetValueFromToken(token);
            return Ok(await _projectService.GetProjectById(projectId, userId));
        }

        [HttpGet("user/own")]
        public async Task<ActionResult> GetAllUsersOwnProject()
        {
            var token = Request.Headers["x-auth-token"].ToString();
            var userId = _jwtFactory.GetValueFromToken(token);
            return Ok(await _projectService.GetAllUsersOwnProject(userId));
        }

        [HttpGet("user/own/archive")]
        public async Task<ActionResult> GetAllUsersArchivedProjects()
        {
            var token = Request.Headers["x-auth-token"].ToString();
            var userId = _jwtFactory.GetValueFromToken(token);
            return Ok(await _projectService.GetOwnArchivedProjects(userId));
        }

        [HttpGet("user")]
        public async Task<ActionResult> GetAllUsersProjects()
        {
            var token = Request.Headers["x-auth-token"].ToString();
            var userId = _jwtFactory.GetValueFromToken(token);
            return Ok(await _projectService.GetAllUsersProjects(userId));
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
            var token = Request.Headers["x-auth-token"].ToString();
            var userId = _jwtFactory.GetValueFromToken(token);
            return Ok(await _projectService.UpdateProject(updatedProject, userId));
        }

        [HttpPut("archive")]
        public async Task<ActionResult> MoveToArchive(int projectId)
        {
            var token = Request.Headers["x-auth-token"].ToString();
            var userId = _jwtFactory.GetValueFromToken(token);
            await _projectService.MoveToArchive(projectId, userId);
            return NoContent();
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteProject(int projectId)
        {
            var token = Request.Headers["x-auth-token"].ToString();
            var userId = _jwtFactory.GetValueFromToken(token);
            await _projectService.DeleteProject(projectId, userId);
            return NoContent();
        }
    }
}
