using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using CarlssonsWPF.Views.Projekt;

namespace CarlssonsWPF.Views.Kunde
{
    class CustomerSpecViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public ObservableCollection<Customer> customers { get; set; } = new ObservableCollection<Customer>();
        public partial class KundeSearch : Page
    {
        private Frame _frame;
        public KundeSearch(Frame frame)
        {
            InitializeComponent();
            _frame = frame;
        }

        private void KundeSearch_Click(object sender, RoutedEventArgs e)
        {
            _frame.Navigate(new SearchResults(_frame));
            string searchTerm = SearchBox.Text;
            FetchData(searchTerm);     

        }
        private void KundeMainWindow_Click(object sender, RoutedEventArgs e)
        {
            _frame.Navigate(new KundeMainWindow(_frame));
        }

    }
}
