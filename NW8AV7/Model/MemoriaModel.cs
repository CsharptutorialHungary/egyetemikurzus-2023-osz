using System.Text.Json;

namespace NW8AV7.Model
{
    internal class MemoriaModel
    {
        private static MemoriaModel instance;

        public static MemoriaModel Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MemoriaModel();
                }
                return instance;
            }
        }

        public List<Felhasznalo> Felhasznalok { get; private set; }

        public MemoriaModel()
        {
            Felhasznalok = new List<Felhasznalo>();
        }

        public async Task Betoltes(string utvonal)
        {
            string jsonString = File.ReadAllText(utvonal);
            Felhasznalok = JsonSerializer.Deserialize<List<Felhasznalo>>(jsonString);
        }

        public async Task Mentes(string utvonal)
        {
            string jsonString = JsonSerializer.Serialize(Felhasznalok);
            File.WriteAllText(utvonal, jsonString);
        }

    }
}
