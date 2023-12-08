using IdezetGenerator.Modell;
using Newtonsoft.Json;
using System.Text;

namespace IdezetGeneratorNS
{
    public class Program
    {
        public static async Task Main()
        {
            try
            {
                var quotes = await GetQuote("idezetek.json");

                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write("Szia! Hogy hívnak:");
                Console.ResetColor();
                string name = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(name))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("Kérlek, adj meg egy érvényes nevet!");
                    Console.ResetColor();
                    name = Console.ReadLine();
                }

                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write("Szia " + name + "! Szeretnél egy új idézetet létrehozni?(igen/nem)");
                Console.ResetColor();
                string wantANewQuote = Console.ReadLine()?.ToLower();
                if (wantANewQuote == "igen")
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write("Kérlek, add meg az új idézet hangulatát: ");
                    Console.ResetColor();
                    string newMood = Console.ReadLine()?.ToLower();
                    while (string.IsNullOrWhiteSpace(newMood) || !existingMood(newMood))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("Kérlek, adj meg egy érvényes hangulatot!");
                        Console.ResetColor();
                        newMood = Console.ReadLine()?.ToLower();
                    }

                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write("Kérlek, írd be az új idézet szövegét: ");
                    Console.ResetColor();
                    string newText = Console.ReadLine();

                    while (string.IsNullOrWhiteSpace(newText))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("Kérlek, adj meg egy tényleges idézetet!");
                        Console.ResetColor();
                        newText = Console.ReadLine()?.ToLower();
                    }

                    quotes.Add(new Quote(newMood, newText));
                    await saveQuotes("idezetek.json", quotes);
                }


                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write("Milyen kedved van ma?(boldog, szomorú, mérges, unalom, irigy, ideges, semleges):");
                Console.ResetColor();

                string mood = Console.ReadLine()?.ToLower();

                if (string.IsNullOrWhiteSpace(mood) || !existingMood(mood))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("Kérlek, adj meg egy érvényes hangulatot!");
                    Console.ResetColor();
                    mood = Console.ReadLine()?.ToLower();
                }

                var res = quotes.Where(q => q.mood == mood).OrderBy(q => q.text);
                if (res.Any())
                {
                    Random random = new Random();
                    var randomQuote = res.ElementAt(random.Next(res.Count()));
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine("Az idézet: " + randomQuote.text);
                    Console.ResetColor();
                }

                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write("Szeretnél még egy idézetet? (igen/nem): ");
                Console.ResetColor();
                string wantAnotherQuote = Console.ReadLine()?.ToLower();

                while (wantAnotherQuote == "igen")
                {
                    Random random = new Random();
                    var randomQuote = res.ElementAt(random.Next(res.Count()));
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine("Az idézet: " + randomQuote.text);
                    Console.ResetColor();
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write("Szeretnél még egy idézetet? (igen/nem): ");
                    Console.ResetColor();
                    wantAnotherQuote = Console.ReadLine()?.ToLower();
                }
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Köszönöm, hogy használtad a programot!");
                Console.ResetColor();

            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Hiba történt: " + ex.Message);
                Console.ResetColor();
            }
        }

        public static async Task<List<Quote>> GetQuote(string filePath)
        {
            string json = await File.ReadAllTextAsync(filePath, Encoding.UTF8);
            List<Quote> quotes = JsonConvert.DeserializeObject<List<Quote>>(json);
            return quotes;
        }

        public static bool existingMood(string mood)
        {
            string[] existing = { "boldog", "szomorú", "mérges", "unalom", "irigy", "ideges", "semleges" };
            return existing.Contains(mood.ToLower());
        }

        public static async Task saveQuotes(string filePath, List<Quote> quotes)
        {
            string json = JsonConvert.SerializeObject(quotes, Formatting.Indented);
            await File.WriteAllTextAsync(filePath, json);
        }
    }
}