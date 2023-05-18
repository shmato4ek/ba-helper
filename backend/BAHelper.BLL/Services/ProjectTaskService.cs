using AutoMapper;
using BAHelper.BLL.Exceptions;
using BAHelper.BLL.Services.Abstract;
using BAHelper.Common.DTOs.ProjectTask;
using BAHelper.Common.DTOs.StatisticData;
using BAHelper.Common.DTOs.Subtask;
using BAHelper.Common.Enums;
using BAHelper.DAL.Context;
using BAHelper.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using ServiceStack.Text;

namespace BAHelper.BLL.Services
{
    public class ProjectTaskService : BaseService
    {
        private readonly MailService _mailService;
        public ProjectTaskService(BAHelperDbContext context, IMapper mapper, MailService mailService)
        :base(context, mapper) 
        {
            _mailService = mailService;
        }

        public async Task<ProjectTaskDTO> AddProjectTask(NewProjectTaskDTO newProjectTaskDto, int userId)
        {
            var projectEntity = await _context
                .Projects
                .Include(project => project.Tasks)
                .FirstOrDefaultAsync(p => p.Id == newProjectTaskDto.ProjectId);
            if (projectEntity == null) 
            {
                throw new NotFoundException(nameof(Project), newProjectTaskDto.ProjectId);
            }
            if (projectEntity.AuthorId != userId)
            {
                throw new NoAccessException(userId);
            }
            if (projectEntity.Tasks == null)
            {
                projectEntity.Tasks = new List<ProjectTask>();
            }
            var projectTaskEntity = _mapper.Map<ProjectTask>(newProjectTaskDto);
            projectTaskEntity.ProjectId = newProjectTaskDto.ProjectId;
            projectEntity.Hours += projectTaskEntity.Hours;
            projectEntity.Tasks.Add(projectTaskEntity);
            _context.Update(projectEntity);
            await _context.SaveChangesAsync();
            return _mapper.Map<ProjectTaskDTO>(projectTaskEntity);
        }

        public async Task<ProjectTaskDTO> UpdateTask(UpdateProjectTaskDTO updatedProjectTask, int userId)
        {
            var projectTaskEntity = await _context
                .Tasks
                .FirstOrDefaultAsync(p => p.Id == updatedProjectTask.Id);
            if (projectTaskEntity is null)
            {
                throw new NotFoundException(nameof(ProjectTask), updatedProjectTask.Id);
            }
            var projectEntity = await _context
                .Projects
                .FirstOrDefaultAsync(project => project.Id == projectTaskEntity.ProjectId);
            if (projectEntity is null)
            {
                throw new NotFoundException(nameof(Project), projectTaskEntity.ProjectId);
            }
            if (projectEntity.AuthorId != userId)
            {
                throw new NoAccessException(userId);
            }

            projectTaskEntity.Deadine = updatedProjectTask.DeadLine;
            projectTaskEntity.TaskName = updatedProjectTask.TaskName;
            projectTaskEntity.Description = updatedProjectTask.Description;
            projectEntity.Hours = updatedProjectTask.Hours;
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
                throw new NotFoundException(nameof(Subtask), updatedSubtask.Id);
            }
            var taskEntity = await _context
                .Tasks
                .FirstOrDefaultAsync(task => task.Id == subtaskEntity.TaskId);
            if (taskEntity is null)
            {
                throw new NotFoundException(nameof(ProjectTask), subtaskEntity.TaskId);
            }
            var projectEntity = await _context
                .Projects
                .FirstOrDefaultAsync(project => project.Id == taskEntity.ProjectId);
            if (projectEntity is null)
            {
                throw new NotFoundException(nameof(Project), taskEntity.ProjectId);
            }
            if (projectEntity.AuthorId != userId)
            {
                throw new NoAccessException(userId);
            }

            subtaskEntity.Name = updatedSubtask.Name;
            _context.Subtasks.Update(subtaskEntity);
            await _context.SaveChangesAsync();
            return _mapper.Map<SubtaskDTO>(subtaskEntity);
        }

