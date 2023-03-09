using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAHelper.DAL.Entities
{
    public class ScheduleDay
    {
        public int Id { get; set; }
        public int ScheduleId { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public List<ProjectTask> Tasks { get; set; }
        public double HoursOfWeeks { get; set; }
    }
}
