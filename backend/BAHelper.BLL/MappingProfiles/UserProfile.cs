using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BAHelper.Common.DTOs.User;
using BAHelper.DAL.Entities;

namespace BAHelper.BLL.MappingProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile() 
        {
            CreateMap<NewUserDTO, User>();
            CreateMap<User, UserDTO>();
            CreateMap<User, UserInfoDTO>();
            CreateMap<UserDTO, UserInfoDTO>();
        }
    }
}
