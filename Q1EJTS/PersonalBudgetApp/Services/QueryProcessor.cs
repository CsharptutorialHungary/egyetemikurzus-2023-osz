using Q1EJTS.PersonalBudgetApp.Categories;
using Q1EJTS.PersonalBudgetApp.Data;
using Q1EJTS.PersonalBudgetApp.Query;
using Q1EJTS.PersonalBudgetApp.UserInterface.Input;


namespace Q1EJTS.PersonalBudgetApp.Services
{
    class QueryProcessor : IQueryProcessor
    {
        IUserInput userInput = new UserInput();
        public void ExecuteYearFilter()
        {
            Console.WriteLine("Év? ");
            if (int.TryParse(Console.ReadLine(), out int year) && userInput.IsValidYear(year))
            {
                TransactionManager.PrintTransaction(TransactionQueries.FilterTransactionsByYear(year));
            }
            else
            {
                throw new FormatException("Érvénytelen év");
            }
        }

        public void ExecuteDateSorting()
        {
            SortingOrder order = userInput.GetSortingOrderFromUserInput();
            TransactionManager.PrintTransaction(TransactionQueries.SortTransactionsByDate(order));
        }
        public void ExecuteCategoryFilter()
        {
            FinancialCategory financialCategory = userInput.GetFinancialCategoryFromUserInput();
            TransactionManager.PrintTransaction(TransactionQueries.FilterTransactionByCategory(financialCategory));
        }
        public void ExecuteCategoryGroupBy()
        {
            TransactionQueries.GroupTransactionsByCategory();
        }
        public void ExecuteIncomeAndOutcomeGroupBy()
        {
            TransactionQueries.GroupByIncomeAndOutcome();
        }
        public void ExecuteDateRangeFilter()
        {
            DateTime startDate = userInput.GetValidDateFromUserInput();
            DateTime endDate = userInput.GetValidDateFromUserInput();
            if (startDate <= endDate)
            {
                TransactionManager.PrintTransaction(TransactionQueries.FilterTransactionsByDateRange(startDate, endDate));
            }
            else
            {
                TransactionManager.PrintTransaction(TransactionQueries.FilterTransactionsByDateRange(endDate, startDate));
            }

        }
        public void ExecuteMoneyRangeFilter()
        {
            Money startMoney = userInput.GetMoneyAmountFromUserInput();
            Money endMoney = userInput.GetMoneyAmountFromUserInput();
            if (startMoney <= endMoney)
            {
                TransactionManager.PrintTransaction(TransactionQueries.FilterTransactionsByMoneyRange(startMoney, endMoney));
            }
            else
            {
                TransactionManager.PrintTransaction(TransactionQueries.FilterTransactionsByMoneyRange(endMoney, startMoney));
            }
        }
        public void ManageQueries()
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
    }
}
