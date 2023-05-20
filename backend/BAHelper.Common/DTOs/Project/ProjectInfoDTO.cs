using BAHelper.Common.DTOs.ProjectTask;
using BAHelper.Common.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAHelper.Common.DTOs.Project
{
    public class ProjectInfoDTO
    {
        public int Id { get; set; }
        public DateTime Deadline { get; set; }
        public int AuthorId { get; set; }
        public string AuthorName { get; set; }
        public string ProjectName { get; set; }
        public string Description { get; set; }
        public double Hours { get; set; }
        public bool CanEdit { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime ArchivedDate { get; set; }
        public List<ProjectTaskInfoDTO> Tasks { get; set; }
        public List<UserInfoDTO> Users { get; set; }
    }
}
