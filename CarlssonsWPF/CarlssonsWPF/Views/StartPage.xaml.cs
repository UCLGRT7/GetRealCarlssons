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
using CarlssonsWPF.Views.Kunde;
using CarlssonsWPF.Views.Projekt;

namespace CarlssonsWPF.Views
{

    public partial class StartPage : Page
    {
        
        private Frame _frame;
        public StartPage(Frame frame)
        {
            InitializeComponent();
            _frame = frame;
        }
        private void project_Click(object sender, RoutedEventArgs e)
        {


            _frame.Navigate(new ProjektMainWindow(_frame));

        }

        private void customer_Click(object sender, RoutedEventArgs e)
        {


            _frame.Navigate(new KundeMainWindow(_frame));
        }

        private void reminder_Click(object sender, RoutedEventArgs e)
        {
            _frame.Navigate(new RemindersWindow(_frame));
        }
    }
}
