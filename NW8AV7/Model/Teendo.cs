namespace NW8AV7.Model
{
    internal class Teendo
    {
        private int prioritas;

        public string Nev { get; set; }
        public string Leiras { get; set; }
        public int Prioritas
        {
            get
            {
                return prioritas;
            }
            set
            {
                if (value < 1)
                {
                    prioritas = 1;
                }
                else if (value > 5)
                {
                    prioritas = 5;
                }
                else
                {
                    prioritas = value;
                }
            }
        }

        public Teendo(string nev, string leiras, int prioritas)
        {
            Nev = nev;
            Leiras = leiras;
            Prioritas = prioritas;
        }

        public override string ToString()
        {
            return "Teendo nev: " + Nev + ", leiras: "+ Leiras + ", prioritas: "+Prioritas+".";
        }
    }
}
