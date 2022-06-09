using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vakcina.WPF.Models;

namespace Vakcina.WPF.Repositories
{
    public class OltasRepository
    {
        private VakcinaContext _context;
        public OltasRepository(VakcinaContext context)
        {
            _context = context;
        }

        private int _totalItems;

        public List<oltas> GetAll(
            int page = 1,
            int itemsPerPage = 20,
            string search = null,
            string sortBy = null,
            bool ascending = true)
        {
            
            var query = _context.Oltasok
                .Include(x => x.vakcina)
                .Include(x => x.orvos)
                .OrderByDescending(x => x.datum_utolso)
                .AsQueryable();

            // TODO: 08. Bejelentkezett felhasználóra szűkíteni az adatokat, ha nem 'admin' szerepkörű

            // TODO: 02.a Keresés készítése (szöveg, szám(ToString!), dátum értékekkel)
            // Keresés
            if (!string.IsNullOrWhiteSpace(search))
            {
                search = search.ToLower();
                int.TryParse(search, out int szam);
                DateTime.TryParse(search, out DateTime datum);

                query = query.Where( x =>
                    x.taj_szam == szam ||
                    x.vakcina.megnevezes.ToLower().Contains(search) ||
                    x.oltas_szam == szam ||
                    x.datum_utolso.Equals(datum) ||
                    x.orvos.megjelenitendo_nev.ToLower().Contains(search)
                    );
            }

            // TODO: 03. Sorbarendezés készítése
            if (!string.IsNullOrEmpty(sortBy))
            {
                switch (sortBy)
                {
                    case "taj_szam":
                        query = ascending ? query.OrderBy(x => x.taj_szam) :
                            query.OrderByDescending(x => x.taj_szam);
                        break;

                    case "vakcina.megnevezes":
                        query = ascending ? query.OrderBy(x => x.vakcina.megnevezes) :
                            query.OrderByDescending(x => x.vakcina.mennyiseg);
                        break;
                    case "orvos.megjelenitendo_nev":
                        query = ascending ? query.OrderBy(x => x.orvos.megjelenitendo_nev) :
                            query.OrderByDescending(x => x.orvos.megjelenitendo_nev);
                        break;
                }
            }


                // Összes találat kiszámítása
                _totalItems = query.Count();

            // Oldaltördelés
            if (page + itemsPerPage > 0)
            {
                query = query.Skip((page - 1) * itemsPerPage).Take(itemsPerPage);
            }
            return query.ToList();
        }

        public int TotalItems => _totalItems;
    }
}
