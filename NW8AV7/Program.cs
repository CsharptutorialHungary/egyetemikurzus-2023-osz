// See https://aka.ms/new-console-template for more information
using NW8AV7.Logika;
using NW8AV7.Model;

// Létre hozzuk az adatbázis fájlt, ha még nem létezik.
if (!File.Exists("felhasznalok.json"))
{
    await MemoriaModel.Instance.Mentes("felhasznalok.json");
}

// Betöltés
await MemoriaModel.Instance.Betoltes("felhasznalok.json");

// Főoldal meghívása
FelhasznaloKezeles felhasznalo = new FelhasznaloKezeles();
felhasznalo.Fooldal();

// Mentés
await MemoriaModel.Instance.Mentes("felhasznalok.json");
