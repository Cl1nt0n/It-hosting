using It_hosting_2._0.Model.Tools;
using It_hosting_2._0.Models.DBModels;
using It_hosting_2._0.View;
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
    internal class RepositoryStackPanelViewModel
    {
        //private ICollection<Repository> _repositories;
        //private User _user;
        private Repository _repository;
        private CommandTemplate _openingRepositoryCommand;
        private Window _window;

        public RepositoryStackPanelViewModel(Repository repository, Window window)
#pragma warning restore CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
        {
            Repository = repository;
            _window = window;
        }

        public Repository Repository
        {
            get => _repository;
            set
            {
                _repository = value;
                OnPropertyChanged(nameof(Repository));
            }
        }

        public CommandTemplate OpeningRepositoryViewCommand
        {
            get
            {
                if (_openingRepositoryCommand == null)
                {
                    _openingRepositoryCommand = new CommandTemplate(obj =>
                    {
                        OpenRepositoryWindow();
                    });
                }

                return _openingRepositoryCommand;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OpenRepositoryWindow()
        {
            RepositoryView repositoryView = new RepositoryView();
            RepositoryViewModel repositoryViewModel = new RepositoryViewModel(Repository, _window);

            _window.Hide();

            repositoryView.DataContext = repositoryViewModel;
            repositoryView.ShowDialog();

            _window.Show();
        }

        public void OnPropertyChanged([CallerMemberName] string propertyName = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
