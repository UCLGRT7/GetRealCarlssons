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
using CarlssonsWPF.ViewModel;
using CarlssonsWPF.Views.Projekt;

namespace CarlssonsWPF.Views.Kunde
{
    public partial class KundeSearch : Page
    {
        private Frame _frame;

        public KundeSearch(Frame frame)
        {
            InitializeComponent();
            _frame = frame;
            DataContext = new KundeSearchViewModel();
        }

        private void KundeSearch_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is KundeSearchViewModel viewModel)
            {
                viewModel.SearchCustomer();

                var searchResults = viewModel.Customers;

                if (searchResults.Any())
                {
                    _frame.Navigate(new SearchResults(_frame, searchResults));
                }
                else
                {
                    //MessageBox.Show("Ingen kunder fundet.");
                }
            }
        }

        private void GoBack_Click(object sender, RoutedEventArgs e)
        {
            _frame.Navigate(new KundeMainWindow(_frame));
        }
    }
}
