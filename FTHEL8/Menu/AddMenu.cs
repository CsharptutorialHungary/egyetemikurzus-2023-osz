using FTHEL8.Data;
using FTHEL8.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTHEL8.Menu
{
    public class AddMenu : Menu
    {
        public AddMenu() : base([], [])
        {
            AddOption("Add an employee", AddEmployee);
            AddOption("Add a department", AddDepartment);
            AddOption("Add a class", AddClass);
            AddOption("Add a project", AddProject);
            AddOption("Back", Back);
        }

        private void AddProject()
        {
            throw new NotImplementedException();
        }

        private void AddClass()
        {
            throw new NotImplementedException();
        }

        private void AddDepartment()
        {
            throw new NotImplementedException();
        }

        private static async void AddEmployee()
        {
            try
            {
                Console.WriteLine("Enter Employee ID: ");
                string employeeId = Console.ReadLine();

                Console.WriteLine("Enter Name: ");
                string name = Console.ReadLine();

                Console.WriteLine("Enter Phone Number: ");
                string phoneNumber = Console.ReadLine();

                Console.WriteLine("Enter Email: ");
                string email = Console.ReadLine();

                Console.WriteLine("Enter Position: ");
                string position = Console.ReadLine();

                Console.WriteLine("Enter Salary, must be a number: ");
                int salary = int.Parse(Console.ReadLine());

                Console.WriteLine("Enter a valid Department from the list: ");
                List<Department> departmentList = new List<Department>();
                departmentList = await Database.ReadDepartmentsAsync();
                foreach (Department department in departmentList)
                {
                    Console.Write(department.Name + " ");
                }
                Console.WriteLine();


                string departmentName = Console.ReadLine();

                bool success = await Database.AddEmployeeAsync(employeeId, name, phoneNumber, email, position, salary, departmentName);

                if (success)
                {
                    Console.WriteLine("Employee added successfully!");
                }
                else
                {
                    Console.WriteLine("Failed to add employee. Please check your input.");
                }
            }catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
            }
        }

    }
}
