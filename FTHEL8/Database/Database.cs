using FTHEL8.Models;
using System.Data.SQLite;

namespace FTHEL8.Data
{
    public class Database
    {
        private static SQLiteConnection CreateConnection()
        {
            SQLiteConnection sqlite_conn;
            sqlite_conn = new SQLiteConnection("Data Source=./Database/Database.db;");
            try
            {
                sqlite_conn.Open();
            }
            catch (SQLiteException)
            {
                Console.Error.WriteLine("Database connection failed, maybe sqlite db file is in the wrong place!");
            }
            return sqlite_conn;
        }

        public static List<Employee>  ReadEmployees()
        {

            List<Employee> employees = new List<Employee>();

            using (var connection = CreateConnection())
            {
                using (var command = new SQLiteCommand("SELECT * FROM employees", connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Employee employee = new Employee
                            {
                                EmployeeId = reader["employee_id"].ToString()?? "",
                                Password = reader["password"].ToString() ?? "",
                                Name = reader["name"].ToString(),
                                PhoneNumber = reader["phone"].ToString(),
                                Email = reader["email"].ToString(),
                                Position = reader["position"].ToString(),
                                Salary = Convert.ToInt32(reader["salary"]),
                                Department = DepartmentReader(reader["department"].ToString() ?? "")
                            };

                            employees.Add(employee);
                        }
                    }
                }
            }

            return employees;
        }

        public static List<Department> ReadDepartments()
        {
            List<Department> departments = new List<Department>();
            using (var connection = CreateConnection())
            {
                using (var command = new SQLiteCommand("SELECT * FROM departments", connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Department department = new Department
                            {
                                Name = reader["name"].ToString() ?? "",
                                Task = reader["task"].ToString(),
                                DepartmentLeader = EmployeeReader(reader["department_leader"].ToString() ?? ""),
                                Class = ClassReader(reader["class_name"].ToString() ?? "")
                            };

                            departments.Add(department);
                        }
                    }
                }
            }
            return departments;
        }

        public static List<Class> ReadClasses()
        {
            List<Class> classes = new List<Class>();
            using (var connection = CreateConnection())
            {
                using (var command = new SQLiteCommand("SELECT * FROM classes", connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Class class_ = new Class
                            {
                                Name = reader["name"].ToString() ?? "",
                                Task = reader["task"].ToString(),
                                ClassLeader = EmployeeReader(reader["class_leader"].ToString() ?? "")
                            };

                            classes.Add(class_);
                        }
                    }
                }
            }
            return classes;
        }

        public static List<Project> ReadProjects()
        {
            List<Project> projects = new List<Project>();
            using (var connection = CreateConnection())
            {
                using (var command = new SQLiteCommand("SELECT * FROM projects", connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Project project = new Project
                            {
                                Name = reader["name"].ToString() ?? "",
                                Deadline = (DateTime)reader["deadline"],
                                Description = reader["description"].ToString(),
                                ProjectLeader = EmployeeReader(reader["project_leader"].ToString() ?? ""),
                                ClassName = ClassReader(reader["class_name"].ToString() ?? "") 
                            };

                            projects.Add(project);
                        }
                    }
                }
            }
            return projects;
        }

        public static List<ProjectMembers> ReadProjectMembers()
        {
            List<ProjectMembers> projectmembers = new List<ProjectMembers>();
            using (var connection = CreateConnection())
            {
                using (var command = new SQLiteCommand("SELECT * FROM project_members", connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ProjectMembers projectmember = new ProjectMembers
                            {
                                Name = ProjectReader(reader["project_name"].ToString() ?? ""),
                                Employee = EmployeeReader(reader["employee_id"].ToString() ?? "")
                            };

                            projectmembers.Add(projectmember);
                        }
                    }
                }
            }
            return projectmembers;
        }

        public static List<ProjectReports> ReadProjectReports()
        {
            List<ProjectReports> projectreports = new List<ProjectReports>();
            using (var connection = CreateConnection())
            {
                using (var command = new SQLiteCommand("SELECT * FROM project_reports", connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ProjectReports projectreport = new ProjectReports
                            {
                                Name = ProjectReader(reader["project_name"].ToString() ?? ""),
                                Employee = EmployeeReader(reader["employee_id"].ToString() ?? ""),
                                Report = reader["report"].ToString() ?? ""
                            };

                            projectreports.Add(projectreport);
                        }
                    }
                }
            }
            return projectreports;
        }


        private static Department? DepartmentReader(string departmentName)
        {
            using (var connection = CreateConnection())
            {
                using (var command = new SQLiteCommand($"SELECT * FROM departments WHERE name = '{departmentName}'", connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Department department = new Department
                            {
                                Name = reader["name"].ToString() ?? "",
                                Task = reader["task"].ToString(),
                                DepartmentLeader = EmployeeReader(reader["department_leader"].ToString() ?? ""),
                                Class = ClassReader(reader["class_name"].ToString() ?? "")
                            };

                            return department;
                        }
                    }
                }
            }

            return null;
        }

        private static Employee? EmployeeReader(string employeeId)
        {
            using (var connection = CreateConnection())
            {
                using (var command = new SQLiteCommand($"SELECT * FROM employees WHERE employee_id = '{employeeId}'", connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Employee employee = new Employee
                            {
                                EmployeeId = reader["employee_id"].ToString() ?? "",
                                Password = reader["password"].ToString() ?? "",
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

        private static Class? ClassReader(string className)
        {
            using (var connection = CreateConnection())
            {
                using (var command = new SQLiteCommand($"SELECT * FROM classes WHERE name = '{className}'", connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Class class_ = new Class
                            {
                                Name = reader["name"].ToString()?? "",
                                Task = reader["task"].ToString(),
                                ClassLeader = EmployeeReader(reader["class_leader"].ToString()?? ""),
                            };

                            return class_;
                        }
                    }
                }
            }

            return null;
        }

        private static Project? ProjectReader(string projectName)
        {
            using (var connection = CreateConnection())
            {
                using (var command = new SQLiteCommand($"SELECT * FROM projects WHERE name = '{projectName}'", connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Project project = new Project
                            {
                                Name = reader["name"].ToString() ?? "",
                                Description = reader["description"].ToString(),
                                Deadline = (DateTime)reader["deadline"],
                                ProjectLeader = EmployeeReader(reader["project_leader"].ToString() ?? ""),
                                ClassName = ClassReader(reader["class_name"].ToString() ?? "")
                            };

                            return project;
                        }
                    }
                }
            }

            return null;
        }

    }
}

