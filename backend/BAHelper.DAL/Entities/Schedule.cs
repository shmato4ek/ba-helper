using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAHelper.DAL.Entities
{
    public class Schedule
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public List<ScheduleDay> Days { get; set; }
    }
}
