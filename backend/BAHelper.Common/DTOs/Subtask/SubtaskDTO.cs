using BAHelper.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAHelper.Common.DTOs.Subtask
{
    public class SubtaskDTO
    {
        public int Id { get; set; }
        public int TaskId { get; set; }
        public string Name { get; set; }
        public TaskState TaskState { get; set; }
    }
}
