﻿using FTHEL8.Data;

namespace FTHEL8.Menu
{
    public class DeleteMenu : Menu
    {
        public DeleteMenu()
        {
            Console.WriteLine();
        }

        private async void DeleteEmployee()
        {
            try
            {
                Console.Write("Enter Employee ID to delete: ");
                string employeeId = Console.ReadLine() ?? "";

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
                Console.Error.WriteLine($"Error deleting employee: {ex.Message}");
            }
        }

        private async void DeleteDepartment()
        {
            try
            {
                Console.Write("Enter Department Name to delete: ");
                string departmentName = Console.ReadLine() ?? "";
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
                Console.Error.WriteLine($"Error deleting department: {ex.Message}");
            }
        }

        private async void DeleteProject()
        {
            try
            {
                Console.Write("Enter Project Name to delete: ");
                string projectName = Console.ReadLine() ?? "";
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
                Console.Error.WriteLine($"Error deleting project: {ex.Message}");
            }
        }

        private async void DeleteClass()
        {
            try
            {
                Console.Write("Enter Class Name to delete: ");
                string className = Console.ReadLine() ?? "";
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
                Console.Error.WriteLine($"Error deleting class: {ex.Message}");
            }
        }

        public Task Display()
        {
            throw new NotImplementedException();
        }

        public Menu GetNextMenu()
        {
            Console.WriteLine("Going back to Main Menu.");
            return new MainMenu();
        }

        public Menu GetPreviousMenu()
        {
            throw new NotImplementedException();
        }
    }
}
