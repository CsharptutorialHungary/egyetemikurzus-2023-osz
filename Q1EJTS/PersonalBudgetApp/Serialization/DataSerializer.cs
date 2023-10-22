using PersonalBudgetApp.Transactions;
using System.Text.Json;

namespace PersonalBudgetApp.Serialization
{
    internal class DataSerializer
    { 
        public static async Task Serialization(string filename, List<Transaction> Transactions)
        {
            CheckJson(filename);
            try
            {
                using FileStream createStream = File.Create(filename);
                await JsonSerializer.SerializeAsync(createStream, Transactions);
                await createStream.DisposeAsync();
            }
            catch (IOException exp)
            {
                Console.WriteLine($"Hiba a szerializálás közben: {exp.Message}");
            }
            catch (JsonException jsonExp)
            {
                Console.WriteLine($"Hiba a JSON szerializálás közben: {jsonExp.Message}");
            }
        }

        public static async Task<List<Transaction>> DeSerialization(string filename)
        {
            CheckJson(filename);
            List<Transaction>? transactions = new List<Transaction>();

            try
            {
                using FileStream openStream = File.OpenRead(filename);
                transactions = await JsonSerializer.DeserializeAsync<List<Transaction>>(openStream);
            }
            catch (IOException exp)
            {
                Console.WriteLine($"Hiba a deszerializálás közben: {exp.Message}");
            }

            return transactions;
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
