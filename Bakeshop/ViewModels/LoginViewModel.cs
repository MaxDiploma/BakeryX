using Bakeshop.CommandHandler;
using Bakeshop.EF;
using Bakeshop.StaticResources;
using Bakeshop.Views;
using GalaSoft.MvvmLight;
using System;
using System.Data.Entity;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Bakeshop.ViewModels
{
    public class LoginViewModel : ObservableObject, IDisposable
    {
        private readonly BakeshopContext _context;
        private string _username;
        private ICommand _loginCommand;

        public LoginViewModel()
        {
            _context = new BakeshopContext();
        }

        public ICommand LoginCommand
        {
            get
            {
                return _loginCommand ?? (_loginCommand = new BaseCommandHandler(param => LoginHandler(param), true));
            }
        }

        public Action CloseAction { get; set; }

        public string Username
        {
            get { return _username; }
            set { Set(ref _username, value); }
        }

        public string Password { get; set; }

        private async void LoginHandler(object param)
        {
            var passwordBox = param as PasswordBox;
            Password = passwordBox.Password;

            if (string.IsNullOrEmpty(Password) && string.IsNullOrEmpty(Username))
            {
                MessageBox.Show("Fields couldn't be empty.", "Exception", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            var user = await _context.BakeshopWorkers.FirstOrDefaultAsync(u => u.Email == Username && u.Password == Password);

            if (user == null)
            {
                MessageBox.Show("Email or password incorrect.", "Exception", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            CurrentUserManagment.FillUserWithData(user);

            var menuView = new MenuView();
            menuView.Show();
            CloseAction();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }


    }
}
