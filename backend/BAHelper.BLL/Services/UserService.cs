using AutoMapper;
using BAHelper.BLL.Exceptions;
using BAHelper.BLL.MappingProfiles;
using BAHelper.BLL.Services.Abstract;
using BAHelper.Common.DTOs.ProjectTask;
using BAHelper.Common.DTOs.StatisticData;
using BAHelper.Common.DTOs.User;
using BAHelper.Common.Enums;
using BAHelper.Common.Security;
using BAHelper.DAL.Context;
using BAHelper.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            userEntity.Statistics = new List<StatisticData>();
            for (int i = 0; i < 8; i++)
            {
                userEntity.Statistics.Add(new StatisticData() { TaskCount = 0, TaskTopic = (TopicTag)i});
            }

            var isUserExist = await _context
                .Users
                .FirstOrDefaultAsync(user => user.Email == newUser.Email) != null;
            if (isUserExist)
            {
                throw new ExistUserException(newUser.Email);
            }
            _context.Users.Add(userEntity);
            await _context.SaveChangesAsync();
            return _mapper.Map<UserDTO>(userEntity);
        }

        public async Task<List<UserDTO>> GetAllUsers()
        {
            var allUsers = await _context
                .Users
                .ToListAsync();
            var allUsersDTO = _mapper.Map<List<UserDTO>>(allUsers);
            return allUsersDTO;
        }

        public async Task<List<ProjectTaskDTO>> GetAllUsersTasks(int userId)
        {
            var userEntity = await _context
                .Users
                .Include(user => user.Tasks)
                .FirstOrDefaultAsync(user => user.Id == userId);
            if (userEntity is null)
            {
                throw new NotFoundException(nameof(User), userId);
            }
            var tasksEntity = userEntity.Tasks;
            return _mapper.Map<List<ProjectTaskDTO>>(tasksEntity);
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
                userEntity.Password = SecurityHelper.HashPassword(updatedUser.NewPassword, salt);
            }
            userEntity.Name = updatedUser.Name;
            userEntity.Email = updatedUser.Email;
            userEntity.IsAgreedToNotification = updatedUser.IsAgreedToNotification;
            _context.Users.Update(userEntity);
            await _context.SaveChangesAsync();
            return _mapper.Map<UserInfoDTO>(userEntity);
        }
    }
}
