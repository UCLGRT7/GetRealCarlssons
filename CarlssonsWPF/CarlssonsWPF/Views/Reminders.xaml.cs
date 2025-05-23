using System.Windows;
using System.Windows.Controls;
using CarlssonsWPF.ViewModel;
using CarlssonsWPF.Views.Kunde;
using CarlssonsWPF.Views.Projekt;

namespace CarlssonsWPF.Views
{
    //private ProjektMainPageViewModel _projektMainPageViewModel;
    public partial class RemindersWindow : Page
    {
        private readonly RemindersViewModel remindersViewModel;
        private readonly Frame _frame;
        public RemindersWindow(Frame frame)
        {
            InitializeComponent();
            _frame = frame;
            remindersViewModel = new RemindersViewModel();
            DataContext = remindersViewModel;
        }

        private void projects_Click(object sender, RoutedEventArgs e)
        {
            _frame.Navigate(new ProjektMainWindow(_frame));
        }

        private void customer_Click(object sender, RoutedEventArgs e)
        {
            _frame.Navigate(new KundeMainWindow(_frame));
        }

        private void startPage_Click(object sender, RoutedEventArgs e)
        {
            _frame.Navigate(new StartPage(_frame));
        }

    }
}
