namespace FTHEL8.Menu
{
    public class MainMenu : Menu
    {
        public MainMenu() : base([], [])
        {
            Console.WriteLine();
            Console.Write("Which functionality do you want to use?");
            Menu queryMenu = new QueryMenu();
            Menu deleteMenu = new DeleteMenu();
            Menu addMenu = new AddMenu();
            Menu modifyMenu = new ModifyMenu();

            AddOption("Query Data", queryMenu.Display);
            AddOption("Add Data", addMenu.Display);
            AddOption("Modify Data", modifyMenu.Display);
            AddOption("Delete Data", deleteMenu.Display);
            AddOption("Exit", Exit);
        }

        private void AddData()
        {
            Console.WriteLine("Selected: Add Data");
        }

        private void Exit()
        {
            Console.WriteLine("Exiting the program.");
            Environment.Exit(0);
        }
    }
}
