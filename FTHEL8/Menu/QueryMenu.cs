using FTHEL8.Data;
using FTHEL8.Models;
using System.Runtime.CompilerServices;

namespace FTHEL8.Menu
{
    public class QueryMenu : Menu
    {
        private readonly MainMenu mainMenu;
        public QueryMenu(MainMenu mainMenu)
        {
            this.mainMenu = mainMenu;
            Add("Get Project with the Most Workers", async () => await GetProjectThatHasTheMostWorkersAsync());
            Add("Get Highest Paid Employees Data", async () => await GetHighestPaidEmployeesDataAsync());
            Add("Back to Main Menu", GetPreviousMenu);
        }

        public void GetPreviousMenu()
        {
            mainMenu.Display();
        }

        private async Task GetProjectThatHasTheMostWorkersAsync()
        {
            try
            {
                List<ProjectMembers> projectMembers = await Database.ReadProjectMembersAsync();

                if (projectMembers.Count > 0)
                {
                    var projectWithMostWorkers = projectMembers.OrderByDescending(p => p.Employees!.Count).FirstOrDefault();

                    if (projectWithMostWorkers != null)
                    {
                        Console.WriteLine();
                        Console.WriteLine($"Project with the Most Workers: {projectWithMostWorkers.ProjectName}");
                        Console.WriteLine($"Number of Workers: {projectWithMostWorkers.Employees!.Count}");
                        string employeeNames = string.Join(", ", projectWithMostWorkers.Employees);
                        Console.WriteLine($"Workers: {employeeNames}");
                    }
                    else
                    {
                        Console.WriteLine("No projects found.");
                    }
                }
                else
                {
                    Console.WriteLine("No projects found.");
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
            }

        }

        private async Task GetHighestPaidEmployeesDataAsync()
        {
            try
            {
                List<Employee> employees = await Database.ReadEmployeesAsync();

                if (employees.Count > 0)
                {
                    Employee highestPaidEmployee = employees.OrderByDescending(x => x.Salary).FirstOrDefault()!;

                    if (highestPaidEmployee != null)
                    {
                        Console.WriteLine("Employee with the Highest Salary:");
                        Console.WriteLine($"Employee ID: {highestPaidEmployee.EmployeeId}, Name: {highestPaidEmployee.Name}, Phone: {highestPaidEmployee.PhoneNumber}, Email: {highestPaidEmployee.Email}, Position: {highestPaidEmployee.Position}, Salary: {highestPaidEmployee.Salary}, Department: {highestPaidEmployee.DepartmentName}");
                    }
                    else
                    {
                        Console.WriteLine("No employees found.");
                    }
                }
                else
                {
                    Console.WriteLine("No employees found.");
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
            }
        }

        private void GetEmployeesData()
        {
            List<Employee> employees = Database.ReadEmployeesAsync().Result;
            Console.WriteLine("All employees data:");
            foreach (Employee employee in employees)
            {
                Console.WriteLine($"Employee ID: {employee.EmployeeId}, Name: {employee.Name}, Phone number: {employee.PhoneNumber}, Email: {employee.Email}, Position: {employee.Position}, Salary: {employee.Salary}, Department: {employee.DepartmentName}");
            }
            Console.WriteLine();
        }

        private void GetClassesData()
        {
            List<Class> classes = Database.ReadClassesAsync().Result;
            Console.WriteLine("All classes data:");
            foreach (Class class_ in classes)
            {
                Console.WriteLine($"Class name: {class_.Name}, Task: {class_.Task}, Class leader: {class_.ClassLeader}");
            }
            Console.WriteLine();
        }

        private void GetProjectsData()
        {
            List<Project> projects = Database.ReadProjectsAsync().Result;
            Console.WriteLine("All projects data:");
            foreach (Project project in projects)
            {
                Console.WriteLine($"Project name: {project.Name}, Description: {project.Description}, Deadline: {project.Deadline}, Project leader: {project.ProjectLeader}, Class: {project.ClassName}");
            }
            Console.WriteLine();
        }

        private void GetDepartmentsData()
        {
            List<Department> departments = Database.ReadDepartmentsAsync().Result;
            Console.WriteLine("All departments data:");
            foreach (Department department in departments)
            {
                Console.WriteLine($"Department name: {department.Name}, Task: {department.Task}, Department leader: {department.DepartmentLeader}, Class name: {department.ClassName}");
            }
        }

        private void GetProjectMembersData()
        {
            List<ProjectMembers> projectMembers = Database.ReadProjectMembersAsync().Result;
            Console.WriteLine("All project members data:");
            foreach (var member in projectMembers)
            {
                string employeeNames = string.Join(", ", member.Employees!);
                Console.WriteLine($"Project name: {member.ProjectName}, Employee: {employeeNames}");
            }
            Console.WriteLine();
        }
    }
}
