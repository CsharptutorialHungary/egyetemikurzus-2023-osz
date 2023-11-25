namespace NW8AV7.Model
{
    internal class Felhasznalo
    {
        public string Nev { get; set; }
        public List<Teendo> Teendok { get; }

        public Felhasznalo(string nev, List<Teendo> teendok)
        {
            Nev = nev;
            Teendok = teendok;
        }

        public override string ToString()
        {
            string toString = Nev + " felhasználó teendői: \n";

            foreach (var teendo in Teendok)
            {
                toString += teendo.ToString() + "\n";
            }

            return toString;
        }
    }
}