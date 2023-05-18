using BAHelper.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAHelper.DAL.Entities
{
    public class ClusterData
    {
        public int Id { get; set; }
        public int ClusterId { get; set; }
        public TopicTag Topic { get; set; }
        public double Quality { get; set; }
    }
}
