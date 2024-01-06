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
        public static IEnumerable<Transaction> SortTransactionsByDate(SortingOrder order)
        {
            if (order == SortingOrder.Descending)
            {
                return TransactionManager.Transactions.OrderByDescending(t => t.Date).ToList();
            }
            return TransactionManager.Transactions.OrderBy(t => t.Date).ToList();

        }

        public static IEnumerable<Transaction> FilterTransactionByCategory(FinancialCategory financialCategory)
        {
            return TransactionManager.Transactions.Where(x => x.Category == financialCategory).ToList();
        }
        
        public static IEnumerable<Transaction> FilterTransactionsByYear(int year)
        {
            return TransactionManager.Transactions.Where(x => x.Date.Year == year).ToList();
        }

        public static Dictionary<FinancialCategory, List<Transaction>> GroupTransactionsByCategory()
        {
            Dictionary<FinancialCategory, List<Transaction>> groupByCategory = TransactionManager.Transactions.GroupBy(x => x.Category).ToDictionary(group => group.Key, group => group.ToList());
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

        public static Dictionary<string,decimal> GroupByIncomeAndOutcome()
        {
            var income = TransactionManager.Transactions.Where(x => x.Category == FinancialCategory.Income).Sum(x => x.Total.Amount);
            var outcome = TransactionManager.Transactions.Where(x => x.Category != FinancialCategory.Income).Sum(x => x.Total.Amount);
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

        public static IEnumerable<Transaction> FilterTransactionsByDateRange(DateTime startDate, DateTime endDate)
        {
            return TransactionManager.Transactions.Where(x => x.Date >= startDate && x.Date <= endDate).ToList();
        }

        public static IEnumerable<Transaction> FilterTransactionsByMoneyRange(Money startAmount, Money endAmount)
        {
            return TransactionManager.Transactions.Where(x => x.Total >= startAmount && x.Total <= endAmount).ToList();
        }

    }

    
}
