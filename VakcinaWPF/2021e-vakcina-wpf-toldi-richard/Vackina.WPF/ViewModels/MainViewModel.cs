using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using Microsoft.Toolkit.Mvvm.Input;
using System.Windows;
using Vakcina.WPF.Repositories;
using Vakcina.WPF.Services;
using Vakcina.WPF.Views;

namespace Vakcina.WPF.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        public MainViewModel()
        {
            UpdateViewCommand = new RelayCommand<string>(Execute);
            LogoutCommand = new RelayCommand<Window>(Close);
            SelectedViewModel = new OltasViewModel(Ioc.Default.GetService<OltasRepository>());
            // TODO: 10. Bejelentkezett felhasználónév megjelenítése
            LoggedUser = CurrentUser.Name;
        }

        private ObservableObject _selectedViewModel;
        public ObservableObject SelectedViewModel
        {
            get { return _selectedViewModel; }
            set
            {
                SetProperty(ref _selectedViewModel, value);
            }
        }
        public RelayCommand<string> UpdateViewCommand { get; }
        public RelayCommand<Window> LogoutCommand { get; set; }

        public string LoggedUser { get; }

        public void Execute(string parameter)
        {
            if (parameter.ToString() == "oltas")
            {
                SelectedViewModel = new OltasViewModel(Ioc.Default.GetService<OltasRepository>());
            }
        }
        private void Close(Window window)
        {
            var loginView = new LoginWindow();
            loginView.Show();
            window.Close();
        }
    }
}
