using It_hosting_2._0.Models.DBModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace It_hosting_2._0.ViewModel
{
    internal class CommitsViewModel
    {
        private ObservableCollection<CommitViewModel> _commitsViewModels;

        public CommitsViewModel(int fileId)
        {
            CommitsViewModels = new ObservableCollection<CommitViewModel>();
            using (ithostingContext db = new ithostingContext())
            {
                List<Commit> commits = db.Commits.Where(x => x.Id == fileId).ToList();
                foreach (Commit commit in commits)
                {
                    CommitsViewModels.Add(new CommitViewModel(commit.Text));
                }
            }
        }

        public ObservableCollection<CommitViewModel> CommitsViewModels 
        { 
            get => _commitsViewModels; 
            set
            {
                _commitsViewModels = value;
                OnPropertyChanged(nameof(CommitsViewModels));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    internal class CommitViewModel
    {
        private string _text;

        public CommitViewModel(string text)
        {
            Text = text;
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

        public string Text
        {
            get => _text;
            set
            {
                _text = value;
                OnPropertyChanged(nameof(Text));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
