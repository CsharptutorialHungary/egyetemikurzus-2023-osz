using FTHEL8.Data;
using FTHEL8.Models;

namespace FTHEL8.Menu
{
    public class ModifyMenu : Menu
    {
        private readonly MainMenu mainMenu;
        public ModifyMenu(MainMenu mainMenu)
        {
            this.mainMenu = mainMenu;
            Add("Modify a class", ModifyClass);
            Add("Modify a project", ModifyProject);
            Add("Modify project members", ModifyProjectMembers);
            Add("Modify an employee", ModifyEmployee);
            Add("Back to main menu", GetPreviousMenu);
        }

        public void GetPreviousMenu()
        {
            mainMenu.Display();
        }

        private void ModifyClass()
        {
            try
            {
                List<Class> classList = Database.ReadClassesAsync().Result;
                foreach (Class class_ in classList)
                {
                    Console.Write(class_.Name + " ");
                }
                Console.WriteLine();
                Console.Write("Enter the Class Name to modify from the list: ");
                string className = Console.ReadLine() ?? "";

                Class existingClass = classList.FirstOrDefault(x => x.Name == className)!;

                if (existingClass != null)
                {
                    Console.WriteLine($"Class found: {existingClass.Name}");

                    Console.Write("Enter new Task: ");
                    string newTask = Console.ReadLine() ?? "";

                    List<Employee> employeeList = Database.ReadEmployeesAsync().Result;
                    foreach (Employee employee in employeeList)
                    {
                        Console.Write(employee.EmployeeId + " ");
                    }
                    Console.WriteLine();
                    Console.Write("Enter new Class Leader ID from the list: ");
                    string newClassLeaderId = Console.ReadLine() ?? "";

                    bool validClassLeader = employeeList.Any(e => e.EmployeeId == newClassLeaderId);

                    if (validClassLeader)
                    {
                        bool success = Database.ModifyClass(className, newTask, newClassLeaderId).Result;

                        if (success)
                        {
                            Console.WriteLine("Class modified successfully!");
                        }
                        else
                        {
                            Console.WriteLine("Failed to modify class. Please check your input.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Failed to add class. Please check your input.");
                    }
                }
                else
                {
                    Console.WriteLine("Class not found.");
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
            }
        }

        private void ModifyProject()
        {
            try
            {
                List<Project> projectList = Database.ReadProjectsAsync().Result;
                foreach (Project project in projectList)
                {
                    Console.Write(project.Name + " ");
                }
                Console.WriteLine();
                Console.Write("Enter the Project Name to modify from the list: ");
                string projectName = Console.ReadLine() ?? "";

                Project existingProject = projectList.FirstOrDefault(x => x.Name == projectName)!;

                if (existingProject != null)
                {
                    Console.WriteLine($"Project found: {existingProject.Name}");

                    Console.Write("Enter new Description: ");
                    string newDescription = Console.ReadLine() ?? "";

                    Console.Write("Enter new Deadline (yyyy-MM-dd): ");
                    DateTime newDeadline = DateTime.Parse(Console.ReadLine() ?? "");

                    List<Employee> employeeList = Database.ReadEmployeesAsync().Result;
                    foreach (Employee employee in employeeList)
                    {
                        Console.Write(employee.EmployeeId + " ");
                    }
                    Console.WriteLine();
                    Console.Write("Enter the new Project Leader ID from the list: ");
                    string newProjectLeaderId = Console.ReadLine() ?? "";

                    List<Class> classList = Database.ReadClassesAsync().Result;
                    foreach (Class class_ in classList)
                    {
                        Console.Write(class_.Name + " ");
                    }
                    Console.WriteLine();
                    Console.Write("Enter the new Class Name from the list: ");
                    string newClassName = Console.ReadLine() ?? "";

                    bool validProjectLeader = employeeList.Any(e => e.EmployeeId == newProjectLeaderId);
                    bool validClass = classList.Any(c => c.Name == newClassName);

                    if (validProjectLeader && validClass)
                    {

                        bool success = Database.ModifyProject(projectName, newDescription, newDeadline, newProjectLeaderId, newClassName).Result;

                        if (success)
                        {
                            Console.WriteLine("Project modified successfully!");
                        }
                        else
                        {
                            Console.WriteLine("Failed to modify project. Please check your input.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid Project Leader or Class Name. Please enter valid values.");
                    }
                }
                else
                {
                    Console.WriteLine("Project not found.");
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
            }
        }

        private void ModifyProjectMembers()
        {
            try
            {
                List<ProjectMembers> projectMembersList = Database.ReadProjectMembersAsync().Result;
                foreach (ProjectMembers projectMember in projectMembersList)
                {
                    Console.Write(projectMember.ProjectName + " ");
                }
                Console.WriteLine();
                Console.Write("Enter the Project Name to modify members: ");
                string projectName = Console.ReadLine() ?? "";

                ProjectMembers existingProjectMembers = projectMembersList.FirstOrDefault(x => x.ProjectName == projectName)!;

                if (existingProjectMembers != null)
                {
                    Console.Write($"Project members found for: {existingProjectMembers.ProjectName}");
                    Console.WriteLine();

                    List<Employee> employeeList = Database.ReadEmployeesAsync().Result;
                    foreach (Employee employee in employeeList)
                    {
                        Console.Write(employee.EmployeeId + " ");
                    }
                    Console.WriteLine();
                    Console.Write("Enter new Employee IDs separated by commas: ");
                    string newEmployeeIds = Console.ReadLine() ?? "";

                    List<string> newEmployeeIdList = newEmployeeIds.Split(',').Select(s => s.Trim()).ToList();

                    if (newEmployeeIdList.All(id => employeeList.Any(e => e.EmployeeId == id)))
                    {
                        bool success = Database.ModifyProjectMembers(projectName, newEmployeeIdList).Result;

                        if (success)
                        {
                            Console.WriteLine("Project members modified successfully!");
                        }
                        else
                        {
                            Console.WriteLine("Failed to modify project members. Please check your input.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("One or more entered Employee IDs do not exist in the employees list. Please check your input.");
                    }
                }
                else
                {
                    Console.WriteLine("Project members not found.");
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
            }
        }


        private void ModifyEmployee()
        {
            try
            {
                List<Employee> employeeList = Database.ReadEmployeesAsync().Result;
                foreach (Employee employee in employeeList)
                {
                    Console.Write(employee.EmployeeId + " ");
                }
                Console.WriteLine();
                Console.Write("Enter the Employee ID to modify: ");
                string employeeId = Console.ReadLine() ?? "";

                Employee existingEmployee = Database.ReadEmployeesAsync().Result.FirstOrDefault(x => x.EmployeeId == employeeId)!;

                if (existingEmployee != null)
                {
                    Console.WriteLine($"Employee found: {existingEmployee.Name}");

                    Console.Write("Enter new Name: ");
                    string newName = Console.ReadLine() ?? "";

                    Console.Write("Enter new Phone Number: ");
                    string newPhoneNumber = Console.ReadLine() ?? "";

                    Console.Write("Enter new Email: ");
                    string newEmail = Console.ReadLine() ?? "";

                    Console.Write("Enter new Position: ");
                    string newPosition = Console.ReadLine() ?? "";

                    Console.Write("Enter new Salary, must be a number: ");
                    int newSalary = int.Parse(Console.ReadLine() ?? "");
                    List<Department> departmentList = Database.ReadDepartmentsAsync().Result;
                    foreach (Department department in departmentList)
                    {
                        Console.Write(department.Name + " ");
                    }
                    Console.WriteLine();
                    Console.Write("Enter a valid Department from the list: ");

                    string newDepartmentName = Console.ReadLine() ?? "";

                    bool validDepartment = departmentList.Any(d => d.Name == newDepartmentName);

                    if (validDepartment)
                    {
                        bool success = Database.ModifyEmployee(employeeId, newName, newPhoneNumber, newEmail, newPosition, newSalary, newDepartmentName).Result;

                        if (success)
                        {
                            Console.WriteLine("Employee modified successfully!");
                        }
                        else
                        {
                            Console.WriteLine("Failed to modify employee. Please check your input.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid Department. Please enter a valid value.");
                    }
                }
                else
                {
                    Console.WriteLine("Employee not found.");
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
            }
        }
    }
}