        public async Task<SubtaskDTO> AddSubtask(NewSubtaskDTO newSubtask, int userId)
        {
            var taskEntity = await _context
                .Tasks
                .FirstOrDefaultAsync(t => t.Id == newSubtask.TaskId);
            if(taskEntity is null)
            {
                throw new NotFoundException(nameof(ProjectTask), newSubtask.TaskId);
            }
            var projectEntity = await _context
                .Projects
                .FirstOrDefaultAsync(project => project.Id == taskEntity.ProjectId);

            if (projectEntity is null)
            {
                throw new NotFoundException(nameof(Project), taskEntity.ProjectId);
            }
            if(userId != projectEntity.AuthorId)
            {
                throw new NoAccessException(userId);
            }    
            var subtaskEntity = _mapper.Map<Subtask>(newSubtask);
            if (taskEntity.Subtasks == null)
            {
                taskEntity.Subtasks = new List<Subtask>();
            }
            taskEntity.Subtasks.Add(subtaskEntity);
            _context.Update(taskEntity);
            await _context.SaveChangesAsync();
            return _mapper.Map<SubtaskDTO>(subtaskEntity);
        }

        public async Task<List<SubtaskDTO>> GetAllSubtasks(int taskId)
        {
            var taskEntity = await _context
                .Tasks
                .FirstOrDefaultAsync(t => t.Id == taskId);
            if(taskEntity is null)
            {
                throw new NotFoundException(nameof(ProjectTask), taskId);
            }
            var subtasks = await _context
                .Subtasks
                .Where(t => t.TaskId == taskEntity.Id)
                .ToListAsync();
            return _mapper.Map<List<SubtaskDTO>>(subtasks);
        }

        public async Task<ProjectTaskDTO> AddUserToTask(int taskId, string email, int userId)
        {
            var taskEntity = await _context
                .Tasks
                .Include(task => task.Users)
                .FirstOrDefaultAsync(task => task.Id == taskId);

            if (taskEntity is null) 
            {
                throw new NotFoundException(nameof(ProjectTask), taskId);
            }

            var userEntity = await _context
                .Users
                .FirstOrDefaultAsync(user => user.Email == email);
            if (userEntity is null) 
            {
                throw new UserNotFoundException(email);
            }

            var projectEntity = await _context
                .Projects
                .FirstOrDefaultAsync(project => project.Id == taskEntity.ProjectId);

            if (projectEntity is null)
            {
                throw new NotFoundException(nameof(Project), taskEntity.ProjectId);
            }

            if (projectEntity.AuthorId != userId)
            {
                throw new NoAccessException(userId);
            }

            if (taskEntity.Users == null)
            {
                taskEntity.Users = new List<User>();
            }

            if (taskEntity.Users.FirstOrDefault(user => user.Id == userId) != null)
            {
                throw new ExistUserException(email);
            }

            //Will be removed
            //****
            taskEntity.Users = new List<User>();
            taskEntity.Users.Add(userEntity);
            //taskEntity.Users.Add(userEntity);
            _context.Tasks.Update(taskEntity);
            await _context.SaveChangesAsync();

            if (userEntity.IsAgreedToNotification)
            {
                var authorEntity = await _context
                    .Users
                    .FirstOrDefaultAsync(user => user.Id == userId);
                string message = $"{authorEntity.Name} asigned task ({taskEntity.TaskName}) of project ({projectEntity.ProjectName}) to you";
                await _mailService.SendMail(userEntity.Email, "Added to task", message, userEntity.Name);
            }

            return _mapper.Map<ProjectTaskDTO>(taskEntity);

        }

        public async Task<List<ProjectTaskDTO>> GetAllUsersTasksByProject(int userId, int projectId)
        {
            var userEntity = await _context
                .Users
                .Include(user => user.Tasks)
                .FirstOrDefaultAsync(user => user.Id == userId);
            if (userEntity is null)
            {
                throw new NotFoundException(nameof(User), userId);
            }
            var userTasksEntity = userEntity.Tasks.Where(task => task.ProjectId == projectId).ToList();
            return _mapper.Map<List<ProjectTaskDTO>>(userTasksEntity);
        }

