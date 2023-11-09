using Q1EJTS.PersonalBudgetApp.Services;
using Q1EJTS.PersonalBudgetApp.Transactions;
using Newtonsoft.Json;

namespace Q1EJTS.PersonalBudgetApp.Serialization
{
    internal class DataSerializer
    {
        public static async Task Serialize(string filename)
        {
            CheckJson(filename);
            string json = JsonConvert.SerializeObject(TransactionManager.Transaction, Formatting.Indented);
            try
            {
                await File.WriteAllTextAsync(filename, json);
            }
            catch (IOException exp)
            {
                Console.WriteLine($"Hiba a szerializálás közben: {exp.Message}");
            }
        }

        public static async Task<List<Transaction>?> Deserialize(string filename)
        {
            CheckJson(filename);
            try
            {
                string json = await File.ReadAllTextAsync(filename);
                List<Transaction>? transactions = JsonConvert.DeserializeObject<List<Transaction>>(json);
                return transactions;
            }
            catch (IOException exp)
            {
                Console.WriteLine($"Hiba történt {exp.Message}");
                return null;
            }
        }


        private static void CheckJson(string filename)
        {
            if (!filename.EndsWith("json"))
            {
                Console.WriteLine("A fájlnak json kiterjesztésűnek kell lennie!");
                return;
            }
        }

    }
}
