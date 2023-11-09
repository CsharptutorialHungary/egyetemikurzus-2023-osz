using Q1EJTS.PersonalBudgetApp.Categories;
using Q1EJTS.PersonalBudgetApp.Data;
using Q1EJTS.PersonalBudgetApp.Services;
using Q1EJTS.PersonalBudgetApp.Transactions;
using Q1EJTS.PersonalBudgetApp.Serialization;
using Q1EJTS.PersonalBudgetApp.Query;
<<<<<<< HEAD
=======
>>>>>>> 69207ff4c547273c1460738d56014da3331818e2

namespace Q1EJTS.PersonalBudgetApp.UserInterface
{
    internal class UserInterface
    {
        private BalanceManager _balanceManager = new BalanceManager(new Money(0));
<<<<<<< HEAD
        private DateTime _minimumDate = new DateTime(1900, 01, 01);
        private FinancialCategory[] _availableCategories =
        {
            FinancialCategory.Income,
            FinancialCategory.Food,
            FinancialCategory.Transportation,
            FinancialCategory.Rent,
            FinancialCategory.Utilities,
            FinancialCategory.Entertainment,
            FinancialCategory.Health,
            FinancialCategory.Other
        };

        private string GetFilePathFromUserInputAsync()
        {
            Console.WriteLine("Adjon meg egy fájlnevet: ");
            string filename = Console.ReadLine();
            return filename;
        }
        private async Task ExecuteDeSerializationAsync()
        {
            
            try
            {
                string filename = GetFilePathFromUserInputAsync();
                var list = await DataSerializer.Deserialize(filename);
                if (list == null)
                {
                    Console.WriteLine("Hiba történt");
                }
                else
                {
                    TransactionManager.AddTransaction(list, _balanceManager);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hiba történt: {ex.Message}");
            }
        }
        private async Task ExecuteSerializationAsync()
        {
            try
            {
                string filename = GetFilePathFromUserInputAsync();
                await DataSerializer.Serialize(filename);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hiba történt: {ex.Message}");
            }
        }

        
        private bool IsValidDate(DateTime date)
        {
            DateTime now = DateTime.Now;
            return date <= now && date >= _minimumDate;
        }
        private DateTime GetValidDateFromUserInput()
        {
            while (true)
            {
                Console.WriteLine("Dátum (yyyy-MM-dd): ");
                if (DateTime.TryParse(Console.ReadLine(), out DateTime date) && IsValidDate(date))
                {
                    return date;
                }
                Console.WriteLine("Érvénytelen dátum formátum vagy érvénytelen dátum. Kérlek próbáld újra.");
            }
        }

        private Money GetMoneyAmountFromUserInput()
        {
            while (true)
            {
                Console.WriteLine("Összeg: ");
                if (decimal.TryParse(Console.ReadLine(), out decimal amount))
                {
                    return new Money(amount);
                }
                Console.WriteLine("Érvénytelen összeg, kérlek próbáld újra.");
            }
        }
        private FinancialCategory GetFinancialCategoryFromUserInput()
        {
            Console.WriteLine("Választható kategóriák: Income, Food, Rent, Utilities, Transportation, Entertainment, Health, Other");
            Console.Write("Kategória: ");
            string? input = Console.ReadLine();

            if (Enum.TryParse<FinancialCategory>(input, true, out FinancialCategory category) && Array.Exists(_availableCategories, c => c == category))
            {
                return category;
            }
            else
            {
                return FinancialCategory.Other;
            }
        }
        private bool IsValidYear(int year)
        {
            int currentYear = DateTime.Now.Year;
            return year >= _minimumDate.Year && year <= currentYear;
        }

        private void ExecuteYearFilter()
        {
            Console.WriteLine("Év? ");
            if (int.TryParse(Console.ReadLine(), out int year) && IsValidYear(year))
            {
                TransactionManager.PrintTransaction(TransactionQueries.FilterTransactionsByYear(year));
            }
            else
            {
                Console.WriteLine("Érvénytelen év!");
            }
        }
        public SortingOrder GetSortingOrderFromUserInput()
        {
            Console.WriteLine("Kérlek válassz rendezési sorrendet:");
            Console.WriteLine("1. Növekvő sorrend");
            Console.WriteLine("2. Csökkenő sorrend");

            while (true)
            {
                Console.Write("Választás (1 vagy 2): ");
                string userInput = Console.ReadLine();

                if (userInput == "1")
                {
                    return SortingOrder.Ascending;
                }
                else if (userInput == "2")
                {
                    return SortingOrder.Descending;
                }
                else
                {
                    Console.WriteLine("Érvénytelen választás. Kérlek válassz 1 vagy 2.");
                }
            }
        }
        private void ExecuteDateSorting()
        {
            SortingOrder order = GetSortingOrderFromUserInput();
            TransactionManager.PrintTransaction(TransactionQueries.SortTransactionsByDate(order));
        }
        private void ExecuteCategoryFilter()
        {
            FinancialCategory financialCategory = GetFinancialCategoryFromUserInput();
            TransactionManager.PrintTransaction(TransactionQueries.FilterTransactionByCategory(financialCategory));
        }
        private void ExecuteCategoryGroupBy()
        {
            TransactionQueries.GroupTransactionsByCategory();
        }
        private void ExecuteIncomeAndOutcomeGroupBy()
        {
            TransactionQueries.GroupByIncomeAndOutcome();
        }
        private void ExecuteDateRangeFilter()
        {
            DateTime startDate = GetValidDateFromUserInput();
            DateTime endDate = GetValidDateFromUserInput();
            if (startDate <= endDate)
            {
                TransactionManager.PrintTransaction(TransactionQueries.FilterTransactionsByDateRange(startDate, endDate));
            }
            else
            {
                TransactionManager.PrintTransaction(TransactionQueries.FilterTransactionsByDateRange(endDate, startDate));
            }

        }
        private void ExecuteMoneyRangeFilter()
        {
            Money startMoney = GetMoneyAmountFromUserInput();
            Money endMoney = GetMoneyAmountFromUserInput();
            if (startMoney <= endMoney) {
                TransactionManager.PrintTransaction(TransactionQueries.FilterTransactionsByMoneyRange(startMoney, endMoney));
            }
            else
            {
                TransactionManager.PrintTransaction(TransactionQueries.FilterTransactionsByMoneyRange(endMoney, startMoney));
            }
        }
        private void ManageQueries()
        {
            while (true)
            {
                Console.WriteLine("1. Tranzakciók dátum szerint");
                Console.WriteLine("2. Tranzakciók szűrése kategória szerint");
                Console.WriteLine("3. Tranzakciók szűrése év szerint");
                Console.WriteLine("4. Tranzakciók csoportosítása kategóriák szerint");
                Console.WriteLine("5. Tranzakciók csoportosítása bevételek és kiadások szerint");
                Console.WriteLine("6. Tranzakciók szűrése dátum intervallum szerint");
                Console.WriteLine("7. Tranzakciók szűrése pénz intervallum szerint");
                Console.WriteLine("0. Vissza");
                Console.Write("Válassz egy műveletet: ");
                int choice;
                int.TryParse(Console.ReadLine(), out choice);
                switch (choice)
                {
                    case 1:
                        ExecuteDateSorting();
                        break;
                    case 2:
                        ExecuteCategoryFilter();
                        break;
                    case 3:
                        ExecuteYearFilter();
                        break;
                    case 4:
                        ExecuteCategoryGroupBy();
                        break;
                    case 5:
                        ExecuteIncomeAndOutcomeGroupBy();
                        break;
                    case 6:
                        ExecuteDateRangeFilter();
                        break;
                    case 7:
                        ExecuteMoneyRangeFilter();
                        break;

                }
                if (choice == 0)
                {
                    break;
                }
            }
        }
=======
>>>>>>> 69207ff4c547273c1460738d56014da3331818e2
        private void RecordTransaction()
        {
            try
            {
                Console.WriteLine("Adjon meg egy tranzakciót:");
<<<<<<< HEAD
                
                DateTime date = GetValidDateFromUserInput();
                Money money = GetMoneyAmountFromUserInput();
                FinancialCategory category = GetFinancialCategoryFromUserInput();

                Transaction transaction = new Transaction(date, money, category);
                TransactionManager.AddTransaction(transaction, _balanceManager);
                Console.WriteLine("Sikeres rögzítés!");
            }
            catch (FormatException ex)
            {
                Console.WriteLine($"Hiba történt: {ex.Message}");
=======
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
>>>>>>> 69207ff4c547273c1460738d56014da3331818e2

            } catch (Exception ex)
            {
                Console.WriteLine($"Ismeretlen hiba történt: {ex.Message}");
            }
        }
<<<<<<< HEAD
        public async Task Run()
        {
            Console.WriteLine("Add meg a kezdő(jelenlegi) egyenlegedet!");
            decimal initialBalanceAmount;
            while (!decimal.TryParse(Console.ReadLine(), out initialBalanceAmount) || initialBalanceAmount <= 0)
=======
        public void Run()
        {
            Console.WriteLine("Add meg a kezdő(jelenlegi) egyenlegedet!");
            decimal initialBalanceAmount;
            while (!decimal.TryParse(Console.ReadLine(), out initialBalanceAmount) || initialBalanceAmount < 0)
>>>>>>> 69207ff4c547273c1460738d56014da3331818e2
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
<<<<<<< HEAD
                Console.WriteLine("5. Tranzakciók mentése fájlba");
                Console.WriteLine("6. Tranzakciók beolvasása fájlból");
                Console.WriteLine("7. Képernyő törlése");
=======
                Console.WriteLine("5. Képernyő törlése");
>>>>>>> 69207ff4c547273c1460738d56014da3331818e2
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
<<<<<<< HEAD
                        ManageQueries();
                        break;
                    case 5:
                        await ExecuteSerializationAsync(); 
                        break;
                    case 6:
                        await ExecuteDeSerializationAsync();
                        break;
                    case 7:
                        Console.Clear();
                        break;
                    default: Console.WriteLine("Érvénytelen művelet!"); break;
=======
                        break;
                    case 5:
                        Console.Clear();
                        break;
                    default: Console.WriteLine("Érvénytelen művelet!"); break;


>>>>>>> 69207ff4c547273c1460738d56014da3331818e2
                }
            }
        }
        public async static Task Main()
        {
<<<<<<< HEAD
            await new UserInterface().Run();
=======
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
>>>>>>> 69207ff4c547273c1460738d56014da3331818e2
        }

    }
}
