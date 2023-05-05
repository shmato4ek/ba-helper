using BAHelper.Common.DTOs.Subtask;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAHelper.Common.DTOs.ProjectTask
{
    public class NewProjectTaskDTO
    {
        public DateTime Deadine { get; set; }
        public string TaskName { get; set; }
        public string Description { get; set; }
        public double Hours { get; set; }
    }
}
