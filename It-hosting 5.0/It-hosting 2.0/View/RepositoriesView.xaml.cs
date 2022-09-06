using It_hosting_2._0.Models.DBModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace It_hosting_2._0.View
{
    /// <summary>
    /// Логика взаимодействия для RepositoriesView.xaml
    /// </summary>
    public partial class RepositoriesView : Window
    {
        private List<Button> _buttons = new List<Button>();
        private User _user;

        public RepositoriesView()
        {
            InitializeComponent();

            using (ithostingContext db = new ithostingContext())
            {
                List<Repository> repositories = db.Repositories.ToList();

                //foreach (var item in CategoryWrapPanel.Children)
                //{
                //    if (item is Button button)
                //    {
                //        _buttons.Add(button);
                //    }
                //}

                //User = user;
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

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
