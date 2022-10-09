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
        private List<StringCommitFileViewModel> _commitFileStringsViewModels;
        private string _fileTitle;
        private int _fileId;

        public CommitFileViewModel(string text)
        {
            _commitFileStringsViewModels = new List<StringCommitFileViewModel>();

            List<string> strings = new List<string>();
            strings = text.Split("\n").ToList();

            for (int i = 0; i < strings.Count; i++)
            {
                _commitFileStringsViewModels.Add(new StringCommitFileViewModel($"{i + 1}.  " + strings[i]));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public List<StringCommitFileViewModel> CommitFileStringsViewModels
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
        private string _background;

        public StringCommitFileViewModel(string str)
        {
            _fileString = str;
            if (str.Contains("<span class='+'>") || str.Contains("</span>"))
            {

                Background = "LimeGreen";
            }
            else
            {
                if (str.Contains("<span class='-'>") && str.Contains("<span class='+'>") != true)
                {
                    Background = "Firebrick";
                }
                else
                {
                    Background = "DimGray";
                }
            }
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

        public string Background
        {
            get => _background;
            set
            {
                _background = value;
                OnPropertyChanged(nameof(Background));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
