using Newtonsoft.Json;
using Q1EJTS.PersonalBudgetApp.Categories;
using Q1EJTS.PersonalBudgetApp.Data;

namespace Q1EJTS.PersonalBudgetApp.Transactions
{
    [JsonObject]
    public class Transaction
    {
        [JsonProperty("Date")]
        public DateTime Date { get; }
        [JsonProperty("Amount")]
        public Money Total { get; }
        [JsonProperty("Category")]
        public FinancialCategory Category { get; }

        [JsonConstructor]
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

