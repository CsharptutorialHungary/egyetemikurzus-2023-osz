using Q1EJTS.PersonalBudgetApp.Services;

namespace Q1EJTS.PersonalBudgetApp.Data
{
    public class Balance
    {
        private Money _currentBalance;
        public Money CurrentBalance 
        {   get { return _currentBalance; }
            private set
            {
                if (value < 0)
                {
                    _currentBalance = new Money(0);
                }
                else
                {
                    _currentBalance = value;
                }
            }
        }

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
