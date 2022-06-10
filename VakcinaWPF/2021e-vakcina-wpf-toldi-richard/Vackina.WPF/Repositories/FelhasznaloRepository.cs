using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Vakcina.WPF.Models;
using Vakcina.WPF.Services;

namespace Vakcina.WPF.Repositories
{
    public class FelhasznaloRepository
    {
        private VakcinaContext _context;
        public FelhasznaloRepository(VakcinaContext context)
        {
            _context = context;
        }

        public string Authenticate(string username, string password)
        {

            if (!_context.Database.CanConnect())
            {
                return "Az adatbázis nem elérhető.";
            }

            // TODO: 06. Authentikálás befejezése
            var dbUser = _context.Orvosok.AsNoTracking().SingleOrDefault(x => x.felhasznalo_nev == username);
            if (dbUser != null)
            {
                if (password == dbUser.jelszo)
                {
                    // TODO: 07. Bejelentkezett felhasználó adatainak tárolása
                    CurrentUser.Admin = dbUser.admin;
                    CurrentUser.Id = dbUser.id;
                    CurrentUser.Name = dbUser.megjelenitendo_nev;
                    CurrentUser.Username = dbUser.felhasznalo_nev;
                    // Ha sikeres a belépés: 
                    return Application.Current.Resources["loginSuccess"].ToString();
                }
                else
                {
                    // Ha elrontotta a jelszót, ezzel tér vissza: 
                    return Application.Current.Resources["loginFail"].ToString();
                }

            }
            else
            {
                // Ha nem létezik ilyen felhasználó:
                return Application.Current.Resources["loginNoUser"].ToString();
            }
        }
    }
}
