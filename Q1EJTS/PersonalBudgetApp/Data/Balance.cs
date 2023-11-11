using Q1EJTS.PersonalBudgetApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Q1EJTS.PersonalBudgetApp.Data
{
    public class Balance
    {
        public Money CurrentBalance { get; private set; }

        public Balance(Money initialBalance)
        {
            CurrentBalance = initialBalance;
        }
        public void UpdateBalance(Money change)
        {
            CheckBalance(change);
            CurrentBalance += change;
        }

        private void CheckBalance(Money change)
        {
            if (CurrentBalance + change < 0)
            {
                throw new LowBalanceException("Az egyenleg nem lehet negatív");
            }
        }
    }
}
