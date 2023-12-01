using System.Text;

class Program
{
    static void Main()
    {
     
        string csvFilePath = Path.Combine(Directory.GetCurrentDirectory(), @"idezetek.csv");

        Console.WriteLine(csvFilePath);

        if (!File.Exists(csvFilePath))
        {
            Console.WriteLine("Hiba: A CSV fájl nem található.");
            return;
        }

        Console.Write("Szia! Hogy hívnak:");
        string name = Console.ReadLine();
        Console.Write("Szia "+name+"! Milyen kedved van ma?(boldog, szomorú, mérges, unalom, irigy, ideges, semleges):");
        string kedv = Console.ReadLine();

        string quotes= GetQuote(csvFilePath, kedv);

        if (quotes != null)
        {
            Console.WriteLine("Az idézet: " + quotes);
        }
        else
        {
            Console.WriteLine("Nincs idézet a megadott kedvre.");
        }
    }

    static string GetQuote(string filePath, string kedv)
    {
    
        string[] lines = File.ReadAllLines(filePath, Encoding.UTF8);

        List<string> quotes = new List<string>();

        foreach (var line in lines)
        {
            string[] parts = line.Split(';');

            if (parts.Length >= 2 && parts[0].Trim().Equals(kedv, StringComparison.OrdinalIgnoreCase))
            {
                quotes.Add(parts[1].Trim());
            }
        }

        if (quotes.Count > 0)
        {
            Random random = new Random();
            int randomIndex = random.Next(quotes.Count);
            return quotes[randomIndex];
        }

        return null;
    }
}