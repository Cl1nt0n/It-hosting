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
        private Window _window;
        private User _user;
        private ICollection<Repository> _repositories;
        private ObservableCollection<RepositoryStackPanelViewModel> _repositoriesView;

        public RepositoriesViewModel(Window window, User user)
        {
            _window = window;
            _user = user;

            using (ithostingContext db = new ithostingContext())
            {
                _repositories = db.Repositories.Where(x => x.UserId == User.Id).ToList();
            }

            RepositoriesView = new ObservableCollection<RepositoryStackPanelViewModel>();

            foreach (var item in _repositories)
            {
                RepositoriesView.Add(new RepositoryStackPanelViewModel(item, window));
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

        public ObservableCollection<RepositoryStackPanelViewModel> RepositoriesView
        {
            get => _repositoriesView;
            set
            {
                _repositoriesView = value;
                OnPropertyChanged(nameof(RepositoriesView));
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
        
        private void OpenCreatingRepositoryWindow()
        {
            CreatingRepositoryView creatingRepositoryView = new CreatingRepositoryView();
            CreatingRepositoryViewModel creatingRepositoryViewModel = new CreatingRepositoryViewModel(User);

            _window.Hide();

            creatingRepositoryView.DataContext = creatingRepositoryViewModel;
            creatingRepositoryView.ShowDialog();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
