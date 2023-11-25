using Q1EJTS.PersonalBudgetApp.Transactions;

namespace Q1EJTS.PersonalBudgetApp.Services
{
    static class TransactionManager
    {
        private static List<Transaction> _transactions = new();
        public static List<Transaction> Transaction
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
        public static void AddTransaction(List<Transaction> transactions, IBalanceManager balanceManager)
        {
            foreach (Transaction transaction in transactions)
            {
                AddTransaction(transaction, balanceManager);
            }
        }

        public static void PrintTransaction(List<Transaction> transactions)
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