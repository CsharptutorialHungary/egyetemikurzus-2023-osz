using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ConsoleApp_ZD6VEU
{
    internal class JatekBeallitas
    {
        public int kerdesMenyiseg = 10;
        public int ido = 10;
        public List<Kerdes> kerdesek {  get; set; }
        public List<Kerdes> jatekKerdesek { get; set; }

        public string appPath = AppContext.BaseDirectory;

        public string fileName = "kerdesek.txt";

        public string valasztotTema = "alap";

        public void temakKivalasztasa()
        {
            temakBeallitasa();
            int input = 0;
            while (input != -1)
            {
                Console.Clear();
                Console.WriteLine("Téma kiválasztása! ");
                Console.WriteLine("Választott téma: " + valasztotTema);
                Console.WriteLine();
                Console.WriteLine("Lehetséges témák: ");
                Console.WriteLine();

                for (int i = 0; i < Kerdes.temak.Count; i++)
                {
                    Console.WriteLine((i+1) + ": " + Kerdes.temak[i]);
                }
                Console.WriteLine();
                Console.WriteLine("9: alap (10 random kérdés)");
                Console.WriteLine();
                Console.WriteLine("0: Kilépés");

                input = 0;
                ConsoleKeyInfo keyInfo = Console.ReadKey();
                try
                {
                    int.TryParse(keyInfo.KeyChar.ToString(), out input);
                }
                catch (IOException e)
                {
                    Console.WriteLine("Nem jó input!");
                }

                input--;
                if (input >= 0 && input <= Kerdes.temak.Count())
                {
                    jatekKerdesek = (List<Kerdes>)kerdesek.Where(k => k.tema == Kerdes.temak[input])
                                                          .OrderBy(k => k.kerdes)
                                                          .ToList();
                    valasztotTema = Kerdes.temak[input];
                }
                if(input == 8) 
                {
                    veletlenKerdesek();
                    valasztotTema = "alap";
                }
            }
        }

        public void temakBeallitasa()
        {
            kerdesek.ForEach(k => { k.temakFeltoltese(); });
        }

        public void veletlenKerdesek()
        {
            Random random = new Random();
            List<Kerdes> kivalasztottKerdesek = new List<Kerdes>();

            if (kerdesMenyiseg <= kerdesek.Count())
            {
                List<Kerdes> kerdesekMasolat = new List<Kerdes>(kerdesek);

                for (int i = 0; i < kerdesMenyiseg; i++)
                {
                    int index = random.Next(kerdesekMasolat.Count);
                    kivalasztottKerdesek.Add(kerdesekMasolat[index]);
                    kerdesekMasolat.RemoveAt(index);
                }
            }

            jatekKerdesek = kivalasztottKerdesek;
        }
    

        public async Task<bool> kerdesekKimentese() 
        {
            try
            {
                string jsonString = JsonSerializer.Serialize(kerdesek, new JsonSerializerOptions()
                {                  
                    WriteIndented = true,
                    Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping                  
                });

                var path = Path.Combine(appPath, fileName);
                using (var textFile = File.CreateText(path))
                {
                    textFile.WriteLine(jsonString);
                }
            } catch(IOException e) 
            {
                Console.WriteLine(e.Message);
                return false;
            }
            return true;
        }

        public async Task<bool> kerdesekBeolvasasa()
        {
            try
            {
                string? jsonString = null;
                var path = Path.Combine(appPath, fileName);
                using (var reader = File.OpenText(path))
                {
                    string? line = null;
                    do
                    {
                        line = reader.ReadLine();
                        jsonString += line;
                    }
                    while (line != null);
                }
                if(jsonString == null) 
                {
                    return false;
                }
                kerdesek = JsonSerializer.Deserialize<List<Kerdes>>(jsonString, new JsonSerializerOptions() 
                {
                    WriteIndented = true,
                    Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                })!;
            }
            catch(IOException e) 
            {
                Console.WriteLine(e.Message);
                return false;
            }
            return true;
        }
    }
}
