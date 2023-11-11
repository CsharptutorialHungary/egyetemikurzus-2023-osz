using Q1EJTS.PersonalBudgetApp.Categories;
using Q1EJTS.PersonalBudgetApp.Data;
using Q1EJTS.PersonalBudgetApp.Transactions;

namespace Q1EJTS.PersonalBudgetApp.Services
{
    class BalanceManager : IBalanceManager
    {
        private Balance _balance { get; set; }

        public BalanceManager(Money initialBalance)
        {
            _balance = new Balance(initialBalance);
        }

        public Money GetCurrentBalance()
        {
            return _balance.CurrentBalance;
        }

        public void UpdateBalance(Transaction transaction)
        {
            Money change = (transaction.Category == FinancialCategory.Income) ? transaction.Total : -transaction.Total;
            _balance.UpdateBalance(change);
        }
        
    }
}
