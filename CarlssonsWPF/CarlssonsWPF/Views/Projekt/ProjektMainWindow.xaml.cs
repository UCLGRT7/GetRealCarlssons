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
using CarlssonsWPF.ViewModel;

namespace CarlssonsWPF.Views.Projekt
{

    public partial class ProjektMainWindow : Page
    {
        private ProjektMainPageViewModel projektMainPageViewModel;
        private Frame _frame;

        public ProjektMainWindow(Frame frame) 
        {
            InitializeComponent();
            _frame = frame;
            projektMainPageViewModel = new ProjektMainPageViewModel();
            DataContext = projektMainPageViewModel;
        }

        private void startPage_Click(object sender, RoutedEventArgs e)
        {
            _frame.Navigate(new StartPage(_frame));
        }
    }
}
