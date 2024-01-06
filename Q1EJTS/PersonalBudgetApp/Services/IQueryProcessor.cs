

namespace Q1EJTS.PersonalBudgetApp.Services
{
    interface IQueryProcessor
    {
        void ExecuteYearFilter();
        void ExecuteDateSorting();
        void ExecuteCategoryFilter();
        void ExecuteCategoryGroupBy();
        void ExecuteIncomeAndOutcomeGroupBy();
        void ExecuteDateRangeFilter();
        void ExecuteMoneyRangeFilter();
        void ManageQueries();
    }
}
