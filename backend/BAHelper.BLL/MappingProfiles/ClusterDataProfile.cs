using AutoMapper;
using BAHelper.Common.DTOs.Cluster;
using BAHelper.Common.DTOs.ClusterData;
using BAHelper.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAHelper.BLL.MappingProfiles
{
    public class ClusterDataProfile : Profile
    {
        public ClusterDataProfile() 
        {
            CreateMap<ClusterDataDTO, ClusterData>();
            CreateMap<ClusterData, ClusterDataDTO>();
        }
    }
}
