using KássaGergő_KássaDávid.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace KássaGergő_KássaDávid.Commands
{
    public static class StudentCommand
    {
        /// <summary>
        /// League of Legends karakter nevekkel és randomizált értékekkel feltölt egy Student listát
        /// </summary>
        /// <param name="numberOfStudents">Studentek száma. Max 165</param>
        public static async void Initialize(int numberOfStudents = 165)
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

            if(numberOfStudents > 165)
            {
                numberOfStudents = 165;
            }

            Random random = new Random();

            List<Student> students = new List<Student>();

            HashSet<string> addedNames = new HashSet<string>();

            for (int i = 0; i < numberOfStudents; i++)
            {
                int actualNumber = random.Next(0,165);

                string name = names[actualNumber];

                while(true)
                {
                    if(!addedNames.Contains(name))
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

                string id = $"h{random.Next(1000,9001)}";

                double average = Math.Round(random.NextDouble() * (5 - 1) + 1, 2);

                Student student = new Student(name, age, courses, credits, email, id, average);

                students.Add(student);
            }

            await SaveStudentsToJSON(students);
        }

        /// <summary>
        /// Egy Student listát elment JSON formátú fileba.
        /// </summary>
        /// <param name="students">Student lista</param>
        /// <returns></returns>
        public static async Task SaveStudentsToJSON(List<Student> students)
        {
            string fileName = "..//..//..//students.json";
            
            using FileStream createStream = File.Create(fileName);

            var options = new JsonSerializerOptions { WriteIndented = true };

            await JsonSerializer.SerializeAsync(createStream, students, options);
            
            await createStream.DisposeAsync();
        }
    }
}
