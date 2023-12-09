using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ConsoleApp_ZD6VEU
{
    internal class Jatek
    {

        CancellationTokenSource _cts = new();

        public JatekBeallitas jatekBeallitas;

        public int jelenlegiIdo { get; set; }

        public int adottValasz { get; set; }

        public int helyesValasz = 0;

        public int helytelenValasz = 0;

        public int jelenlegiKerdes = 0;

        public bool pluszIdo = true;

        public bool felezes = true;

        public void  inicializalas()
        {
            helyesValasz = 0;

            helytelenValasz = 0;

            jelenlegiKerdes = 0;

            pluszIdo = true;

            felezes = true;
    }
        public void Inditas()
        {
            inicializalas();
            Tajekoztat();
            Console.WriteLine("Indulhat? (Nyomjon meg egy bármilyen gombot)");
            Console.ReadKey();
            Console.Clear();

            while (jatekBeallitas.jatekKerdesek.Count != jelenlegiKerdes)
            {
                jelenlegiIdo = jatekBeallitas.ido;
                Kiiratas();
                Kor();
                if (adottValasz == jatekBeallitas.jatekKerdesek[jelenlegiKerdes].helyes)
                {
                    Console.Clear();
                    Console.WriteLine("Megfelelő válasz!");
                    helyesValasz++;
                }
                else if (adottValasz == 0)
                {
                    Console.Clear();
                    Console.WriteLine("Az idő letelt!");
                    helytelenValasz++;
                }
                else {
                    Console.Clear();
                    Console.WriteLine("Sajnos nem megfelelő válasz!");
                    helytelenValasz++;
                }
                Console.WriteLine("Tovább? (Nyomjon meg egy gombot)");
                Console.ReadKey();
                Console.Clear();
                jelenlegiKerdes++;
            }
            Console.WriteLine("A játékos időnek vége!");
            Console.WriteLine(jatekBeallitas.jatekKerdesek.Count() + " darab kérdésből " + helyesValasz + " sikerült eltalálni.");
            Console.ReadKey();
        }

        public void Tajekoztat()
        {
            Console.WriteLine("Minden választási lehetőség elött fel van sorolva egy szám.\n" +
                "A lehetőség kiválasztásához az adott számot kell leütni a billentyűzeten.\n" +
                "Összesen két segítsége van, amiket egy játék során egyszer használhat.");
            Console.WriteLine();
        }

        public void Kiiratas()
        {
            Kerdes kerdes = jatekBeallitas.jatekKerdesek[jelenlegiKerdes];

            Console.Clear();
            Console.WriteLine("Kérdes: " + kerdes.kerdes);
            Console.WriteLine();
            Console.WriteLine("1: " + kerdes.valaszA);
            Console.WriteLine();
            Console.WriteLine("2: " + kerdes.valaszB);
            Console.WriteLine();
            Console.WriteLine("3: " + kerdes.valaszC);
            Console.WriteLine();
            Console.WriteLine("4: " + kerdes.valaszD);
            Console.WriteLine();
            Console.WriteLine();
            if (pluszIdo)
            {
                Console.WriteLine("5: Plusz idő kérése");
            }
            if (felezes)
            {
                Console.WriteLine("6: Válasz lehetőségek felezése");
            }
            Console.WriteLine();
            Console.WriteLine("Hátra lévő idő: " + jelenlegiIdo);
        }

        public void Kor()
        {
            _cts = new CancellationTokenSource();

            Thread egy = new Thread(Ido);
            Thread ketto = new Thread(Valasz);

            egy.Start();
            ketto.Start();

            egy.Join();
            ketto.Join();
        }

        public void Ido()
        {
            while (jelenlegiIdo > 0 && !_cts.Token.IsCancellationRequested)
            {
                Kiiratas();
                Thread.Sleep(1000);
                jelenlegiIdo--;
            }
            _cts.Cancel();
        }

        public void Valasz()
        {
            int input = 0;
            while (!_cts.Token.IsCancellationRequested)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo keyInfo = Console.ReadKey();
                    if (int.TryParse(keyInfo.KeyChar.ToString(), out input))
                    {
                        if (input >= 1 && input <= 4)
                        {
                            _cts.Cancel();
                            break;
                        }
                        else if (input == 5 && pluszIdo)
                        {
                            pluszIdoSegitseg();
                        }
                        else if (input == 6 && felezes)
                        {
                            felezoSegitseg();
                        }
                    }
                    else

                    {
                        Console.WriteLine("Nem jó input");
                    }
                }
            }
            adottValasz = input;
        }

        public void felezoSegitseg()
        {
            if (jatekBeallitas.jatekKerdesek[jelenlegiKerdes].helyes == 1)
            {
                jatekBeallitas.jatekKerdesek[jelenlegiKerdes].valaszC = "";
                jatekBeallitas.jatekKerdesek[jelenlegiKerdes].valaszD = "";
            }
            else if (jatekBeallitas.jatekKerdesek[jelenlegiKerdes].helyes == 2)
            {
                jatekBeallitas.jatekKerdesek[jelenlegiKerdes].valaszA = "";
                jatekBeallitas.jatekKerdesek[jelenlegiKerdes].valaszC = "";
            }
            else if (jatekBeallitas.jatekKerdesek[jelenlegiKerdes].helyes == 3)
            {
                jatekBeallitas.jatekKerdesek[jelenlegiKerdes].valaszB = "";
                jatekBeallitas.jatekKerdesek[jelenlegiKerdes].valaszD = "";
            }
            else if (jatekBeallitas.jatekKerdesek[jelenlegiKerdes].helyes == 4)
            {
                jatekBeallitas.jatekKerdesek[jelenlegiKerdes].valaszC = "";
                jatekBeallitas.jatekKerdesek[jelenlegiKerdes].valaszA = "";
            }
            felezes = false;
        }

        public void pluszIdoSegitseg() 
        {
            jelenlegiIdo += 10;
            pluszIdo = false;
        }
    }
}
