
namespace Q1EJTS.PersonalBudgetApp.UserInterface
{
    interface IMenu
    {
        void RecordTransaction();
        Task ExecuteSerializationAsync();
        Task ExecuteDeSerializationAsync();
        void ExecuteGetCurrentBalance();
        void DisplayMainMenu();
    }
}
