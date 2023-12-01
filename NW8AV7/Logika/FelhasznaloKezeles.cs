using NW8AV7.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NW8AV7.Logika
{
    internal class FelhasznaloKezeles
    {
        public void Fooldal()
        {
            Console.WriteLine("--------------------------------------------");
            Console.WriteLine("----------------KEZDOOLDAL------------------");
            Console.WriteLine("--------------------------------------------");
            Console.WriteLine("Kérlek válassz az alábbi lehetőségek közül:");
            Console.WriteLine("1 - Felhasználó kiválasztása");
            Console.WriteLine("2 - Felhasználó létrehozása");
            Console.WriteLine("3 - Felhasználó törlése");
            Console.WriteLine("4 - Felhasználó módosítása");

            int ertek = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine();

            switch (ertek)
            {
                case 1:
                    FelhasznaloKivalasztas();
                    break;
                case 2:
                    FelhasznaloLetrehozas();
                    break;
                case 3:
                    FelhasznaloTorles();
                    break;
                case 4:
                    FelhasznaloModosit();
                    break;
            }
        }

        private void FelhasznaloKivalasztas()
        {
            Console.WriteLine("--------------------------------------------");
            Console.WriteLine("-----------FELHASZNALÓ VÁLASZTÁS------------");
            Console.WriteLine("--------------------------------------------");
            int azonosito = FelhasznaloValasztas();

            Felhasznalo valasztottFelhasznalo = FelhasznaloKeres(azonosito);

            Console.WriteLine("Te a "+valasztottFelhasznalo.Nev+" választottad.");
            Console.WriteLine();

            // Teendő lista kezelés főoldalát kell meghívni
            Fooldal();
        }

        private void FelhasznaloLetrehozas()
        {
            Console.WriteLine("--------------------------------------------");
            Console.WriteLine("----------FELHASZNALÓ LÉTREHOZÁSA-----------");
            Console.WriteLine("--------------------------------------------");
            Console.WriteLine("Kérem a felhasznaló nevét: ");
            String nev = Console.ReadLine();

            Console.WriteLine();

            Felhasznalo ujFelhasznalo = new Felhasznalo(nev, new List<Teendo>()); 
            MemoriaModel.Instance.Felhasznalok.Add(ujFelhasznalo);

            Fooldal();
        }

        private void FelhasznaloTorles()
        {
            Console.WriteLine("--------------------------------------------");
            Console.WriteLine("------------FELHASZNALÓ TÖRLÉSE-------------");
            Console.WriteLine("--------------------------------------------");
            int azonosito = FelhasznaloValasztas();

            Felhasznalo valasztottFelhasznalo = FelhasznaloKeres(azonosito);

            MemoriaModel.Instance.Felhasznalok.Remove(valasztottFelhasznalo);

            Console.WriteLine(valasztottFelhasznalo.Nev+" törölve lett!");
            Console.WriteLine();

            Fooldal();
        }

        private void FelhasznaloModosit()
        {
            Console.WriteLine("--------------------------------------------");
            Console.WriteLine("-----------FELHASZNALÓ MÓDOSÍTAS------------");
            Console.WriteLine("--------------------------------------------");
            int azonosito = FelhasznaloValasztas();

            Felhasznalo valasztottFelhasznalo = FelhasznaloKeres(azonosito);

            Console.WriteLine("Kérem az új nevét a "+valasztottFelhasznalo.Nev+" felhasználónak: ");
            
            String nev = Console.ReadLine();

            Console.WriteLine();

            valasztottFelhasznalo.Nev = nev;

            Fooldal();
        }

        private int FelhasznaloValasztas()
        {
            Console.WriteLine("Kérlek válassz az alábbi felhasználók közül:");

            int azonosito = 0;
            foreach (var felhasznalo in MemoriaModel.Instance.Felhasznalok)
            {
                azonosito++;
                Console.WriteLine(azonosito + " - " + felhasznalo.Nev);
            }

            return Convert.ToInt32(Console.ReadLine());
        }

        private Felhasznalo FelhasznaloKeres(int sorszam)
        {
            Felhasznalo valasztottFelhasznalo = null;
            int azonosito = 0;
            foreach (var felhasznalo in MemoriaModel.Instance.Felhasznalok)
            {
                azonosito++;
                if (sorszam == azonosito)
                {
                    valasztottFelhasznalo = felhasznalo;

                }
            }
            return valasztottFelhasznalo;
        }
    }
}
