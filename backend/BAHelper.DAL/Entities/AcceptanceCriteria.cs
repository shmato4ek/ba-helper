﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAHelper.DAL.Entities
{
    public class AcceptanceCriteria
    {
        public int Id { get; set; }
        public int UserStoryId { get; set; }
        public string Text { get; set; }
    }
}
