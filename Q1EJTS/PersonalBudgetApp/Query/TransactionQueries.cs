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
        public static List<Transaction> SortTransactionsByDate(SortingOrder order)
        {
            if (order == SortingOrder.Descending)
            {
                return TransactionManager.Transaction.OrderByDescending(t => t.Date).ToList();
            }
            return TransactionManager.Transaction.OrderBy(t => t.Date).ToList();

        }

        public static List<Transaction> FilterTransactionByCategory(FinancialCategory financialCategory)
        {
            return TransactionManager.Transaction.Where(x => x.Category == financialCategory).ToList();
        }
        
        public static List<Transaction> FilterTransactionsByYear(int year)
        {
            return TransactionManager.Transaction.Where(x => x.Date.Year == year).ToList();
        }

        public static Dictionary<FinancialCategory, List<Transaction>> GroupTransactionsByCategory()
        {
            Dictionary<FinancialCategory, List<Transaction>> groupByCategory = TransactionManager.Transaction.GroupBy(x => x.Category).ToDictionary(group => group.Key, group => group.ToList());
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
            var income = TransactionManager.Transaction.Where(x => x.Category == FinancialCategory.Bevetel).Sum(x => x.Total.Amount);
            var outcome = TransactionManager.Transaction.Where(x => x.Category != FinancialCategory.Bevetel).Sum(x => x.Total.Amount);
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

        public static List<Transaction> FilterTransactionsByDateRange(DateTime startDate, DateTime endDate)
        {
            return TransactionManager.Transaction.Where(x => x.Date >= startDate && x.Date <= endDate).ToList();
        }

        public static List<Transaction> FilterTransactionsByMoneyRange(Money startAmount, Money endAmount)
        {
            return TransactionManager.Transaction.Where(x => x.Total >= startAmount && x.Total <= endAmount).ToList();
        }

    }

    
}
