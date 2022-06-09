using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Vakcina.WPF.Repositories;
using Vakcina.WPF.Views;

namespace Vakcina.WPF.ViewModels
{
    public class LoginViewModel : ObservableObject
    {
        private FelhasznaloRepository _repository;
        public RelayCommand LoginCommand { get; set; }
        public LoginViewModel(FelhasznaloRepository repository)
        {
            _repository = repository;
            LoginCommand = new RelayCommand(Login, CanLogin);
        }
        public LoginViewModel()
        {

        }

        private string _username;
        public string Username
        {
            get { return _username; }
            set 
            {
                SetProperty(ref _username, value);
                LoginCommand.NotifyCanExecuteChanged(); 
            }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set 
            {
                SetProperty(ref _password, value);
                LoginCommand.NotifyCanExecuteChanged(); 
            }
        }

        private string _errorMessage;
        public string ErrorMessage
        {
            get { return _errorMessage; }
            set { SetProperty(ref _errorMessage, value); }
        }

        private bool CanLogin()
        {
            return !string.IsNullOrWhiteSpace(_username) && !string.IsNullOrWhiteSpace(_password);
        }

        private void Login()
        {
            ErrorMessage = _repository.Authenticate(_username, _password);
            if (ErrorMessage == Application.Current.Resources["loginSuccess"].ToString())
            {
                var mw = new MainWindow();
                Application.Current.Windows[0].Close();
                mw.Show();
            }
        }
    }
}
