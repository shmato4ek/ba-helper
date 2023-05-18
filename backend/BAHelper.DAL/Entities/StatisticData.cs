using BAHelper.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAHelper.DAL.Entities
{
    public class StatisticData
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public TopicTag TaskTopic { get; set; }
        public double TaskQuality { get; set; }
        public int TaskCount { get; set; } = 0;
    }
}
