using BAHelper.Common.DTOs.ScheduleDay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAHelper.Common.DTOs.Schedule
{
    public class ScheduleDTO
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public List<ScheduleDayDTO> Days { get; set; }
    }
}
