using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using CarlssonsWPF.Model;
using CarlssonsWPF.Views.Projekt;


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
            //MessageBox.Show($"Antal kunder fundet: {customers.Count}");
        }

        private void KundeSearch_Click(object sender, RoutedEventArgs e)
        {
            //_frame.Navigate(new KundeSearch(_frame));
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
