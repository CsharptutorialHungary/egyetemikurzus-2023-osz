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
            CurrentBalance += change;
        }
    }
}
