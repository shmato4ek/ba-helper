using AutoMapper;
using AutoMapper.Configuration.Conventions;
using BAHelper.BLL.Exceptions;
using BAHelper.BLL.Services.Abstract;
using BAHelper.Common.DTOs.Project;
using BAHelper.Common.DTOs.ProjectTask;
using BAHelper.Common.DTOs.User;
using BAHelper.DAL.Context;
using BAHelper.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Bcpg.OpenPgp;
using ServiceStack;
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
        private readonly ProjectTaskService _projectTaskService;
        public ProjectService(BAHelperDbContext context, IMapper mapper, ProjectTaskService projectTaskService)
            : base(context, mapper) 
        {
            _projectTaskService = projectTaskService;
        }

        public async Task<ProjectInfoDTO> CreateProject(NewProjectDTO newProject, int userId)
        {
            var projectEntity = new Project();
            projectEntity.ProjectName = newProject.ProjectName;
            projectEntity.Deadline = newProject.Deadline;
            projectEntity.AuthorId = userId;
            projectEntity.Description = newProject.Description;
            projectEntity.Hours = 0;
            projectEntity.IsDeleted = false;
            _context.Projects.Add(projectEntity);
            await _context.SaveChangesAsync();
            var unregisteredUsers = new List<string>();
            foreach (var email in newProject.UsersEmails)
            {
                if (await UserEmailCheck(email))
                {
                    await AddUserToProject(projectEntity.Id, email, userId);
                }
                else
                {
                    unregisteredUsers.Add(email);
                }
            }
            foreach (var task in newProject.Tasks)
            {
                await _projectTaskService.AddProjectTask(task, projectEntity.Id, userId);
            }
            var projectTasksEntity = await _context
                .Tasks
                .Where(task => task.Id == projectEntity.Id)
                .Include(task => task.Users)
                .Include(task => task.Subtasks)
                .ToListAsync();
            var createdProject = await _context
                .Projects
                .Include(project => project.Users)
                .FirstOrDefaultAsync(project => project.Id == projectEntity.Id);
            var userEntity = await _context
                .Users
                .FirstOrDefaultAsync(user => user.Id == userId);
            var createdProjectDto = _mapper.Map<ProjectInfoDTO>(createdProject);
            createdProjectDto.AuthorName = userEntity.Name;
            createdProjectDto.Tasks = _mapper.Map<List<ProjectTaskInfoDTO>>(projectTasksEntity);
            createdProjectDto.CanEdit = true;
            return createdProjectDto;
        }

        public async Task<ProjectInfoDTO> GetProjectById(int projectId, int userId)
        {
            var projectEntity = await _context
                .Projects
                .Include(project => project.Users)
                .FirstOrDefaultAsync(project => project.Id == projectId);
            if (projectEntity is null)
            {
                throw new NotFoundException(nameof(Project), projectId);
            }
            var tasksEntity = await _context
                .Tasks
                .Where(task => task.ProjectId == projectId)
                .Include(task => task.Users)
                .Include(task => task.Subtasks)
                .ToListAsync();
            var projectDto = _mapper.Map<ProjectInfoDTO>(projectEntity);
            if (projectDto.AuthorId == userId)
            {
                projectDto.CanEdit = true;
            }
            var userEntity = await _context
                .Users
                .FirstOrDefaultAsync(user => user.Id == projectEntity.AuthorId);
            projectDto.AuthorName = userEntity.Name;
            projectDto.Tasks = _mapper.Map<List<ProjectTaskInfoDTO>>(tasksEntity);
            return projectDto;
        }

        private async Task<bool> UserEmailCheck(string email)
        {
            var foundUser = await _context
                .Users
                .FirstOrDefaultAsync(user => user.Email == email);
            return foundUser != null;
        }

        public async Task<ProjectInfoDTO> UpdateProject(UpdateProjectDTO updatedProject, int userId)
        {
            var projectEntity = await _context
                .Projects
                .Include(project => project.Users)
                .FirstOrDefaultAsync(p => p.Id == updatedProject.Id);
            if (projectEntity is null)
            {
                throw new NotFoundException(nameof(Project), updatedProject.Id);
            }
            if (userId != projectEntity.AuthorId)
            {
                throw new Exception("No access to project.");
            }
            projectEntity.ProjectName = updatedProject.ProjectName;
            projectEntity.Deadline = updatedProject.Deadline;
            projectEntity.Description = updatedProject.Description;
            var unregisteredUsers = new List<string>();
            var oldEmails = new List<string>();
            foreach (var user in projectEntity.Users)
            {
                oldEmails.Add(user.Email);
            }
            foreach (var email in updatedProject.Users)
            {
                if (!oldEmails.Contains(email))
                {
                    if (await UserEmailCheck(email))
                    {
                        await AddUserToProject(projectEntity.Id, email, userId);
                    }
                    else
                    {
                        unregisteredUsers.Add(email);
                    }
                }
            }
            foreach (var email in oldEmails)
            {
                if (!updatedProject.Users.Contains(email))
                {
                    var user = projectEntity.Users.FirstOrDefault(user => user.Email == email);
                    projectEntity.Users.Remove(user);
                }
            }
            _context.Update(projectEntity);
            await _context.SaveChangesAsync();
            var updatedProjectDto = _mapper.Map<ProjectInfoDTO>(projectEntity);
            updatedProjectDto.CanEdit = true;
            var tasksDto = await _context
                .Tasks
                .Where(task => task.ProjectId == projectEntity.Id)
                .Include(task => task.Users)
                .Include(task => task.Subtasks)
                .ToListAsync();
            updatedProjectDto.Tasks = _mapper.Map<List<ProjectTaskInfoDTO>>(tasksDto);
            return updatedProjectDto;
        }

        public async Task<List<ProjectInfoDTO>> GetAllUsersOwnProject(int userId)
        {
            var projectsEntity = await _context
                .Projects
                .Where(project => project.AuthorId == userId)
                .Include(project => project.Users)
                .Include(project => project.Tasks)
                .ToListAsync();

            var projectsDto = _mapper.Map<List<ProjectInfoDTO>>(projectsEntity);
            foreach (var project in projectsDto)
            {
                if (project.AuthorId == userId)
                {
                    project.CanEdit = true;
                }
            }
            return projectsDto;
        }

        public async Task<List<ProjectInfoDTO>> GetAllUsersProjects(int userId)
        {
            var userEntity = await _context
                .Users
                .FirstOrDefaultAsync(user => user.Id == userId);
            if (userEntity is null)
            {
                throw new NotFoundException(nameof(User), userId);
            }
            var projectsEntity = await _context
                .Projects
                .Where(project => project.Users.Contains(userEntity))
                .Include(project => project.Users)
                .Include(project => project.Tasks)
                .ThenInclude(task => task.Subtasks)
                .ToListAsync();

            var projectsDto = _mapper.Map<List<ProjectInfoDTO>>(projectsEntity);
            foreach (var project in projectsDto)
            {
                var user = await _context.Users.FirstOrDefaultAsync(user => user.Id == project.AuthorId);
                project.AuthorName = user.Name;
            }

            return projectsDto;
        }

        public async Task<List<ProjectTaskInfoDTO>> GetAllProjectTasks(int projectId, int userId)
        {
            var projectEntity = await _context
                .Projects
                .FirstOrDefaultAsync(p => p.Id == projectId);
            if (projectEntity is null)
            {
                throw new NotFoundException(nameof(Project), projectId);
            }
            var projectTasks = await _context
                .Tasks
                .Where(t => t.ProjectId == projectEntity.Id)
                .Include(task => task.Subtasks)
                .Include(task => task.Users)
                .ToListAsync();

            var projectTasksDto = _mapper.Map<List<ProjectTaskInfoDTO>>(projectTasks);
            foreach(var task in projectTasksDto)
            {
                if (task.Users.FirstOrDefault(user => user.Id == userId) != null)
                {
                    task.CanEditState = true;
                }
            }
            return projectTasksDto;
        }

        public async Task<ProjectDTO> AddUserToProject(int projectId, string email, int userId)
        {
            var projectEntity = await _context
                .Projects
                .Include(project => project.Users)
                .FirstOrDefaultAsync(project => project.Id == projectId);
            if (projectEntity is null)
            {
                throw new NotFoundException(nameof(Project), projectId);
            }
            if (projectEntity.AuthorId != userId)
            {
                throw new Exception("No access to project.");
            }

            var userEntity = await _context
                .Users
                .FirstOrDefaultAsync(user => user.Email == email);
            if (userEntity is null)
            {
                throw new UserNotFoundException(email);
            }
            if (projectEntity.Users == null)
            {
                projectEntity.Users = new List<User>();
            }
            projectEntity.Users.Add(userEntity);
            _context.Projects.Update(projectEntity);
            await _context.SaveChangesAsync();
            return _mapper.Map<ProjectDTO>(projectEntity);
        }

        public async Task MoveToArchive(int projectId, int userId)
        {
            var projectEntity = await _context
                .Projects
                .FirstOrDefaultAsync(project => project.Id == projectId);
            if (projectEntity is null)
            {
                throw new NotFoundException(nameof(Project), projectId);
            }

            if (projectEntity.AuthorId != userId)
            {
                throw new Exception("No access to project.");
            }

            projectEntity.IsDeleted = true;
            _context.Projects.Update(projectEntity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProject(int projectId, int userId)
        {
            var projectEntity = await _context
                .Projects
                .FirstOrDefaultAsync(project => project.Id == projectId);
            if (projectEntity == null)
            {
                throw new NotFoundException(nameof(Project), projectId);
            }
            if (projectEntity.AuthorId != userId)
            {
                throw new Exception("No access to project.");
            }

            _context.Projects.Remove(projectEntity);
            _context.SaveChanges();
        }
    }
}
