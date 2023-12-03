using FTHEL8.Menu;

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
