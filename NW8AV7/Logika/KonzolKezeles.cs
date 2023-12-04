using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NW8AV7.Logika
{
    internal class KonzolKezeles
    {
        public void FejlecMutatasa(string cim)
        {
            Console.WriteLine();
            Console.WriteLine("--------------------------------------------");
            Console.WriteLine($" --> {cim.ToUpper()}");
            Console.WriteLine("--------------------------------------------");
        }

        public void InstrukcioaAdas(string leiras, string[] lehetosegek = null)
        {
            Console.WriteLine(leiras);
            if (lehetosegek != null)
            {
                foreach (var lehetoseg in lehetosegek)
                {
                    Console.WriteLine(lehetoseg);
                }
            }
            Console.WriteLine();
        }

        public int SzamBeolvasas()
        {
            try
            {
                return Convert.ToInt32(Console.ReadLine());
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public string SzovegBeolvasas()
        {
            return Console.ReadLine();
        }

        public void HibaMutatasa(string hibaLeirasa)
        {
            Console.WriteLine("### HIBA ### "+hibaLeirasa);
        }

        public void InformacioMutatasa(string informacio)
        {
            Console.WriteLine(">>> " + informacio);
        }
    }
}
