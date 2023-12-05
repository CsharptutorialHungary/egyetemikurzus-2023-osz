namespace FTHEL8.Models
{
    public class Project
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
        public DateTime? Deadline { get; set; }
        public string? ProjectLeader {  get; set; }
        public string? ClassName { get; set; }

    }
}
