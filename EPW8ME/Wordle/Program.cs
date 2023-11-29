using System.Text.Json;
using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace EWP8ME
{
    internal class Program
    {
        static void Main(string[] args)
        {

            GameController gameController = new GameController();
            gameController.startGameController();


            //string answer = Console.ReadLine();
            //Console.WriteLine(answer.Length);
            //Console.ReadKey();
            
            
            /*List<Player> players = new List<Player>();
            var newplayer = new Player
            {
                name = "Anonymus",
                score = 10,
                round = 3,
            };
            var newplayer2 = new Player
            {
                name = "TesztElek",
                score = 40,
                round = 5,
            };
            players.Add(newplayer);
            players.Add(newplayer2);*/
            /*
            string jsonString = JsonSerializer.Serialize(newplayer, new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNameCaseInsensitive = true,
            });

            Console.WriteLine(jsonString);
            Console.WriteLine("--------------------");
            var desserializalt = JsonSerializer.Deserialize<Player>(jsonString);
            Console.WriteLine(desserializalt.name);
            Console.WriteLine(desserializalt.score);
            Console.WriteLine(desserializalt.round);
            Console.ReadKey();*/

            /*
            List<Player> players = new List<Player>();
            var newplayer = new Player
            {
                name = "Anonymus2",
                score = 10,
                round = 3,
            };
            var newplayer2 = new Player
            {
                name = "TesztElek3",
                score = 40,
                round = 5,
            };
            players.Add(newplayer);
            players.Add(newplayer2);

            var appPath = AppContext.BaseDirectory;
            var fileName = "Playertest.json";
            var path = Path.Combine(appPath, fileName);

            try
            {
                // Serialize the list of players to a JSON array
                string jsonString = JsonSerializer.Serialize(players, new JsonSerializerOptions
                {
                    WriteIndented = true,
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });

                // Write the JSON content to the file
                File.WriteAllText(path, jsonString);

                Console.WriteLine($"Players data saved to {path}");
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message);
            }*/




            /*
            Console.Write("1st try (100pts): ");
            string vlaami=Console.ReadLine();
            Console.WriteLine(vlaami);
            Console.ReadKey();
            /*
            Guess guess = new Guess("PLANE", "LEAPT");
            Console.WriteLine(guess.getFeedback());
            Console.ReadKey();
            */
        }
    }
}