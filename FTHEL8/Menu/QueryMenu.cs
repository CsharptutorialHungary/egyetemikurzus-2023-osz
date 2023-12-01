using FTHEL8.Data;
using FTHEL8.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTHEL8.Menu
{
    public class QueryMenu : Menu
    {
        public QueryMenu() : base([], [])
        {
            AddOption("Get employees data", GetEmployeesData);
            AddOption("Option 2", Option2);
            AddOption("Back", Back);
        }

        private static void GetEmployeesData()
        {
            List<Employee> employees = Database.ReadEmployees();
            Console.WriteLine("Employees:");
            foreach (var employee in employees)
            {
                Console.WriteLine($"Employee ID: {employee.EmployeeId}, Name: {employee.Name}, Position: {employee.Position}, Salary: {employee.Salary}");
            }
        }

        private static void Option2()
        {
            Console.WriteLine("Selected: Option 2 in Query Menu");
        }

    }
}
