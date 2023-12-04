using NW8AV7.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NW8AV7.Logika
{
    internal class TeendoKezeles
    {
        private KonzolKezeles konzolKezeles;

        private Felhasznalo felhasznalo;
        
        public TeendoKezeles(Felhasznalo felhasznalo)
        {
            konzolKezeles = new KonzolKezeles();
            this.felhasznalo = felhasznalo;
        }

        public void Fooldal()
        {
            konzolKezeles.FejlecMutatasa(felhasznalo.Nev + " - Főoldal");
            konzolKezeles.InstrukcioaAdas("Kérlek válassz az alábbi lehetőségek közül:",
                new string[]
                {
                    "1 - Teendő listázása",
                    "2 - Teendő hozzáadása",
                    "3 - Teendő törlése",
                    "4 - Teendő módosítása"
                });

            int valasztottFunkcio = konzolKezeles.SzamBeolvasas();

            switch (valasztottFunkcio)
            {
                case 1:
                    TeendoListazasa();
                    break;
                case 2:
                    TeendoLetrehozas();
                    break;
                case 3:
                    TeendoTorles();
                    break;
                case 4:
                    TeendoModosit();
                    break;
                default:
                    konzolKezeles.HibaMutatasa("Helytelen számot adtál meg.");
                    Fooldal();
                    break;
            }
        }

        private void TeendoModosit()
        {
            Console.WriteLine("Teendo modositasa");
        }

        private void TeendoTorles()
        {
            Console.WriteLine("Teendo töröl");
        }

        private void TeendoLetrehozas()
        {
            Console.WriteLine("Teendo létrehoz");
        }

        private void TeendoListazasa()
        {
            Console.WriteLine("Teendo listazas");
        }
    }
}
