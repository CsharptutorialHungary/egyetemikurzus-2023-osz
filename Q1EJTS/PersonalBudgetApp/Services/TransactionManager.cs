using PersonalBudgetApp.Transactions;

namespace PersonalBudgetApp.Services
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
        public static void AddTransaction(Transaction transaction)
        {
            _transactions.Add(transaction);
        }
        public static void RemoveTransaction(Transaction transaction)
        {
            _transactions.Remove(transaction);
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