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
        static async Task Main(string[] args)
        {
            GameController gameController = new GameController();
            await gameController.startGameController();

            //FileManager<string> filem = new FileManager<string>("Answers.json");
            //List<string> valaszonk = await filem.LoadDataAsync();
            //Console.Write("valaszok betoltve");
            //foreach (string val in valaszonk) {  Console.WriteLine(val); }
            //Console.ReadKey();
        }
    }
}