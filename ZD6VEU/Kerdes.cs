using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_ZD6VEU
{
    public record Kerdes
    {
        public Kerdes(string kerdes, string valaszA, string valaszB, string valaszC, string valaszD, int helyes, string tema)
        {
            this.kerdes = kerdes;
            this.valaszA = valaszA;
            this.valaszB = valaszB;
            this.valaszC = valaszC;
            this.valaszD = valaszD;
            this.helyes = helyes;
            this.tema = tema;
        }

        public static List<string> temak = new List<string>();
        public string kerdes { get; set; }
        public string valaszA { get; set; }
        public string valaszB { get; set; }
        public string valaszC { get; set; }
        public string valaszD { get; set; }
        public int helyes { get; set; }
        public string tema { get; set; }

        public void temakFeltoltese()
        {
            if (!temak.Contains(this.tema))
            {
                temak.Add(this.tema);
            }
        }
    }
}
