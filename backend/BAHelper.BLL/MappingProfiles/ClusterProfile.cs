using AutoMapper;
using BAHelper.Common.DTOs.Cluster;

namespace BAHelper.BLL.MappingProfiles
{
    public class ClusterProfile : Profile
    {
        public ClusterProfile() 
        {
            CreateMap<ClusterDTO, ClusterInfoDTO>();
        }
    }
}
