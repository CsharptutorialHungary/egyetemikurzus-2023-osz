using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTHEL8.Menu
{
    class MainMenu : Menu
    {
        public MainMenu() : base([], [])
        {
            Console.WriteLine("Which functionality do you want to use? \n");
            Menu queryMenu = new QueryMenu();

            AddOption("Query Data", queryMenu.Display);
            AddOption("Add Data", AddData);
            AddOption("Delete Data", DeleteData);
            AddOption("Exit", Exit);
        }

        private static void QueryData()
        {
            Console.WriteLine("Selected: Query Data");
        }

        private static void AddData()
        {
            Console.WriteLine("Selected: Add Data");
        }

        private static void DeleteData()
        {
            Console.WriteLine("Selected: Delete Data");
        }

        private static void Exit()
        {
            Console.WriteLine("Exiting the program.");
            Environment.Exit(0);
        }
    }
}
