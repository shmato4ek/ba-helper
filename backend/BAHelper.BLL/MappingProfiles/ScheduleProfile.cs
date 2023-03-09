using AutoMapper;
using BAHelper.Common.DTOs.Schedule;
using BAHelper.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAHelper.BLL.MappingProfiles
{
    public class ScheduleProfile : Profile
    {
        public ScheduleProfile()
        {
            CreateMap<NewScheduleDTO, Schedule>();
            CreateMap<Schedule, ScheduleDTO>();
        }
    }
}
