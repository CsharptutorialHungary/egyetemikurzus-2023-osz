using System.Data.SQLite;
using System.Diagnostics;
using FTHEL8.Data;
using FTHEL8.Menu;
using FTHEL8.Models;

namespace FTHEL8
{
    class Program
    {
        static void Main()
        {
            MainMenu mainMenu = new MainMenu();

            while (true)
            {
                mainMenu.Display();
            }
        }
    }
}
