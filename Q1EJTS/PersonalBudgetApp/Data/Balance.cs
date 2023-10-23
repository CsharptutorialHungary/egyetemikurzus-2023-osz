using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalBudgetApp.Data
{
    class Balance
    {
        private Money _currentBalance;
        private static Balance? instance;
        public static Balance Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Balance();
                }
                return instance;
            }
        }
        private Balance()
        {
            _currentBalance = new Money(0);
            
        }

        public void AddMoneyToCurrentBalance(Money money)
        {
            this._currentBalance = new Money(this._currentBalance.Amount + money.Amount);
        }
        public void SubtractMoneyFromCurrentBalance(Money money)
        {
            this._currentBalance = new Money(this._currentBalance.Amount - money.Amount);
        }

    }
}
