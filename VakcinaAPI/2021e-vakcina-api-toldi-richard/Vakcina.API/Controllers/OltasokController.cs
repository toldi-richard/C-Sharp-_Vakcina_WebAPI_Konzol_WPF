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

        // Átalakító metódus
        private OltasDTO ConvertToDTO(oltas oltas)
        {
            if (oltas == null)
            {
                return null;
            }
            return new OltasDTO(

                oltas.taj_szam,
                oltas.vakcina.megnevezes,
                oltas.orvos.felhasznalo_nev,
                oltas.datum_utolso,
                oltas.oltas_szam);
        }

        // GET: api/oltasok/2022-03-01
        [HttpGet]
        // TODO: 06. Útvonal szabály javítása
        [Route("{date}")]
        public async Task<int> GetCount(DateTime date)
        {
            // TODO: 05. Oltások számának szűkítése megadott dátumra
            return await _context.Oltasok.Where(x => x.datum_utolso == date).CountAsync();
        }

        // GET: api/oltasok
        // TODO: 08. megfelelő attribútum pótlása
        // TODO: 07. láthatóság beállítása 
        // TODO: 09.b DTO osztály használata
        [HttpGet]
        [Route("")]
        public async Task<IEnumerable<OltasDTO>> GetOltasok()
        {
            // TODO: 09.a Eredmény átalakítása DTO osztállyá
            var oltasok = await _context.Oltasok
                .Include(x => x.vakcina)
                .Include(x => x.orvos)
                .OrderBy(x => x.datum_utolso)
                .ToListAsync();

            return oltasok.Select(x => ConvertToDTO(x)).ToList();
        }

        // POST: api/oltasok
        [HttpPost]
        public async Task<IActionResult> PostOltas(oltas oltas)
        {
            // TODO: 10.a oltás meglétének ellenőrzése
            var letezik = await _context.Oltasok.FindAsync(oltas.taj_szam);
            if (letezik != null)
            {
                return Conflict("Ezzel a TAJ számmal már lett rögzítve oltás.");
            }

            var vakcina = await _context.Vakcinak.FindAsync(oltas.vakcina_id);
            // TODO: 11.b Ha a vakcina mennyisége 0 vagy annál kisebb, akkor hibaüzenet
            if (vakcina.mennyiseg <= 0)
            {
                return BadRequest("Elfogyott a választott vakcina.");
            }

            // TODO: 12.a Vakcina mennyiségének csökkentése
            vakcina.mennyiseg--;
            _context.Entry(vakcina).State = EntityState.Modified;


            // 13.a oltás szám mennyiségének növelése
            // 13.b oltás dátumának frissítése
            oltas.datum_utolso = DateTime.Now;
            oltas.oltas_szam=1;
            await _context.AddAsync(oltas);
            await _context.SaveChangesAsync();

            return Ok(oltas);
        }

        // PUT: api/oltasok
        [HttpPut]
        public async Task<IActionResult> PutOltas(oltas oltas)
        {
            // TODO: 10.b
            // Single() is jó lehet, 1 elemet ad vissza
            // .SingleOrDefault ha több van akkor exceptiont dob, ha csak 1
            // visszaadja, ha nincs akkor null értéket ad vissza

            var letezike = await _context.Oltasok.FindAsync(oltas.taj_szam);
            if (letezike == null)
            {
                return Conflict("Ezzel a TAJ még nem lett rögzítve oltás.");
            }

            // TODO: 11.a vakcina rekord kikeresése másik táblából,
            // Ezt az alsó sort ki kell kommentelni, és máshogy helyettesíteni
            //vakcina vakcina = new vakcina();

            var vakcina = await _context.Vakcinak.FindAsync(letezike.vakcina_id);
            //11.b:
            if (vakcina.mennyiseg <= 0 )
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
            letezike.datum_utolso = DateTime.Now;
            letezike.oltas_szam++;
            _context.Entry(letezike).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            // TODO: 14. Státuszkód megváltoztatása
            return NoContent();
        }
    }
}
