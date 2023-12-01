// See https://aka.ms/new-console-template for more information
using NW8AV7.Logika;
using NW8AV7.Model;

// Mentés
/*
MemoriaModel.Instance.Felhasznalok.Add(new Felhasznalo("Teszt felhasznalo", new List<Teendo>() { new Teendo("Teszt teendo", "nagyon fonts", 5) }));
MemoriaModel.Instance.Mentes("felhasznalok.json");
*/

// Betöltés
MemoriaModel.Instance.Betoltes("felhasznalok.json");

// Főoldal meghívása
FelhasznaloKezeles felhasznalo = new FelhasznaloKezeles();
felhasznalo.Fooldal();

// Mentés
MemoriaModel.Instance.Mentes("felhasznalok.json");
