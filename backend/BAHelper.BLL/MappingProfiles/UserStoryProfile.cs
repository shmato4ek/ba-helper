using AutoMapper;
using BAHelper.Common.DTOs.UserStory;
using BAHelper.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAHelper.BLL.MappingProfiles
{
    public class UserStoryProfile : Profile
    {
        public UserStoryProfile() 
        {
            CreateMap<NewUserStoryDTO, UserStory>();
            CreateMap<UserStory, UserStoryDTO>();
        }
    }
}
