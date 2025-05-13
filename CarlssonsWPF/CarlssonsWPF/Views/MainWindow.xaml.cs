using System.Collections.ObjectModel;
using System.Text;
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
using CarlssonsWPF.ViewModel.IRepositories;
using CarlssonsWPF.Views.Projekt;

namespace CarlssonsWPF.Views
{
    public partial class MainWindow : Window
    {
       


        public MainWindow()
        {
            
            InitializeComponent();
            
            //_frame = new Frame(); // Initialize the _frame field to avoid null reference  
            MainFrame.Navigate(new StartPage(MainFrame));

        }

    }
}