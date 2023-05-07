﻿using BAHelper.Common.DTOs.ProjectTask;
using BAHelper.Common.DTOs.User;

namespace BAHelper.Common.DTOs.Project
{
    public class ProjectDTO
    {
        public int Id { get; set; }
        public DateTime Deadline { get; set; }
        public int AuthorId { get; set; }
        public string? ProjectName { get; set; }
        public string? Description { get; set; }
        public double Hours { get; set; }
        public bool IsDeleted { get; set; }
        public bool CanEdit { get; set; } = false;
        public List<ProjectTaskDTO>? Tasks { get; set; }
        public List<UserDTO>? Users { get; set; }
    }
}
