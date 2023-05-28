using AutoMapper;
using BAHelper.Common.DTOs.ProjectTask;
using BAHelper.DAL.Entities;

namespace BAHelper.BLL.MappingProfiles
{
    public class ProjectTaskProfile : Profile
    {
        public ProjectTaskProfile()
        {
            CreateMap<NewProjectTaskDTO, ProjectTask>();
            CreateMap<ProjectTask, ProjectTaskDTO>()
                .ForMember(p => p.TaskState, opt => opt.MapFrom(t => (int)t.TaskState));
            CreateMap<ProjectTask, ProjectTaskInfoDTO>()
                .ForMember(p => p.Deadline, opt => opt.MapFrom(t => t.Deadine.ToString("s")))
                .ForMember(p => p.TaskState, opt => opt.MapFrom(t => (int)t.TaskState))
                .ForMember(p => p.Tags, opt => opt.MapFrom(t => t.Tags.Select(t => (int)t)));
        }
    }
}
