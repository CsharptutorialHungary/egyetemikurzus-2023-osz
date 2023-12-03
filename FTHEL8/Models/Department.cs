namespace FTHEL8.Models
{
    public class Department
    {
        public required string Name { get; set; }
        public string? Task { get; set; }
        public Employee? DepartmentLeader {  get; set; }
        public Class? ClassName { get; set; }

    }
}
