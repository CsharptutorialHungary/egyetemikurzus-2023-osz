using Q1EJTS.PersonalBudgetApp.Categories;
using Q1EJTS.PersonalBudgetApp.Data;
using Q1EJTS.PersonalBudgetApp.Transactions;

namespace Q1EJTS.PersonalBudgetApp.Services
{
    class CreditBalanceManager : IBalanceManager
    {
        private Balance _balance { get; set; }

        public CreditBalanceManager(Money initialBalance)
        {
            _balance = new Balance(initialBalance);
        }

        public Money GetCurrentBalance()
        {
            return _balance.CurrentBalance;
        }

        public void UpdateBalance(Transaction transaction)
        {
            CheckBalance(transaction.Total);
            Money change = (transaction.Category == FinancialCategory.Income) ? transaction.Total : -transaction.Total;
            _balance.UpdateBalance(change);
        }
        private void CheckBalance(Money change)
        {
            if (_balance.CurrentBalance - change < 0)
            {
                throw new LowBalanceException("Elutasítva, nincs elég fedezet!");
            }
        }
    }
}
