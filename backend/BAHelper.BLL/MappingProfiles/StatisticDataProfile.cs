using AutoMapper;
using BAHelper.Common.DTOs.StatisticData;
using BAHelper.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAHelper.BLL.MappingProfiles
{
    public class StatisticDataProfile : Profile
    {
        public StatisticDataProfile()
        {
            CreateMap<NewStatisticDataDTO, StatisticData>();
            CreateMap<StatisticData, StatisticDataDTO>();
            CreateMap<StatisticDataDTO, StatisticData>();
        }
    }
}
