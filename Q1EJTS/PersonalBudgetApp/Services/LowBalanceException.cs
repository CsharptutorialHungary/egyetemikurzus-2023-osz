namespace Q1EJTS.PersonalBudgetApp.Services
{
    class LowBalanceException : Exception
    {
        public LowBalanceException(string message) : base(message) {  }
    }
}
