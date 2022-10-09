using It_hosting_2._0.Model.Tools;
using It_hosting_2._0.Models.DBModels;
using It_hosting_2._0.View;
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
    internal class PullRequestViewModel
    {
        private List<PRBranchesViewModel> _PRBranchesViewModels;
        private Repository _repository;
        private Window _window;

        public PullRequestViewModel(Window window, int repositoryId, int currentBranchId)
        {
            
            PRBranchesViewModels = new List<PRBranchesViewModel>();
            _window = window;

            using (ithostingContext db = new ithostingContext())
            {
                List<Branch> Branches = new List<Branch>();
                Branches = db.Branches.Where(x => x.RepositoryId == repositoryId && x.Id != currentBranchId).ToList();

                foreach (var item in Branches)
                {
                    PRBranchesViewModels.Add(new PRBranchesViewModel(item, _window));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public List<PRBranchesViewModel> PRBranchesViewModels
        {
            get => _PRBranchesViewModels;
            set
            {
                _PRBranchesViewModels = value;
                OnPropertyChanged(nameof(PRBranchesViewModels));
            }
        }

        public void OnPropertyChanged([CallerMemberName] string propertyName = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    internal class PRBranchesViewModel
    {
        private CommandTemplate _openingBranchView;
        private Branch _branch;
        private Window _window;

        public PRBranchesViewModel(Branch branch, Window window)
        {
            _branch = branch;
            _window = window;
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

        public CommandTemplate OpeningBranchView
        {
            get
            {
                if (_openingBranchView == null)
                {
                    _openingBranchView = new CommandTemplate(obj =>
                    {
                        OpenBranchView();
                    });
                }

                return _openingBranchView;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OpenBranchView()
        {
            BranchView branchView = new BranchView();
            BranchViewModel branchViewModel = new BranchViewModel(_window, Branch);

            branchView.DataContext = branchViewModel;
            branchView.ShowDialog();
        }

        public void OnPropertyChanged([CallerMemberName] string propertyName = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
