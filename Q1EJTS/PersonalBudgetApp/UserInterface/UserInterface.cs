using Q1EJTS.PersonalBudgetApp.Categories;
using Q1EJTS.PersonalBudgetApp.Data;
using Q1EJTS.PersonalBudgetApp.Services;
using Q1EJTS.PersonalBudgetApp.Transactions;
using Q1EJTS.PersonalBudgetApp.Serialization;
using Q1EJTS.PersonalBudgetApp.Query;

namespace Q1EJTS.PersonalBudgetApp.UserInterface
{
    internal class UserInterface
    {
        private BalanceManager _balanceManager = new BalanceManager(new Money(0));
        private void RecordTransaction()
        {
            try
            {
                Console.WriteLine("Adjon meg egy tranzakciót:");
                Console.WriteLine("Dátum (yyyy-MM-dd): ");
                if (!DateTime.TryParse(Console.ReadLine(), out DateTime date))
                {
                    throw new FormatException("Érvénytelen dátum formátum.");
                }
                Console.WriteLine("Összeg: ");
                if (!decimal.TryParse(Console.ReadLine(), out decimal amount))
                {
                    throw new FormatException("Érvénytelen összeg formátum.");
                }
                Console.Write("Kategória (Income, Food, Rent, Utilities, Transportation, Entertainment, Health, Other): ");
                if (Enum.TryParse<FinancialCategory>(Console.ReadLine(), out FinancialCategory category))
                {
                    Transaction transaction = new Transaction(date, new Money(amount), category);
                    TransactionManager.AddTransaction(transaction, _balanceManager);
                    Console.WriteLine("Sikeres rögzítés!");
                }
                else
                {
                    Transaction transaction = new Transaction(date, new Money(amount), FinancialCategory.Other);
                    TransactionManager.AddTransaction(transaction, _balanceManager);
                    Console.WriteLine("Sikeres rögzítés!");
                }
            }catch (FormatException ex)
            {
                Console.WriteLine($"Hiba: {ex.Message}");

            } catch (Exception ex)
            {
                Console.WriteLine($"Ismeretlen hiba történt: {ex.Message}");
            }
        }
        public void Run()
        {
            Console.WriteLine("Add meg a kezdő(jelenlegi) egyenlegedet!");
            decimal initialBalanceAmount;
            while (!decimal.TryParse(Console.ReadLine(), out initialBalanceAmount) || initialBalanceAmount < 0)
            {
                Console.WriteLine("Érvénytelen összeg. Kérjük, adjon meg egy pozitív összeget.");
            }
            _balanceManager = new BalanceManager(new Money(initialBalanceAmount));
            while (true)
            {
                Console.WriteLine("1. Tranzakció rögzítése");
                Console.WriteLine("2. Egyenleg lekérdezése");
                Console.WriteLine("3. Tranzakciók listázása");
                Console.WriteLine("4. Tranzakciók lekérdezése és szűrése");
                Console.WriteLine("5. Képernyő törlése");
                Console.WriteLine("0. Kilépés");
                Console.Write("Válassz egy műveletet: ");
                int choice;
                int.TryParse(Console.ReadLine(), out choice);
                switch (choice)
                {
                    case 0:
                        Environment.Exit(0);
                        break;
                    case 1:
                        RecordTransaction();
                        break;
                    case 2:
                        Console.WriteLine($"Jelenlegi egyenlege: {_balanceManager.GetCurrentBalance()}");
                        break;
                    case 3:
                        TransactionManager.PrintTransaction();
                        break;
                    case 4:
                        break;
                    case 5:
                        Console.Clear();
                        break;
                    default: Console.WriteLine("Érvénytelen művelet!"); break;


                }
            }
        }
        public async static Task Main()
        {
            UserInterface userInterface = new UserInterface();
            userInterface.Run();
            /*
            Task task = new Task(() => userInterface.Run());
            task.Start();
            Console.WriteLine("Adjon meg tranzakciókat: ");
            Transaction transaction = new(new DateTime(2023, 09, 17), new Money(23_000), FinancialCategory.Entertainment);
            Transaction transaction2 = new(new DateTime(2023, 09, 17), new Money(200_000), FinancialCategory.Income);
            TransactionManager.AddTransaction(transaction);
            TransactionManager.AddTransaction(transaction2);
            _ = DataSerializer.Serialization("teszt.json", TransactionManager.Transaction);

            List<Transaction> deserializedTransactions = await DataSerializer.DeSerialization("teszt.json");
            TransactionManager.PrintTransaction(deserializedTransactions);

            TransactionManager.PrintTransaction(TransactionQueries.FilterTransactionsByYear(2020, TransactionManager.Transaction));
            */
        }

    }
}
