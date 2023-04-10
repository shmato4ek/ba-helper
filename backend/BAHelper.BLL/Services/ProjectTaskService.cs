using AutoMapper;
using BAHelper.BLL.Services.Abstract;
using BAHelper.Common.DTOs.Project;
using BAHelper.Common.DTOs.ProjectTask;
using BAHelper.Common.DTOs.Subtask;
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

        public async Task<ProjectDTO> AddProjectTask(NewProjectTaskDTO newProjectTaskDto)
        {
            var projectEntity = await _context
                .Projects
                .Include(project => project.Tasks)
                .FirstOrDefaultAsync(p => p.Id == newProjectTaskDto.ProjectId);
            if (projectEntity != null)
            {
                var projectTaskEntity = _mapper.Map<ProjectTask>(newProjectTaskDto);
                if(projectEntity.Tasks == null)
                {
                    projectEntity.Tasks = new List<ProjectTask>();
                }
                projectEntity.Tasks.Add(projectTaskEntity);
                _context.Update(projectEntity);
                await _context.SaveChangesAsync();
                return _mapper.Map<ProjectDTO>(projectEntity);
            }
            return null;
        }

        public async Task<ProjectTaskDTO> UpdateTask(UpdateProjectTaskDTO updatedProjectTask)
        {
            var projectTaskEntity = await _context
                .Tasks
                .FirstOrDefaultAsync(p => p.Id == updatedProjectTask.Id);
            if (projectTaskEntity != null)
            {
                projectTaskEntity.Deadine = updatedProjectTask.DeadLine;
                projectTaskEntity.TaskName = updatedProjectTask.TaskName;
                projectTaskEntity.Description = updatedProjectTask.Description;
                _context.Update(projectTaskEntity);
                await _context.SaveChangesAsync();
                return _mapper.Map<ProjectTaskDTO>(projectTaskEntity);
            }
            return null;
        }

        public async Task<ProjectTaskDTO> AddSubtask(NewSubtaskDTO newSubtask)
        {
            var taskEntity = await _context
                .Tasks
                .FirstOrDefaultAsync(t => t.Id == newSubtask.TaskId);
            if (taskEntity != null)
            {
                var subtaskEntity = _mapper.Map<Subtask>(newSubtask);
                taskEntity.Subtasks.Add(subtaskEntity);
                _context.Update(subtaskEntity);
                await _context.SaveChangesAsync();
                return _mapper.Map<ProjectTaskDTO>(taskEntity);
            }
            return null;
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

        public async Task<ProjectTaskDTO> AddUserToTask(int taskId, int userId)
        {
            var taskEntity = await _context
                .Tasks
                .Include(task => task.Users)
                .FirstOrDefaultAsync(task => task.Id == taskId);
            if (taskEntity != null)
            {
                var userEntity = await _context.Users.FirstOrDefaultAsync(user => user.Id == userId);
                if (userEntity != null)
                {
                    if(taskEntity.Users == null)
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
                return null;
            }
            return null;
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
            var projectEntity = await _context.Projects
                .Include(project => project.Tasks)
                .FirstOrDefaultAsync(project => project.Id == projectId);
            if (projectEntity != null)
            {
                var tasks = projectEntity.Tasks;
                return _mapper.Map<List<ProjectTaskDTO>>(tasks);
            }
            return null;
        }
    }
}
