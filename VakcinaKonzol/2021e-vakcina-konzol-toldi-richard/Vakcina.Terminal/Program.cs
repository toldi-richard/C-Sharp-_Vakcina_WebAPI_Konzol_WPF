using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Vakcina.Terminal.Model;

namespace Vakcina.Terminal
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Adatok beolvasása...");

            // TODO: 01. adatok beolvasása és tárolása OOP formában

            // Lista amibe tároljuk az adatokat
            var felhasznalok = new List<Felhasznalok>();

            // Beolvasás metódus
            void Beolvasas()
            {
                using (var sr = new StreamReader("../../../Forrás/felhasznalok.csv"))
                {
                    sr.ReadLine();

                    while (!sr.EndOfStream)
                    {
                        var sorTomb = sr.ReadLine().Split(";");

                        int id = Convert.ToInt32(sorTomb[0]);
                        string felhasznalo_nev = sorTomb[1];
                        string jelszo = sorTomb[2];
                        string megjelenitendo_nev = sorTomb[3];
                        string munkakor = sorTomb[4];
                        int admin = Convert.ToInt32(sorTomb[5]);
                        int regisztralt = Convert.ToInt32(sorTomb[6]);

                        felhasznalok.Add(new Felhasznalok(id, felhasznalo_nev, jelszo, megjelenitendo_nev, munkakor, admin, regisztralt));
                    }
                }
            }

            Beolvasas();
            Console.WriteLine();

            // TODO: 02. beolvasott elemek száma
            Console.WriteLine("Beolvasott sorok száma: " + felhasznalok.Count());
            Console.WriteLine();

            // TODO: 03. hibásan felvitt adat keresése

            // Itt amit meg lehetett volna spórólni az a sorszám...mert az i+1 volt tulképpen
            // Elég lett volna egy for, bejárni az alaplistát és if-el ha a i-edik lista eleme
            // admin aki nem regisztrált tegye a hibás listába....az i+1-t és azt elég lett volna foreachel kiíratni, ha meg ennek a hossza nulla akkor kiíratni hogy nincs hibás adat

            int sorszam = 0;
            List<int> hibas_adatok_sorszama = new List<int>();

            var hibas_felhasznalo = felhasznalok.Where(x => x.regisztralt == 0 && x.admin == 1).ToList();

            if (hibas_felhasznalo.Count != 0)
            {
                for (int x = 0; x < hibas_felhasznalo.Count; x++)
                {
                    for (int i = 0; i < felhasznalok.Count; i++)
                    {
                        sorszam++;

                        if (felhasznalok[i] == hibas_felhasznalo[x])
                        {
                            hibas_adatok_sorszama.Add(sorszam);
                        }
                    }
                    sorszam = 0;
                }
                hibas_adatok_sorszama.ForEach(x => Console.WriteLine("Hibásan felvitt adat a következő sorban van: " + x));
            }
            else
            {
                Console.WriteLine("Nincs hibásan felvitt adat");
            }
            Console.WriteLine();


            // TODO: 04. Felhasználónév jelszó egyezés
            var egyezesek = felhasznalok.Where(x => x.felhasznalo_nev == x.jelszo).ToList();
            Console.WriteLine("Az alábbi felhasználóknak szükséges a jelszócsere: ");
            egyezesek.ForEach(x => Console.WriteLine("\t * "+x.megjelenitendo_nev));

            //foreach (var item in egyezesek)
            //{
            //    Console.WriteLine(item.megjelenitendo_nev);
            //}

            // TODO: 05. Szűrés, rendezés, jelszó titkosítás
            var orvosok = felhasznalok.Where(x => x.munkakor == "orvos").OrderBy(x=>x.id).ToList();
            var orvosok_hashelve= new List<Felhasznalok>();
            // a lista jó
            //orvosok.ForEach(x => Console.WriteLine(x.megjelenitendo_nev));   

            foreach (var item in orvosok)
            {
                orvosok_hashelve.Add(new Felhasznalok(item.id, item.felhasznalo_nev, BCrypt.Net.BCrypt.HashPassword(item.jelszo), item.megjelenitendo_nev, item.munkakor, item.admin, item.regisztralt));
            }

            //orvosok_hashelve.ForEach(x => Console.WriteLine(x.jelszo));

            // TODO: 06. export készítése
            try
            {
                StreamWriter file = new StreamWriter("../../../Forrás/orvosok.sql", false, Encoding.UTF8);
                foreach (var item in orvosok_hashelve)
                {
                    if (item.regisztralt==1)
                    {
                        file.WriteLine("UPDATE orvos SET jelszo = '"+item.jelszo+"' WHERE id = "+item.id+";");
                    } 
                    else
                    {
                        file.WriteLine("INSERT INTO orvos (felhasznalo_nev,jelszo,megjelenitendo_nev,admin) VALUES ('"+item.felhasznalo_nev+"','"+item.jelszo+"','"+item.megjelenitendo_nev+"',"+item.admin+"); ");
                    }
                }


                file.Flush();
                file.Close();
                Console.WriteLine("\n Az export fájl elkészült: orvosok.sql");
                Console.ReadKey();
            }
            catch (DirectoryNotFoundException)
            {
                Console.WriteLine("A mappa nem létezik!");
                Console.ReadKey();
            }
            catch (IOException)
            {
                Console.WriteLine("A fájl írása közben hiba lépett fel!");
                Console.ReadKey();
            }
        }
    }
}
