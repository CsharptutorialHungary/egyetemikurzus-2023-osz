using Q1EJTS.PersonalBudgetApp.Categories;
using Q1EJTS.PersonalBudgetApp.Data;
using System.Text.Json.Serialization;

namespace Q1EJTS.PersonalBudgetApp.Transactions
{
    public class Transaction
    {
        [JsonPropertyName("Date")]
        public DateTime Date { get; }
        [JsonPropertyName("Money")]
        public Money Total { get; }
        [JsonPropertyName("Category")]
        public FinancialCategory Category { get; }

        public Transaction(DateTime date, Money amount, FinancialCategory category)
        {
            Date = date;
            Total = amount;
            Category = category;
        }

        public override string? ToString()
        {
            return $"Tranzakció napja: {Date.Year}.{Date.Month}.{Date.Day}, költsége: {Total}, kategóriája: {Category}";
        }

    }
}

