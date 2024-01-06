
namespace Q1EJTS.PersonalBudgetApp.Data
{
    public sealed record Money(decimal Amount)
    {
        public override string ToString() => $"{Amount} HUF";

        public static implicit operator decimal(Money money) => money.Amount;

        public static Money operator -(Money money1, Money money2) => new Money(money1.Amount - money2.Amount);

        public static Money operator +(Money money1, Money money2) => new Money(money1.Amount + money2.Amount);

        public static Money operator -(Money money1) => new Money(-money1.Amount);

        public static implicit operator Money(decimal amount) => new Money(amount);

    }
}