        public async Task<List<ProjectTaskDTO>> GetAllTasksByProjectId(int projectId)
        {
            var projectEntity = await _context
                .Projects
                .FirstOrDefaultAsync(project => project.Id == projectId);
            if (projectEntity is null)
            {
                throw new NotFoundException(nameof(Project), projectId);
            }
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
            if(taskEntity is null)
            {
                throw new NotFoundException(nameof(ProjectTask), taskId);
            }
            var userEntity = taskEntity
                .Users
                .FirstOrDefault(user => user.Id == userId);
            if (userEntity is null)
            {
                throw new NoAccessException(userId);
            }
            bool wasInPending = taskEntity.TaskState == TaskState.Pending;
            bool isInProgress = taskState == TaskState.InProgress;
            if (wasInPending && isInProgress) 
            {
                taskEntity.TaskStart = DateTime.UtcNow;
            }
            taskEntity.TaskState = taskState;
            _context.Tasks.Update(taskEntity);
            _context.SaveChanges();
            if (taskState == TaskState.Done)
            {
                var projectEntity = await _context
                    .Projects
                    .FirstOrDefaultAsync(p => p.Id == taskEntity.ProjectId);
                var authorEntity = await _context
                    .Users
                    .FirstOrDefaultAsync(user => user.Id == projectEntity.AuthorId);
                if (authorEntity.IsAgreedToNotification)
                {
                    string message = $"{userEntity.Name} have done task ({taskEntity.TaskName}) in project ({projectEntity.ProjectName}).";
                    await _mailService.SendMail(authorEntity.Email, "Task done", message, authorEntity.Name);
                }
            }
            return _mapper.Map<ProjectTaskDTO>(taskEntity);
        }

        public async Task<ProjectTaskDTO> ApproveTask(int taskId, int userId)
        {
            var taskEntity = await _context
                .Tasks
                .Include(task => task.Users)
                .FirstOrDefaultAsync(task => task.Id == taskId);
            if (taskEntity is null) 
            {
                throw new NotFoundException(nameof(ProjectTask), taskId);
            }
            var projectEntity = await _context
                .Projects
                .FirstOrDefaultAsync(project => project.Id == taskEntity.ProjectId);
            if (projectEntity is null)
            {
                throw new NotFoundException(nameof(Project), taskEntity.ProjectId);
            }
            if (projectEntity.AuthorId != userId)
            {
                throw new NoAccessException(userId);
            }
            taskEntity.TaskState = TaskState.Approved;
            //taskEntity.TaskEnd = DateTime.UtcNow;
            _context.Tasks.Update(taskEntity);
            _context.SaveChanges();
            var userEntityId = taskEntity.Users.FirstOrDefault().Id;
            await UpdateSatistic(taskEntity.Id, userEntityId);
            if (taskEntity.Users.Count != 0)
            {
                foreach (var user in taskEntity.Users)
                {
                    if (user.IsAgreedToNotification)
                    {
                        string message = $"Your task {taskEntity.TaskName} was approved.";
                        await _mailService.SendMail(user.Email, "Task approved", message, user.Name);
                    }
            }
            }
            return _mapper.Map<ProjectTaskDTO>(taskEntity);
        }

