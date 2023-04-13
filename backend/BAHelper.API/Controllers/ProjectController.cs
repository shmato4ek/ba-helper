using BAHelper.BLL.Services;
using BAHelper.Common.DTOs.Project;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> CreateProject([FromBody] NewProjectDTO newProject)
        {
            return Ok(await _projectService.CreateProject(newProject));
        }

        [HttpGet("Own projects")]
        public async Task<IActionResult> GetAllUsersProject(int userId)
        {
            var projects = await _projectService.GetAllUsersOwnProject(userId);
            return Ok(projects);
        }

        [HttpGet("AllTasks")]
        public async Task<IActionResult> GetAllProjectTasks(int projectId)
        {
            var tasks = await _projectService.GetAllProjectTasks(projectId);
            return Ok(tasks);
        }

        [HttpPut("Add user to project")]
        public async Task<IActionResult> AddUserToProject(int projectId, int userId)
        {
            return Ok(await _projectService.AddUserToProject(projectId, userId));
        }

        //[HttpPost]
        //public async Task<IActionResult> CreateProject([FromBody] NewProjectDTO newProject)
        //{
        //    var project = await _projectService.CreateProject(newProject);
        //    await _scheduleService.CreateSchedule();
        //    return Ok(project);
        //}

        [HttpPut]
        public async Task<IActionResult> UpdateProject([FromBody] UpdateProjectDTO updatedProject)
        {
            var project = await _projectService.UpdateProject(updatedProject);
            return Ok(project);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteProject(int projectId, int userId)
        {
            return Ok(await _projectService.DeleteProject(projectId, userId));
        }
    }
}
