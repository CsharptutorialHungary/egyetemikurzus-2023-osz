using StudentTerminal.Models;

namespace StudentTerminal.Commands
{
    public static class MenuCommand
    {
        public static async Task Menu()
        {
            List<Student> students = await StudentCommand.LoadStudentsFromJson();

            Console.Clear();
            Console.WriteLine("Válassz egy műveletet:");
            Console.WriteLine("1 - Tanulók összes adatainak listázása");
            Console.WriteLine("2 - Tanuló adatainak szűrése a neve alapján");
            Console.WriteLine("3 - Tanulók rendezése átlag alapján");
            Console.WriteLine("4 - Statisztika a tanulókról");
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("5 - Kilépés");
            Console.ResetColor();

            Console.Write("> ");

            string? menuAction = Console.ReadLine();

            int chosenMenuAction;

            if (menuAction == string.Empty)
            {
                chosenMenuAction = 0;
            }
            else
            {
                chosenMenuAction = Convert.ToInt32(menuAction);
            }

            while (chosenMenuAction != 5)
            {
                Console.Clear();

                switch (chosenMenuAction)
                {
                    case 1:
                        StudentCommand.Write(students);
                        Console.WriteLine("\nA folytatáshoz nyomj meg egy gombot!");
                        Console.ReadKey();
                        await Menu();
                        return;
                    case 2:

                        Console.Write("Add meg a tanuló nevét: ");
                        string? selectedStudentName = Console.ReadLine();
                        Console.Clear();
                        if (!students.Select(item => item.Name.ToUpper()).Contains(selectedStudentName?.ToUpper()))
                        {
                            Console.WriteLine("Nincs ilyen tanuló az adatbázisunkban!");
                            Console.WriteLine("Az alábbi tanulók közül lehet választani:");
                            StudentCommand.WriteOnlyData(students.Select(item => item.Name).ToList());
                        }
                        else
                        {
                            StudentCommand.Write(students.Select(item => new {
                                item.Name,
                                item.Age,
                                item.Average,
                            }).Where(item => item.Name.ToUpper() == selectedStudentName?.ToUpper()).ToList());
                        }
                        Console.WriteLine("\nA folytatáshoz nyomj meg egy gombot!");
                        Console.ReadKey();
                        await Menu();
                        return;
                    case 3:
                        StudentCommand.Write(students.OrderByDescending(item => item.Average).ToList());
                        Console.WriteLine("\nA folytatáshoz nyomj meg egy gombot!");
                        Console.ReadKey();
                        await Menu();
                        return;
                    case 4:
                        Console.WriteLine($"Tanulók száma: {students.Count}");
                        Console.WriteLine($"Tanulók összesített átlaga: {students.Average(item => item.Average).ToString("0.00")}");
                        var bestStudent = students.OrderByDescending(item => item.Average).Select(item => new { item.Name, item.Average }).ToList()[0];
                        Console.WriteLine($"Legjobb átlaggal rendelkező tanuló: [{bestStudent.Name}]({bestStudent.Average.ToString("0.00")})");
                        var mostCoursesTakenStudent = students.OrderByDescending(item => item.Courses).Select(item => new { item.Name, item.Courses }).ToList()[0];
                        Console.WriteLine($"Legtöbb kurzust felvett tanuló: [{mostCoursesTakenStudent.Name}]({mostCoursesTakenStudent.Courses})");
                        var oldestStudent = students.OrderByDescending(item => item.Age).Select(item => new { item.Name, item.Age }).ToList()[0];
                        Console.WriteLine($"Legidősebb tanuló: [{oldestStudent.Name}]({oldestStudent.Age} éves)");
                        Console.WriteLine("\nA folytatáshoz nyomj meg egy gombot!");
                        Console.ReadKey();
                        await Menu();
                        return;
                    default:
                        Console.WriteLine("Nincs ilyen művelet!\nA folytatáshoz nyomj meg egy gombot!");
                        Console.ReadKey();
                        await Menu();
                        return;
                }
