using Q1EJTS.PersonalBudgetApp.Data;
using Q1EJTS.PersonalBudgetApp.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Q1EJTS.PersonalBudgetApp.Services
{
    internal interface IBalanceManager
    {
        public Money GetCurrentBalance();
        void UpdateBalance(Transaction transaction);
        
    }
}
