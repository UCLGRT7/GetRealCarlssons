using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using CarlssonsWPF.Data.FileRepositories;
using CarlssonsWPF.Model;
using CarlssonsWPF.ViewModel;

namespace CarlssonsWPF.Views.Projekt
{
 
    public partial class SearchProjectView : Page
    {
        private SearchProjectVM _searchProjectViewModel;
        private Frame _frame;

        public SearchProjectView(Frame frame)
        {
            InitializeComponent();
            _frame = frame;
            _searchProjectViewModel = new SearchProjectVM();
            DataContext = _searchProjectViewModel;
        }

        private void GoBack_Click(object sender, RoutedEventArgs e)
        {
            if (_frame?.CanGoBack == true)
                _frame.GoBack();
        }

        private void SearchProject_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is SearchProjectVM searchProjectViewModel)
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
            else
            {
                MessageBox.Show("Kunne ikke finde det valgte projekt", "Validering", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void ResultListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}

