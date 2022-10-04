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
using File = It_hosting_2._0.Models.DBModels.File;

namespace It_hosting_2._0.ViewModel
{
    internal class BranchViewModel
    {
        private Branch _branch;
        private CommandTemplate _uploadingFile;
        private List<FilesViewModel> _filesViewModels;

        public BranchViewModel(Window window, Branch branch)
        {
            Branch = branch;
            FilesViewModels = new List<FilesViewModel>();

            using (ithostingContext db = new ithostingContext())
            {
                List<File> files = new List<File>();
                files = db.Files.Where(x => x.BranchId == Branch.Id).ToList();

                foreach (var item in files)
                {
                    FilesViewModels.Add(new FilesViewModel(item));
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

        public List<FilesViewModel> FilesViewModels
        {
            get => _filesViewModels;
            set
            {
                _filesViewModels = value;
                OnPropertyChanged(nameof(FilesViewModels));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void UploadFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "(*.txt; *.cs)| *.txt; *.cs|All files (*.*)|*.*";
            // Открываем окно диалога с пользователем.
            if (openFileDialog.ShowDialog() == true)
            {
                using (ithostingContext db = new ithostingContext())
                {
                    string path = openFileDialog.FileName;
                    string title = openFileDialog.FileName.Split(@"\").Last();
                    StreamReader sr = new StreamReader(openFileDialog.FileName);

                    List<File> uploadedFiles = db.Files.Where(x => x.BranchId == Branch.Id && x.Title == title).ToList();

                    if (uploadedFiles.Count != 0)
                    {
                        File file = db.Files.Where(x => x.BranchId == Branch.Id && x.Title == title).First();
                        db.Commits.Add(new Commit { Text = file.Text, FileId = file.Id, CreatingDate = DateTime.Now });
                        db.Files.Where(x => x.BranchId == Branch.Id && x.Title == title).First().Text = sr.ReadToEnd();
                        db.SaveChanges();
                        return;
                    }

                    db.Files.Add(new File { BranchId = Branch.Id, Text = sr.ReadToEnd(), Title = openFileDialog.FileName.Split(@"\").Last() });
                    db.SaveChanges();
                }
            }
        }

        //private bool HasRegisteredFileExstension(string fileExstension)
        //{
        //    RegistryKey rkRoot = Registry.ClassesRoot;
        //    RegistryKey rkFileType = rkRoot.OpenSubKey(fileExstension);

        //    return rkFileType != null;
        //}

        public void OnPropertyChanged([CallerMemberName] string propertyName = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    internal class FilesViewModel
    {
        private File _file;
        private string _fileTtile;
        private CommandTemplate _openingFile;

        public FilesViewModel(File file)
        {
            _file = file;
            FileTitle = file.Title;
        }

        public File File
        {
            get => _file;
            set
            {
                _file = value;
                OnPropertyChanged(nameof(File));
            }
        }

        public string FileTitle
        {
            get => _fileTtile;
            set
            {
                _fileTtile = value;
                OnPropertyChanged(nameof(FileTitle));
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
            FileViewModel fileViewModel = new FileViewModel(File.Title, File.Text, File.Id);

            fileView.DataContext = fileViewModel;
            fileView.ShowDialog();
        }

        public void OnPropertyChanged([CallerMemberName] string propertyName = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
