using NW8AV7.Model;

namespace NW8AV7.Logika
{
    internal class FelhasznaloKezeles
    {
        private KonzolKezeles konzolKezeles = new KonzolKezeles();

        public void Fooldal()
        {
            konzolKezeles.FejlecMutatasa("Főoldal");
            konzolKezeles.InstrukcioaAdas("Kérlek válassz az alábbi lehetőségek közül:",
                new string[]
                {
                    "1 - Felhasználó kiválasztása",
                    "2 - Felhasználó létrehozása",
                    "3 - Felhasználó törlése",
                    "4 - Felhasználó módosítása",
                    "5 - Kilépés"
                });

            int valasztottFunkcio = konzolKezeles.SzamBeolvasas();

            switch (valasztottFunkcio)
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
                case 5:
                    return;
                default:
                    konzolKezeles.HibaMutatasa("Helytelen számot adtál meg.");
                    Fooldal();
                    break;
            }
        }

        private void FelhasznaloKivalasztas()
        {
            konzolKezeles.FejlecMutatasa("Felhasználó kiválasztása");
            konzolKezeles.InstrukcioaAdas("Kérlek válassz az alábbi felhasználók közül:", MemoriaModel.Instance.Felhasznalok.ToFormattedString());

            int azonosito = konzolKezeles.SzamBeolvasas();
            Felhasznalo valasztottFelhasznalo = FelhasznaloKeres(azonosito);

            if (valasztottFelhasznalo == null)
            {
                konzolKezeles.HibaMutatasa("Helytelen számot adtál meg. Próbáld újra!\n");
                FelhasznaloKivalasztas();
                return;
            }

            konzolKezeles.InformacioMutatasa("Te a "+valasztottFelhasznalo.Nev+" választottad.");

            new TeendoKezeles(valasztottFelhasznalo).Fooldal();
        }

        private void FelhasznaloLetrehozas()
        {
            konzolKezeles.FejlecMutatasa("Felhasználó létrehozása");
            konzolKezeles.InstrukcioaAdas("Kérem a felhasznaló nevét: ");

            String nev = konzolKezeles.SzovegBeolvasas();

            Felhasznalo ujFelhasznalo = new Felhasznalo(nev, new List<Teendo>()); 
            MemoriaModel.Instance.Felhasznalok.Add(ujFelhasznalo);

            Fooldal();
        }

        private void FelhasznaloTorles()
        {
            konzolKezeles.FejlecMutatasa("Felhasználó törlése");
            konzolKezeles.InstrukcioaAdas("Kérlek válassz az alábbi felhasználók közül:", MemoriaModel.Instance.Felhasznalok.ToFormattedString());

            int azonosito = konzolKezeles.SzamBeolvasas();
            Felhasznalo valasztottFelhasznalo = FelhasznaloKeres(azonosito);

            MemoriaModel.Instance.Felhasznalok.Remove(valasztottFelhasznalo);

            konzolKezeles.InformacioMutatasa(valasztottFelhasznalo.Nev+" törölve lett!");

            Fooldal();
        }

        private void FelhasznaloModosit()
        {
            konzolKezeles.FejlecMutatasa("Felhasználó módosítása");
            konzolKezeles.InstrukcioaAdas("Kérlek válassz az alábbi felhasználók közül:", MemoriaModel.Instance.Felhasznalok.ToFormattedString());

            int azonosito = konzolKezeles.SzamBeolvasas();
            Felhasznalo valasztottFelhasznalo = FelhasznaloKeres(azonosito);

            konzolKezeles.InstrukcioaAdas("Kérem az új nevét a "+valasztottFelhasznalo.Nev+" felhasználónak: ");
            String nev = konzolKezeles.SzovegBeolvasas();

            valasztottFelhasznalo.Nev = nev;

            Fooldal();
        }

        private Felhasznalo FelhasznaloKeres(int sorszam)
        {
            return MemoriaModel.Instance.Felhasznalok.ElementAtOrDefault(sorszam - 1);
        }
    }
}
