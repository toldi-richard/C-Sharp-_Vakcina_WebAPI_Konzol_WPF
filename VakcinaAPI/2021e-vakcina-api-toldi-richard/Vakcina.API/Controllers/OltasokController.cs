using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vakcina.API.DTO;
using Vakcina.API.Models;

namespace Vakcina.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OltasokController : ControllerBase
    {
        private readonly VakcinaContext _context;

        public OltasokController(VakcinaContext context)
        {
            _context = context;
        }

        private OltasDTO ConvertToDTO(oltas oltas)
        {
            if (oltas == null)
            {
                return null;
            }

            return new OltasDTO(
                       oltas.taj_szam,
                       oltas.vakcina.megnevezes,
                       oltas.orvos.megjelenitendo_nev,
                       oltas.datum_utolso,
                       oltas.oltas_szam
                );

        }

        // GET: api/oltasok/2022-03-01
        [HttpGet]
        // TODO: 06. Útvonal szabály javítása
        [Route("{datum}")]
        public async Task<int> GetCount( DateTime datum)
        {
            // TODO: 05. Oltások számának szűkítése megadott dátumra
            return await _context.Oltasok.Where(x => x.datum_utolso == datum).CountAsync();
        }

        // GET: api/oltasok
        // TODO: 08. megfelelő attribútum pótlása
        // TODO: 07. láthatóság beállítása 
        // TODO: 09.b DTO osztály használata
        public async Task<IEnumerable<OltasDTO>> GetOltasok()
        {
            // TODO: 09.a Eredmény átalakítása DTO osztállyá
            var oltasok = await _context.Oltasok
                .Include(x => x.orvos)
                .Include(x => x.vakcina)
                .OrderBy(x => x.datum_utolso)
                .ToListAsync();

            return oltasok.Select(x => ConvertToDTO(x)).ToList();
        }

        // POST: api/oltasok
        [HttpPost]
        public async Task<IActionResult> PostOltas(oltas oltas)
        {
            // TODO: 10.a oltás meglétének ellenőrzése
            bool letezik = await _context.Oltasok.AnyAsync(x => x.taj_szam == oltas.taj_szam);
            if (letezik)
            {
                return Conflict("Ezzel a TAJ számmal már lett rögzítve oltás.");
            }
            // TODO: 11.b Ha a vakcina mennyisége 0 vagy annál kisebb, akkor hibaüzenet
            var vakcina = await _context.Vakcinak.FindAsync(oltas.vakcina_id);
            if (vakcina.mennyiseg <= 0)
            {
                return BadRequest("Elfogyott a választott vakcina.");
            }
            // TODO: 12.a Vakcina mennyiségének csökkentése
            if (vakcina.mennyiseg > 0)
            {
                vakcina.mennyiseg--;
                _context.Entry(vakcina).State = EntityState.Modified;
            }

            // 13.a oltás szám mennyiségének növelése
            // 13.b oltás dátumának frissítése
            oltas.datum_utolso = DateTime.Now;
            oltas.oltas_szam++;
            await _context.AddAsync(oltas);
            await _context.SaveChangesAsync();

            return Ok(oltas);
        }

        // PUT: api/oltasok
        [HttpPut]
        public async Task<IActionResult> PutOltas(oltas oltas)
        {
            // TODO: 10.b
            var letezik = await _context.Oltasok.FindAsync(oltas.taj_szam);
            if (letezik == null)
            {
                return Conflict("Ezzel a TAJ még nem lett rögzítve oltás.");
            }

            // TODO: 11.a vakcina rekord kikeresése másik táblából,
            var vakcina = await _context.Vakcinak.FindAsync(letezik.vakcina_id);
            if (vakcina.mennyiseg <= 0)
            {
                return BadRequest("Elfogyott a választott vakcina.");
            }
            // TODO: 12.b

            if (vakcina.mennyiseg > 0)
            {
                vakcina.mennyiseg--;
                _context.Entry(vakcina).State = EntityState.Modified;
            }

            // TODO: 13.a
            // TODO: 13.b
            letezik.datum_utolso = DateTime.Now;
            letezik.oltas_szam++;
            _context.Entry(letezik).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            // TODO: 14. Státuszkód megváltoztatása
            return NoContent();
        }
    }
}
