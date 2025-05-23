using System.Linq;
using System.Windows;
using System.Windows.Controls;
using CarlssonsWPF.ViewModel;

namespace CarlssonsWPF.Views.Kunde
{
    public partial class CustomerSearch : Page
    {
        private Frame _frame;

        public CustomerSearch(Frame frame)
        {
            InitializeComponent();
            _frame = frame;
            DataContext = new CustomerSearchVM();
        }

        private void KundeSearch_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is CustomerSearchVM viewModel)
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
            _frame.Navigate(new CustomerMainPage(_frame));
        }
    }
}