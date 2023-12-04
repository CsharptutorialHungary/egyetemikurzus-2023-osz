using FTHEL8.Data;
using Newtonsoft.Json;

namespace FTHEL8.Menu
{
    public class MainMenu : Menu
    {
        public MainMenu() : base()
        {
            InitializeOptions();
        }

        private void InitializeOptions()
        {
            Add("QueryMenu",ShowQueryMenu);
            Add("AddMenu", ShowAddMenu);
            Add("DeleteMenu", ShowDeleteMenu);
            Add("ModifyMenu", ShowModifyMenu);
            Add("Exit", Exit);
        }

        private void Exit()
        {
            Console.WriteLine("Exiting application...");
            Environment.Exit(0);
        }

        private void ShowAddMenu()
        {
            Console.WriteLine("AddMenu selected");
        }

        private void ShowQueryMenu()
        {
            QueryMenu querymenu = new QueryMenu(this);
            querymenu.Display();
        }

        private void ShowDeleteMenu()
        {
            Console.WriteLine("DeleteMenu selected");
        }

        private void ShowModifyMenu()
        {
            Console.WriteLine("ModifyMenu selected");
        }
    }
}
