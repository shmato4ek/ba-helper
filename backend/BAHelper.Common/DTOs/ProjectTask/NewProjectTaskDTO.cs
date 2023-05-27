using BAHelper.Common.Enums;

namespace BAHelper.Common.DTOs.ProjectTask
{
    public class NewProjectTaskDTO
    {
        public DateTime Deadline { get; set; }
        public int ProjectId { get; set; }
        public List<TopicTag>? Tags { get; set; }
        public string? TaskName { get; set; }
        public string? Description { get; set; }
        public double Hours { get; set; }
    }
}
