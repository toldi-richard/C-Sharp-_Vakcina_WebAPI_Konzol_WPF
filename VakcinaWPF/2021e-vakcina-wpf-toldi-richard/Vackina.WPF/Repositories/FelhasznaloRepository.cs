using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Vakcina.WPF.Models;

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
                return Application.Current.Resources["dbFail"].ToString();
            }

            // TODO: 06. Authentikálás befejezése

            // Ha sikeres a belépés: 
            return Application.Current.Resources["loginSuccess"].ToString();
            // TODO: 07. Bejelentkezett felhasználó adatainak tárolása
            // Ha elrontotta a jelszót, ezzel tér vissza: 
            return Application.Current.Resources["loginFail"].ToString();
            // Ha nem létezik ilyen felhasználó:
            return Application.Current.Resources["loginNoUser"].ToString();
        }
    }
}
