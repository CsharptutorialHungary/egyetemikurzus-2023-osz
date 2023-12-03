using FTHEL8.Data;
using FTHEL8.Models;

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
            AddOption("Add an employee to a project", AddEmployeeToProject);
            AddOption("Back", Back);
        }

        private async void AddEmployeeToProject()
        {
            try
            {
                Console.WriteLine("Enter Employee ID from the list: ");
                List<Employee> employees = await Database.ReadEmployeesAsync();
                foreach (Employee employee in employees)
                {
                    Console.Write(employee.EmployeeId + " ");
                }
                Console.WriteLine();
                string employeeId = Console.ReadLine() ?? "";

                Console.WriteLine("Enter Project Name from the list: ");
                List<Project> projectList = await Database.ReadProjectsAsync();
                foreach (Project project in projectList)
                {
                    Console.Write(project.Name + " ");
                }
                Console.WriteLine();
                string projectName = Console.ReadLine() ?? "";

                bool success = await Database.AddEmployeeToProjectAsync(employeeId, projectName);

                if (success)
                {
                    Console.WriteLine("Employee added to project successfully!");
                }
                else
                {
                    Console.WriteLine("Failed to add employee to project. Please check your input.");
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
            }
        }

        private async void AddDepartment()
        {
            try
            {
                Console.WriteLine("Enter Department Name: ");
                string departmentName = Console.ReadLine() ?? "";

                Console.WriteLine("Enter Task: ");
                string task = Console.ReadLine() ?? "";

                Console.WriteLine("Enter Department Leader from the list: ");
                List<Employee> employees = await Database.ReadEmployeesAsync();
                foreach (Employee employee in employees)
                {
                    Console.Write(employee.EmployeeId + " ");
                }
                Console.WriteLine();
                string departmentLeaderId = Console.ReadLine() ?? "";

                Console.WriteLine("Enter Class Leader from the list: ");
                List<Class> classList = await Database.ReadClassesAsync();
                foreach (Class class_ in classList)
                {
                    Console.Write(class_.Name + " ");
                }
                Console.WriteLine();
                string classLeaderName = Console.ReadLine() ?? "";

                bool success = await Database.AddDepartmentAsync(departmentName, task, departmentLeaderId, classLeaderName);

                if (success)
                {
                    Console.WriteLine("Department added successfully!");
                }
                else
                {
                    Console.WriteLine("Failed to add department. Please check your input.");
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
            }
        }

        private async void AddClass()
        {
            try
            {
                Console.WriteLine("Enter Class Name: ");
                string className = Console.ReadLine() ?? "";

                Console.WriteLine("Enter Task: ");
                string task = Console.ReadLine() ?? "";

                Console.WriteLine("Enter Class Leader: ");
                List<Employee> employees = await Database.ReadEmployeesAsync();
                foreach (Employee employee in employees)
                {
                    Console.Write(employee.EmployeeId + " ");
                }
                Console.WriteLine();
                string classLeaderId = Console.ReadLine() ?? "";

                bool success = await Database.AddClassAsync(className, task, classLeaderId);

                if (success)
                {
                    Console.WriteLine("Class added successfully!");
                }
                else
                {
                    Console.WriteLine("Failed to add class. Please check your input.");
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
            }
        }

        private async void AddProject()
        {
            try
            {
                Console.WriteLine("Enter Project Name: ");
                string projectName = Console.ReadLine() ?? "";

                Console.WriteLine("Enter Deadline (yyyy-MM-dd): ");
                DateTime deadline = DateTime.Parse(Console.ReadLine() ?? "");

                Console.WriteLine("Enter Description: ");
                string description = Console.ReadLine() ?? "";

                Console.WriteLine("Enter Project Leader: ");
                List<Employee> employees = await Database.ReadEmployeesAsync();
                foreach (Employee employee in employees)
                {
                    Console.Write(employee.EmployeeId + " ");
                }
                Console.WriteLine();
                string projectLeaderId = Console.ReadLine() ?? "";

                Console.WriteLine("Enter Class Name: ");
                List<Class> classList = await Database.ReadClassesAsync();
                foreach (Class class_ in classList)
                {
                    Console.Write(class_.Name + " ");
                }
                Console.WriteLine();
                string className = Console.ReadLine() ?? "";

                bool success = await Database.AddProjectAsync(projectName, description, deadline, projectLeaderId, className);

                if (success)
                {
                    Console.WriteLine("Project added successfully!");
                }
                else
                {
                    Console.WriteLine("Failed to add project. Please check your input.");
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
            }
        }

        private async void AddEmployee()
        {
            try
            {
                Console.WriteLine("Enter Employee ID: ");
                string employeeId = Console.ReadLine() ?? "";

                Console.WriteLine("Enter Name: ");
                string name = Console.ReadLine() ?? "";

                Console.WriteLine("Enter Phone Number: ");
                string phoneNumber = Console.ReadLine() ?? "";

                Console.WriteLine("Enter Email: ");
                string email = Console.ReadLine() ?? "";

                Console.WriteLine("Enter Position: ");
                string position = Console.ReadLine() ?? "";

                Console.WriteLine("Enter Salary, must be a number: ");
                int salary = int.Parse(Console.ReadLine() ?? "");

                Console.WriteLine("Enter a valid Department from the list: ");
                List<Department> departmentList = new List<Department>();
                departmentList = await Database.ReadDepartmentsAsync();
                foreach (Department department in departmentList)
                {
                    Console.Write(department.Name + " ");
                }
                Console.WriteLine();


                string departmentName = Console.ReadLine() ?? "";

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
