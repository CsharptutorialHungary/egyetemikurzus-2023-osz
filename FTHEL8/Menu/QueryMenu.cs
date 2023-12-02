using FTHEL8.Data;
using FTHEL8.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTHEL8.Menu
{
    public class QueryMenu : Menu
    {
        public QueryMenu() : base([], [])
        {
            Console.WriteLine();
            AddOption("Get employees data", GetEmployeesData);
            AddOption("Get classes data", GetClassesData);
            AddOption("Get departments data", GetDepartmentsData);
            AddOption("Get projects data", GetProjectsData);
            AddOption("Get project members data", GetProjectMembersData);
            AddOption("Back", Back);
        }

        private async void GetEmployeesData()
        {
            List<Employee> employees = await Database.ReadEmployeesAsync();
            foreach (Employee employee in employees)
            {
                Console.WriteLine($"Employee ID: {employee.EmployeeId}, Name: {employee.Name}, Phone number: {employee.PhoneNumber}, Email: {employee.Email}, Position: {employee.Position}, Salary: {employee.Salary}, Department: {employee.Department?.Name}");
            }
            Console.WriteLine();
        }

        private async void GetClassesData()
        {
            List<Class> classes = await Database.ReadClassesAsync();
            foreach (Class class_ in classes)
            {
                Console.WriteLine($"Class name: {class_.Name}, Task: {class_.Task}, Class leader: {class_.ClassLeader?.Name}");
            }
            Console.WriteLine();
        }

        private async void GetProjectsData()
        {
            List<Project> projects = await Database.ReadProjectsAsync();
            foreach (Project project in projects)
            {
                Console.WriteLine($"Project name: {project.Name}, Description: {project.Description}, Deadline: {project.Deadline}, Project leader: {project.ProjectLeader?.Name}, Class: {project.ClassName?.Name}");
            }
            Console.WriteLine();
        }

        private async void GetDepartmentsData()
        {
            List<Department> departments = await Database.ReadDepartmentsAsync();
            foreach (Department department in departments)
            {
                Console.WriteLine($"Department name: {department.Name}, Task: {department.Task}, Department leader: {department.DepartmentLeader?.Name}, Class name: {department.ClassName?.Name}");
            }
        }

        private async void GetProjectMembersData()
        {
            List<ProjectMembers> projectMembers = await Database.ReadProjectMembersAsync();
            foreach (var member in projectMembers)
            {
                string employeeNames = string.Join(", ", member.Employees.Select(e => e.Name));
                Console.WriteLine($"Project name: {member.ProjectName?.Name}, Employee: {employeeNames}");
            }
            Console.WriteLine();
        }

    }
}
