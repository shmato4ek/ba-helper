using BAHelper.Common.DTOs.ProjectTask;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAHelper.Common.DTOs.Project
{
    public class UpdateProjectDTO
    {
        public int Id { get; set; }
        public DateTime Deadline { get; set; }
        public string ProjectName { get; set; }
        public string Description { get; set; }
        public List<string> Users { get; set; }
    }
}
