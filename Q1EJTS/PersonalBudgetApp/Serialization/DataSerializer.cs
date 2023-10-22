using PersonalBudgetApp.Transactions;
using System.Text.Json;
using System.Text.Json.Serialization;

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
                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    Converters = { new DateTimeConverter() }
                };
                await JsonSerializer.SerializeAsync(createStream, Transactions, options);
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
                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    Converters = { new DateTimeConverter() }
                };
                transactions = await JsonSerializer.DeserializeAsync<List<Transaction>>(openStream, options);
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
        private class DateTimeConverter : JsonConverter<DateTime>
        {
            public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                return DateTime.Parse(reader.GetString());
            }

            public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
            {
                writer.WriteStringValue(value.ToString("yyyy-MM-ddTHH:mm:ss"));
            }
        }
    }
}
