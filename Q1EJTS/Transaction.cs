using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PersonalBudgetApp.Categories;
using PersonalBudgetApp.Data;

namespace PersonalBudgetApp.Transactions
{
    public class Transaction
    {
        public DateTime Date { get; }
        public Money Amount { get; }
        public FinancialCategory Category { get; }

        public Transaction(DateTime date, Money amount, FinancialCategory category)
        {
            Date = date;
            Amount = amount;
            Category = category;
        }
    }
}
