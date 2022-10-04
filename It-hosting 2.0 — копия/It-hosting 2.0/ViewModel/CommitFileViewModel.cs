using It_hosting_2._0.Model.Tools;
using It_hosting_2._0.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace It_hosting_2._0.ViewModel
{
    internal class CommitFileViewModel
    {
        private List<StringViewModel> _commitFileStringsViewModels;
        private string _fileTitle;
        private int _fileId;

        public CommitFileViewModel(string text)
        {
            _commitFileStringsViewModels = new List<StringViewModel>();

            List<string> strings = new List<string>();
            strings = text.Split("\n").ToList();

            for (int i = 0; i < strings.Count; i++)
            {
                _commitFileStringsViewModels.Add(new StringViewModel($"{i + 1}.  " + strings[i]));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public List<StringViewModel> CommitFileStringsViewModels
        {
            get => _commitFileStringsViewModels;
            set
            {
                _commitFileStringsViewModels = value;
                OnPropertyChanged(nameof(CommitFileStringsViewModels));
            }
        }

        public string FileTitle
        {
            get => _fileTitle;
            set
            {
                _fileTitle = value;
                OnPropertyChanged(nameof(FileTitle));
            }
        }

        public int FileId
        {
            get => _fileId;
            set
            {
                _fileId = value;
                OnPropertyChanged(nameof(FileId));
            }
        }

        public void OnPropertyChanged([CallerMemberName] string propertyName = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    internal class StringCommitFileViewModel
    {
        private string _fileString;

        public StringCommitFileViewModel(string str)
        {
            _fileString = str;
        }

        public string FileString
        {
            get => _fileString;
            set
            {
                _fileString = value;
                OnPropertyChanged(nameof(FileString));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
