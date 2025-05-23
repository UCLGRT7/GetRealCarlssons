using System.Windows;
using System.Windows.Controls;
using CarlssonsWPF.Model;
using CarlssonsWPF.ViewModel;

namespace CarlssonsWPF.Views.Kunde
{
    // Interaction logic for CustomerSpecPage.xaml
    public partial class CustomerSpecPage : Page
    {
        private Frame _frame;
        public CustomerSpecPage(Frame frame, Customer selectedCustomer)
        {
            InitializeComponent();
            _frame = frame;
            DataContext = new CustomerSpecViewModel(selectedCustomer);

        }
        private void BackFromCustomerSpec_click(object sender, RoutedEventArgs e)
        {
            _frame.Navigate(new KundeMainWindow(_frame));

        }

    }
}