        private async Task UpdateSatistic(int taskId, int userId)
        {
            var userEntity = await _context
                .Users
                .Include(user => user.Statistics)
                .FirstOrDefaultAsync(user => user.Id == userId);
            var taskEntity = await _context
                .Tasks
                .FirstOrDefaultAsync(task => task.Id == taskId);
            foreach (var topic in taskEntity.Tags)
            {
                var newStatistic = new StatisticDataDTO();
                newStatistic.UserId = userId;
                newStatistic.TaskTopic = topic;
                var timeDifference = (TimeSpan)(taskEntity.TaskEnd - taskEntity.TaskStart);
                var diff = timeDifference.TotalHours;
                double taskQuality = 50 + (taskEntity.Hours - diff)/ taskEntity.Hours*50;

                if (taskQuality >= 0)
                {
                    newStatistic.TaskQuality = taskQuality;
                }
                else
                {
                    newStatistic.TaskQuality = 0;
                }
                var statistic = userEntity.Statistics.FirstOrDefault(stat => stat.TaskTopic == topic);
                if (statistic.TaskCount == 0)
                {
                    statistic.TaskQuality = taskQuality;
                    statistic.TaskCount++;
                }
                else
                {
                    statistic.TaskQuality = (statistic.TaskQuality*statistic.TaskCount + taskQuality) / (statistic.TaskCount + 1);
                    statistic.TaskCount++;
                }
            }
            int statisticCount = 0;
            foreach (var statistic in userEntity.Statistics)
            {
                statisticCount += statistic.TaskCount;
            }
            if (!userEntity.IsEnoughData && statisticCount >= 10)
            {
                userEntity.IsEnoughData = true;
            }
            _context.Users.Update(userEntity);
            await _context.SaveChangesAsync();
        }

        public async Task<SubtaskDTO> ChangeSubtaskState(int subtaskId, TaskState taskState, int userId)
        {
            var subtaskEntity = await _context
                .Subtasks
                .FirstOrDefaultAsync(subtask => subtask.Id == subtaskId);
            if(subtaskEntity is null)
            {
                throw new NotFoundException(nameof(Subtask), subtaskId);
            }
            var taskEntity = await _context
                .Tasks
                .Include(task => task.Users)
                .FirstOrDefaultAsync(task => task.Id == subtaskEntity.TaskId);
            if (taskEntity is null) 
            {
                throw new NotFoundException(nameof(ProjectTask), subtaskEntity.TaskId);
            }
            var foundUser = taskEntity.Users.FirstOrDefault(user => user.Id == userId);
            if (foundUser is null)
            {
                throw new NoAccessException(userId);
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
                throw new NotFoundException(nameof(Subtask), subtaskId);
            }
            var taskEntity = await _context
                .Tasks
                .FirstOrDefaultAsync(task => task.Id == subtaskEntity.TaskId);
            if (taskEntity is null)
            {
                throw new NotFoundException(nameof(ProjectTask), subtaskEntity.TaskId);
            }
            var projectEntity = await _context
                .Projects
                .FirstOrDefaultAsync(project => project.Id == taskEntity.ProjectId);
            if (projectEntity is null)
            {
                throw new NotFoundException(nameof(Project), taskEntity.ProjectId);
            }
            if (projectEntity.AuthorId != userId)
            {
                throw new NoAccessException(userId);
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
                throw new NotFoundException(nameof(ProjectTask), taskId);
            }
            var projectEntity = await _context
                .Projects
                .FirstOrDefaultAsync(project => project.Id == taskEntity.ProjectId);
            if (projectEntity is null)
            {
                throw new NotFoundException(nameof(Project), taskEntity.ProjectId);
            }
            if (projectEntity.AuthorId != userId)
            {
                throw new NoAccessException(userId);
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
                throw new NotFoundException(nameof(Subtask), subtaskId);
            }
            var taskEntity = await _context
                .Tasks
                .FirstOrDefaultAsync(task => task.Id == subtaskEntity.TaskId);
            if (taskEntity is null)
            {
                throw new NotFoundException(nameof(ProjectTask), subtaskEntity.TaskId);
            }
            var projectEntity = await _context
                .Projects
                .FirstOrDefaultAsync(project => project.Id == taskEntity.ProjectId);
            if (projectEntity is null)
            {
                throw new NotFoundException(nameof(Project), taskEntity.ProjectId);
            }

            if (projectEntity.AuthorId != userId)
            {
                throw new NoAccessException(userId);
            }    
            _context.Subtasks.Remove(subtaskEntity);
            await _context.SaveChangesAsync();
        }
    }
}
