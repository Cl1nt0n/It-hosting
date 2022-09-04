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
        private string _confrimPassword;

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

        public string ConfrimPassword
        {
            get => _confrimPassword;
            set
            {
                _password = value;
                OnPropertyChanged(nameof(ConfrimPassword));
            }
        }

        #region Команды

        public CommandTemplate AddUserCommand
        {
            get
            {
                if (_addUserCommand == null && string.IsNullOrWhiteSpace(_login) && string.IsNullOrWhiteSpace(_userName)
                    && string.IsNullOrWhiteSpace(_password))
                {
                    if (_password == _confrimPassword)
                    {
                        _addUserCommand = new CommandTemplate(obj =>
                        {
                            AddUser();
                        });
                    }
                }
                else
                {
                    MessageBox.Show("Поля не должны быть пустыми !");
                    return null;
                }

                return _addUserCommand;
            }
        }

        #endregion

        public void AddUser()
        {
            using (ithostingContext db = new ithostingContext())
            {
                User user = new User();

                user = new User { Login = _login, Password = _password, Nickname = _userName };
                db.Users.Add(user);
                db.SaveChanges();

                MessageBox.Show("Пользователь добавлен успешно !");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
