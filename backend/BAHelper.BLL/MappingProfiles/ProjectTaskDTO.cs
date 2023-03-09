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
    public class ProjectTaskDTO : Profile
    {
        public ProjectTaskDTO()
        {
            CreateMap<NewProjectTaskDTO, ProjectTask>();
            CreateMap<ProjectTask, ProjectTaskDTO>();
        }
    }
}
