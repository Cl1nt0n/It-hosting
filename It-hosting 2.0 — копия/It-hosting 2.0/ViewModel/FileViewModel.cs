using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Collections.Immutable;

namespace It_hosting_2._0.ViewModel
{
    internal class FileViewModel
    {
        private List<StringViewModel> _fileStringsViewModels;

        public FileViewModel(string filepath)
        {
            _fileStringsViewModels = new List<StringViewModel>();
            StreamReader sr = new StreamReader(filepath);
            List<string> strings = new List<string>();
            strings = sr.ReadLine().Split("\n").ToList();


            foreach (var item in strings)
            {
                _fileStringsViewModels.Add(new StringViewModel("1." + item)); 
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public List<StringViewModel> FileStringsViewModel
        { 
            get => _fileStringsViewModels; 
            set
            {
                _fileStringsViewModels = value;
                OnPropertyChanged(nameof(FileStringsViewModel));
            }
        }

        public void OnPropertyChanged([CallerMemberName] string propertyName = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    internal class StringViewModel
    {
        private string _fileString;

        public StringViewModel(string str)
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
