using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAHelper.Common.DTOs.ScheduleDay
{
    public class NewScheduleDayDTO
    {
        public int ScheduleId { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public double HoursOfWork { get; set; }
        public double AvailableHoursOfWork { get; set; }
    }
}
