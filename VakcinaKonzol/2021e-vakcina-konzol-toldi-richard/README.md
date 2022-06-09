# Vakcina Konzol

A kórház új szoftveréhez szükséges az adatbázis tartalmi frissítése. Az titkárság már összegyűjtött egy listát az új és meglévő felhasználókról. A feladat, hogy a kapott CSV forrásfájlban lévő hibákat ki kell szűrni, majd egy adatbázis scriptet készíteni a belőle lévő adatokból.

- Az adatok beolvasásához használja a Forrás mappában lévő _felhasznalok.csv_ fájlt.
- Az egyes feladatokban a kiírásokat a minta szerint készítse el.
- A feladat megoldásához használja a mellékelt Vakcina.Terminal projektet.

**1.)** Olvassa be a felhasznalok.csv állományban található adatokat és tárolja el egy OOP szemléletnek megfelelő adatszerkezetben. (4 pont)

**2.)** Határozza meg és írja ki a képernyőre a minta szerint, hogy hány felhasználó van az állományban. (1 pont)

**3.)** Hibás adatnak számít az olyan felhasználó, aki nem regisztrált a rendszerben (0-ás érték), de valamilyen hiba folytán _admin_ jogosultságot kapott (1-es érték). Ha a titkárság megkapja a hibás sor számát, akkor javítják a rendszerükben.

Határozza meg és írja ki a képernyőre a minta szerint, hogy melyik sorokban találhatók hibás adatok! A sor számozása a legelső sortól kezdődik. Amennyiben nincsenek hibás adatok írja ki a képernyőre, hogy _„Nincs hibásan felvitt adat&quot;._ (3 pont)

**4.)** Az készülendő szoftverhez szükség lesz erős felhasználói jelszavakra. Gyűjtse össze és írja ki a képernyőre a minta szerint azokat a személyeket, akiknek a felhasználónevük és a jelszavuk megegyezik. (3 pont)

**5.)** Az adatbázis script elkészítéséhez szűrje ki az orvos munkakörrel ellátott felhasználókat és rendezze őket &#39;id&#39; azonosító alapján. A meglévő felhasználók jelszavait titkosítsa BCrypt hasheléssel. Használja hozzá a BCrypt.Net-Next könyvtárt. (3 pont)

**6.)** Hozzon létre orvosok.sql néven egy új állományt, amibe a szűrt, rendezett és titkosított jelszavakkal elláttot orvosok adatait kell írnia.

A már regisztrált felhasználóknál írjon a fájlba egy UPDATE parancsot, ahol a jelszó frissül. Nem regisztrált felhasználóknál egy INSERT parancsot kell a fájlba írnia. 

Mindkét parancsra lát példát a mintában, figyeljen oda az aposztrófokra az értékek kiíratásánál. A helyes működésről tesztelheti őket a _vp_vakcina_ adatbázison. (4 pont)

**Összesen** : 15 pont

**Minták**

A kimeneti minták a Forrás/Vakcina Konzol - Feladatleírás.pdf fájlban.
