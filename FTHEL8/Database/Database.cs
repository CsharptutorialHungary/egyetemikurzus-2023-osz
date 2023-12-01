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
                            Department = await DepartmentReaderAsync(reader["department"].ToString() ?? "")
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
                            string employeeIdsString = reader["EmployeeIds"].ToString();
                            var employeeIds = employeeIdsString.Split(',').ToList();

                            var employeeTasks = employeeIds.Select(EmployeeReaderAsync).ToList();
                            var employeeResults = await Task.WhenAll(employeeTasks);
                            var employees = employeeResults.ToList();

                            ProjectMembers projectMember = new ProjectMembers
                            {
                                ProjectName = await ProjectReaderAsync(reader["ProjectName"].ToString()),
                                Employees = employees
                            };

                            projectmembers.Add(projectMember);
                        }
                    }
                }
            }
            return projectmembers;
        }

        public static async Task<List<ProjectReports>> ReadProjectReportsAsync()
        {
            List<ProjectReports> projectreports = new List<ProjectReports>();
            using (var connection = await CreateConnectionAsync())
            {

                using (var command = new SQLiteCommand("SELECT * FROM project_reports", connection))
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        ProjectReports projectreport = new ProjectReports
                        {
                            Name = await ProjectReaderAsync(reader["project_name"].ToString()),
                            Employee = await EmployeeReaderAsync(reader["employee_id"].ToString()),
                            Report = reader["report"].ToString() ?? ""
                        };

                        projectreports.Add(projectreport);
                    }
                }
            }
            return projectreports;
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
                                Salary = Convert.ToInt32(reader["salary"])
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
            using (var connection = await CreateConnectionAsync())
            {
                using (var command = new SQLiteCommand($"DELETE FROM employees WHERE employee_id = '{employeeId}'", connection))
                {
                    int rowsAffected = await command.ExecuteNonQueryAsync();

                    return rowsAffected > 0;
                }
            }
        }

        public static async Task<bool> DeleteClassAsync(string className)
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

        public static async Task<bool> DeleteDepartmentAsync(string departmentName)
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

        public static async Task<bool> DeleteProjectAsync(string projectName)
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

        public static async Task<bool> AddEmployeeAsync(string employeeId, string name, string phoneNumber, string email, string position, int salary, string departmentName)
        {
            using (var connection = await CreateConnectionAsync())
            {
                try
                {
                    var department = await DepartmentReaderAsync(departmentName);
                    if (department == null)
                    {
                        Console.WriteLine("Department does not exist. Please add the department first.");
                        return false;
                    }

                    using (var command = new SQLiteCommand(connection))
                    {
                        command.CommandText = $"INSERT INTO employees (employee_id, name, phone, email, position, salary, department) VALUES ({employeeId}, {name}, {phoneNumber}, {email}, {position}, {salary}, {departmentName})";

                        await command.ExecuteNonQueryAsync();
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error adding employee: {ex.Message}");
                    return false;
                }
            }
        }

    }
}

