using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using CarlssonsWPF.Model;

namespace CarlssonsWPF.Views.Kunde
{
    public partial class SearchResults : Page
    {
        private Frame _frame;
        public ObservableCollection<Customer> Customers { get; set; }


        public SearchResults(Frame frame, ObservableCollection<Customer> customers)
        {
            InitializeComponent();
            _frame = frame;
            Customers = customers;
            DataContext = this;
        }

        private void SearchCustomerDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (CustomerDataGrid.SelectedItem is Customer selectedCustomer)
            {
                // Navigér til CustomerSpecPage med den valgte kunde
                _frame.Navigate(new CustomerSpecPage(_frame, selectedCustomer));
            }
        }
        private void BackFromSearchResults_Click(object sender, RoutedEventArgs e)
        {
            _frame.Navigate(new KundeMainWindow(_frame));
        }

    }
}