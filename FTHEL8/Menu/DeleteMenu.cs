using FTHEL8.Data;
using FTHEL8.Models;

namespace FTHEL8.Menu
{
    public class DeleteMenu : Menu
    {
        private readonly MainMenu mainMenu;
        public DeleteMenu(MainMenu mainMenu)
        {
            this.mainMenu = mainMenu;
            Add("Delete an employee", DeleteEmployee);
            Add("Delete a department", DeleteDepartment);
            Add("Delete a project", DeleteProject);
            Add("Delete a class", DeleteClass);
            Add("Back to the main menu", GetPreviousMenu);
        }

        public void GetPreviousMenu()
        {
            mainMenu.Display();
        }

        private void DeleteEmployee()
        {
            try
            {
                List<Employee> employeeList = Database.ReadEmployeesAsync().Result;
                foreach (Employee employee in employeeList)
                {
                    Console.Write(employee.EmployeeId + " ");
                }
                Console.WriteLine();
                Console.Write("Enter Employee ID to delete: ");
                string employeeId = Console.ReadLine() ?? "";

                if (Database.DeleteEmployeeAsync(employeeId).Result)
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

        private void DeleteDepartment()
        {
            try
            {
                List<Department> departmentList = Database.ReadDepartmentsAsync().Result;
                foreach (Department department in departmentList)
                {
                    Console.Write(department.Name + " ");
                }
                Console.WriteLine();
                Console.Write("Enter Department Name to delete: ");
                string departmentName = Console.ReadLine() ?? "";
                if(Database.DeleteDepartmentAsync(departmentName).Result)
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

        private void DeleteProject()
        {
            try
            {
                List<Project> projectList = Database.ReadProjectsAsync().Result;
                foreach (Project project in projectList)
                {
                    Console.Write(project.Name + " ");
                }
                Console.WriteLine();
                Console.Write("Enter Project Name to delete: ");
                string projectName = Console.ReadLine() ?? "";
                if (Database.DeleteProjectAsync(projectName).Result)
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

        private void DeleteClass()
        {
            try
            {
                List<Class> classList = Database.ReadClassesAsync().Result;
                foreach (Class class_ in classList)
                {
                    Console.Write(class_.Name + " ");
                }
                Console.WriteLine();
                Console.Write("Enter Class Name to delete: ");
                string className = Console.ReadLine() ?? "";
                if (Database.DeleteClassAsync(className).Result)
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

    }
}
