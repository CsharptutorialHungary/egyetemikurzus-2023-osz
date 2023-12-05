using FTHEL8.Data;
using FTHEL8.Models;

namespace FTHEL8.Menu
{
    public class AddMenu : Menu
    {
        private readonly MainMenu mainMenu;
        public AddMenu(MainMenu mainMenu)
        {
            this.mainMenu = mainMenu;
            Add("Add Employee", AddEmployee);
            Add("Add Department", AddDepartment);
            Add("Add Class", AddClass);
            Add("Add Project", AddProject);
            Add("Add Employee to Project", AddEmployeeToProject);
            Add("Back to Main Menu", GetPreviousMenu);
        }

        public void GetPreviousMenu()
        {
            mainMenu.Display();
        }

        private void AddEmployeeToProject()
        {
            try
            {
                List<Employee> employees = Database.ReadEmployeesAsync().Result;
                foreach (Employee employee in employees)
                {
                    Console.Write(employee.EmployeeId + " ");
                }
                Console.WriteLine();
                Console.Write("Enter Employee ID from the list: ");
                string employeeId = Console.ReadLine() ?? "";

                List<Project> projectList = Database.ReadProjectsAsync().Result;
                foreach (Project project in projectList)
                {
                    Console.Write(project.Name + " ");
                }
                Console.WriteLine();
                Console.Write("Enter Project Name from the list: ");

                string projectName = Console.ReadLine() ?? "";

                bool validEmployee = employees.Any(e => e.EmployeeId == employeeId);
                bool validProject = projectList.Any(p => p.Name == projectName);

                if (validEmployee && validProject)
                {
                    bool success = Database.AddEmployeeToProjectAsync(employeeId, projectName).Result;

                    if (success)
                    {
                        Console.WriteLine("Employee added to project successfully!");
                    }
                    else
                    {
                        Console.WriteLine("Failed to add employee to project. Please check your input.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid Employee ID or Project Name. Please enter valid values.");
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
            }
        }

        private void AddDepartment()
        {
            try
            {
                Console.Write("Enter Department Name: ");
                string departmentName = Console.ReadLine() ?? "";
                bool isNotValidDepartmentName = Database.ReadDepartmentsAsync().Result.Any(d => d.Name == departmentName);
                if (isNotValidDepartmentName && string.IsNullOrWhiteSpace(departmentName))
                {
                    Console.WriteLine("This department name is already in the database or can't be added!");
                    return;
                }

                Console.Write("Enter Task: ");
                string task = Console.ReadLine() ?? "";

                List<Employee> employees = Database.ReadEmployeesAsync().Result;
                foreach (Employee employee in employees)
                {
                    Console.Write(employee.EmployeeId + " ");
                }
                Console.WriteLine();
                Console.Write("Enter Department Leader from the list: ");
                string departmentLeaderId = Console.ReadLine() ?? "";
                List<Class> classList = Database.ReadClassesAsync().Result;
                foreach (Class class_ in classList)
                {
                    Console.Write(class_.Name + " ");
                }
                Console.WriteLine();

                Console.Write("Enter Class Leader from the list: ");
                string classLeaderName = Console.ReadLine() ?? "";

                bool validDepartmentLeader = employees.Any(e => e.EmployeeId == departmentLeaderId);
                bool validClassLeader = classList.Any(c => c.Name == classLeaderName);

                if (validDepartmentLeader && validClassLeader)
                {
                    bool success = Database.AddDepartmentAsync(departmentName, task, departmentLeaderId, classLeaderName).Result;

                    if (success)
                    {
                        Console.WriteLine("Department added successfully!");
                    }
                    else
                    {
                        Console.WriteLine("Failed to add department. Please check your input.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid Department Leader or Class Leader. Please enter valid values.");
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
            }
        }

        private void AddClass()
        {
            try
            {
                Console.Write("Enter Class Name: ");
                string className = Console.ReadLine() ?? "";
                bool isNotValidClassName = Database.ReadClassesAsync().Result.Any(c => c.Name == className);
                if (isNotValidClassName && string.IsNullOrWhiteSpace(className))
                {
                    Console.WriteLine("This class name is already in the database or can't be added!");
                    return;
                }

                Console.Write("Enter Task: ");
                string task = Console.ReadLine() ?? "";

                List<Employee> employees = Database.ReadEmployeesAsync().Result;
                foreach (Employee employee in employees)
                {
                    Console.Write(employee.EmployeeId + " ");
                }
                Console.WriteLine();
                Console.Write("Enter Class Leader from the list: ");
                string classLeaderId = Console.ReadLine() ?? "";

                bool validClassLeader = employees.Any(e => e.EmployeeId == classLeaderId);

                if (validClassLeader)
                {
                    bool success = Database.AddClassAsync(className, task, classLeaderId).Result;

                    if (success)
                    {
                        Console.WriteLine("Class added successfully!");
                    }
                    else
                    {
                        Console.WriteLine("Failed to add class. Please check your input.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid Class Leader. Please enter a valid value.");
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
            }
        }

        private void AddProject()
        {
            try
            {
                Console.Write("Enter Project Name: ");
                string projectName = Console.ReadLine() ?? "";
                bool isNotValidProject = Database.ReadProjectsAsync().Result.Any(p => p.Name == projectName);
                if(isNotValidProject && string.IsNullOrWhiteSpace(projectName))
                {
                    Console.WriteLine("This project name is already in the database or can't be added!");
                    return;
                }

                Console.Write("Enter Deadline (yyyy-MM-dd): ");
                DateTime deadline = DateTime.Parse(Console.ReadLine() ?? "");

                Console.Write("Enter Description: ");
                string description = Console.ReadLine() ?? "";

                List<Employee> employees = Database.ReadEmployeesAsync().Result;
                foreach (Employee employee in employees)
                {
                    Console.Write(employee.EmployeeId + " ");
                }
                Console.WriteLine();

                Console.Write("Enter Project Leader from the list: ");
                string projectLeaderId = Console.ReadLine() ?? "";
                List<Class> classList = Database.ReadClassesAsync().Result;
                foreach (Class class_ in classList)
                {
                    Console.Write(class_.Name + " ");
                }
                Console.WriteLine();

                Console.Write("Enter Class Name from the list: ");
                string className = Console.ReadLine() ?? "";

                bool validProjectLeader = employees.Any(e => e.EmployeeId == projectLeaderId);
                bool validClass = classList.Any(c => c.Name == className);

                if (validProjectLeader && validClass)
                {
                    bool success = Database.AddProjectAsync(projectName, description, deadline, projectLeaderId, className).Result;

                    if (success)
                    {
                        Console.WriteLine("Project added successfully!");
                    }
                    else
                    {
                        Console.WriteLine("Failed to add project. Please check your input.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid Project Leader or Class Name. Please enter valid values.");
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
            }
        }

        private void AddEmployee()
        {
            try
            {
                Console.Write("Enter Employee ID: ");
                string employeeId = Console.ReadLine() ?? "";
                bool isNotValidEmployeeId = Database.ReadEmployeesAsync().Result.Any(e => e.Name == employeeId);
                if (isNotValidEmployeeId && string.IsNullOrWhiteSpace(employeeId))
                {
                    Console.WriteLine("This employee ID is already in the database or can't be added!");
                    return;
                }

                Console.Write("Enter Name: ");
                string name = Console.ReadLine() ?? "";

                Console.Write("Enter Phone Number: ");
                string phoneNumber = Console.ReadLine() ?? "";

                Console.Write("Enter Email: ");
                string email = Console.ReadLine() ?? "";

                Console.Write("Enter Position: ");
                string position = Console.ReadLine() ?? "";

                Console.Write("Enter Salary, must be a number: ");
                int salary = int.Parse(Console.ReadLine() ?? "");

                List<Department> departmentList = new List<Department>();
                departmentList = Database.ReadDepartmentsAsync().Result;
                foreach (Department department in departmentList)
                {
                    Console.Write(department.Name + " ");
                }
                Console.WriteLine();

                Console.Write("Enter a valid Department from the list: ");


                string departmentName = Console.ReadLine() ?? "";

                bool validDepartment = departmentList.Any(d => d.Name == departmentName);

                if (validDepartment)
                {
                    bool success = Database.AddEmployeeAsync(employeeId, name, phoneNumber, email, position, salary, departmentName).Result;

                    if (success)
                    {
                        Console.WriteLine("Employee added successfully!");
                    }
                    else
                    {
                        Console.WriteLine("Failed to add employee. Please check your input.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid Department. Please enter a valid value.");
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
            }
        }
    }
}
