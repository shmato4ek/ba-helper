﻿using BAHelper.Common.DTOs.Subtask;
using BAHelper.Common.DTOs.User;
using BAHelper.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAHelper.Common.DTOs.ProjectTask
{
    public class ProjectTaskInfoDTO
    {
        public int Id { get; set; }
        public DateTime DeadLine { get; set; }
        public string TaskName { get; set; }
        public string Description { get; set; }
        public double Hours { get; set; }
        public bool CanEditState { get; set; } = false;
        public List<UserInfoDTO> Users { get; set; }
        public List<SubtaskDTO> Subtasks { get; set; }
        public TaskState TaskState { get; set; } = TaskState.Pending;
    }
}
