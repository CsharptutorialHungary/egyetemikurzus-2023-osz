## Követelmények:

### Technikai követelmények
| .NET 6 | ✔
|:------:|:-:|
| Konzolos alkalmazás | ✔

### Nem kihagyható elemek:
| Legyen benne kivételkezelés (`try-catch`) | ✔
|-------------------------------------------|---|
| Legalább a képenyőre írjon ki hibaüzeneteket | ✔

### Kötelezelő elemek
| adat olvasása fájlból szerializáció segítségével (pl.: Adat betöltés és/vagy mentés JSON/XML fájlból/fájlba) | ✔
|:------------------------------------------------------------------------------------------------------------:|:-:|
| legyen benne saját immutable type (pl.: `record class`) | ✔
| legyen benne LINQ segítségével: szűrés (`where`), csoportosítás (`group by`), rendezés (`order by`), agregáció (Pl.: `Min()`, `Max()`, `First()`, `FirstOrDefault`, `Average()`, stb...) közül legalább kettő | ❌
| legyen benne generikus kollekció (pl.: `List<T>`, `Stack<T>`, stb...) | ✔
| legyen benne aszinkron rész (`async` és `Task`) | ✔ | ✔

---
# Unit tesztek
---
## SaveStudentsToJSON Tesztek:

### TestThat_SaveStudentsToJSON_Runs:
| Érték | Elvárt eredmény | Megfelelt
|:-----:|:---------------:|:-------:|
|`True`|`True`|✔|

### TestThat_SaveStudentsToJSON_CreatesNewFolder:
| Érték | Elvárt eredmény | Megfelelt
|:-----:|:---------------:|:-------:|
|`True`|`True`|✔|

### TestThat_SaveStudentsToJSON_CreatesNewFile:
| Érték | Elvárt eredmény | Megfelelt
|:-----:|:---------------:|:-------:|
|`True`|`True`|✔|
