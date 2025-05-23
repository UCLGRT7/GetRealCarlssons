using System.Windows;
using System.Windows.Controls;

namespace CarlssonsWPF.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(new StartPage(MainFrame));
        }
        public Frame AppFrame => MainFrame;
    }
}