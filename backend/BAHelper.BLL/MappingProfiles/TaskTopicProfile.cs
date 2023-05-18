using AutoMapper;
using BAHelper.Common.DTOs.TaskTopic;
using BAHelper.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAHelper.BLL.MappingProfiles
{
    public class TaskTopicProfile : Profile
    {
        public TaskTopicProfile() 
        {
            CreateMap<TaskTopic, TaskTopicDTO>();
            CreateMap<TaskTopicDTO, TaskTopic>();
        }
    }
}
