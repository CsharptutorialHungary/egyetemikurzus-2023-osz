using System.Numerics;

namespace Q1EJTS.PersonalBudgetApp.Data
{
    public record Money(decimal Amount)
    {
        public override string? ToString()
        {
            return $"{Amount} HUF";
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public static implicit operator decimal(Money money)
        {
            return money.Amount;
        }

        public static Money operator -(Money money1, Money money2)
        {
            decimal resultAmount = money1.Amount - money2.Amount;
            return new Money(resultAmount);
        }

        public static Money operator +(Money money1, Money money2)
        {
            decimal resultAmount = money1.Amount + money2.Amount;
            return new Money(resultAmount);
        }
        public static Money operator -(Money money1)
        {
            return new Money(-money1.Amount);
        }

    }

}