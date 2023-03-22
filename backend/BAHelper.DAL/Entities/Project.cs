﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAHelper.DAL.Entities
{
    public class Project
    {
        public int Id { get; set; }
        public DateTime Deadline { get; set; }
        public int AuthorId { get; set; }
        public string ProjectName { get; set; }
        public List<ProjectTask> Tasks { get; set; }
        public List<User> Users { get; set; }
    }
}
