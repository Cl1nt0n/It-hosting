using It_hosting_2._0.Model.Tools;
using It_hosting_2._0.Models.DBModels;
using It_hosting_2._0.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace It_hosting_2._0.ViewModel
{
    internal class UserProfileViewModel
    {
        private CommandTemplate _openingRepositoriesCommand;
        private Window _window;

        private string _userName;
        private string _login;
        private User _user;

        public UserProfileViewModel(Window window, User user)
        {

            //пофиксить
            User = user;
            _window = window;
            UserName = user.Nickname;
            Login = user.Login;
        }

        public CommandTemplate OpeningRepositoriesCommand
        {
            get
            {
                if (_openingRepositoriesCommand == null)
                {
                    _openingRepositoriesCommand = new CommandTemplate(obj =>
                    {
                        OpenRepositories();
                    });
                }

                return _openingRepositoriesCommand;
            }
        }

        public User User
        {
            get => _user;
            set
            {
                _user = value;
                OnPropertyChanged(nameof(User));
            }
        }    

        public string UserName
        {
            get => _userName;
            set
            {
                _userName = value;
                OnPropertyChanged(nameof(UserName));
            }
        }

        public string Login
        {
            get => _login;
            set
            {
                _login = value;
                OnPropertyChanged(nameof(Login));
            }
        }

        private void OpenRepositories()
        {
            RepositoriesView repositoriesView = new RepositoriesView(); 
            RepositoriesViewModel repositoriesViewModel = new RepositoriesViewModel();

            _window.Hide();

            repositoriesView.DataContext = repositoriesView;
            repositoriesView.ShowDialog();

            _window.Show();
        }

        public void OnClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
