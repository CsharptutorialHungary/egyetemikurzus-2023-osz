﻿namespace FTHEL8.Models
{
    public class Project
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
        public DateTime? Deadline { get; set; }
        public Employee? ProjectLeader {  get; set; }
        public Class? ClassName { get; set; }

    }
}
