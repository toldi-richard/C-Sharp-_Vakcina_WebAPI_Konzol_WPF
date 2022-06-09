using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Vakcina.Terminal.Models;

namespace Vakcina.Terminal
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Adatok beolvasása...");


            // TODO: 01. adatok beolvasása és tárolása OOP formában

            var felhasznalok = new List<Felhasznalok>();

            void Beolvasas()
            {
                using ( var sr = new StreamReader("../../../Forrás/felhasznalok.csv"))
                {
                    sr.ReadLine();
                    while (!sr.EndOfStream)
                    {
                        var sor = sr.ReadLine().Split(";");

                        int id = Convert.ToInt32(sor[0]);
                        string felhasznalo_nev = sor[1];
                        string jelszo = sor[2];
                        string megjelenitendo_nev = sor[3];
                        string munkakor = sor[4];
                        int admin = Convert.ToInt32(sor[5]);
                        int regisztralt = Convert.ToInt32(sor[6]);

                        felhasznalok.Add(new Felhasznalok(id, felhasznalo_nev,jelszo,megjelenitendo_nev,munkakor,admin,regisztralt));
                    }
                }
            }

            Beolvasas();
            felhasznalok.ForEach(x => Console.WriteLine(x.megjelenitendo_nev));
            // TODO: 02. beolvasott elemek száma
            Console.WriteLine("\n A beolvasott sorok száma: "+felhasznalok.Count());
            // TODO: 03. hibásan felvitt adat keresése

            // TODO: 04. Felhasználónév jelszó egyezés
            Console.WriteLine();
            var egyezes = felhasznalok.Where(x => x.felhasznalo_nev == x.jelszo).ToList();
            egyezes.ForEach(x => Console.WriteLine(x.megjelenitendo_nev));

            // TODO: 05. Szűrés, rendezés, jelszó titkosítás
            var orvosok = felhasznalok.Where(x => x.munkakor == "orvos")
                .OrderBy(x => x.id)
                .ToList();
            var orvosok_titkos = new List<Felhasznalok>();

            orvosok.ForEach(x => orvosok_titkos.Add(new Felhasznalok(x.id,x.felhasznalo_nev, BCrypt.Net.BCrypt.HashPassword(x.jelszo), x.megjelenitendo_nev,x.munkakor,x.admin,x.regisztralt)));

            orvosok_titkos.ForEach(x => Console.WriteLine(x.jelszo));
            // TODO: 06. export készítése

            try
            {
                StreamWriter file = new StreamWriter("../../../Forrás/valami.sql", false, Encoding.UTF8);
                foreach (var item in orvosok)
                {
                    if (item.regisztralt == 1)
                    {
                        file.WriteLine("Reegisztrált!");
                    }
                    else
                    {
                        file.WriteLine("Nem regisztrált!");
                    }
                }

                file.Flush();
                file.Close();
                Console.WriteLine("\n A fájl írása sikeres volt!");
            }
            catch (DirectoryNotFoundException)
            {

                Console.WriteLine("A mappa nem található!"); ;
            }            
            catch (IOException)
            {

                Console.WriteLine("A fájl írása közben hiba lépett fel!"); ;
            }

            Console.ReadKey();
        }
    }
}
