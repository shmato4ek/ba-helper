using BAHelper.Common.DTOs.ProjectTask;
using BAHelper.Common.DTOs.Schedule;
using BAHelper.Common.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAHelper.Common.DTOs.Project
{
    public class ProjectDTO
    {
        public int Id { get; set; }
        public DateTime Deadline { get; set; }
        public int AuthorId { get; set; }
        public string ProjectName { get; set; }
        public double Hours { get; set; }
        public bool IsDeleted { get; set; }
        public List<ProjectTaskDTO> Tasks { get; set; }
        public List<UserDTO> Users { get; set; }
    }
}
