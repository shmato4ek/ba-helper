using AutoMapper;
using BAHelper.BLL.Services.Abstract;
using BAHelper.Common.DTOs.Project;
using BAHelper.Common.DTOs.ProjectTask;
using BAHelper.Common.DTOs.User;
using BAHelper.DAL.Context;
using BAHelper.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Bcpg.OpenPgp;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAHelper.BLL.Services
{
    public class ProjectService : BaseService
    {
        public ProjectService(BAHelperDbContext context, IMapper mapper)
            : base(context, mapper) { }

        public async Task<ProjectDTO> CreateProject(NewProjectDTO newProject, int userId)
        {
            var projectEntity = _mapper.Map<Project>(newProject);
            projectEntity.AuthorId = userId;
            projectEntity.Hours = 0;
            projectEntity.IsDeleted = false;
            _context.Projects.Add(projectEntity);
            await _context.SaveChangesAsync();
            return _mapper.Map<ProjectDTO>(projectEntity);
        }

        public async Task<ProjectDTO> UpdateProject(UpdateProjectDTO updatedProject)
        {
            var projectEntity = await _context.Projects.FirstOrDefaultAsync(p => p.Id == updatedProject.Id);
            if (projectEntity != null)
            {
                projectEntity.ProjectName = updatedProject.ProjectName;
                projectEntity.Deadline = updatedProject.Deadline;
                _context.Update(projectEntity);
                await _context.SaveChangesAsync();
                return _mapper.Map<ProjectDTO>(projectEntity);
            }
            return null;
        }

        public async Task<List<ProjectDTO>> GetAllUsersOwnProject(int userId)
        {
            var projectsEntity = await _context.Projects.Where(project => project.AuthorId == userId).ToListAsync();
            if (projectsEntity != null)
            {
                return _mapper.Map<List<ProjectDTO>>(projectsEntity);
            }
            return null;
        }

        public async Task<List<ProjectDTO>> GetAllUsersProjects(int userId)
        {
            var userEntity = await _context
                .Users
                .FirstOrDefaultAsync(user => user.Id == userId);
            if (userEntity is null)
            {
                return null;
            }
            var projectsEntity = await _context
                .Projects
                .Include(project => project.Tasks)
                .ThenInclude(task => task.Subtasks)
                .Where(project => project.Users.Contains(userEntity))
                .ToListAsync();
            return _mapper.Map<List<ProjectDTO>>(projectsEntity);
        }

        public async Task<List<ProjectTaskDTO>> GetAllProjectTasks(int projectId)
        {
            var projectEntity = await _context.Projects.FirstOrDefaultAsync(p => p.Id == projectId);
            if (projectEntity != null)
            {
                var projectTasks = await _context.Tasks.Where(t => t.ProjectId == projectEntity.Id).ToListAsync();
                return _mapper.Map<List<ProjectTaskDTO>>(projectTasks);
            }
            return null;
        }

        public async Task<ProjectDTO> AddUserToProject(int projectId, string email, int userId)
        {
            var projectEntity = await _context
                .Projects
                .Include(project => project.Users)
                .FirstOrDefaultAsync(project => project.Id == projectId);
            if (projectEntity is null || projectEntity.AuthorId != userId)
            {
                return null;
            }
            var userEntity = await _context
                .Users
                .FirstOrDefaultAsync(user => user.Email == email);
            if (userEntity is null)
            {
                return null;
            }
            if (projectEntity.Users == null)
            {
                projectEntity.Users = new List<User>();
            }
            projectEntity.Users.Add(userEntity);
            _context.SaveChanges();
            return _mapper.Map<ProjectDTO>(projectEntity);
        }

        public async Task MoveToArchive(int projectId, int userId)
        {
            var projectEntity = await _context
                .Projects
                .FirstOrDefaultAsync(project => project.Id == projectId);
            if (projectEntity is null)
            {
                return;
            }

            if (projectEntity.AuthorId != userId)
            {
                return;
            }

            projectEntity.IsDeleted = true;
            _context.Projects.Update(projectEntity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProject(int projectId, string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);
            var userId = jwtToken.Claims.FirstOrDefault(claim => claim.Type == "id")?.Value;
            if (userId == null) 
            {
                return;
            }
            var projectEntity = await _context
                .Projects
                .FirstOrDefaultAsync(project => project.Id == projectId);
            if (projectEntity == null)
            {
                return;
            }
            if (projectEntity.AuthorId != Convert.ToInt32(userId))
            {
                return;
            }

            _context.Projects.Remove(projectEntity);
            _context.SaveChanges();
        }
    }
}
