using BAHelper.Common.DTOs.Subtask;
using BAHelper.Common.DTOs.User;
using BAHelper.Common.Enums;
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
        public double Hours { get; set; }
        public List<TopicTag> Tags { get; set; }
        public DateTime? TaskStart { get; set; }
        public DateTime? TaskEnd { get; set; }
        public List<UserInfoDTO> Users { get; set; }
        public List<SubtaskDTO> Subtasks { get; set; }
        public int TaskState { get; set; }
    }
}
