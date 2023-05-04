using AutoMapper;
using BAHelper.Common.DTOs.UserStoryFormula;
using BAHelper.DAL.Entities;

namespace BAHelper.BLL.MappingProfiles
{
    public class UserStoryFormulaProfile : Profile
    {
        public UserStoryFormulaProfile() 
        {
            CreateMap<NewUserStoryFormulaDTO, UserStoryFormula>();
            CreateMap<UserStoryFormula, UserStoryFormulaDTO>();

        }
    }
}
