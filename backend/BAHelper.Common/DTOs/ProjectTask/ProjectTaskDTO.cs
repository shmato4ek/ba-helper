using BAHelper.Common.DTOs.Subtask;
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
        public double TimeForTask { get; set; }
        public int ProjectId { get; set; }
        public string TaskName { get; set; }
        public string Description { get; set; }
        public List<SubtaskDTO> Subtasks { get; set; }
    }
}
