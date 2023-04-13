using AutoMapper;
using BAHelper.BLL.MappingProfiles;
using BAHelper.BLL.Services.Abstract;
using BAHelper.Common.DTOs.ProjectTask;
using BAHelper.Common.DTOs.User;
using BAHelper.DAL.Context;
using BAHelper.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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
            var UserEntity = _mapper.Map<User>(newUser);
            _context.Users.Add(UserEntity);
            await _context.SaveChangesAsync();
            return _mapper.Map<UserDTO>(UserEntity);
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
    }
}
