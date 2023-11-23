using System.Text.Json;
using System;
using System.IO;
using System.Collections.Generic;


namespace EWP8ME
{    
    internal class Program
    {
        static void Main(string[] args)
        {
            GameController gameController = new GameController();
            gameController.AddNewWord("asdss");

            

            //Console.WriteLine(gameController.GetAnAnswer());


            Console.WriteLine("Ezt nem kéne látni");
            gameController.startGameController();
        }
    }
}