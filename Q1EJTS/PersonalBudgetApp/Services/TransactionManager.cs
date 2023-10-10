using PersonalBudgetApp.Transactions;

namespace PersonalBudgetApp.Services
{
    internal class TransactionManager
    {
        private static IEnumerable<Transaction> Transactions = new List<Transaction>();

        public IEnumerable<Transaction> Transaction
        {
            get { return Transactions; }
        }
        public void ClearTransactions()
        {
            Transactions = new List<Transaction>();
        }

    }
}