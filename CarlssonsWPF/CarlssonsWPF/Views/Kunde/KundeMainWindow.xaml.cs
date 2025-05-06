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
    

    public partial class KundeMainWindow : Page
    {
        private CustomerViewModel viewModel;

        private Frame _frame;
        public KundeMainWindow(Frame frame)
        {
            InitializeComponent();
            _frame = frame;
            viewModel = new CustomerViewModel();
            DataContext = viewModel;
        }


        private void HomescreenButton_Click(object sender, RoutedEventArgs e)
        {
            _frame.Navigate(new StartPage(_frame));
        }
        private void AddCustomerButton_Click(object sender, RoutedEventArgs e)
        {
            _frame.Navigate(new AddCustomerPage(_frame));
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
                        if (viewModel.customers.All(c => c.Name != nyKunde.Name))
                        {
                            viewModel.AddCustomer(nyKunde);
                        }
                    }
                }), System.Windows.Threading.DispatcherPriority.Background);
            }
        }
        private void CustomerDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (CustomerDataGrid.SelectedItem is Customer selectedCustomer)
            {
                _frame.Navigate(new CustomerSpecPage(_frame, selectedCustomer));
            }
        }

    }
}
