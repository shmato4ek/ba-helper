using AutoMapper;
using BAHelper.Common.DTOs.AcceptanceCriteria;
using BAHelper.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAHelper.BLL.MappingProfiles
{
    public class AcceptanceCriteriaProfile : Profile
    {
        public AcceptanceCriteriaProfile() 
        {
            CreateMap<NewAcceptanceCriteriaDTO, AcceptanceCriteria>();
            CreateMap<AcceptanceCriteria, AcceptanceCriteriaDTO>();
        }
    }
}
