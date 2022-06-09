using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Vakcina.WPF.Models;
using Vakcina.WPF.Repositories;
using Vakcina.WPF.ViewModels;

namespace Vakcina.WPF
{
    // TODO: 09. XAML fájl, Login ablakkal való indítás

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            var serviceprovider = ConfigureServices();
            Ioc.Default.ConfigureServices(serviceprovider);
            InitializeComponent();
        }

        /// <summary>
        /// Configures the services for the application.
        /// </summary>
        private static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();
            string connectionString = ConfigurationManager.ConnectionStrings["VakcinaDB"].ConnectionString;
            services.AddDbContext<VakcinaContext>(options =>
                options.UseMySql(connectionString, ServerVersion.Parse("10.4.21-mariadb")));
            services.AddScoped<OltasRepository>();
            services.AddScoped<FelhasznaloRepository>();

            services.AddTransient<LoginViewModel>();
            services.AddTransient<MainViewModel>();
            services.AddTransient<OltasViewModel>();

            return services.BuildServiceProvider();
        }
    }
}
