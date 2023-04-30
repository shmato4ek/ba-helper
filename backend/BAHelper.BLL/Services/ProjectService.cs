﻿using AutoMapper;
using BAHelper.BLL.Exceptions;
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
            return _mapper.Map<ProjectInfoDTO>(projectEntity);
        }

        private async Task<bool> UserEmailCheck(string email)
        {
            var foundUser = await _context
                .Users
                .FirstOrDefaultAsync(user => user.Email == email);
            return foundUser != null;
        }

        public async Task<ProjectDTO> UpdateProject(UpdateProjectDTO updatedProject)
        {
            var projectEntity = await _context
                .Projects
                .FirstOrDefaultAsync(p => p.Id == updatedProject.Id);
            if (projectEntity is null)
            {
                throw new NotFoundException(nameof(Project), updatedProject.Id);
            }
            projectEntity.ProjectName = updatedProject.ProjectName;
            projectEntity.Deadline = updatedProject.Deadline;
            _context.Update(projectEntity);
            await _context.SaveChangesAsync();
            return _mapper.Map<ProjectDTO>(projectEntity);
        }

        public async Task<List<ProjectDTO>> GetAllUsersOwnProject(int userId)
        {
            var projectsEntity = await _context
                .Projects
                .Where(project => project.AuthorId == userId)
                .ToListAsync();

            return _mapper.Map<List<ProjectDTO>>(projectsEntity);
        }

        public async Task<List<ProjectDTO>> GetAllUsersProjects(int userId)
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
                .Include(project => project.Tasks)
                .ThenInclude(task => task.Subtasks)
                .Where(project => project.Users.Contains(userEntity))
                .ToListAsync();
            return _mapper.Map<List<ProjectDTO>>(projectsEntity);
        }

        public async Task<List<ProjectTaskDTO>> GetAllProjectTasks(int projectId)
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
                .ToListAsync();
            return _mapper.Map<List<ProjectTaskDTO>>(projectTasks);
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
