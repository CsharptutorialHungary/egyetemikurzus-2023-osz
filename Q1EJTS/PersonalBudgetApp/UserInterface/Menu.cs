using Q1EJTS.PersonalBudgetApp.Categories;
using Q1EJTS.PersonalBudgetApp.Data;
using Q1EJTS.PersonalBudgetApp.Serialization;
using Q1EJTS.PersonalBudgetApp.Services;
using Q1EJTS.PersonalBudgetApp.Transactions;
using Q1EJTS.PersonalBudgetApp.UserInterface.Input;

namespace Q1EJTS.PersonalBudgetApp.UserInterface
{
    class Menu : IMenu
    {
        private IUserInput _userInput { get;} = new UserInput(); 
        private IBalanceManager _balanceManager { get; set; } = new DebitBalanceManager(0);
        public IUserInput UserInput { get { return _userInput; } }
        public IBalanceManager BalanceManager { get { return _balanceManager; } set { _balanceManager = value; } }
        public void DisplayMainMenu()
        {
            Console.WriteLine("1. Tranzakció rögzítése");
            Console.WriteLine("2. Egyenleg lekérdezése");
            Console.WriteLine("3. Tranzakciók listázása");
            Console.WriteLine("4. Tranzakciók lekérdezése és szűrése");
            Console.WriteLine("5. Tranzakciók mentése fájlba");
            Console.WriteLine("6. Tranzakciók beolvasása fájlból");
            Console.WriteLine("7. Képernyő törlése");
            Console.WriteLine("0. Kilépés");
        }
        public async Task ExecuteDeSerializationAsync()
        {
            try
            {
                string? filename = _userInput.GetFilePathFromUserInput();
                var list = await DataSerializer.Deserialize(filename);
                if (list == null)
                {
                    Console.WriteLine("Hiba történt");
                }
                else
                {
                    TransactionManager.AddTransaction(list, _balanceManager);
                    Console.WriteLine("Sikeres fájlbeolvasás!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hiba történt: {ex.Message}");
            }
        }
        public async Task ExecuteSerializationAsync()
        {
            try
            {
                string filename = _userInput.GetFilePathFromUserInput();
                await DataSerializer.Serialize(filename);
                Console.WriteLine("Sikeres mentés!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hiba történt: {ex.Message}");
            }
        }
        public void RecordTransaction()
        {
            try
            {
                Console.WriteLine("Adjon meg egy tranzakciót:");

                DateTime date = _userInput.GetValidDateFromUserInput();
                Money money = _userInput.GetMoneyAmountFromUserInput();
                FinancialCategory category = _userInput.GetFinancialCategoryFromUserInput();

                Transaction transaction = new Transaction(date, money, category);
                TransactionManager.AddTransaction(transaction, _balanceManager);
                Console.WriteLine("Sikeres rögzítés!");
            }
            catch (FormatException ex)
            {
                Console.WriteLine($"Hiba történt: {ex.Message}");
            }
            catch (LowBalanceException exp)
            {
                Console.WriteLine(exp.Message);
            }
            catch (Exception)
            {
                Console.WriteLine($"Ismeretlen hiba történt");
            }
        }
        
        public void ExecuteGetCurrentBalance()
        {
            Console.WriteLine($"Jelenlegi egyenlege: {_balanceManager.GetCurrentBalance()}");
        }
    }
}
