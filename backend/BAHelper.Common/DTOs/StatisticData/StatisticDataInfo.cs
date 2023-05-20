﻿using BAHelper.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAHelper.Common.DTOs.StatisticData
{
    public class StatisticDataInfo
    {
        public TopicTag TaskTopic { get; set; }
        public double TaskQuality { get; set; }
        public int TaskCount { get; set; }
    }
}
