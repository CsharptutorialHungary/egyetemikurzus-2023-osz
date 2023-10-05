namespace PersonalBudgetApp.Data
{
    public record Money(decimal Amount, string Currency)
    {
        public override string? ToString()
        {
            return $"{Amount} {Currency}";
        }
    }

}