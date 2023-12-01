using FTHEL8.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FTHEL8.Data;

namespace FTHEL8.Menu
{
    public class DeleteMenu : Menu
    {
        public DeleteMenu() : base([], [])
        {
            Console.WriteLine();
            AddOption("Delete an employee", DeleteEmployee);
            AddOption("Delete a department", DeleteDepartment);
            AddOption("Delete a project", DeleteProject);
            AddOption("Delete a class", DeleteClass);
            AddOption("Back", Back);
        }

        private async void DeleteEmployee()
        {
            try
            {
                Console.Write("Enter Employee ID to delete: ");
                string employeeId = Console.ReadLine();

                if (await Database.DeleteEmployeeAsync(employeeId))
                {
                    Console.WriteLine($"Employee with ID {employeeId} deleted successfully.");
                }
                else
                {
                    Console.WriteLine($"Employee with ID {employeeId} is not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting employee: {ex.Message}");
            }
        }

        private async void DeleteDepartment()
        {
            try
            {
                Console.Write("Enter Department Name to delete: ");
                string departmentName = Console.ReadLine();
                if(await Database.DeleteDepartmentAsync(departmentName))
                {
                    Console.WriteLine($"Department with Name {departmentName} deleted successfully.");
                }
                else
                {
                    Console.WriteLine($"Department with Name {departmentName} is not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting department: {ex.Message}");
            }
        }

        private async void DeleteProject()
        {
            try
            {
                Console.Write("Enter Project Name to delete: ");
                string projectName = Console.ReadLine();
                if (await Database.DeleteProjectAsync(projectName))
                {
                    Console.WriteLine($"Project with Name {projectName} deleted successfully.");
                }
                else
                {
                    Console.WriteLine($"Project with Name {projectName} is not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting project: {ex.Message}");
            }
        }

        private async void DeleteClass()
        {
            try
            {
                Console.Write("Enter Class Name to delete: ");
                string className = Console.ReadLine();
                if (await Database.DeleteClassAsync(className))
                {
                    Console.WriteLine($"Class with Name {className} deleted successfully.");
                }
                else
                {
                    Console.WriteLine($"Class with Name {className} is not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting class: {ex.Message}");
            }
        }

    }
}
