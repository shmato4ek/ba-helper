using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAHelper.DAL.Entities
{
    public class Project
    {
        public int Id { get; set; }
        public DateTime Deadline { get; set; }
        public int UserId { get; set; }
        public int Difficulty { get; set; }
        public string ProjectName { get; set; }
        public Schedule Schedule { get; set; }
        public List<ProjectTask> Tasks { get; set; }
    }
}
