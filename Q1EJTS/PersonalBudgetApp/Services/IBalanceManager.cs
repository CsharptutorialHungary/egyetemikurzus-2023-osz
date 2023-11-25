using Q1EJTS.PersonalBudgetApp.Data;
using Q1EJTS.PersonalBudgetApp.Transactions;

namespace Q1EJTS.PersonalBudgetApp.Services
{
    internal interface IBalanceManager
    {
        public Money GetCurrentBalance();
        void UpdateBalance(Transaction transaction);
        
    }
}
