using Q1EJTS.PersonalBudgetApp.Categories;
using Q1EJTS.PersonalBudgetApp.Data;
using Q1EJTS.PersonalBudgetApp.Services;
using Q1EJTS.PersonalBudgetApp.Transactions;

namespace Q1EJTS.PersonalBudgetApp.Query
{
    public enum SortingOrder
    {
        Ascending,
        Descending
    }
    static class TransactionQueries
    {
        public static List<Transaction> SortTransactionsByDate(SortingOrder order, List<Transaction> transactions)
        {
            if (order == SortingOrder.Descending)
            {
                return transactions.OrderByDescending(t => t.Date).ToList();
            }
            return transactions.OrderBy(t => t.Date).ToList();

        }

        public static List<Transaction> FilterTransactionByCategory(FinancialCategory financialCategory, List<Transaction> transactions)
        {
            return transactions.Where(x => x.Category == financialCategory).ToList();
        }

        public static List<Transaction> FilterTransactionsByMonth(int numberOfMonth, List<Transaction> transactions)
        {
            if (numberOfMonth > 12 || numberOfMonth < 1)
            {
                throw new ArgumentException("Érvénytelen hónap!");
            }
            return transactions.Where(x => x.Date.Month == numberOfMonth).ToList();
        }

        public static List<Transaction> FilterTransactionsByYear(int year, List<Transaction> transactions)
        {
            return transactions.Where(x => x.Date.Year == year).ToList();
        }

        public static Dictionary<FinancialCategory, List<Transaction>> GroupTransactionsByCategory(List<Transaction> transactions)
        {
            Dictionary<FinancialCategory, List<Transaction>> groupByCategory = transactions.GroupBy(x => x.Category).ToDictionary(group => group.Key, group => group.ToList());
            foreach (KeyValuePair<FinancialCategory, List<Transaction>> entry in groupByCategory)
            {
                Console.WriteLine(entry.Key + ":");
                foreach (Transaction transactionEntry in entry.Value)
                {
                    Console.WriteLine(transactionEntry);
                }
            }
            return groupByCategory;
        }

        public static Dictionary<string,decimal> GroupByIncomeAndOutcomeSum(List<Transaction> transactions)
        {
            var income = transactions.Where(x => x.Category == FinancialCategory.Income).Sum(x => x.Total.Amount);
            var outcome = transactions.Where(x => x.Category != FinancialCategory.Income).Sum(x => x.Total.Amount);
            Dictionary<string, decimal> groupByIncomeAndOutcome = new Dictionary<string, decimal>();
            groupByIncomeAndOutcome.Add("Income", income);
            groupByIncomeAndOutcome.Add("Outcome", outcome);
            foreach (KeyValuePair<string, decimal> entry in groupByIncomeAndOutcome)
            {
                Console.WriteLine(entry.Key + ":");
                Console.WriteLine("\t"+entry.Value);
            }
                return groupByIncomeAndOutcome;
        }

        public static List<Transaction> FilterTransactionsByDateRange(DateTime startDate, DateTime endDate, List<Transaction> transactions)
        {
            if (endDate < startDate)
            {
                throw new ArgumentException("A végdátum korábban van mint a kezdődátum!");
            }
            return transactions.Where(x => x.Date >= startDate && x.Date <= endDate).ToList();
        }

        public static List<Transaction> FilterTransactionsByMoneyRange(int startAmount, int endAmount, List<Transaction> transactions)
        {
            return transactions.Where(x => x.Total.Amount >= startAmount && x.Total.Amount <= endAmount).ToList();
        }

    }

    
}
