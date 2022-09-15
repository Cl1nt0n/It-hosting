using It_hosting_2._0.Model.Tools;
using It_hosting_2._0.Models.DBModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace It_hosting_2._0.ViewModel
{
    internal class SignUpViewModel
    {
        private CommandTemplate _addUserCommand;


        private string _userName;
        private string _login;
        private string _password;
        private string _confirmPassword;

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

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        public string ConfirmPassword
        {
            get => _confirmPassword;
            set
            {
                _confirmPassword = value;
                OnPropertyChanged(nameof(ConfirmPassword));
            }
        }

        #region Команды

        public CommandTemplate AddUserCommand
        {
            get
            {
                if (_addUserCommand == null)
                {
                    _addUserCommand = new CommandTemplate(obj =>
                    {
                        AddUser();
                    });
                }

                return _addUserCommand;
            }
        }

        #endregion

        public void AddUser()
        {
            using (ithostingContext db = new ithostingContext())
            {
                if (string.IsNullOrWhiteSpace(_login) == false && string.IsNullOrWhiteSpace(_userName) == false
                    && string.IsNullOrWhiteSpace(_password) == false)
                {
                    if (_password == _confirmPassword)
                    {
                        User user = new User { Login = _login, Password = _password, Nickname = _userName };
                        db.Users.Add(user);
                        db.SaveChanges();

                        MessageBox.Show("Пользователь добавлен успешно !");
                    }
                }
                else
                {
                    MessageBox.Show("Поля не должны быть пустыми !");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
