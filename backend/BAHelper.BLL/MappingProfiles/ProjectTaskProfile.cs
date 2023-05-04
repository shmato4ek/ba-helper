using AutoMapper;
using BAHelper.Common.DTOs.ProjectTask;
using BAHelper.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAHelper.BLL.MappingProfiles
{
    public class ProjectTaskProfile : Profile
    {
        public ProjectTaskProfile()
        {
            CreateMap<NewProjectTaskDTO, ProjectTask>();
            CreateMap<ProjectTask, ProjectTaskDTO>();
            CreateMap<ProjectTask, ProjectTaskInfoDTO>();
        }
    }
}
