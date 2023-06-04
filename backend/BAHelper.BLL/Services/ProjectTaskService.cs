using AutoMapper;
using BAHelper.BLL.Exceptions;
using BAHelper.BLL.Services.Abstract;
using BAHelper.Common.DTOs.ProjectTask;
using BAHelper.Common.DTOs.StatisticData;
using BAHelper.Common.Enums;
using BAHelper.DAL.Context;
using BAHelper.DAL.Entities;
using Microsoft.EntityFrameworkCore;

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

        public async Task<ProjectTaskInfoDTO> AddProjectTask(NewProjectTaskDTO newProjectTaskDto, int userId)
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
            projectTaskEntity.Deadine = newProjectTaskDto.Deadline;
            projectEntity.Hours += projectTaskEntity.Hours;
            projectEntity.Tasks.Add(projectTaskEntity);
            _context.Update(projectEntity);
            await _context.SaveChangesAsync();
            return _mapper.Map<ProjectTaskInfoDTO>(projectTaskEntity);
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

            taskEntity.Users = new List<User>();
            taskEntity.Users.Add(userEntity);
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


        public async Task<ProjectTaskDTO> ChangeTaskState(int userId, int taskId, int taskState)
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
            bool isInProgress = (TaskState)taskState == TaskState.InProgress;
            if (wasInPending && isInProgress) 
            {
                taskEntity.TaskStart = DateTime.UtcNow;
            }
            taskEntity.TaskState = (TaskState)taskState;
            _context.Tasks.Update(taskEntity);
            _context.SaveChanges();
            if ((TaskState)taskState == TaskState.Done)
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

        public async Task ApproveTask(int taskId, int userId)
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
                double K;
                if (diff <= 0.5*taskEntity.Hours)
                {
                    K = 1;
                }
                else if (diff < taskEntity.Hours)
                {
                    K = (taskEntity.Hours - diff) / taskEntity.Hours;
                }
                else
                {
                    K = 0;
                }
                double C;
                if (taskEntity.Hours >= 60)
                {
                    C = 1;
                }
                else if (taskEntity.Hours >= 40)
                {
                    C = 0.9;
                }
                else
                {
                    C = 0.8;
                }
                double taskQuality = 100 * K * C;

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
            if (!userEntity.IsEnoughData && statisticCount >= 5)
            {
                userEntity.IsEnoughData = true;
            }
            _context.Users.Update(userEntity);
            await _context.SaveChangesAsync();
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

        public async Task AddRandomTasks(int projectId, int count)
        {
            var projectEntity = await _context
                .Projects
                .Include(p => p.Tasks)
                .FirstOrDefaultAsync(p => p.Id == projectId);
                var rnd = new Random();
                List<double> randomHour = new List<double>(){40, 45, 50, 55, 60};
            for (int i = 0; i < count; i++)
            {
                NewProjectTaskDTO newTask = new NewProjectTaskDTO();
                newTask.Deadline = projectEntity.Deadline;
                newTask.Description = "";
                newTask.Hours = randomHour[rnd.Next(randomHour.Count)];
                newTask.ProjectId = projectId;
                newTask.Tags = new List<TopicTag>() {(TopicTag)rnd.Next(8)};
                newTask.TaskName = $"Завдання {i}";
                projectEntity.Tasks.Add(_mapper.Map<ProjectTask>(newTask));
                _context.Projects.Update(projectEntity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
