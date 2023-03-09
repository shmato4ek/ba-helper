using AutoMapper;
using BAHelper.Common.DTOs.ScheduleDay;
using BAHelper.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAHelper.BLL.MappingProfiles
{
    public class ScheduleDayeProfile : Profile
    {
        public ScheduleDayeProfile()
        {
            CreateMap<NewScheduleDayDTO, ScheduleDay>();
            CreateMap<ScheduleDay, ScheduleDayDTO>();
        }
    }
}
