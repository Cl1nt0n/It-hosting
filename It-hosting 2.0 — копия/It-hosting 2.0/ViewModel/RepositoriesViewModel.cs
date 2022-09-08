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
    internal class RepositoriesViewModel
    {
        private CommandTemplate _openingCreatingRepositoryCommand;
        private Window _window;
        private User _user;

        public RepositoriesViewModel(Window window, User user)
        {
            _window = window;
            _user = user;
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

        public CommandTemplate OpeningCreatingRepositoryCommand
        {
            get
            {
                if (_openingCreatingRepositoryCommand == null)
                {
                    _openingCreatingRepositoryCommand = new CommandTemplate(obj =>
                    {
                        OpenCreatingRepositoryWindow();
                    });
                }

                return _openingCreatingRepositoryCommand;
            }
        }

        private void OpenCreatingRepositoryWindow()
        {
            CreatingRepositoryView creatingRepositoryView = new CreatingRepositoryView();
            CreatingRepositoryViewModel creatingRepositoryViewModel = new CreatingRepositoryViewModel(creatingRepositoryView, User);

            _window.Hide();

            creatingRepositoryView.DataContext = creatingRepositoryViewModel;
            creatingRepositoryView.ShowDialog();

            _window.Show();
        }

        //private void AddRepository()
        //{
        //    using (ithostingContext db = new ithostingContext())
        //    {
        //        Repository repository = new Repository { };


        //    }
        //}

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
