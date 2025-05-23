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
using CarlssonsWPF.Helpers;
using CarlssonsWPF.Model;
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

        private void SearchProject_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is SearchProjectViewModel searchProjectViewModel)
            {
                searchProjectViewModel.SearchProject();

                // Let the view model handle populating the collections
                // No need to manually update SearchResults here

                if (!searchProjectViewModel.SearchResults.Any())
                {
                    MessageBox.Show("Ingen projekter fundet");
                }
            }
        }

        private void ResultListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (ResultListBox.SelectedItem is Project selectedProject)
            {
                _frame.Navigate(new ViewProjectView(_frame, selectedProject));
            }
        }

        private void ResultListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}

