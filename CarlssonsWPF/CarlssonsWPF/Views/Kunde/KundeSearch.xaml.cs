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

        //private void KundeSearch_Click(object sender, RoutedEventArgs e)
        //{
        //    if (_Navn != null && _Adresse != null && _Postnummer != null && _By != null && _Tlfnummer != null && _Email != null)
        //    {
        //        string searchTerm = _Navn.Text;
        //        string searchTerm2 = _Adresse.Text;
        //        string searchTerm3 = _Postnummer.Text;
        //        string searchTerm4 = _By.Text;
        //        string searchTerm5 = _Tlfnummer.Text;
        //        string searchTerm6 = _Email.Text;

        //        // Så alle søgefelter er udfyldt
        //        if (!string.IsNullOrWhiteSpace(searchTerm) &&
        //            !string.IsNullOrWhiteSpace(searchTerm2) &&
        //            !string.IsNullOrWhiteSpace(searchTerm3) &&
        //            !string.IsNullOrWhiteSpace(searchTerm4) &&
        //            !string.IsNullOrWhiteSpace(searchTerm5) &&
        //            !string.IsNullOrWhiteSpace(searchTerm6))
        //        {
        //            _frame.Navigate(new KundeSearch(_frame));
        //        }
        //        else
        //        {
        //            MessageBox.Show("Indtast tekst");
        //        }
        //    }
        //    else
        //    {
        //        MessageBox.Show("Søgefeltet er ikke initialiseret.");
        //    }
        //}

        private void FetchData(string searchTerm)
        {
            throw new NotImplementedException();
        }

        private void KundeMainWindow_Click(object sender, RoutedEventArgs e)
        {
            _frame.Navigate(new KundeMainWindow(_frame));
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
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
                    MessageBox.Show("Ingen kunder fundet.");
                }
            }
        }


    }
}
