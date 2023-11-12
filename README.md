# TO-DO List:
**Nem kihagyható elemek:**
* Legyen benne kivételkezelés (`try-catch`) [❌]
* Legalább a képenyőre írjon ki hibaüzeneteket [❌]

**Kötelezelő elemek** - Ezek közül egy kihagyható vagy cserélhető, ha Unit és/vagy Integration tesztek tartoznak a projekthez:

* adat olvasása fájlból szerializáció segítségével (pl.: Adat betöltés és/vagy mentés JSON/XML fájlból/fájlba) [✔]
* legyen benne saját immutable type (pl.: `record class`) [✔]
* legyen benne LINQ segítségével: szűrés (`where`), csoportosítás (`group by`), rendezés (`order by`), agregáció (Pl.: `Min()`, `Max()`, `First()`, `FirstOrDefault`, `Average()`, stb...) közül legalább kettő [❌]{0}
* legyen benne generikus kollekció (pl.: `List<T>`, `Stack<T>`, stb...) [❌]
* legyen benne aszinkron rész (`async` és `Task`) [✔][✔]

## Technikai követelmények

* .NET 6 [✔]
* Konzolos alkalmazás [✔]
