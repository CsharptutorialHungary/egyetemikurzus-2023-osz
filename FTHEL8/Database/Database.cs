using FTHEL8.Models;
using System.Data.SQLite;

namespace FTHEL8.Data
{
    public class Database
    {
        private static async Task<SQLiteConnection> CreateConnectionAsync()
        {
            SQLiteConnection sqlite_conn = new SQLiteConnection("Data Source=./Database/Database.db;");

            try
            {
                await sqlite_conn.OpenAsync();
            }
            catch (SQLiteException)
            {
                Console.Error.WriteLine("Database connection failed, maybe sqlite db file is in the wrong place!");
            }

            return sqlite_conn;
        }

        public static async Task<List<Employee>> ReadEmployeesAsync()
        {
            List<Employee> employees = new List<Employee>();

            using (var connection = await CreateConnectionAsync())
            {
                using (var command = new SQLiteCommand("SELECT * FROM employees", connection))
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        Employee employee = new Employee
                        {
                            EmployeeId = reader["employee_id"].ToString() ?? "",
                            Name = reader["name"].ToString(),
                            PhoneNumber = reader["phone"].ToString(),
                            Email = reader["email"].ToString(),
                            Position = reader["position"].ToString(),
                            Salary = Convert.ToInt32(reader["salary"]),
                            DepartmentName = reader["department"].ToString() ?? ""
                        };

                        employees.Add(employee);
                    }
                }
            }

