using BAHelper.Common.DTOs.Subtask;
using BAHelper.Common.DTOs.User;

namespace BAHelper.Common.DTOs.ProjectTask
{
    public class ProjectTaskInfoDTO
    {
        public int Id { get; set; }
        public string? Deadline { get; set; }
        public string? TaskName { get; set; }
        public string? Description { get; set; }
        public double Hours { get; set; }
        public bool CanEditState { get; set; } = false;
        public List<int>? Tags { get; set; }
        public List<UserInfoDTO>? Users { get; set; }
        public List<SubtaskDTO>? Subtasks { get; set; }
        public int TaskState { get; set; }
    }
}
