using BAHelper.Common.DTOs.ProjectTask;
using BAHelper.Common.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAHelper.Common.DTOs.Project
{
    public class NewProjectDTO
    {
        public DateTime Deadline { get; set; }
        public string ProjectName { get; set; }
        public string Description { get; set; }
        //public List<NewProjectTaskDTO> Tasks { get; set; }
        public List<string>? Users { get; set; }
    }
}
