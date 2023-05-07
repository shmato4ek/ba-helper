namespace BAHelper.Common.DTOs.ProjectTask
{
    public class NewProjectTaskDTO
    {
        public DateTime Deadine { get; set; }
        public int ProjectId { get; set; }
        public string? TaskName { get; set; }
        public string? Description { get; set; }
        public double Hours { get; set; }
    }
}
