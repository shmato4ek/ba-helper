using BAHelper.Common.DTOs.ProjectTask;
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
        public int UserId { get; set; }
        public int Difficulty { get; set; }
        public string ProjectName { get; set; }
    }
}
