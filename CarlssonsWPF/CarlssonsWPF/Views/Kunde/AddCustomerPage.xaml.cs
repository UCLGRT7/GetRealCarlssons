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
using CarlssonsWPF.Model;
using CarlssonsWPF.ViewModel;

namespace CarlssonsWPF.Views.Kunde
{
    /// <summary>  
    /// Interaction logic for AddCustomerPage.xaml  
    /// </summary>  
    public partial class AddCustomerPage : Page
    {
        private Frame _frame;
      
        private AddCustomerVM _addCustomerViewModel; // Add a private instance of AddCustomerVM




        public AddCustomerPage(Frame frame)
        {
            InitializeComponent();
            _frame = frame;
            _addCustomerViewModel = new AddCustomerVM(); // Initialize the CustomerVM instance  
           
            DataContext = _addCustomerViewModel;

        }

      
        private void BackFromAddCustomer_Click(object sender, RoutedEventArgs e)
        {
            _frame.Navigate(new CustomerMainPage(_frame));
        }

        private void AddCustomerAndGoToSpec_click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NameTextBox.Text))
            {
                MessageBox.Show("Du skal udfylde navn for at oprette en kunde.", "Manglende navn", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            _addCustomerViewModel.AddCustomer(); // kald metoden direkte

            var addedCustomer = _addCustomerViewModel.customers.Last();
            _frame.Navigate(new CustomerSpecPage(_frame, addedCustomer));
        }
    }
}
