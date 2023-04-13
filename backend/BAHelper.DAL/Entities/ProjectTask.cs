using BAHelper.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAHelper.DAL.Entities
{
    public class ProjectTask
    {
        public int Id { get; set; }
        public DateTime Deadine { get; set; }
        public int ProjectId { get; set; }
        public string TaskName { get; set; }
        public string Description { get; set; }
        public List<User> Users { get; set; }
        public List<Subtask> Subtasks { get; set; }
        public TaskState TaskState { get; set; } = TaskState.Pending;
    }
}
