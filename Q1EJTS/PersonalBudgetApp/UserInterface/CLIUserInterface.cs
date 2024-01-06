using Q1EJTS.PersonalBudgetApp.Data;
using Q1EJTS.PersonalBudgetApp.Services;

namespace Q1EJTS.PersonalBudgetApp.UserInterface
{
    class CLIUserInterface
    {
        private IQueryProcessor _queryProcessor = new QueryProcessor();
        private Menu menu = new Menu();
        public async Task Run()
        {
            Console.WriteLine("Add meg a kezdő(jelenlegi) egyenlegedet!");
            decimal initialBalanceAmount;
            while (!decimal.TryParse(Console.ReadLine(), out initialBalanceAmount))
            {
                Console.WriteLine("Érvénytelen összeg. Kérjük, adjon meg egy pozitív összeget.");
            }
            menu.BalanceManager = new DebitBalanceManager(initialBalanceAmount);
            while (true)
            {
                menu.DisplayMainMenu();
                Console.Write("Válassz egy műveletet: ");
                int choice;
                int.TryParse(Console.ReadLine(), out choice);
                switch (choice)
                {
                    case 0:
                        Environment.Exit(0);
                        break;
                    case 1:
                        menu.RecordTransaction();
                        break;
                    case 2:
                        menu.ExecuteGetCurrentBalance();
                        break;
                    case 3:
                        TransactionManager.PrintTransaction();
                        break;
                    case 4:
                        _queryProcessor.ManageQueries();
                        break;
                    case 5:
                        await menu.ExecuteSerializationAsync();
                        break;
                    case 6:
                        await menu.ExecuteDeSerializationAsync();
                        break;
                    case 7:
                        Console.Clear();
                        break;
                    default: Console.WriteLine("Érvénytelen művelet!"); break;
                }
            }
        }
    }
}
