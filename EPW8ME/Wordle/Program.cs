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
        }
    }
}