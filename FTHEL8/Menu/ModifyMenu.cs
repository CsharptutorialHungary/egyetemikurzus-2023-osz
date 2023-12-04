using FTHEL8.Data;
using FTHEL8.Models;

namespace FTHEL8.Menu
{
    public class ModifyMenu : Menu
    {
        public ModifyMenu()
        {
        }

        private void ModifyClass()
        {
            try
            {
                Console.WriteLine("Enter the Class Name to modify: ");
                List<Class> classList = Database.ReadClassesAsync().Result;
                foreach (Class class_ in classList)
                {
                    Console.Write(class_.Name + " ");
                }
                Console.WriteLine();
                string className = Console.ReadLine() ?? "";

                Class existingClass = classList.FirstOrDefault(x => x.Name == className)!;

                if (existingClass != null)
                {
                    Console.WriteLine($"Class found: {existingClass.Name}");

                    Console.WriteLine("Enter new Task: ");
                    string newTask = Console.ReadLine() ?? "";

                    Console.WriteLine("Enter new Class Leader ID from the list: ");
                    List<Employee> employeeList = Database.ReadEmployeesAsync().Result;
                    foreach (Employee employee in employeeList)
                    {
                        Console.Write(employee.EmployeeId + " ");
                    }
                    Console.WriteLine();
                    string newClassLeaderId = Console.ReadLine() ?? "";

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
                    Console.WriteLine("Class not found.");
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
            }
        }

        private async void ModifyProject()
        {
            try
            {
                Console.WriteLine("Enter the Project Name to modify: ");
                List<Project> projectList = await Database.ReadProjectsAsync();
                foreach (Project project in projectList)
                {
                    Console.Write(project.Name + " ");
                }
                Console.WriteLine();
                string projectName = Console.ReadLine() ?? "";

                Project existingProject = projectList.FirstOrDefault(x => x.Name == projectName)!;

                if (existingProject != null)
                {
                    Console.WriteLine($"Project found: {existingProject.Name}");

                    Console.WriteLine("Enter new Description: ");
                    string newDescription = Console.ReadLine() ?? "";

                    Console.WriteLine("Enter new Deadline (yyyy-MM-dd): ");
                    DateTime newDeadline = DateTime.Parse(Console.ReadLine() ?? "");

                    Console.WriteLine("Enter the new Project Leader ID from the list: ");
                    List<Employee> employeeList = await Database.ReadEmployeesAsync();
                    foreach (Employee employee in employeeList)
                    {
                        Console.Write(employee.EmployeeId + " ");
                    }
                    Console.WriteLine();
                    string newProjectLeaderId = Console.ReadLine() ?? "";

                    Console.WriteLine("Enter the new Class Name from the list: ");
                    List<Class> classList = await Database.ReadClassesAsync();
                    foreach (Class class_ in classList)
                    {
                        Console.Write(class_.Name + " ");
                    }
                    Console.WriteLine();
                    string newClassName = Console.ReadLine() ?? "";

                    bool success = await Database.ModifyProject(projectName, newDescription, newDeadline, newProjectLeaderId, newClassName);

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
                    Console.WriteLine("Project not found.");
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
            }
        }

        private async void ModifyProjectMembers()
        {
            try
            {
                Console.WriteLine("Enter the Project Name to modify members: ");
                List<ProjectMembers> projectMembersList = await Database.ReadProjectMembersAsync();
                foreach (ProjectMembers projectMember in projectMembersList)
                {
                    Console.Write(projectMember.ProjectName + " ");
                }
                Console.WriteLine();
                string projectName = Console.ReadLine() ?? "";

                ProjectMembers existingProjectMembers = projectMembersList.FirstOrDefault(x => x.ProjectName == projectName)!;

                if (existingProjectMembers != null)
                {
                    Console.WriteLine($"Project members found for: {existingProjectMembers.ProjectName}");

                    Console.WriteLine("Enter new Employee IDs separated by commas: ");
                    string newEmployeeIds = Console.ReadLine() ?? "";

                    List<string> newEmployeeIdList = newEmployeeIds.Split(',').Select(s => s.Trim()).ToList();

                    bool success = await Database.ModifyProjectMembers(projectName, newEmployeeIdList);

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
                    Console.WriteLine("Project members not found.");
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
            }
        }


        private async void ModifyEmployee()
        {
            try
            {
                Console.WriteLine("Enter the Employee ID to modify: ");
                List<Employee> employeeList = await Database.ReadEmployeesAsync();
                foreach (Employee employee in employeeList)
                {
                    Console.Write(employee.EmployeeId + " ");
                }
                Console.WriteLine();
                string employeeId = Console.ReadLine() ?? "";

                Employee existingEmployee = Database.ReadEmployeesAsync().Result.FirstOrDefault(x => x.EmployeeId == employeeId)!;

                if (existingEmployee != null)
                {
                    Console.WriteLine($"Employee found: {existingEmployee.Name}");

                    Console.WriteLine("Enter new Name: ");
                    string newName = Console.ReadLine() ?? "";

                    Console.WriteLine("Enter new Phone Number: ");
                    string newPhoneNumber = Console.ReadLine() ?? "";

                    Console.WriteLine("Enter new Email: ");
                    string newEmail = Console.ReadLine() ?? "";

                    Console.WriteLine("Enter new Position: ");
                    string newPosition = Console.ReadLine() ?? "";

                    Console.WriteLine("Enter new Salary, must be a number: ");
                    int newSalary = int.Parse(Console.ReadLine() ?? "");

                    Console.WriteLine("Enter a valid Department from the list: ");
                    List<Department> departmentList = await Database.ReadDepartmentsAsync();
                    foreach (Department department in departmentList)
                    {
                        Console.Write(department.Name + " ");
                    }
                    Console.WriteLine();

                    string newDepartmentName = Console.ReadLine() ?? "";

                    bool success = await Database.ModifyEmployee(employeeId, newName, newPhoneNumber, newEmail, newPosition, newSalary, newDepartmentName);

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
                    Console.WriteLine("Employee not found.");
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
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
