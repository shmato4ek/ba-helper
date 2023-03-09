using AutoMapper;
using BAHelper.Common.DTOs.Subtask;
using BAHelper.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAHelper.BLL.MappingProfiles
{
    public class SubtaskProfile : Profile
    {
        public SubtaskProfile()
        {
            CreateMap<NewSubtaskDTO, Subtask>();
            CreateMap<Subtask, SubtaskDTO>();
        }
    }
}
