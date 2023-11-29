using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTHEL8.Models
{
    public class Employee
    {
        public string EmployeeId { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Position { get; set; }
        public int Salary { get; set; }
        public Department Department { get; set; }

    }
}
