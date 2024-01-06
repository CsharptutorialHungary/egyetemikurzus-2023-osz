using Q1EJTS.PersonalBudgetApp.Transactions;

namespace Q1EJTS.PersonalBudgetApp.Services
{
    static class TransactionManager
    {
        private static List<Transaction> _transactions = new();
        public static List<Transaction> Transactions
        {
            get { return _transactions; }
        }
        public static void ClearTransactions()
        {
            _transactions = new List<Transaction>();
        }
        public static void AddTransaction(Transaction transaction, IBalanceManager balanceManager)
        {
            balanceManager.UpdateBalance(transaction);
            _transactions.Add(transaction);
        }
        public static void AddTransaction(IEnumerable<Transaction> transactions, IBalanceManager balanceManager)
        {
            foreach (Transaction transaction in transactions)
            {
                AddTransaction(transaction, balanceManager);
            }
        }

        public static void PrintTransaction(IEnumerable<Transaction> transactions)
        {
            foreach (Transaction transaction in transactions)
            {
                Console.WriteLine(transaction);
            }
        }
        public static void PrintTransaction()
        {
            foreach (Transaction transaction in _transactions)
            {
                Console.WriteLine(transaction);
            }
        }

    }
}