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
    /// Interaction logic for CustomerSpecPage.xaml
    /// </summary>
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
