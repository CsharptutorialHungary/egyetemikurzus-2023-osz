using NW8AV7.Model;

namespace NW8AV7.Logika
{
    internal class TeendoKezeles
    {
        private readonly KonzolKezeles konzolKezeles;

        private readonly Felhasznalo felhasznalo;
        
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
                    "4 - Teendő módosítása",
                    "5 - Kilépés",
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
                case 5:
                    return;
                default:
                    konzolKezeles.HibaMutatasa("Helytelen számot adtál meg.");
                    Fooldal();
                    break;
            }
        }

        private void TeendoModosit()
        {
            konzolKezeles.FejlecMutatasa(felhasznalo.Nev + " - Teendő módosítása");
        }

        private void TeendoTorles()
        {
            konzolKezeles.FejlecMutatasa(felhasznalo.Nev + " - Teendő törlése");
        }

        private void TeendoLetrehozas()
        {
            konzolKezeles.FejlecMutatasa(felhasznalo.Nev + " - Teendő létrehozása");

            konzolKezeles.InstrukcioaAdas("Kérem a teendő nevét: ");
            string nev = konzolKezeles.SzovegBeolvasas();

            konzolKezeles.InstrukcioaAdas("Kérem a teendő leírását: ");
            string leiras = konzolKezeles.SzovegBeolvasas();

            int prioritas = 0;
            while (!(prioritas >= 1 && prioritas <= 5))
            {
                konzolKezeles.InstrukcioaAdas("Kérem a teendő prioritását (1 - 5 közötti érték): ");
                prioritas = konzolKezeles.SzamBeolvasas();

                if (prioritas < 1 || prioritas > 5)
                {
                    konzolKezeles.HibaMutatasa("Helytelen prioritási érték. Próbáld újra!");
                }
            }

            Teendo teendo = new Teendo(nev, leiras, prioritas);
            felhasznalo.Teendok.Add(teendo);

            Fooldal();
        }

        private void TeendoListazasa()
        {
            konzolKezeles.FejlecMutatasa(felhasznalo.Nev + " - Teendő listázása");
            konzolKezeles.InstrukcioaAdas("Kérlek válassz az alábbi lehetőségek közül:",
               new string[]
               {
                    "1 - Egyszerű listázása (hozzáadás sorrendjében)",
                    "2 - Prioritási sorrendben listázás (elől a legfontosabb)",
                    "3 - Legfontosabbak listázása",
               });

            int valasztottFunkcio = konzolKezeles.SzamBeolvasas();

            switch (valasztottFunkcio)
            {
                case 1:
                    EgyszeruListazas();
                    break;
                case 2:
                    PrioritasiSorrendbenListazas();
                    break;
                case 3:
                    LegfontosabbListazas();
                    break;
                default:
                    konzolKezeles.HibaMutatasa("Helytelen számot adtál meg.");
                    TeendoListazasa();
                    break;
            }
        }

        private void LegfontosabbListazas()
        {
            konzolKezeles.FejlecMutatasa(felhasznalo.Nev + " - Teendő listázása (csak a legfontosabbak)");

            int maxPrioritas = felhasznalo.Teendok.Max(teendo => teendo.Prioritas);
            var legfontosabbak = felhasznalo.Teendok.Where(teendo => teendo.Prioritas == maxPrioritas);

            foreach (var teendo in legfontosabbak)
            {
                konzolKezeles.InformacioMutatasa(teendo.ToString());
            }

            Fooldal();
        }

        private void PrioritasiSorrendbenListazas()
        {
            konzolKezeles.FejlecMutatasa(felhasznalo.Nev + " - Teendő listázása (prioritási sorrendben)");

            var prioritasiSorrend = felhasznalo.Teendok.OrderByDescending(teendo => teendo.Prioritas);

            foreach (var teendo in prioritasiSorrend)
            {
                konzolKezeles.InformacioMutatasa(teendo.ToString());
            }

            Fooldal();
        }

        private void EgyszeruListazas()
        {
            konzolKezeles.FejlecMutatasa(felhasznalo.Nev + " - Teendő listázása (egyszerű)");
            
            foreach (var teendo in felhasznalo.Teendok)
            {
                konzolKezeles.InformacioMutatasa(teendo.ToString());
            }

            Fooldal();
        }
    }
}
