using FTHEL8.Data;
using Newtonsoft.Json;

namespace FTHEL8.Menu
{
    public class MainMenu : Menu
    {
        public MainMenu() : base([], [])
        {
            Console.WriteLine();
            Console.Write("Which functionality do you want to use?");
            Menu queryMenu = new QueryMenu();
            Menu deleteMenu = new DeleteMenu();
            Menu addMenu = new AddMenu();
            Menu modifyMenu = new ModifyMenu();

            AddOption("Query Data", queryMenu.Display);
            AddOption("Add Data", addMenu.Display);
            AddOption("Modify Data", modifyMenu.Display);
            AddOption("Delete Data", deleteMenu.Display);
            AddOption("Make a backup save of the data", BackupToJson);
            AddOption("Exit", Exit);
        }

        private void AddData()
        {
            Console.WriteLine("Selected: Add Data");
        }

        private void Exit()
        {
            Console.WriteLine("Exiting the program.");

            BackupToJson();

            Environment.Exit(0);
        }

        private void BackupToJson()
        {
            var employees = Database.ReadEmployeesAsync().Result;
            var departments = Database.ReadDepartmentsAsync().Result;
            var classes = Database.ReadClassesAsync().Result;
            var projects = Database.ReadProjectsAsync().Result;
            var projectMembers = Database.ReadProjectMembersAsync().Result;

            var employeesBackupPath = "employees_backup.json";
            var departmentsBackupPath = "departments_backup.json";
            var classesBackupPath = "classes_backup.json";
            var projectsBackupPath = "projects_backup.json";
            var projectMembersBackupPath = "projectMembers_backup.json";


            File.WriteAllText(employeesBackupPath, JsonConvert.SerializeObject(employees, Formatting.Indented));
            File.WriteAllText(departmentsBackupPath, JsonConvert.SerializeObject(departments, Formatting.Indented));
            File.WriteAllText(classesBackupPath, JsonConvert.SerializeObject(classes, Formatting.Indented));
            File.WriteAllText(projectsBackupPath, JsonConvert.SerializeObject(projects, Formatting.Indented));
            File.WriteAllText(projectMembersBackupPath, JsonConvert.SerializeObject(projectMembers, Formatting.Indented));

            Console.WriteLine("Backup created successfully:");
        }
    }
}
