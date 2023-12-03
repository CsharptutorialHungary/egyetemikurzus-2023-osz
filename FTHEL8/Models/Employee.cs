namespace FTHEL8.Models
{
    public class Employee
    {
        public required string EmployeeId { get; set; }
        public string? Name { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? Position { get; set; }
        public int? Salary { get; set; }
        public string? DepartmentName { get; set; }

    }
}
