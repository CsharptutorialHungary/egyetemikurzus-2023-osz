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

        internal static Task<bool> ModifyProject(string projectName, string newDescription, DateTime newDeadline, string newProjectLeaderId, string newClassName)
        {
            throw new NotImplementedException();
        }

        internal static Task<bool> ModifyProjectMembers(string projectName, List<string> newEmployeeIdList)
        {
            throw new NotImplementedException();
        }

        internal static Task<bool> ModifyEmployee(string employeeId, string newName, string newPhoneNumber, string newEmail, string newPosition, int newSalary, string newDepartmentName)
        {
            throw new NotImplementedException();
        }

        internal static Task<bool> AddEmployeeToProjectAsync(string employeeId, string projectName)
        {
            throw new NotImplementedException();
        }

        internal static Task<bool> AddDepartmentAsync(string departmentName, string task, string departmentLeaderId, string classLeaderName)
        {
            throw new NotImplementedException();
        }

        internal static Task<bool> AddClassAsync(string className, string task, string classLeaderId)
        {
            throw new NotImplementedException();
        }

        internal static Task<bool> AddProjectAsync(string projectName, string description, DateTime deadline, string projectLeaderId, string className)
        {
            throw new NotImplementedException();
        }

        internal static Task<bool> AddEmployeeAsync(string employeeId, string name, string phoneNumber, string email, string position, int salary, string departmentName)
        {
            throw new NotImplementedException();
        }

        internal static Task<bool> DeleteProjectAsync(string projectName)
        {
            throw new NotImplementedException();
        }

        internal static Task<bool> DeleteEmployeeAsync(string employeeId)
        {
            throw new NotImplementedException();
        }

        internal static Task<bool> DeleteDepartmentAsync(string departmentName)
        {
            throw new NotImplementedException();
        }

        internal static Task<bool> DeleteClassAsync(string className)
        {
            throw new NotImplementedException();
        }
    }
}
