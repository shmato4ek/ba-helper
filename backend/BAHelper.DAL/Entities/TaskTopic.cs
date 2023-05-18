using BAHelper.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAHelper.DAL.Entities
{
    public class TaskTopic
    {
        public int Id { get; set; }
        public int TaskId { get; set; }
        public TopicTag Tag { get; set; }
    }
}
