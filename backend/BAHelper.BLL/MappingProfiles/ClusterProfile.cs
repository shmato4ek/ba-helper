using AutoMapper;
using BAHelper.Common.DTOs.Cluster;
using BAHelper.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAHelper.BLL.MappingProfiles
{
    public class ClusterProfile : Profile
    {
        public ClusterProfile() 
        {
            CreateMap<ClusterDTO, Cluster>();
            CreateMap<Cluster, ClusterDTO>();
        }
    }
}
