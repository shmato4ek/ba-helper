using AutoMapper;
using BAHelper.Common.DTOs.UserStoryFormula;
using BAHelper.DAL.Entities;
using Microsoft.Vbe.Interop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAHelper.BLL.MappingProfiles
{
    public class UserStoryFormulaProfile : Profile
    {
        public UserStoryFormulaProfile() 
        {
            CreateMap<UserStoryFormulaDTO, UserStoryFormula>();
            CreateMap<UserStoryFormula, UserStoryFormulaDTO>();

        }
    }
}
