using Q1EJTS.PersonalBudgetApp.Categories;
using Q1EJTS.PersonalBudgetApp.Data;
using Q1EJTS.PersonalBudgetApp.Query;

namespace Q1EJTS.PersonalBudgetApp.UserInterface
{
    interface IUserInput
    {
        string GetFilePathFromUserInput();
        DateTime GetValidDateFromUserInput();
        Money GetMoneyAmountFromUserInput();
        FinancialCategory GetFinancialCategoryFromUserInput();
        SortingOrder GetSortingOrderFromUserInput();
    }
}
