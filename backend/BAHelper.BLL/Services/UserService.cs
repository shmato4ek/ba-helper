using AutoMapper;
using BAHelper.BLL.Exceptions;
using BAHelper.BLL.Services.Abstract;
using BAHelper.Common.DTOs.ProjectTask;
using BAHelper.Common.DTOs.StatisticData;
using BAHelper.Common.DTOs.User;
using BAHelper.Common.Enums;
using BAHelper.Common.Security;
using BAHelper.DAL.Context;
using BAHelper.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace BAHelper.BLL.Services
{
    public class UserService : BaseService
    {
        public UserService(BAHelperDbContext context, IMapper mapper)
            : base(context, mapper)
        {

        }
        public async Task<UserDTO> CreateUser(NewUserDTO newUser)
        {
            var userEntity = _mapper.Map<User>(newUser);

            var salt = SecurityHelper.GetRandomBytes();
            userEntity.Salt = Convert.ToBase64String(salt);
            userEntity.Password = SecurityHelper.HashPassword(newUser.Password, salt);

            var isUserExist = await _context
                .Users
                .FirstOrDefaultAsync(user => user.Email == newUser.Email) != null;
            if (isUserExist)
            {
                throw new ExistUserException(newUser.Email);
            }
            _context.Users.Add(userEntity);
            await _context.SaveChangesAsync();
            var createdUser = await _context
                .Users
                .FirstOrDefaultAsync(user => user.Email == newUser.Email);
            userEntity.Statistics = new List<StatisticData>();
            for (int i = 1; i <= 8; i++)
            {
                var newStatistic = new NewStatisticDataDTO() { TaskCount = 0, TaskQuality = 0, TaskTopic = (TopicTag)i, UserId = createdUser.Id };
                userEntity.Statistics.Add(_mapper.Map<StatisticData>(newStatistic));
            }
            return _mapper.Map<UserDTO>(createdUser);
        }

        public async Task DeleteUser(int userId)
        {
            var userEntity = await _context
                .Users
                .FirstOrDefaultAsync(user => user.Id == userId);
            if(userEntity is null)
            {
                throw new NotFoundException(nameof(User), userId);
            }
            _context.Users.Remove(userEntity);
            await _context.SaveChangesAsync();
        }

        public async Task<UserDTO> GetUserById(int userId)
        {
            var userEntity = await _context
                .Users
                .FirstOrDefaultAsync(user => user.Id == userId);
            if (userEntity is null)
            {
                throw new NotFoundException(nameof(User), userId);
            }
            return _mapper.Map<UserDTO>(userEntity);
        }

        public async Task<UserInfoDTO> UpdateUser(UpdateUserDTO updatedUser, int userId)
        {
            var userEntity = await _context
                .Users
                .FirstOrDefaultAsync(user => user.Id == userId);
            if (userEntity is null)
            {
                throw new NotFoundException(nameof(User), userId);
            }
            if (updatedUser.ChangePassword == true)
            {
                if (!SecurityHelper.IsValidPassword(userEntity.Password, updatedUser.OldPassword, userEntity.Salt))
                {
                    throw new InvalidUserNameOrPasswordException();
                }
                var salt = SecurityHelper.GetRandomBytes();
                userEntity.Password = SecurityHelper.HashPassword(updatedUser.Password, salt);
            }
            userEntity.Name = updatedUser.Name;
            userEntity.Email = updatedUser.Email;
            userEntity.IsAgreedToNotification = updatedUser.IsAgreedToNotification;
            _context.Users.Update(userEntity);
            await _context.SaveChangesAsync();
            return _mapper.Map<UserInfoDTO>(userEntity);
        }

        public async Task<List<StatisticDataInfo>> GetUserStatistic(int userId)
        {
            var userEntity = await _context
                .Users
                .Include(user => user.Statistics)
                .FirstOrDefaultAsync(user => user.Id == userId);
            if (userEntity is null)
            {
                throw new NotFoundException(nameof(User), userId);
            }
            var statistics = _mapper.Map<List<StatisticDataInfo>>(userEntity.Statistics);
            statistics.Sort(CompareStatistic);
            return statistics;
        }

        private static int CompareStatistic(StatisticDataInfo x, StatisticDataInfo y)
        {
            if (x == null)
            {
                if (y == null)
                {
                    return 0;
                }
                else
                {
                    return -1;
                }
            }
            else
            {
                if (y == null)
                {
                    return 1;
                }
                else
                {
                    int retval = y.TaskQuality.CompareTo(x.TaskQuality);

                    if (retval != 0)
                    {
                        return retval;
                    }
                    else
                    {
                        return y.TaskQuality.CompareTo(x.TaskQuality);
                    }
                }
            }
        }

    }
}
