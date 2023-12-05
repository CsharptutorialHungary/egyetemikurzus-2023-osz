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
            Add("Go to the query menu",ShowQueryMenu);
            Add("Go to the add menu", ShowAddMenu);
            Add("Go to the modify menu", ShowModifyMenu);
            Add("Go to the delete menu", ShowDeleteMenu);
            Add("Exit", Exit);
        }

        private void Exit()
        {
            Console.WriteLine("Exiting application...");
            Environment.Exit(0);
        }

        private void ShowAddMenu()
        {
            AddMenu addMenu = new AddMenu(this);
            addMenu.Display();
        }

        private void ShowQueryMenu()
        {
            QueryMenu queryMenu = new QueryMenu(this);
            queryMenu.Display();
        }

        private void ShowDeleteMenu()
        {
            DeleteMenu deleteMenu = new DeleteMenu(this);
            deleteMenu.Display();
        }

        private void ShowModifyMenu()
        {
            ModifyMenu modifyMenu = new ModifyMenu(this);
            modifyMenu.Display();
        }
    }
}
