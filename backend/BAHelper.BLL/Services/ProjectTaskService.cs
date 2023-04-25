using AutoMapper;
using BAHelper.BLL.Services.Abstract;
using BAHelper.Common.DTOs.Project;
using BAHelper.Common.DTOs.ProjectTask;
using BAHelper.Common.DTOs.Subtask;
using BAHelper.Common.Enums;
using BAHelper.DAL.Context;
using BAHelper.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAHelper.BLL.Services
{
    public class ProjectTaskService : BaseService
    {
        public ProjectTaskService(BAHelperDbContext context, IMapper mapper)
        :base(context, mapper) { }

        public async Task<ProjectDTO> AddProjectTask(NewProjectTaskDTO newProjectTaskDto, int userId)
        {
            var projectEntity = await _context
                .Projects
                .Include(project => project.Tasks)
                .FirstOrDefaultAsync(p => p.Id == newProjectTaskDto.ProjectId);
            if (projectEntity == null) 
            {
                return null;
            }
            if (projectEntity.AuthorId != userId)
            {
                return null;
            }
            if (projectEntity.Tasks == null)
            {
                projectEntity.Tasks = new List<ProjectTask>();
            }
            var projectTaskEntity = _mapper.Map<ProjectTask>(newProjectTaskDto);
            projectEntity.Hours += projectTaskEntity.Hours;
            projectEntity.Tasks.Add(projectTaskEntity);
            _context.Update(projectEntity);
            await _context.SaveChangesAsync();
            return _mapper.Map<ProjectDTO>(projectEntity);
        }

        public async Task<ProjectTaskDTO> UpdateTask(UpdateProjectTaskDTO updatedProjectTask, int userId)
        {
            var projectTaskEntity = await _context
                .Tasks
                .FirstOrDefaultAsync(p => p.Id == updatedProjectTask.Id);
            if (projectTaskEntity is null)
            {
                return null;
            }
            var projectEntity = await _context
                .Projects
                .FirstOrDefaultAsync(project => project.Id == projectTaskEntity.ProjectId);
            if (projectEntity is null || projectEntity.AuthorId != userId)
            {
                return null;
            }
            projectTaskEntity.Deadine = updatedProjectTask.DeadLine;
            projectTaskEntity.TaskName = updatedProjectTask.TaskName;
            projectTaskEntity.Description = updatedProjectTask.Description;
            _context.Update(projectTaskEntity);
            await _context.SaveChangesAsync();
            return _mapper.Map<ProjectTaskDTO>(projectTaskEntity);
        }

        public async Task<SubtaskDTO> UpdateSubtask(UpdateSubtaskDTO updatedSubtask, int userId)
        {
            var subtaskEntity = await _context
                .Subtasks
                .FirstOrDefaultAsync(subtask => subtask.Id == updatedSubtask.Id);
            if (subtaskEntity is null)
            {
                return null;
            }
            var taskEntity = await _context
                .Tasks
                .FirstOrDefaultAsync(task => task.Id == subtaskEntity.TaskId);
            if (taskEntity is null)
            {
                return null;
            }
            var projectEntity = await _context
                .Projects
                .FirstOrDefaultAsync(project => project.Id == taskEntity.ProjectId);
            if (projectEntity is null || projectEntity.AuthorId != userId)
            {
                return null;
            }
            subtaskEntity.Name = updatedSubtask.Name;
            subtaskEntity.Deadline = updatedSubtask.Deadline;
            subtaskEntity.Description = updatedSubtask.Description;
            _context.Subtasks.Update(subtaskEntity);
            await _context.SaveChangesAsync();
            return _mapper.Map<SubtaskDTO>(subtaskEntity);
        }

        public async Task<ProjectTaskDTO> AddSubtask(NewSubtaskDTO newSubtask, int userId)
        {
            var taskEntity = await _context
                .Tasks
                .FirstOrDefaultAsync(t => t.Id == newSubtask.TaskId);
            if(taskEntity == null)
            {
                return null;
            }
            var projectEntity = await _context
                .Projects
                .FirstOrDefaultAsync(project => project.Id == taskEntity.ProjectId);
            if(userId != projectEntity.AuthorId)
            {
                return null;
            }    
            var subtaskEntity = _mapper.Map<Subtask>(newSubtask);
            if (taskEntity.Subtasks == null)
            {
                taskEntity.Subtasks = new List<Subtask>();
            }
            taskEntity.Subtasks.Add(subtaskEntity);
            _context.Update(taskEntity);
            await _context.SaveChangesAsync();
            return _mapper.Map<ProjectTaskDTO>(taskEntity);
        }

        public async Task<List<SubtaskDTO>> GetAllSubtasks(int taskId)
        {
            var taskEntity = await _context
                .Tasks
                .FirstOrDefaultAsync(t => t.Id == taskId);
            if(taskEntity != null)
            {
                var subtasks = await _context.Subtasks.Where(t => t.TaskId == taskEntity.Id).ToListAsync();
                return _mapper.Map<List<SubtaskDTO>>(subtasks);
            }
            return null;
        }

        public async Task<ProjectTaskDTO> AddUserToTask(int taskId, string email, int userId)
        {
            var taskEntity = await _context
                .Tasks
                .Include(task => task.Users)
                .FirstOrDefaultAsync(task => task.Id == taskId);

            if (taskEntity == null) 
            {
                return null;
            }

            var userEntity = await _context.Users.FirstOrDefaultAsync(user => user.Email == email);
            if (userEntity == null) 
            {
                return null;
            }

            var projectEntity = await _context.Projects.FirstOrDefaultAsync(project => project.Id == taskEntity.ProjectId);
            if (projectEntity == null || projectEntity.AuthorId != userId)
            {
                return null;
            }

            if (taskEntity.Users == null)
            {
                taskEntity.Users = new List<User>();
            }

            if (taskEntity.Users.FirstOrDefault(user => user.Id == userId) == null)
            {
                taskEntity.Users.Add(userEntity);
                _context.Users.Update(userEntity);
                await _context.SaveChangesAsync();
            }

            return _mapper.Map<ProjectTaskDTO>(taskEntity);

        }

        public async Task<List<ProjectTaskDTO>> GetAllUsersTasksByProject(int userId, int projectId)
        {
            var userEntity = await _context
                .Users
                .Include(user => user.Tasks)
                .FirstOrDefaultAsync(user => user.Id == userId);
            if (userEntity != null)
            {
                var userTasksEntity = userEntity.Tasks.Where(task => task.ProjectId == projectId).ToList();
                return _mapper.Map<List<ProjectTaskDTO>>(userTasksEntity);
            }
            return null;
        }

        public async Task<List<ProjectTaskDTO>> GetAllTasksByProjectId(int projectId)
        {
            var tasksEntities = await _context
                .Tasks
                .Include(task => task.Users)
                .Include(task => task.Subtasks)
                .Where(task => task.ProjectId == projectId)
                .ToListAsync();
            return _mapper.Map<List<ProjectTaskDTO>>(tasksEntities);
        }

        public async Task<ProjectTaskDTO> ChangeTaskState(int userId, int taskId, TaskState taskState)
        {
            var taskEntity = await _context
                .Tasks
                .Include(task => task.Users)
                .FirstOrDefaultAsync(task => task.Id == taskId);
            if(taskEntity == null)
            {
                return null;
            }
            var userEntity = taskEntity
                .Users
                .FirstOrDefault(user => user.Id == userId);
            if (userEntity != null)
            {
                taskEntity.TaskState = taskState;
                _context.Tasks.Update(taskEntity);
                _context.SaveChanges();
                return _mapper.Map<ProjectTaskDTO>(taskEntity);
            }
            return null;
        }

        public async Task<ProjectTaskDTO> ApproveTask(int taskId, int userId)
        {
            var taskEntity = await _context
                .Tasks
                .Include(task => task.Users)
                .FirstOrDefaultAsync(task => task.Id == taskId);
            if (taskEntity != null) 
            {
                var projectEntity = await _context
                    .Projects
                    .FirstOrDefaultAsync(project => project.Id == taskEntity.ProjectId);
                if(projectEntity != null) 
                {
                    if(projectEntity.AuthorId == userId)
                    {
                        taskEntity.TaskState = TaskState.Approved;
                        _context.Tasks.Update(taskEntity);
                        _context.SaveChanges();
                        return _mapper.Map<ProjectTaskDTO>(taskEntity);
                    }
                    return null;
                }
                return null;
            }
            return null; 
        }

        public async Task<SubtaskDTO> ChangeSubtaskState(int subtaskId, TaskState taskState, int userId)
        {
            var subtaskEntity = await _context
                .Subtasks
                .FirstOrDefaultAsync(subtask => subtask.Id == subtaskId);
            if(subtaskEntity is null)
            {
                return null;
            }
            var taskEntity = await _context
                .Tasks
                .Include(task => task.Users)
                .FirstOrDefaultAsync(task => task.Id == subtaskEntity.TaskId);
            if (taskEntity is null) 
            {
                return null;
            }
            var foundUser = taskEntity.Users.FirstOrDefault(user => user.Id == userId);
            if (foundUser is null)
            {
                return null;
            }
            subtaskEntity.TaskState = taskState;
            _context.Subtasks.Update(subtaskEntity);
            await _context.SaveChangesAsync();
            return _mapper.Map<SubtaskDTO>(subtaskEntity);
        }

        public async Task<SubtaskDTO> ApproveSubtask(int subtaskId, int userId)
        {
            var subtaskEntity = await _context
                .Subtasks
                .FirstOrDefaultAsync(subtask => subtask.Id == subtaskId);
            if (subtaskEntity is null)
            {
                return null;
            }
            var taskEntity = await _context
                .Tasks
                .FirstOrDefaultAsync(task => task.Id == subtaskEntity.TaskId);
            if (taskEntity is null)
            {
                return null;
            }
            var projectEntity = await _context
                .Projects
                .FirstOrDefaultAsync(project => project.Id == taskEntity.ProjectId);
            if (projectEntity is null)
            {
                return null;
            }
            if (projectEntity.AuthorId != userId)
            {
                return null;
            }
            subtaskEntity.TaskState = TaskState.Approved;
            _context.Subtasks.Update(subtaskEntity);
            await _context.SaveChangesAsync();
            return _mapper.Map<SubtaskDTO>(subtaskEntity);
        }

        public async Task DeleteTask(int taskId, int userId)
        {
            var taskEntity = await _context
                .Tasks
                .FirstOrDefaultAsync(task => task.Id == taskId);
            if(taskEntity is null)
            {
                return;
            }
            var projectEntity = await _context
                .Projects
                .FirstOrDefaultAsync(project => project.Id == taskEntity.ProjectId);
            if (projectEntity is null)
            {
                return;
            }
            if (projectEntity.AuthorId != userId)
            {
                return;
            }
            projectEntity.Hours -= taskEntity.Hours;
            _context.Projects.Update(projectEntity);
            _context.Tasks.Remove(taskEntity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteSubtask(int subtaskId, int userId)
        {
            var subtaskEntity = await _context
                .Subtasks
                .FirstOrDefaultAsync(subtask => subtask.Id == subtaskId);
            if (subtaskEntity is null)
            {
                return;
            }
            var taskEntity = await _context
                .Tasks
                .FirstOrDefaultAsync(task => task.Id == subtaskEntity.TaskId);
            if (taskEntity is null)
            {
                return;
            }
            var projectEntity = await _context
                .Projects
                .FirstOrDefaultAsync(project => project.Id == taskEntity.ProjectId);
            if (projectEntity is null || projectEntity.AuthorId != userId)
            {
                return;
            }
            _context.Subtasks.Remove(subtaskEntity);
            await _context.SaveChangesAsync();
        }
    }
}
