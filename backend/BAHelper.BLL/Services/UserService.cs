using AutoMapper;
using BAHelper.BLL.MappingProfiles;
using BAHelper.BLL.Services.Abstract;
using BAHelper.Common.DTOs.ProjectTask;
using BAHelper.Common.DTOs.User;
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

            var isUserExist = await _context
                .Users
                .FirstOrDefaultAsync(user => user.Email == newUser.Email) != null;
            if (isUserExist)
            {
                return null;
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
            if (userEntity != null)
            {
                var tasksEntity = userEntity.Tasks;
                if (tasksEntity != null)
                {
                    return _mapper.Map<List<ProjectTaskDTO>>(tasksEntity);
                }
                return null;
            }
            return null;
        }

        public async Task<UserDTO> DeleteUser(int userId)
        {
            var userEntity = await _context
                .Users
                .FirstOrDefaultAsync(user => user.Id == userId);
            if(userEntity != null)
            {
                _context.Users.Remove(userEntity);
                _context.SaveChanges();
                return _mapper.Map<UserDTO>(userEntity);
            }
            return null;
        }

        public async Task<UserDTO> GetUserById(int userId)
        {
            var userEntity = await _context
                .Users
                .FirstOrDefaultAsync(user => user.Id == userId);
            return _mapper.Map<UserDTO>(userEntity);
        }
    }
}
