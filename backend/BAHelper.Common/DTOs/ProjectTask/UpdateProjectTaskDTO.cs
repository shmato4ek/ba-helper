using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAHelper.Common.DTOs.ProjectTask
{
    public class UpdateProjectTaskDTO
    {
        public int Id { get; set; }
        public DateTime DeadLine { get; set; }
        public string TaskName { get; set; }
        public double Hours { get; set; }
        public string Description { get; set; }
    }
}
