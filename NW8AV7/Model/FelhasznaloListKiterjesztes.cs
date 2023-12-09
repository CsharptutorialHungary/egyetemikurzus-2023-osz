using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NW8AV7.Model
{
    internal static class FelhasznaloListKiterjesztes
    {
        public static string[] ToFormattedString(this List<Felhasznalo> felhasznalok)
        {
            List<string> felhasznaloLista = new List<string>();

            for (int azonosito = 0; azonosito < felhasznalok.Count; azonosito++)
            {
                felhasznaloLista.Add($"{azonosito + 1} - {felhasznalok[azonosito].Nev}");
            }

            return felhasznaloLista.ToArray();
        }
    }
}
