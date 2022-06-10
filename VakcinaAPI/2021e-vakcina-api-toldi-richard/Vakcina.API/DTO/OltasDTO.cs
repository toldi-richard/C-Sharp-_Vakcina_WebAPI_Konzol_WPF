using System;

namespace Vakcina.API.DTO
{
    public class OltasDTO
    {
        // TODO: 03. Osztály tulajdonságok elkészítése
        public uint taj_szam { get; set; }
        public string vakcina { get; set; }
        public string  orvos { get; set; }
        public DateTime datum_utolso { get; set; }
        public int oltas_szam { get; set; }

        // TODO: 04. Konstruktor elkészítése
        public OltasDTO(uint taj_szam, string vakcina, string orvos, DateTime datum_utolso, int oltas_szam)
        {
            this.taj_szam = taj_szam;
            this.vakcina = vakcina;
            this.orvos = orvos;
            this.datum_utolso = datum_utolso;
            this.oltas_szam = oltas_szam;
        }
    }
}
