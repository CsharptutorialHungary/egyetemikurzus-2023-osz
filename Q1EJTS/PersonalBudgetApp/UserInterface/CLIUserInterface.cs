using Q1EJTS.PersonalBudgetApp.Categories;
using Q1EJTS.PersonalBudgetApp.Data;
using Q1EJTS.PersonalBudgetApp.Services;
using Q1EJTS.PersonalBudgetApp.Transactions;
using Q1EJTS.PersonalBudgetApp.Serialization;
using Q1EJTS.PersonalBudgetApp.Query;

namespace Q1EJTS.PersonalBudgetApp.UserInterface
{
    class CLIUserInterface : IUserInput, IMenu
    {
        private IBalanceManager _balanceManager = new CreditBalanceManager(new Money(0));
        private DateTime _minimumDate = new DateTime(1900, 01, 01);
        private FinancialCategory[] _availableCategories = Enum.GetValues<FinancialCategory>();
        

        public string GetFilePathFromUserInput()
        {
            Console.WriteLine("Adjon meg egy fájlnevet: ");
            string filename = Console.ReadLine()!;
            return filename;
        }
        public async Task ExecuteDeSerializationAsync()
        {
            try
            {
                string? filename = GetFilePathFromUserInput();
                var list = await DataSerializer.Deserialize(filename);
                if (list == null)
                {
                    Console.WriteLine("Hiba történt");
                }
                else
                {
                    TransactionManager.AddTransaction(list, _balanceManager);
                    Console.WriteLine("Sikeres fájlbeolvasás!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hiba történt: {ex.Message}");
            }
        }
        public async Task ExecuteSerializationAsync()
        {
            try
            {
                string filename = GetFilePathFromUserInput();
                await DataSerializer.Serialize(filename);
                Console.WriteLine("Sikeres mentés!");
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
        public DateTime GetValidDateFromUserInput()
        {
            Console.WriteLine("Dátum (yyyy-MM-dd): ");
            if (DateTime.TryParse(Console.ReadLine(), out DateTime date) && IsValidDate(date))
            {
                return date;
            }
            throw new FormatException("Érvénytelen dátum formátum vagy érvénytelen dátum!");
        }

        public Money GetMoneyAmountFromUserInput()
        {
            Console.WriteLine("Összeg: ");
            if (decimal.TryParse(Console.ReadLine(), out decimal amount))
            {
                return new Money(amount);
            }
            throw new FormatException("Érvénytelen összeg!");
        }
        public FinancialCategory GetFinancialCategoryFromUserInput()
        {
            Console.WriteLine($"Választható kategóriák: {string.Join(", ", _availableCategories)}");
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
                throw new FormatException("Érvénytelen év");
            }
        }
        public SortingOrder GetSortingOrderFromUserInput()
        {
            Console.WriteLine("Kérlek válassz rendezési sorrendet:");
            Console.WriteLine("1. Növekvő sorrend");
            Console.WriteLine("2. Csökkenő sorrend");
            Console.Write("Választás (1 vagy 2): ");
            string userInput = Console.ReadLine()!;

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
                throw new FormatException("Érvénytelen választás!");
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
            if (startMoney <= endMoney)
            {
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
                try
                {
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
                }
                catch (FormatException exp)
                {
                    Console.WriteLine(exp.Message);
                }
                catch (Exception)
                {
                    Console.WriteLine("Ismeretlen hiba történt!");
                }
                if (choice == 0)
                {
                    break;
                }
            }
        }
        public void RecordTransaction()
        {
            try
            {
                Console.WriteLine("Adjon meg egy tranzakciót:");

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
            }
            catch (LowBalanceException exp) 
            {
                Console.WriteLine(exp.Message);
            }
            catch (Exception)
            {
                Console.WriteLine($"Ismeretlen hiba történt");
            }
        }
        public void DisplayMainMenu()
        {
            Console.WriteLine("1. Tranzakció rögzítése");
            Console.WriteLine("2. Egyenleg lekérdezése");
            Console.WriteLine("3. Tranzakciók listázása");
            Console.WriteLine("4. Tranzakciók lekérdezése és szűrése");
            Console.WriteLine("5. Tranzakciók mentése fájlba");
            Console.WriteLine("6. Tranzakciók beolvasása fájlból");
            Console.WriteLine("7. Képernyő törlése");
            Console.WriteLine("0. Kilépés");
        }
        public void ExecuteGetCurrentBalance()
        {
            Console.WriteLine($"Jelenlegi egyenlege: {_balanceManager.GetCurrentBalance()}");
        }
        public async Task Run()
        {
            Console.WriteLine("Add meg a kezdő(jelenlegi) egyenlegedet!");
            decimal initialBalanceAmount;
            while (!decimal.TryParse(Console.ReadLine(), out initialBalanceAmount))
            {
                Console.WriteLine("Érvénytelen összeg. Kérjük, adjon meg egy pozitív összeget.");
            }
            _balanceManager = new CreditBalanceManager(new Money(initialBalanceAmount));
            while (true)
            {
                DisplayMainMenu();
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
                        ExecuteGetCurrentBalance();
                        break;
                    case 3:
                        TransactionManager.PrintTransaction();
                        break;
                    case 4:
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
                }
            }
        }
    }
}
