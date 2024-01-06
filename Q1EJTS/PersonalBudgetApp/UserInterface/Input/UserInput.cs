using Q1EJTS.PersonalBudgetApp.Categories;
using Q1EJTS.PersonalBudgetApp.Data;
using Q1EJTS.PersonalBudgetApp.Query;
using Q1EJTS.PersonalBudgetApp.Services;

namespace Q1EJTS.PersonalBudgetApp.UserInterface.Input
{
    internal class UserInput : IUserInput
    {
        private DateTime _minimumDate = new DateTime(1900, 01, 01);
        private FinancialCategory[] _availableCategories = Enum.GetValues<FinancialCategory>();

        public string GetFilePathFromUserInput()
        {
            Console.WriteLine("Adjon meg egy fájlnevet: ");
            string filename = Console.ReadLine()!;
            return filename;
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
        public bool IsValidDate(DateTime date)
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
                return amount;
            }
            throw new FormatException("Érvénytelen összeg!");
        }
        public FinancialCategory GetFinancialCategoryFromUserInput()
        {
            Console.WriteLine($"Választható kategóriák: {string.Join(", ", _availableCategories)}");
            Console.Write("Kategória: ");
            string? input = Console.ReadLine();

            if (Enum.TryParse(input, true, out FinancialCategory category) && Array.Exists(_availableCategories, c => c == category))
            {
                return category;
            }
            else
            {
                return FinancialCategory.Other;
            }
        }
        public bool IsValidYear(int year)
        {
            int currentYear = DateTime.Now.Year;
            return year >= _minimumDate.Year && year <= currentYear;
        }

    }
}
