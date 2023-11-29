using StudentTerminal.Commands;
using StudentTerminal.Models;

namespace StudentCommandTests
{
    public class SaveStudentsToJSONTests
    {
        [TearDown]
        public void DeleteTestResourcesFolder()
        {
            var baseFolder = AppContext.BaseDirectory;

            baseFolder = Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetParent(baseFolder)!.ToString())!.ToString())!.ToString())!.ToString();

            baseFolder = Path.Combine(baseFolder, "Resources");

            var filePath = Path.Combine(baseFolder, "students.json");

            File.Delete(filePath);

            Directory.Delete(baseFolder);
        }

        [Test]
        public async Task TestThat_SaveStudentsToJSON_Runs()
        {
            List<Student> students = await StudentCommand.Initialize(10);

            Task<bool> result = StudentCommand.SaveStudentsToJSON(students);

            Assert.That(result.Result, Is.True);
        }

        [Test]
        public async Task TestThat_SaveStudentsToJSON_CreatesNewFolder()
        {
            List<Student> students = await StudentCommand.Initialize(10);

            var baseFolder = AppContext.BaseDirectory;

            baseFolder = Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetParent(baseFolder)!.ToString())!.ToString())!.ToString())!.ToString();

            baseFolder = Path.Combine(baseFolder, "Resources");

            bool result = Directory.Exists(baseFolder);

            Assert.That(result, Is.True);
        }

        [Test]
        public async Task TestThat_SaveStudentsToJSON_CreatesNewFile()
        {
            List<Student> students = await StudentCommand.Initialize(10);

            var filePath = AppContext.BaseDirectory;

            filePath = Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetParent(filePath)!.ToString())!.ToString())!.ToString())!.ToString();

            filePath = Path.Combine(filePath, "Resources", "students.json");

            bool result = File.Exists(filePath);

            Assert.That(result, Is.True);
        }
    }
}