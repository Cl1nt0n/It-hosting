using It_hosting_2._0.Model.Tools;
using It_hosting_2._0.Models.DBModels;
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
    internal class CreatingRepositoryViewModel
    {
        private CommandTemplate _creatingRepositoryCommand;
        private string _title;
        private string _description;
        private bool _isPrivate;
        private Window _window;
        private User _user;

        public CreatingRepositoryViewModel(Window window, User user)
        {
            _window = window;
            User = user;
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

        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                OnPropertyChanged(nameof(Description));
            }
        }

        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                OnPropertyChanged(nameof(Title));
            }
        }

        public bool IsPrivate
        {
            get => _isPrivate;
            set
            {
                _isPrivate = value;
                OnPropertyChanged(nameof(IsPrivate));
            }
        }

        public CommandTemplate CreatingRepositoryCommand
        {
            get
            {
                if (_creatingRepositoryCommand == null && string.IsNullOrWhiteSpace(_title))
                {
                    _creatingRepositoryCommand = new CommandTemplate(obj =>
                    {
                        CreateRepository();
                    });
                }

                return _creatingRepositoryCommand;
            }
        }

        private void CreateRepository()
        {
            using (ithostingContext db = new ithostingContext())
            {
                Repository repository = new Repository();
                Branch branch = new Branch();
                repository = new Repository { UserId = _user.Id, Title = _title, Description = _description, IsPrivate = _isPrivate, MainBranchId = branch.Id };
                repository.MainBranchId = branch.Id;
                db.Repositories.Add(repository);
                db.SaveChanges();

                MessageBox.Show("Репозиторий создан.");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
