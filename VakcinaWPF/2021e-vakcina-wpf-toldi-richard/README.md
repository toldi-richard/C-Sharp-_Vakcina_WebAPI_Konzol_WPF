# Vakcina WPF

Az oltások lekérdezésére készülendő asztali alkalmazás befejezése lesz a feladat. Az oltásokat keresni, szűrni és lapozni szeretnék a felhasználók. Szükség lesz hitelesítést beépíteni az alkalmazásba. A feladat megoldásához segítenek a kódsorban lévő TODO bejegyzéseket!

Kezdeti lépések:

- Töltse fel a MySQL adatbázist a mellékelt _vp\_vakcina.sql_ fájl segítségével.
- A feladat megoldásához használja a mellékelt _Vakcina.WPF_ projektet.

1. Jelenleg, ha elindítjuk az alkalmazást, az oltásoknál lévő táblázat oszlopai alatt nem a megfelelő értékek szerepelnek. Javítsa ki az _OltasDGView.xaml_ fájlt, hogy a képen látható módon jelenjenek meg az adatok. (2 pont)
2. Az alkalmazásnak szüksége lenne egy univerzális keresőre, ami minden mezőben keres.
  a. Készítsen egy kereső funkciót az _OltasRepository_ osztály _GetAll_ metódusában. (3 pont)
  b. Egészítse ki a keresést a GUI felületen is egy keresőmezővel. (1 pont)
3. Az oszlopok fejléceire kattintva szükség van egy rendező műveletre a táblázaton. Készítsen egy rendező funkciót az _OltasRepository.cs GetAll_ metódusában. (3 pont)
4. A _PagerViewModel_ osztály szolgál majd az adatok lapozására a keresés és a sorba rendezésen kívül. Fejezze be a lapozás műveleteire szolgáló metódusokat.
  a-d. Lehessen előre, hátra, az első és az utolsó oldalra lépni. (4 pont)
5. Az _OltasDGView.xaml_ felületen lent látható navigációs gombok még nem működnek. Társítsa a lapozó parancsokat őket a ViewModel osztályból. (2 pont)
6. A _FelhasznaloRepository_ osztályban még nincs befejezve a felhasználó hitelesítése. A sikeres belépéshez a jó felhasználónév és jelszó páros megadása szükséges. A jelszavak titkosítására nincs szükség. Készítse fel a metódust, sikeres, sikertelen (2) műveletekre, a megadott hibaüzenet alapján. (3 pont)
7. Ha sikeresen bejelentkezett a felhasználó, akkor tárolja el a **8.,10.** feladathoz szükséges felhasználói adatokat a program memóriájában. (3 pont)
8. Módosítsa az _OltasRepository.cs GetAll_ metódusát, hogy a bejelentkezett felhasználóhoz tartozó oltásokat jelenítse meg. Ha _admin_ szerepkörű, akkor jelenítse meg az összeset. (2 pont)
 Ha nem sikerült eltárolni, akkor szűkítse le az 1-es id-jú orvos által adott oltásokra.
9. Állítsa be, hogy az alkalmazás a bejelentkezési képernyővel induljon. (1 pont)
10. Jelenítse meg a bejelentkezett felhasználó nevét a MainWindow állapotsorán. Ehhez használja a már megírt ViewModel osztályt. (1 pont)
 Ha nem sikerült eltárolni, akkor a bejelentkezett felhasználó neve legyen: „Felhasználó".

**Összesen** : 25 pont
