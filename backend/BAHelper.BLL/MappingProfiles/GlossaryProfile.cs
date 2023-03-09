using AutoMapper;
using BAHelper.Common.DTOs.Glossary;
using BAHelper.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAHelper.BLL.MappingProfiles
{
    public class GlossaryProfile : Profile
    {
        public GlossaryProfile()
        {
            CreateMap<NewGlossaryDTO, Glossary>();
            CreateMap<Glossary, GlossaryDTO>();
        }
    }
}
