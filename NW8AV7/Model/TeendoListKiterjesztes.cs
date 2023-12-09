using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NW8AV7.Model
{
    internal static class TeendoListKiterjesztes
    {
        public static string[] ToFormattedString(this List<Teendo> teendok)
        {
            List<string> teendokLista = new List<string>();

            for (int azonosito = 0; azonosito < teendok.Count; azonosito++)
            {
                teendokLista.Add($"{azonosito + 1} - {teendok[azonosito].ToString()}");
            }

            return teendokLista.ToArray();
        }
    }
}
