using System.Linq;
using System.Windows;
using System.Windows.Controls;
using CarlssonsWPF.ViewModel;

namespace CarlssonsWPF.Views.Kunde
{
    public partial class KundeSearch : Page
    {
        private Frame _frame;

        public KundeSearch(Frame frame)
        {
            InitializeComponent();
            _frame = frame;
            DataContext = new KundeSearchViewModel();
        }

        private void KundeSearch_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is KundeSearchViewModel viewModel)
            {
                viewModel.SearchCustomer();

                var searchResults = viewModel.Customers;

                if (searchResults.Any())
                {
                    _frame.Navigate(new SearchResults(_frame, searchResults));
                }
                else
                {
                    //MessageBox.Show("Ingen kunder fundet.");
                }
            }
        }

        private void GoBack_Click(object sender, RoutedEventArgs e)
        {
            _frame.Navigate(new KundeMainWindow(_frame));
        }
    }
}