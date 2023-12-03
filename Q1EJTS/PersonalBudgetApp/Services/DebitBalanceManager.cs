using Q1EJTS.PersonalBudgetApp.Categories;
using Q1EJTS.PersonalBudgetApp.Data;
using Q1EJTS.PersonalBudgetApp.Transactions;

namespace Q1EJTS.PersonalBudgetApp.Services
{
    class DebitBalanceManager : IBalanceManager
    {
        private Balance _balance { get; set; }

        public DebitBalanceManager(Money initialBalance)
        {
            _balance = new Balance(initialBalance);
        }

        public Money GetCurrentBalance()
        {
            return _balance.CurrentBalance;
        }

        public void UpdateBalance(Transaction transaction)
        {
            Money change;
            if (transaction.Category == FinancialCategory.Bevetel)
            {
                change = transaction.Total;
            }
            else
            {
                change = -transaction.Total;
                CheckBalance(change);
            }            
            _balance.UpdateBalance(change);
        }
        private void CheckBalance(Money change)
        {
            if (_balance.CurrentBalance + change < 0)
            {
                throw new LowBalanceException("Elutasítva, nincs elég fedezet!");
            }
        }
    }
}
