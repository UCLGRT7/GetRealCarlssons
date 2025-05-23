using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using CarlssonsWPF.Data.FileRepositories;
using CarlssonsWPF.Helpers;
using CarlssonsWPF.Model;
using CarlssonsWPF.ViewModel;

namespace CarlssonsWPF.Views.Kunde
{

    public partial class CustomerMainPage : Page
    {
        public ICommand BackCommand => CommonCommands.CancelAndGoBackCommand;
        private CustomerVM customerViewModel;
        private AddCustomerVM addCustomerViewModel;

        private Frame _frame;
        public CustomerMainPage(Frame frame)
        {
            InitializeComponent();
            _frame = frame;
            addCustomerViewModel = new AddCustomerVM();
            DataContext = addCustomerViewModel;
        }

        private void HomescreenButton_Click(object sender, RoutedEventArgs e)
        {
            _frame.Navigate(new StartPage(_frame));
        }
        private void AddCustomerButton_Click(object sender, RoutedEventArgs e)
        {
            _frame.Navigate(new AddCustomerPage(_frame));
        }
        private void KundeSearch_Click(object sender, RoutedEventArgs e)
        {
            _frame.Navigate(new CustomerSearch(_frame));
        }
        private void CustomerDataGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            if (e.EditAction == DataGridEditAction.Commit)
            {
                // Vent til redigeringen er færdig, før vi tilføjer
                Dispatcher.BeginInvoke(new Action(() =>
                {
                    var nyKunde = e.Row.Item as Customer;

                    if (nyKunde != null && !string.IsNullOrWhiteSpace(nyKunde.Name))
                    {
                        // Undgå duplikater (valgfrit tjek på navn)
                        if (addCustomerViewModel.customers.All(c => c.Name != nyKunde.Name))
                        {
                            addCustomerViewModel.AddCustomer(nyKunde);
                        }
                    }
                }), System.Windows.Threading.DispatcherPriority.Background);
            }
        }

        private void CustomerDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Get the DataGrid that was clicked, not a row
            var row = sender as DataGridRow;

            if (row?.DataContext is Customer selectedCustomer && !string.IsNullOrEmpty(selectedCustomer.Name))
            {
                var customerRepo = new FileCustomerRepository();
                var fullCustomer = customerRepo.GetByName(selectedCustomer.Name);

                if (fullCustomer != null)
                {
                    _frame.Navigate(new CustomerSpecPage(_frame, selectedCustomer));
                }
            }
        }

    }
}