            return employees;
        }

        public static async Task<List<Department>> ReadDepartmentsAsync()
        {
            List<Department> departments = new List<Department>();
            using (var connection = await CreateConnectionAsync())
            {

                using (var command = new SQLiteCommand("SELECT * FROM departments", connection))
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        Department department = new Department
                        {
                            Name = reader["name"].ToString() ?? "",
                            Task = reader["task"].ToString(),
                            DepartmentLeader = await EmployeeReaderAsync(reader["department_leader"].ToString() ?? ""),
                            ClassName = await ClassReaderAsync(reader["class_name"].ToString() ?? "")
                        };

                        departments.Add(department);
                    }
                }
            }
            return departments;
        }

        public static async Task<List<Class>> ReadClassesAsync()
        {
            List<Class> classes = new List<Class>();
            using (var connection = await CreateConnectionAsync())
            {

                using (var command = new SQLiteCommand("SELECT * FROM classes", connection))
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        Class class_ = new Class
                        {
                            Name = reader["name"].ToString() ?? "",
                            Task = reader["task"].ToString(),
                            ClassLeader = await EmployeeReaderAsync(reader["class_leader"].ToString() ?? "")
                        };

                        classes.Add(class_);
                    }
                }
            }
            return classes;
        }

        public static async Task<List<Project>> ReadProjectsAsync()
        {
            List<Project> projects = new List<Project>();
            using (var connection = await CreateConnectionAsync())
            {

                using (var command = new SQLiteCommand("SELECT * FROM projects", connection))
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        Project project = new Project
                        {
                            Name = reader["name"].ToString() ?? "",
                            Deadline = (DateTime)reader["deadline"],
                            Description = reader["description"].ToString(),
                            ProjectLeader = await EmployeeReaderAsync(reader["project_leader"].ToString() ?? ""),
                            ClassName = await ClassReaderAsync(reader["class_name"].ToString() ?? "")
                        };

                        projects.Add(project);
                    }
                }
            }
            return projects;
        }

        public static async Task<List<ProjectMembers>> ReadProjectMembersAsync()
        {
            List<ProjectMembers> projectmembers = new List<ProjectMembers>();
            using (var connection = await CreateConnectionAsync())
            {

                using (var command = new SQLiteCommand(
                        "SELECT projects.name AS ProjectName, GROUP_CONCAT(employees.employee_id) AS EmployeeIds " +
                        "FROM projects " +
                        "JOIN project_members ON projects.name = project_members.project_name " +
                        "JOIN employees ON project_members.employee_id = employees.employee_id " +
                        "GROUP BY projects.name " +
                        "ORDER BY projects.name;", connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            string employeeIdsString = reader["EmployeeIds"].ToString() ?? "";
                            var employeeIds = employeeIdsString.Split(',').ToList();

                            var employeeTasks = employeeIds.Select(EmployeeReaderAsync).ToList();
                            var employeeResults = await Task.WhenAll(employeeTasks);
                            var employees = employeeResults.ToList();

                            ProjectMembers projectMember = new ProjectMembers
                            {
                                ProjectName = await ProjectReaderAsync(reader["ProjectName"].ToString() ?? ""),
                                Employees = employees!
                            };

                            projectmembers.Add(projectMember);
                        }
                    }
                }
            }
            return projectmembers;
        }

        private async static Task<Department?> DepartmentReaderAsync(string departmentName)
        {
            using (var connection = await CreateConnectionAsync())
            {
                using (var command = new SQLiteCommand($"SELECT * FROM departments WHERE name = '{departmentName}'", connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            Department department = new Department
                            {
                                Name = reader["name"].ToString() ?? "",
                                Task = reader["task"].ToString(),
                                DepartmentLeader = await EmployeeReaderAsync(reader["department_leader"].ToString() ?? ""),
                                ClassName = await ClassReaderAsync(reader["class_name"].ToString() ?? "")
                            };

                            return department;
                        }
                    }
                }
            }

            return null;
        }

        private static async Task<Employee?> EmployeeReaderAsync(string employeeId)
        {
            using (var connection = await CreateConnectionAsync())
            {
                using (var command = new SQLiteCommand($"SELECT * FROM employees WHERE employee_id = '{employeeId}'", connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            Employee employee = new Employee
                            {
                                EmployeeId = reader["employee_id"].ToString() ?? "",
                                Name = reader["name"].ToString(),
                                PhoneNumber = reader["phone"].ToString(),
                                Email = reader["email"].ToString(),
                                Position = reader["position"].ToString(),
                                Salary = Convert.ToInt32(reader["salary"]),
                                DepartmentName = reader["department"].ToString()
                            };

                            return employee;
                        }
                    }
                }
            }

            return null;
        }

        private static async Task<Class?> ClassReaderAsync(string className)
        {
            using (var connection = await CreateConnectionAsync())
            {
                using (var command = new SQLiteCommand($"SELECT * FROM classes WHERE name = '{className}'", connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            Class class_ = new Class
                            {
                                Name = reader["name"].ToString() ?? "",
                                Task = reader["task"].ToString(),
                                ClassLeader = await EmployeeReaderAsync(reader["class_leader"].ToString() ?? "")
                            };

                            return class_;
                        }
                    }
                }
            }

            return null;
        }

        private static async Task<Project?> ProjectReaderAsync(string projectName)
        {
            using (var connection = await CreateConnectionAsync())
            {
                using (var command = new SQLiteCommand($"SELECT * FROM projects WHERE name = '{projectName}'", connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            Project project = new Project
                            {
                                Name = reader["name"].ToString() ?? "",
                                Description = reader["description"].ToString(),
                                Deadline = (DateTime)reader["deadline"],
                                ProjectLeader = await EmployeeReaderAsync(reader["project_leader"].ToString() ?? ""),
                                ClassName = await ClassReaderAsync(reader["class_name"].ToString() ?? "")
                            };

                            return project;
                        }
                    }
                }
            }

            return null;
        }

        public static async Task<bool> DeleteEmployeeAsync(string employeeId)
        {
            try
            {
                using (var connection = await CreateConnectionAsync())
                {
                    using (var command = new SQLiteCommand($"DELETE FROM employees WHERE employee_id = '{employeeId}'", connection))
                    {
                        int rowsAffected = await command.ExecuteNonQueryAsync();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error deleting employee: {ex.Message}");
                return false;
            }
        }

        public static async Task<bool> DeleteClassAsync(string className)
        {
            try
            {
                using (var connection = await CreateConnectionAsync())
                {
                    using (var command = new SQLiteCommand($"DELETE FROM classes WHERE name = '{className}'", connection))
                    {
                        int rowsAffected = await command.ExecuteNonQueryAsync();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error deleting class: {ex.Message}");
                return false;
            }
        }

        public static async Task<bool> DeleteDepartmentAsync(string departmentName)
        {
            try
            {
                using (var connection = await CreateConnectionAsync())
                {
                    using (var command = new SQLiteCommand($"DELETE FROM departments WHERE name = '{departmentName}'", connection))
                    {
                        int rowsAffected = await command.ExecuteNonQueryAsync();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error deleting department: {ex.Message}");
                return false;
            }
        }

        public static async Task<bool> DeleteProjectAsync(string projectName)
        {
            try
            {
                using (var connection = await CreateConnectionAsync())
                {
                    using (var command = new SQLiteCommand($"DELETE FROM projects WHERE name = '{projectName}'", connection))
                    {
                        int rowsAffected = await command.ExecuteNonQueryAsync();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error deleting project: {ex.Message}");
                return false;
            }
        }


        public static async Task<bool> AddEmployeeAsync(string employeeId, string name, string phoneNumber, string email, string position, int salary, string departmentName)
        {
            try
            {
                Department? department = await DepartmentReaderAsync(departmentName);

                if (department != null)
                {
                    using (var connection = await CreateConnectionAsync())
                    {
                        using (var command = new SQLiteCommand(connection))
                        {
                            command.CommandText = "INSERT INTO employees (employee_id, name, phone, email, position, salary, department) " +
                                                  "VALUES (@employeeId, @name, @phoneNumber, @email, @position, @salary, @department)";
                            command.Parameters.AddWithValue("@employeeId", employeeId);
                            command.Parameters.AddWithValue("@name", name);
                            command.Parameters.AddWithValue("@phoneNumber", phoneNumber);
                            command.Parameters.AddWithValue("@email", email);
                            command.Parameters.AddWithValue("@position", position);
                            command.Parameters.AddWithValue("@salary", salary);
                            command.Parameters.AddWithValue("@department", departmentName);

                            await command.ExecuteNonQueryAsync();
                            return true;
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Invalid department name.");
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static async Task<bool> AddDepartmentAsync(string departmentName, string departmentTask, string departmentLeaderId, string className)
        {
            try
            {
                Employee? departmentLeader = await EmployeeReaderAsync(departmentLeaderId);
                Class? class_ = await ClassReaderAsync(className);

                if (departmentLeader != null && class_ != null)
                {
                    using (var connection = await CreateConnectionAsync())
                    {
                        using (var command = new SQLiteCommand(connection))
                        {
                            command.CommandText = "INSERT INTO departments (name, task, department_leader, class_name) " +
                                                  "VALUES (@name, @task, @departmentLeader, @className)";
                            command.Parameters.AddWithValue("@name", departmentName);
                            command.Parameters.AddWithValue("@task", departmentTask);
                            command.Parameters.AddWithValue("@departmentLeader", departmentLeaderId);
                            command.Parameters.AddWithValue("@className", className);

                            await command.ExecuteNonQueryAsync();
                            return true;
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Invalid department leader ID or class name.");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
                return false;
            }
        }

        public static async Task<bool> AddClassAsync(string className, string classTask, string classLeaderId)
        {
            try
            {
                Employee? classLeader = await EmployeeReaderAsync(classLeaderId);

                if (classLeader != null)
                {
                    using (var connection = await CreateConnectionAsync())
                    {
                        using (var command = new SQLiteCommand(connection))
                        {
                            command.CommandText = "INSERT INTO classes (name, task, class_leader) " +
                                                  "VALUES (@name, @task, @classLeader)";
                            command.Parameters.AddWithValue("@name", className);
                            command.Parameters.AddWithValue("@task", classTask);
                            command.Parameters.AddWithValue("@classLeader", classLeaderId);

                            await command.ExecuteNonQueryAsync();
                            return true;
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Invalid class leader ID.");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
                return false;
            }
        }

        public static async Task<bool> AddProjectAsync(string projectName, string projectDescription, DateTime deadline, string projectLeaderId, string className)
        {
            try
            {
                Employee? projectLeader = await EmployeeReaderAsync(projectLeaderId);
                Class? class_ = await ClassReaderAsync(className);

                if (projectLeader != null && class_ != null)
                {
                    using (var connection = await CreateConnectionAsync())
                    {
                        using (var command = new SQLiteCommand(connection))
                        {
                            command.CommandText = "INSERT INTO projects (name, description, deadline, project_leader, class_name) " +
                                                  "VALUES (@name, @description, @deadline, @projectLeader, @className)";
                            command.Parameters.AddWithValue("@name", projectName);
                            command.Parameters.AddWithValue("@description", projectDescription);
                            command.Parameters.AddWithValue("@deadline", deadline);
                            command.Parameters.AddWithValue("@projectLeader", projectLeaderId);
                            command.Parameters.AddWithValue("@className", className);

                            await command.ExecuteNonQueryAsync();
                            return true;
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Invalid project leader ID or class name.");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
                return false;
            }
        }

        public static async Task<bool> AddEmployeeToProjectAsync(string employeeId, string projectName)
        {
            try
            {
                Employee? employee = await EmployeeReaderAsync(employeeId);
                Project? project = await ProjectReaderAsync(projectName);

                if (employee != null && project != null)
                {
                    using (var connection = await CreateConnectionAsync())
                    {
                        using (var command = new SQLiteCommand(connection))
                        {
                            command.CommandText = "INSERT INTO project_members (employee_id, project_name) " +
                                                  "VALUES (@employeeId, @projectName)";
                            command.Parameters.AddWithValue("@employeeId", employeeId);
                            command.Parameters.AddWithValue("@projectName", projectName);

                            await command.ExecuteNonQueryAsync();
                            return true;
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Invalid employee ID or project name.");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
                return false;
            }
        }

        public static async Task<bool> ModifyEmployee(string employeeId, string newName, string newPhoneNumber, string newEmail, string newPosition, int newSalary, string newDepartmentName)
        {
            try
            {
                Department? newDepartment = await DepartmentReaderAsync(newDepartmentName);

                if (newDepartment != null)
                {
                    using (var connection = await CreateConnectionAsync())
                    {
                        using (var command = new SQLiteCommand(connection))
                        {
                            command.CommandText = "UPDATE employees SET name = @newName, phone = @newPhoneNumber, " +
                                                  "email = @newEmail, position = @newPosition, salary = @newSalary, department = @newDepartmentName " +
                                                  "WHERE employee_id = @employeeId";

                            command.Parameters.AddWithValue("@newName", newName);
                            command.Parameters.AddWithValue("@newPhoneNumber", newPhoneNumber);
                            command.Parameters.AddWithValue("@newEmail", newEmail);
                            command.Parameters.AddWithValue("@newPosition", newPosition);
                            command.Parameters.AddWithValue("@newSalary", newSalary);
                            command.Parameters.AddWithValue("@newDepartmentName", newDepartmentName);
                            command.Parameters.AddWithValue("@employeeId", employeeId);

                            await command.ExecuteNonQueryAsync();
                            return true;
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Invalid department name.");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
                return false;
            }
        }

        public static async Task<bool> ModifyClass(string className, string newTask, string newClassLeaderId)
        {
            try
            {
                Employee? newClassLeader = await EmployeeReaderAsync(newClassLeaderId);

                if (newClassLeader != null)
                {
                    using (var connection = await CreateConnectionAsync())
                    {
                        using (var command = new SQLiteCommand(connection))
                        {
                            command.CommandText = "UPDATE classes SET task = @newTask, class_leader = @newClassLeaderId WHERE name = @className";
                            command.Parameters.AddWithValue("@newTask", newTask);
                            command.Parameters.AddWithValue("@newClassLeaderId", newClassLeaderId);
                            command.Parameters.AddWithValue("@className", className);

                            await command.ExecuteNonQueryAsync();
                            return true;
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Invalid Class Leader ID.");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
                return false;
            }
        }

        public static async Task<bool> ModifyProject(string projectName, string newDescription, DateTime newDeadline, string newProjectLeaderId, string newClassName)
        {
            try
            {
                Employee? newProjectLeader = await EmployeeReaderAsync(newProjectLeaderId);

                if (newProjectLeader != null)
                {
                    using (var connection = await CreateConnectionAsync())
                    {
                        using (var command = new SQLiteCommand(connection))
                        {
                            command.CommandText = "UPDATE projects SET description = @newDescription, deadline = @newDeadline, project_leader = @newProjectLeaderId, class_name = @newClassName WHERE name = @projectName";
                            command.Parameters.AddWithValue("@newDescription", newDescription);
                            command.Parameters.AddWithValue("@newDeadline", newDeadline);
                            command.Parameters.AddWithValue("@newProjectLeaderId", newProjectLeaderId);
                            command.Parameters.AddWithValue("@newClassName", newClassName);
                            command.Parameters.AddWithValue("@projectName", projectName);

                            await command.ExecuteNonQueryAsync();
                            return true;
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Invalid Project Leader ID.");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
                return false;
            }
        }

        public static async Task<bool> ModifyProjectMembers(string projectName, List<string> newEmployeeIds)
        {
            try
            {
                List<Employee> newEmployees = new List<Employee>();
                foreach (string employeeId in newEmployeeIds)
                {
                    Employee? employee = await EmployeeReaderAsync(employeeId);
                    if (employee != null)
                    {
                        newEmployees.Add(employee);
                    }
                    else
                    {
                        Console.WriteLine($"Invalid Employee ID: {employeeId}");
                        return false;
                    }
                }

                using (var connection = await CreateConnectionAsync())
                {
                    using (var command = new SQLiteCommand(connection))
                    {
                        command.CommandText = "DELETE FROM project_members WHERE project_name = @projectName";
                        command.Parameters.AddWithValue("@projectName", projectName);
                        await command.ExecuteNonQueryAsync();

                        foreach (Employee employee in newEmployees)
                        {
                            command.CommandText = "INSERT INTO project_members (project_name, employee_id) VALUES (@projectName, @employeeId)";
                            command.Parameters.AddWithValue("@projectName", projectName);
                            command.Parameters.AddWithValue("@employeeId", employee.EmployeeId);
                            await command.ExecuteNonQueryAsync();
                        }

                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
                return false;
            }
        }

    }
}

