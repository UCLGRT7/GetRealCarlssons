using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CarlssonsWPF.ViewModel;
using CarlssonsWPF.Views.Dialogs;


namespace CarlssonsWPF.Views.Projekt
{
    /// <summary>
    /// Interaction logic for SearchProjectView.xaml
    /// </summary>
    public partial class SearchProjectView : Page
    {
        private SearchProjectViewModel _searchProjectViewModel;
        private Frame _frame;

        public SearchProjectView(Frame frame)
        {
            InitializeComponent();
            _frame = frame;
            _searchProjectViewModel = new SearchProjectViewModel();
            DataContext = _searchProjectViewModel;
        }

        private void GoBack_Click(object sender, RoutedEventArgs e)
        {
            if (_frame?.CanGoBack == true)
                _frame.GoBack();
        }
    }
}
