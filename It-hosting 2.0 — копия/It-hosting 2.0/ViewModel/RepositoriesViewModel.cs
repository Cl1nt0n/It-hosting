using It_hosting_2._0.Model.Tools;
using It_hosting_2._0.Models.DBModels;
using It_hosting_2._0.View;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private CommandTemplate _addRepositoryToListBox;
        private Window _window;
        private User _user;
        private ICollection<Repository> _repositories;
        private ObservableCollection<RepositoryStackPanelViewModel> _myRepositories;

#pragma warning disable CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
        public RepositoriesViewModel(Window window, User user)
#pragma warning restore CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
        {
            _window = window;
            _user = user;
            using (ithostingContext db = new ithostingContext())
            {
                _repositories = db.Repositories.Where(x => x.UserId == User.Id).ToList();
            }
            MyRepositories = new ObservableCollection<RepositoryStackPanelViewModel>();
            foreach (var item in _repositories)
            {
                MyRepositories.Add(new RepositoryStackPanelViewModel(item));
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
         public ObservableCollection<RepositoryStackPanelViewModel> MyRepositories
        {
            get => _myRepositories;
            set
            {
                _myRepositories = value;
                OnPropertyChanged(nameof(MyRepositories));
            }
        }

        public ICollection<Repository> Repositories
        {
            get => _repositories;
            set
            {
                _repositories = value;
                OnPropertyChanged(nameof(Repositories));
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

        public CommandTemplate AddRepositoryToListBox
        {
            get
            {
                if (_addRepositoryToListBox == null)
                {
                    _addRepositoryToListBox = new CommandTemplate(obj =>
                    {
                        
                    });
                }

                return _addRepositoryToListBox;
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

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
