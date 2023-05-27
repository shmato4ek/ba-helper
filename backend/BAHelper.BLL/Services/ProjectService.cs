using AutoMapper;
using BAHelper.BLL.Exceptions;
using BAHelper.BLL.Services.Abstract;
using BAHelper.Common.DTOs.Project;
using BAHelper.Common.DTOs.ProjectTask;
using BAHelper.DAL.Context;
using BAHelper.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace BAHelper.BLL.Services
{
    public class ProjectService : BaseService
    {
        private readonly ProjectTaskService _projectTaskService;
        private readonly MailService _mailService;
        public ProjectService(BAHelperDbContext context, IMapper mapper, ProjectTaskService projectTaskService, MailService mailService)
            : base(context, mapper) 
        {
            _projectTaskService = projectTaskService;
            _mailService = mailService;
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
            if (newProject.Users != null)
            {
                foreach (var email in newProject.Users)
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
            if (unregisteredUsers != null)
            {
                var authorEntity = await _context
                    .Users
                    .FirstOrDefaultAsync(user => user.Id == userId);
                string message = $"Hello, {authorEntity.Name} has invited you to the project {newProject.ProjectName}, but you must be registered in BAHelper.";
                foreach (var sendEmail in unregisteredUsers)
                {
                    await _mailService.SendMail(sendEmail, "Invitation to the project", message);
                }
            }
            // foreach (var task in newProject.Tasks)
            // {
            //     await _projectTaskService.AddProjectTask(task, projectEntity.Id, userId);
            // }
            // var projectTasksEntity = await _context
            //     .Tasks
            //     .Where(task => task.Id == projectEntity.Id)
            //     .Include(task => task.Users)
            //     .Include(task => task.Subtasks)
            //     .ToListAsync();
            var createdProject = await _context
                .Projects
                .Include(project => project.Users)
                .FirstOrDefaultAsync(project => project.Id == projectEntity.Id);
            var userEntity = await _context
                .Users
                .FirstOrDefaultAsync(user => user.Id == userId);
            var createdProjectDto = _mapper.Map<ProjectInfoDTO>(createdProject);
            createdProjectDto.AuthorName = userEntity.Name;
            //createdProjectDto.Tasks = _mapper.Map<List<ProjectTaskInfoDTO>>(projectTasksEntity);
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
            var userEntity = await _context
                .Users
                .FirstOrDefaultAsync(user => user.Id == projectEntity.AuthorId);
            projectDto.AuthorName = userEntity.Name;
            projectDto.Tasks = _mapper.Map<List<ProjectTaskInfoDTO>>(tasksEntity);
            if (projectDto.AuthorId == userId)
            {
                projectDto.CanEdit = true;
            }
            else
            {
                foreach(var task in projectDto.Tasks)
                {
                    if(task.Users != null)
                    {
                        if (task.Users.FirstOrDefault(u => u.Id == userId) != null)
                        {
                            task.CanEditState = true;
                        }
                    }
                }
            }
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
                throw new NoAccessException(userId);
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
            var userEntity = await _context
                .Users
                .FirstOrDefaultAsync(user => user.Id == projectEntity.AuthorId);
            updatedProjectDto.AuthorName = userEntity.Name;
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
            var ownProjects = new List<ProjectInfoDTO>();
            foreach (var project in projectsDto) 
            {
                if (project.IsDeleted == false)
                {
                    ownProjects.Add(project);
                }
            }
            return ownProjects;
        }

        public async Task<List<ProjectInfoDTO>> GetOwnArchivedProjects(int userId)
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
            var ownProjects = new List<ProjectInfoDTO>();
            foreach (var project in projectsDto)
            {
                if (project.IsDeleted == true)
                {
                    ownProjects.Add(project);
                }
            }
            return ownProjects;
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
            var usersProjects = new List<ProjectInfoDTO>();
            foreach (var project in projectsDto)
            {
                if (project.IsDeleted == false)
                {
                    usersProjects.Add(project);
                }
            }
            return usersProjects;
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
                throw new NoAccessException(userId);
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
                throw new NoAccessException(userId);
            }

            projectEntity.IsDeleted = true;
            projectEntity.ArchivedDate = DateTime.UtcNow;
            _context.Projects.Update(projectEntity);
            await _context.SaveChangesAsync();
        }

        public async Task RestoreProject(int projectId, int userId)
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
                throw new NoAccessException(userId);
            }
            projectEntity.IsDeleted = false;
            projectEntity.ArchivedDate = new DateTime();
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
                throw new NoAccessException(userId);
            }

            _context.Projects.Remove(projectEntity);
            _context.SaveChanges();
        }

        public async Task DeleteArchivedProjects()
        {
            var currentDate = DateTime.UtcNow;
            var archivedProjectsEntity = await _context
                .Projects
                .Where(project => project.IsDeleted)
                .Where(project => project.ArchivedDate.AddDays(30) > currentDate)
                .ToListAsync();
            foreach (var project in archivedProjectsEntity)
            {
                _context.Projects.Remove(project);
            }
            await _context.SaveChangesAsync();
        }
    }
}
