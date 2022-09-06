using It_hosting_2._0.Model;
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
    internal class SignInViewModel
    {
        private CommandTemplate _signingInCommand;
        private CommandTemplate _signingUpCommand;
        private string _login;
        private string _password;
        private Window _window;

        public SignInViewModel(Window window)
        {
            _window = window;
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

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        public CommandTemplate SigningUpCommand
        {
            get
            {
                if (_signingUpCommand == null)
                {
                    _signingUpCommand = new CommandTemplate(obj =>
                    {
                        OpenSignUpView();
                    });
                }

                return _signingUpCommand;
            }
        }

        public CommandTemplate SigningInCommand
        {
            get
            {
                if (_signingInCommand == null)
                {
                    _signingInCommand = new CommandTemplate(obj =>
                    {
                        User user;
                        SignIn(out user);

                        OpenUserProfileView(user);
                        //if (user == null)
                        //{
                        //    MessageBox.Show("Неправильный логин или пароль !");
                        //}
                        //else
                        //{
                        //    OpenUserProfileView();
                        //}
                    });
                }

                return _signingInCommand;
            }
        }

        private void OpenUserProfileView(User user)
        {
            UserProfileView userProfileView = new UserProfileView();
            UserProfileViewModel userProfileViewModel = new UserProfileViewModel(userProfileView, user);

            _window.Hide();

            userProfileView.DataContext = userProfileViewModel;
            userProfileView.ShowDialog();

            _window.Show();
        }

        public void SignIn(out User user)
        {
            using (ithostingContext db = new ithostingContext())
            {
                user = null;

                List<User> users = db.Users.ToList();

                if (Login != null && Password != null)
                {
                    foreach (User item in users)
                    {
                        if (item.Login == Login && item.Password == Password)
                        {
                            user = item;
                        }
                    }
                }
            }
        }

        public void OpenSignUpView()
        {
            SignUpView signUpWindow = new SignUpView();
            SignUpViewModel signUpViewModel = new SignUpViewModel();

            _window.Hide();

            signUpWindow.DataContext = signUpViewModel;
            signUpWindow.ShowDialog();

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
