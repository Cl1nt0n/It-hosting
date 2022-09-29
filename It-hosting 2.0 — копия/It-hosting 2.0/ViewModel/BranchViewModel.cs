using It_hosting_2._0.Model.Tools;
using It_hosting_2._0.Models.DBModels;
using It_hosting_2._0.View;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;

namespace It_hosting_2._0.ViewModel
{
    internal class BranchViewModel
    {
        private Branch _branch;
        private CommandTemplate _uploadingFile;
        private CommandTemplate _openingFile;
        private string _fileTitle;
        private string _filePath;

        public BranchViewModel(Window window, Branch branch)
        {
            Branch = branch;

            using (ithostingContext db = new ithostingContext())
            {
                if (db.Branches.Where(x => x.Id == Branch.Id).First().File != null)
                {
                    FileTitle = Branch.File.Split(@"\").Last();
                    FilePath = Branch.File;
                }
            }
        }

        public Branch Branch
        {
            get => _branch;
            set
            {
                _branch = value;
                OnPropertyChanged(nameof(Branch));
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

        public string FilePath
        {
            get => _filePath;
            set
            {
                _filePath = value;
                OnPropertyChanged(nameof(FilePath));
            }
        }

        public CommandTemplate UploadingFile
        {
            get
            {
                if (_uploadingFile == null)
                {
                    _uploadingFile = new CommandTemplate(obj =>
                    {
                        UploadFile();
                    });
                }

                return _uploadingFile;
            }
        }

        public CommandTemplate OpeningFile
        {
            get
            {
                if (_openingFile == null)
                {
                    _openingFile = new CommandTemplate(obj =>
                    {
                        OpenFile();
                    });
                }

                return _openingFile;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OpenFile()
        {
            FileView fileView = new FileView();
            FileViewModel fileViewModel = new FileViewModel(FilePath);

            fileView.DataContext = fileViewModel;
            fileView.ShowDialog();
        }

        public void UploadFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "(*.txt; *.cs)| *.txt; *.cs|All files (*.*)|*.*";
            // Открываем окно диалога с пользователем.
            if (openFileDialog.ShowDialog() == true)
            {
                string[] extansions = { ".txt", ".cs" };
                if (extansions.Contains(Path.GetExtension(openFileDialog.FileName)))
                {
                    using (ithostingContext db = new ithostingContext())
                    {
                        db.Branches.Where(x => x.Id == Branch.Id).First().File = openFileDialog.FileName;
                        db.SaveChanges();
                    }
                }

                // Получаем расширение файла, выбранного пользователем.


                // Проверяем есть ли в ОС программа, которая может открыть
                // файл с указанным расширением.
            }
        }

        private bool HasRegisteredFileExstension(string fileExstension)
        {
            RegistryKey rkRoot = Registry.ClassesRoot;
            RegistryKey rkFileType = rkRoot.OpenSubKey(fileExstension);

            return rkFileType != null;
        }

        public void OnPropertyChanged([CallerMemberName] string propertyName = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
