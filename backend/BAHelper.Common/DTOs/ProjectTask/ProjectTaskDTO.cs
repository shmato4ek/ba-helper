using BAHelper.Common.DTOs.Subtask;
using BAHelper.Common.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAHelper.Common.DTOs.ProjectTask
{
    public class ProjectTaskDTO
    {
        public int Id { get; set; }
        public DateTime DeadLine { get; set; }
        public int ProjectId { get; set; }
        public string TaskName { get; set; }
        public string Description { get; set; }
        public List<UserDTO> Users { get; set; }
        public List<SubtaskDTO> Subtasks { get; set; }
    }
}
