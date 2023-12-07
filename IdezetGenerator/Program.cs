using System.Text;
record Quote(string Mood, string Text);
class Program
{
    static async Task Main()
    {
        try
        {
            var quotes = await GetQuote("idezetek.csv");

            Console.Write("Szia! Hogy hívnak:");
            string name = Console.ReadLine();
            Console.Write("Szia " + name + "! Milyen kedved van ma?(boldog, szomorú, mérges, unalom, irigy, ideges, semleges):");
            string mood = Console.ReadLine()?.ToLower();
            var res = quotes.Where(q => q.Mood == mood).OrderBy(q => q.Text);
            if (res.Any())
            {
                Random random = new Random();
                var randomQuote=res.ElementAt(random.Next(res.Count()));
                Console.WriteLine("Az idézet: " + randomQuote.Text);
            }
            else
            {
                Console.WriteLine("Nincs idézet a megadott kedvre.");
            }
        }
        catch(Exception ex)
        {
            Console.WriteLine("Hiba történt: " + ex.Message);
        }
    }


    static async Task<List<Quote>> GetQuote(string filePath)
    {
    
        string[] lines = File.ReadAllLines(filePath, Encoding.UTF8);

        List<Quote> quotes = new List<Quote>();

        using (var streamReader = new StreamReader(filePath))
        {
            string line;
            while((line= await streamReader.ReadLineAsync()) != null)
            {
                var parts= line.Split(';');
                if (parts.Length == 2)
                {
                    quotes.Add(new Quote(parts[0], parts[1]));
                }
            }
        }

        return quotes;

    }
}