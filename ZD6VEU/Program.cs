namespace ConsoleApp_ZD6VEU
{
    internal class Program
    {
        
        static async Task Main(string[] args)
        {
            var beallitas = new JatekBeallitas();
            bool beolvas = await beallitas.kerdesekBeolvasasa();
            if (!beolvas)
            {
                Console.WriteLine("Kérdések betöltése sikertelen");
                return;
            }

            var jatek = new Jatek();
            jatek.jatekBeallitas = beallitas;
            beallitas.veletlenKerdesek();

            bool kilepes = false;
            while (!kilepes) 
            {
                Console.Clear();
                Console.WriteLine("Minden választási lehetőség elött fel van sorolva egy szám.\n" +
                "A lehetőség kiválasztásához az adott számot kell leütni a billentyűzeten.");
                Console.WriteLine();
                Console.WriteLine("1: Játék Inditása");
                Console.WriteLine();
                Console.WriteLine("2: Kérdés Téma Beállítása");
                Console.WriteLine();
                Console.WriteLine("4: Kilépés");

                int input = 0;
                ConsoleKeyInfo keyInfo = Console.ReadKey();
                try
                {
                    int.TryParse(keyInfo.KeyChar.ToString(), out input);
                }
                catch (IOException e)
                { 
                    Console.WriteLine("Nem jó input!");
                }
                switch (input)
                { 
                    case 0:
                        break;
                    case 1:
                        Console.Clear();
                        jatek.Inditas();
                        break;
                    case 2:
                        beallitas.temakKivalasztasa();
                        break;
                    case 4: 
                        kilepes = true;
                        break;
                    default: 
                        break;
                }
            }
            bool mentes = await beallitas.kerdesekKimentese();
            if(!mentes)
            {
                Console.WriteLine("kérdések kimentése hibába ütközött");
            }
        }
    }
}