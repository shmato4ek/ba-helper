using AutoMapper;
using BAHelper.Common.DTOs.Project;
using BAHelper.Common.DTOs.ProjectTask;
using BAHelper.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAHelper.BLL.MappingProfiles
{
    public class ProjectProfile : Profile
    {
        public ProjectProfile()
        {
            CreateMap<NewProjectDTO, Project>();
            CreateMap<Project, ProjectDTO>();
            CreateMap<Project, ProjectInfoDTO>();
            CreateMap<ProjectDTO, Project>();
        }
    }
}
