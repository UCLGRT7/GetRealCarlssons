﻿using System;
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
using CarlssonsWPF.Views.Kunde;
using CarlssonsWPF.Views.Projekt;

namespace CarlssonsWPF.Views
{
    //private ProjectMainPageVM _projektMainPageViewModel;
    public partial class RemindersWindow : Page
    {
        private readonly RemindersVM remindersViewModel;
        private readonly Frame _frame;
        public RemindersWindow(Frame frame)
        {
            InitializeComponent();
            _frame = frame;
            remindersViewModel = new RemindersVM();
            DataContext = remindersViewModel;
        }

        private void projects_Click(object sender, RoutedEventArgs e)
        {
            _frame.Navigate(new ProjectMainWindow(_frame));
        }

        private void customer_Click(object sender, RoutedEventArgs e)
        {
            _frame.Navigate(new CustomerMainPage(_frame));
        }

        private void startPage_Click(object sender, RoutedEventArgs e)
        {
            _frame.Navigate(new StartPage(_frame));
        }


    }
}
