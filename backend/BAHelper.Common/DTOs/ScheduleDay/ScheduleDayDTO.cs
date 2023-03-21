using BAHelper.Common.DTOs.ProjectTask;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAHelper.Common.DTOs.ScheduleDay
{
    public class ScheduleDayDTO
    {
        public int Id { get; set; }
        public int ScheduleId { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public List<ProjectTaskDTO> Tasks { get; set; }
        public double HoursOfWork { get; set; }
        public double AvailableHoursOfWork { get; set; }
    }
}
