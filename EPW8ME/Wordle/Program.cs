using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks.Sources;

namespace EWP8ME
{
    internal class Program
    {
        static void Main(string[] args)
        {
            GameController gameController = new GameController();
            gameController.startGameController();

            /*
            FileManager<Player> playerFileManager = new FileManager<Player>("Players.json");
            List<Player> players = playerFileManager.LoadData();
            for (int i = 0; i < players.Count; i++)
            {
                Console.WriteLine(players[i].name);
            }
            Player newPlayer = new Player
            {
                name = "ANONYMUS",
                score = 0,
                round = 0,
            };
            players.Add(newPlayer);
            for (int i = 0; i < players.Count; i++)
            {
                Console.WriteLine(players[i].name);
            }
            playerFileManager.SaveData(players);
            Console.ReadKey();
            */

            //FileManager<string> answerFileManager = new FileManager<string>("Answers.json");
            //List<string> answers = answerFileManager.LoadData();
            
            //for (int i = 0; i < answers.Count; i++)
            //{
            //    Console.WriteLine(answers[i]);
            //}
            /*answers.Add("PLANE");
            for (int i = 0; i < answers.Count; i++)
            {
                Console.WriteLine(answers[i]);
            }
            answerFileManager.SaveData(answers);*/
            //Console.ReadKey();
        }
    }
}