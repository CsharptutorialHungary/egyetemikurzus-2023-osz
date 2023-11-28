using StudentTerminal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace StudentTerminal.Commands
{
    public static class StudentCommand
    {
        /// <summary>
        /// League of Legends karakter nevekkel és randomizált értékekkel feltölt egy Student listát
        /// </summary>
        /// <param name="numberOfStudents">Generálni kívánt studentek száma</param>
        /// <returns>True, ha lefutott</returns>
        public static async Task<bool> Initialize(int numberOfStudents = 165)
        {
            List<string> names = new List<string>()
            {
                "Aatrox", "Ahri", "Akali", "Akshan", "Alistar", "Amumu", "Anivia", "Annie", "Aphelios", "Ashe", "Aurelion Sol", "Azir", "Bard", "Bel'Veth", "Blitzcrank", "Brand", "Braum", "Briar", "Caitlyn",
                "Camille", "Cassiopeia", "Cho'Gath", "Corki", "Darius", "Diana", "Draven", "Dr. Mundo", "Ekko", "Elise", "Evelynn", "Ezreal", "Fiddlesticks", "Fiora",
                "Fizz", "Galio", "Gangplank", "Garen", "Gnar", "Gragas", "Graves", "Gwen", "Hecarim", "Heimerdinger", "Illaoi", "Irelia", "Ivern", "Janna", "Jarvan IV", "Jax",
                "Jayce", "Jhin", "Jinx", "Kai'Sa", "Kalista", "Karma", "Karthus", "Kassadin", "Katarina", "Kayle", "Kayn", "Kennen", "Kha'Zix", "Kindred", "Kled", "Kog'Maw",
                "K'Sante", "LeBlanc", "Lee Sin", "Leona", "Lillia", "Lissandra", "Lucian", "Lulu", "Lux", "Malphite", "Malzahar", "Maokai", "Master Yi", "Milio", "Miss Fortune",
                "Mordekaiser", "Morgana", "Naafiri", "Nami", "Nasus", "Nautilus", "Neeko", "Nidalee", "Nilah", "Nocturne", "Nunu & Willump", "Olaf", "Orianna", "Ornn", "Pantheon",
                "Poppy", "Pyke", "Qiyana", "Quinn", "Rakan", "Rammus", "Rek'Sai", "Rell", "Renata Glasc", "Renekton", "Rengar", "Riven", "Rumble", "Ryze", "Samira", "Sejuani", "Senna", "Seraphine",
                "Sett", "Shaco", "Shen", "Shyvana", "Singed", "Sion", "Sivir", "Skarner", "Sona", "Soraka", "Swain", "Sylas", "Syndra", "Tahm Kench", "Taliyah", "Talon", "Taric", "Teemo",
                "Thresh", "Tristana", "Trundle", "Tryndamere", "Twisted Fate", "Twitch", "Udyr", "Urgot", "Varus", "Vayne", "Veigar", "Vel'Koz", "Vex", "Vi", "Viego", "Viktor",
                "Vladimir", "Volibear", "Warwick", "Wukong", "Xayah", "Xerath", "Xin Zhao", "Yasuo", "Yone", "Yorick", "Yuumi", "Zac", "Zed", "Zeri", "Ziggs", "Zilean", "Zoe", "Zyra",
            };

            if (numberOfStudents > 165)
            {
                numberOfStudents = 165;
            }

            Random random = new Random();

            List<Student> students = new List<Student>();

            HashSet<string> addedNames = new HashSet<string>();

            for (int i = 0; i < numberOfStudents; i++)
            {
                int actualNumber = random.Next(0, 165);

                string name = names[actualNumber];

                while (true)
                {
                    if (!addedNames.Contains(name))
                    {
                        addedNames.Add(name);
                        break;
                    }

                    actualNumber = random.Next(0, 165);

                    name = names[actualNumber];
                }

                int age = random.Next(18, 51);

                int courses = random.Next(0, 16);

                int credits = random.Next(0, 46);

                string email = $"{name.ToLower().Replace(' ', '_')}@gmail.com";

                string id = $"h{random.Next(1000, 9001)}";

                double average = Math.Round(random.NextDouble() * (5 - 1) + 1, 2);

                Student student = new Student(id, name, age, email, courses, credits, average);

                students.Add(student);
            }

            await SaveStudentsToJSON(students);

            return true;
        }

        /// <summary>
        /// Egy Student listát elment JSON formátú fileba.
        /// </summary>
        /// <param name="students">Student lista</param>
        /// <returns>True, ha lefutott</returns>
        public static async Task<bool> SaveStudentsToJSON(List<Student> students)
        {
            var directory = AppContext.BaseDirectory;

            string fileName = "students.json";

            string baseFolder = Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetParent(directory)!.ToString())!.ToString())!.ToString())!.ToString();

            baseFolder = Path.Combine(baseFolder, "Resources");

            if (!Directory.Exists(Path.Combine(baseFolder, "Resources")))
            {
                try
                {
                    Directory.CreateDirectory(baseFolder);
                }
                catch (IOException fileException)
                {
                    await Console.Error.WriteLineAsync("Az alábbi hiba történt:" + fileException.Message);

                    await Console.Error.WriteLineAsync("A folytatáshoz nyomj meg egy gombot!");
                }
                catch (Exception pokemon)
                {
                    await Console.Error.WriteLineAsync("Az alábbi hiba történt:" + pokemon.Message);

                    await Console.Error.WriteLineAsync("A folytatáshoz nyomj meg egy gombot!");
                }
            }

            var filePath = Path.Combine(baseFolder, fileName);

            try
            {
                using (FileStream createStream = File.Create(filePath))
                {
                    var options = new JsonSerializerOptions
                    {
                        WriteIndented = true,
                        Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                    };

                    await JsonSerializer.SerializeAsync(createStream, students, options);

                    await createStream.DisposeAsync();
                }
            }
            catch (IOException fileException)
            {
                await Console.Error.WriteLineAsync("Az alábbi hiba történt:" + fileException.Message);

                await Console.Out.WriteLineAsync("A folytatáshoz nyomj meg egy gombot!");

                Console.ReadKey();
            }
            catch (Exception pokemon)
            {
                await Console.Error.WriteLineAsync("Az alábbi hiba történt:" + pokemon.Message);

                await Console.Out.WriteLineAsync("A folytatáshoz nyomj meg egy gombot!");

                Console.ReadKey();
            }

            return true;
        }

        /// <summary>
        /// A Resources mappában található JSON fileból készít egy Student listát
        /// </summary>
        /// <returns>Student lista JSON fileból</returns>
        public static async Task<List<Student>> LoadStudentsFromJson()
        {
            var directory = AppContext.BaseDirectory;

            string fileName = "students.json";

            var baseFolder = Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetParent(directory)!.ToString())!.ToString())!.ToString());

            var filePath = Path.Combine(baseFolder!.ToString(), "Resources", fileName);

            if (!File.Exists(filePath))
            {
                await Initialize();
            }

            try
            {
                using (FileStream jsonFile = File.OpenRead(filePath))
                {
                    var options = new JsonSerializerOptions
                    {
                        WriteIndented = true,
                        Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                    };

                    List<Student>? students = await JsonSerializer.DeserializeAsync<List<Student>>(jsonFile, options);

                    return students!;
                }
            }
            catch (IOException fileException)
            {
                await Console.Error.WriteLineAsync("Az alábbi hiba történt:" + fileException.Message);

                await Console.Out.WriteLineAsync("A folytatáshoz nyomj meg egy gombot!");

                Console.ReadKey();
            }
            catch (Exception pokemon)
            {
                await Console.Error.WriteLineAsync("Az alábbi hiba történt:" + pokemon.Message);

                await Console.Out.WriteLineAsync("A folytatáshoz nyomj meg egy gombot!");

                Console.ReadKey();
            }

            return new List<Student>();
        }

        /// <summary>
        /// Lista elemeire meghívja a Console.WriteLine metódust
        /// </summary>
        /// <typeparam name="T">Lista típusa</typeparam>
        /// <param name="list">Lista</param>
        public static void Write<T>(List<T> list)
        {
            ConsoleColor[] consoleColors =
            {
                ConsoleColor.DarkRed,
                ConsoleColor.DarkBlue,
                ConsoleColor.DarkGreen,
                ConsoleColor.DarkGray,
                ConsoleColor.DarkYellow,
                ConsoleColor.DarkMagenta,
                ConsoleColor.DarkCyan,
            };

            Type? type = list[0]?.GetType();

            PropertyInfo[]? properties = type?.GetProperties();

            int indexOfColor = 0;

            foreach (PropertyInfo? item in properties!)
            {
                Console.BackgroundColor = consoleColors[indexOfColor++];

                Console.ForegroundColor = ConsoleColor.White;

                Console.Write($"[{item.Name.ToUpper()}]");

                if (indexOfColor >= consoleColors.Length)
                {
                    indexOfColor = 0;
                }

                Console.ResetColor();
            }

            Console.WriteLine();

            foreach (var item in list)
            {
                foreach (PropertyInfo property in properties)
                {
                    Console.Write($"[{property.GetValue(item)}]");
                }
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Meghívja a ToString metódusát a paraméterből kapott listának az összes elemére
        /// </summary>
        /// <typeparam name="T">Lista típusa</typeparam>
        /// <param name="list">Lista</param>
        public static void WriteOnlyData<T>(List<T> list)
        {
            list.ForEach(item => Console.WriteLine(item));
        }
    }
}
