using AutoMapper;
using BAHelper.Common.DTOs.StatisticData;
using BAHelper.DAL.Entities;

namespace BAHelper.BLL.MappingProfiles
{
    public class StatisticDataProfile : Profile
    {
        public StatisticDataProfile()
        {
            CreateMap<NewStatisticDataDTO, StatisticData>();
            CreateMap<StatisticData, StatisticDataDTO>();
            CreateMap<StatisticDataDTO, StatisticData>();
            CreateMap<StatisticData, StatisticDataInfo>().ForMember(s => s.TaskQuality, opt => opt.MapFrom(d => (int)Math.Round(d.TaskQuality)));
        }
    }
}
