using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vakcina.Terminal.Models
{
    public class Felhasznalok
    {
        public int id { get; set; }
        public string felhasznalo_nev { get; set; }
        public string jelszo { get; set; }
        public string megjelenitendo_nev { get; set; }
        public string munkakor { get; set; }
        public int admin { get; set; }
        public int regisztralt { get; set; }

        public Felhasznalok(int id, string felhasznalo_nev, string jelszo, string megjelenitendo_nev, string munkakor, int admin, int regisztralt)
        {
            this.id = id;
            this.felhasznalo_nev = felhasznalo_nev;
            this.jelszo = jelszo;
            this.megjelenitendo_nev = megjelenitendo_nev;
            this.munkakor = munkakor;
            this.admin = admin;
            this.regisztralt = regisztralt;
        }
    }
}
