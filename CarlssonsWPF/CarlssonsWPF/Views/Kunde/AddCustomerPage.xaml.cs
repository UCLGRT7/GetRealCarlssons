using System.Linq;
using System.Windows;
using System.Windows.Controls;
using CarlssonsWPF.ViewModel;

namespace CarlssonsWPF.Views.Kunde
{ 
    // Interaction logic for AddCustomerPage.xaml  
    public partial class AddCustomerPage : Page
    {
        private Frame _frame;
      
        private AddCustomerViewModel _addCustomerViewModel; // Add a private instance of AddCustomerViewModel

        public AddCustomerPage(Frame frame)
        {
            InitializeComponent();
            _frame = frame;
            _addCustomerViewModel = new AddCustomerViewModel(); // Initialize the CustomerViewModel instance  
           
            DataContext = _addCustomerViewModel;
        }

        private void BackFromAddCustomer_Click(object sender, RoutedEventArgs e)
        {
            _frame.Navigate(new KundeMainWindow(_frame));
        }

        private void AddCustomerAndGoToSpec_click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NameTextBox.Text))
            {
                MessageBox.Show("Du skal udfylde navn for at oprette en kunde.", "Manglende navn", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            _addCustomerViewModel.AddCustomer(); // Kald metoden direkte

            var addedCustomer = _addCustomerViewModel.customers.Last();
            _frame.Navigate(new CustomerSpecPage(_frame, addedCustomer));
        }
    }
}