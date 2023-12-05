using FTHEL8.Models;
using Newtonsoft.Json;
using System.Text;

namespace FTHEL8.Data
{
    public class Database
    {
        private const string JsonFilePath = "./Database/Database.json";


        private static async Task<List<T>> ReadJsonAsync<T>(string filePath)
        {
            try
            {
                using (var streamReader = new StreamReader(filePath))
                {
                    var jsonContent = await streamReader.ReadToEndAsync();
                    return JsonConvert.DeserializeObject<List<T>>(jsonContent) ?? [];
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error reading JSON file: {ex.Message}");
                return [];
            }
        }

        private static async Task<bool> WriteJsonFileAsync<T>(string filePath, List<T> data)
        {
            try
            {
                string jsonString = JsonConvert.SerializeObject(data, Formatting.Indented);
                await File.WriteAllTextAsync(filePath, jsonString, Encoding.UTF8);
                return true;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error writing to JSON file: {ex.Message}");
                return false;
            }
        }

        public static async Task<List<Employee>> ReadEmployeesAsync()
        {
            return await ReadJsonAsync<Employee>("./Database/employees.json");
        }

        public static async Task<List<Department>> ReadDepartmentsAsync()
        {
            return await ReadJsonAsync<Department>("./Database/departments.json");
        }

        public static async Task<List<Class>> ReadClassesAsync()
        {
            return await ReadJsonAsync<Class>("./Database/classes.json");
        }

        public static async Task<List<Project>> ReadProjectsAsync()
        {
            return await ReadJsonAsync<Project>("./Database/projects.json");
        }

        public static async Task<List<ProjectMembers>> ReadProjectMembersAsync()
        {
            return await ReadJsonAsync<ProjectMembers>("./Database/project_members.json");
        }

        public static async Task<bool> ModifyClass(string className, string newTask, string newClassLeaderId)
        {
            try
            {
                List<Class> classes = await ReadJsonAsync<Class>("./Database/classes.json");

                Class classToModify = classes.FirstOrDefault(c => c.Name == className)!;

                if (classToModify != null)
                {
                    classToModify.Task = newTask;
                    classToModify.ClassLeader = newClassLeaderId;

                    await WriteJsonFileAsync("./Database/classes.json", classes);

                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error modifying class: {ex.Message}");
                return false;
            }
        }

        public static async Task<bool> ModifyProject(string projectName, string newDescription, DateTime newDeadline, string newProjectLeaderId, string newClassName)
        {
            try
            {
                List<Project> projects = await ReadJsonAsync<Project>("./Database/projects.json");

                Project projectToModify = projects.FirstOrDefault(p => p.Name == projectName)!;

                if (projectToModify != null)
                {
                    projectToModify.Description = newDescription;
                    projectToModify.Deadline = newDeadline;
                    projectToModify.ProjectLeader = newProjectLeaderId;
                    projectToModify.ClassName = newClassName;

                    await WriteJsonFileAsync("./Database/projects.json", projects);

                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error modifying project: {ex.Message}");
                return false;
            }
        }

        public static async Task<bool> ModifyProjectMembers(string projectName, List<string> newEmployeeIdList)
        {
            try
            {
                List<ProjectMembers> projectMembers = await ReadJsonAsync<ProjectMembers>("./Database/project_members.json");

                ProjectMembers projectMembersToModify = projectMembers.FirstOrDefault(p => p.ProjectName == projectName)!;

                if (projectMembersToModify != null)
                {
                    projectMembersToModify.Employees = newEmployeeIdList;

                    await WriteJsonFileAsync("./Database/project_members.json", projectMembers);

                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error modifying project members: {ex.Message}");
                return false;
            }
        }

        public static async Task<bool> ModifyEmployee(string employeeId, string newName, string newPhoneNumber, string newEmail, string newPosition, int newSalary, string newDepartmentName)
        {
            try
            {
                List<Employee> employees = await ReadJsonAsync<Employee>("./Database/employees.json");

                Employee employeeToModify = employees.FirstOrDefault(e => e.EmployeeId == employeeId)!;

                if (employeeToModify != null)
                {
                    employeeToModify.Name = newName;
                    employeeToModify.PhoneNumber = newPhoneNumber;
                    employeeToModify.Email = newEmail;
                    employeeToModify.Position = newPosition;
                    employeeToModify.Salary = newSalary;
                    employeeToModify.DepartmentName = newDepartmentName;

                    await WriteJsonFileAsync("./Database/employees.json", employees);

                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error modifying employee: {ex.Message}");
                return false;
            }
        }


        public static async Task<bool> AddEmployeeToProjectAsync(string employeeId, string projectName)
        {
            try
            {
                List<ProjectMembers> projectMembers = await ReadJsonAsync<ProjectMembers>("./Database/project_members.json");

                ProjectMembers project = projectMembers.FirstOrDefault(p => p.ProjectName == projectName)!;
                if (project != null)
                {
                    project.Employees?.Add(employeeId);
                    await WriteJsonFileAsync("./Database/project_members.json", projectMembers);

                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error adding employee to project: {ex.Message}");
                return false;
            }
        }

        public static async Task<bool> AddDepartmentAsync(string departmentName, string task, string departmentLeaderId, string className)
        {
            try
            {
                List<Department> departments = await ReadJsonAsync<Department>("./Database/departments.json");

                Department newDepartment = new Department
                {
                    Name = departmentName,
                    Task = task,
                    DepartmentLeader = departmentLeaderId,
                    ClassName = className
                };

                departments.Add(newDepartment);
                await WriteJsonFileAsync("./Database/departments.json", departments);

                return true;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error adding department: {ex.Message}");
                return false;
            }
        }

        public static async Task<bool> AddClassAsync(string className, string task, string classLeaderId)
        {
            try
            {
                List<Class> classes = await ReadJsonAsync<Class>("./Database/classes.json");

                Class newClass = new Class
                {
                    Name = className,
                    Task = task,
                    ClassLeader = classLeaderId
                };

                classes.Add(newClass);
                await WriteJsonFileAsync("./Database/classes.json", classes);

                return true;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error adding class: {ex.Message}");
                return false;
            }
        }

        public static async Task<bool> AddProjectAsync(string projectName, string description, DateTime deadline, string projectLeaderId, string className)
        {
            try
            {
                List<Project> projects = await ReadJsonAsync<Project>("./Database/projects.json");

                Project newProject = new Project
                {
                    Name = projectName,
                    Description = description,
                    Deadline = deadline,
                    ProjectLeader = projectLeaderId,
                    ClassName = className
                };

                projects.Add(newProject);
                await WriteJsonFileAsync("./Database/projects.json", projects);

                List<ProjectMembers> projectMembers = await ReadJsonAsync<ProjectMembers>("./Database/project_members.json");

                ProjectMembers newProjectMembers = new ProjectMembers
                {
                    ProjectName = projectName,
                    Employees = []
                };

                projectMembers.Add(newProjectMembers);
                await WriteJsonFileAsync("./Database/project_members.json", projectMembers);

                return true;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error adding project: {ex.Message}");
                return false;
            }
        }

        public static async Task<bool> AddEmployeeAsync(string employeeId, string name, string phoneNumber, string email, string position, int salary, string departmentName)
        {
            try
            {
                List<Employee> employees = await ReadJsonAsync<Employee>("./Database/employees.json");

                Employee newEmployee = new Employee
                {
                    EmployeeId = employeeId,
                    Name = name,
                    PhoneNumber = phoneNumber,
                    Email = email,
                    Position = position,
                    Salary = salary,
                    DepartmentName = departmentName
                };

                employees.Add(newEmployee);
                await WriteJsonFileAsync("./Database/employees.json", employees);

                return true;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error adding employee: {ex.Message}");
                return false;
            }
        }


        public static async Task<bool> DeleteProjectAsync(string projectName)
        {
            try
            {
                List<Project> projects = await ReadJsonAsync<Project>("./Database/projects.json");

                Project projectToDelete = projects.FirstOrDefault(p => p.Name == projectName)!;

                if (projectToDelete != null)
                {
                    projects.Remove(projectToDelete);
                    await WriteJsonFileAsync("./Database/projects.json", projects);

                    List<ProjectMembers> projectMembers = await ReadJsonAsync<ProjectMembers>("./Database/project_members.json");
                    ProjectMembers projectMembersToDelete = projectMembers.FirstOrDefault(p => p.ProjectName == projectName)!;

                    if (projectMembersToDelete != null)
                    {
                        projectMembers.Remove(projectMembersToDelete);
                        await WriteJsonFileAsync("./Database/project_members.json", projectMembers);
                    }

                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error deleting project: {ex.Message}");
                return false;
            }
        }

        public static async Task<bool> DeleteEmployeeAsync(string employeeId)
        {
            try
            {
                List<Employee> employees = await ReadJsonAsync<Employee>("./Database/employees.json");

                Employee employeeToDelete = employees.FirstOrDefault(e => e.EmployeeId == employeeId)!;

                if (employeeToDelete != null)
                {
                    employees.Remove(employeeToDelete);
                    await WriteJsonFileAsync("./Database/employees.json", employees);
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error deleting employee: {ex.Message}");
                return false;
            }
        }

        public static async Task<bool> DeleteDepartmentAsync(string departmentName)
        {
            try
            {
                List<Department> departments = await ReadJsonAsync<Department>("./Database/departments.json");

                Department departmentToDelete = departments.FirstOrDefault(d => d.Name == departmentName)!;

                if (departmentToDelete != null)
                {
                    departments.Remove(departmentToDelete);
                    await WriteJsonFileAsync("./Database/departments.json", departments);
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error deleting department: {ex.Message}");
                return false;
            }
        }

        public static async Task<bool> DeleteClassAsync(string className)
        {
            try
            {
                List<Class> classes = await ReadJsonAsync<Class>("./Database/classes.json");

                Class classToDelete = classes.FirstOrDefault(c => c.Name == className)!;

                if (classToDelete != null)
                {
                    classes.Remove(classToDelete);
                    await WriteJsonFileAsync("./Database/classes.json", classes);
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error deleting class: {ex.Message}");
                return false;
            }
        }
    }
}
