using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using CarlssonsWPF.Model;
using CarlssonsWPF.ViewModel;
using CarlssonsWPF.Views.Dialogs;

namespace CarlssonsWPF.Views.Projekt
{

    // Interaction logic for ViewProjectView.xaml
    public partial class ViewProjectView : Page
    {

        private ViewProjectViewModel _viewProjectViewModel;

        private Frame _frame;

        public ViewProjectView(Frame frame, Project selectedProject, ServiceEntry selectedService = null)
        {
            InitializeComponent();
            _frame = frame;
            _viewProjectViewModel = new ViewProjectViewModel(selectedProject, selectedService);
            DataContext = _viewProjectViewModel;
        }

        private void DateAutoFormatter(object sender, TextCompositionEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                int caret = textBox.CaretIndex;
                string raw = textBox.Text.Insert(caret, e.Text).Replace("/", "");

                if (raw.Length > 4)
                    raw = raw.Insert(4, "/");
                if (raw.Length > 2)
                    raw = raw.Insert(2, "/");

                textBox.Text = raw;
                textBox.CaretIndex = textBox.Text.Length;
                e.Handled = true;
            }
        }

        private void GoBack_Click(object sender, RoutedEventArgs e)
        {
            NavigateBackToProjectMain();
        }


        private void ServiceMenuButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new ServiceMenuDialog();
            dialog.ShowDialog();

            // Genindlæs ydelser fra fil
            _viewProjectViewModel.Services.Clear();
            var path = System.IO.Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory[..AppDomain.CurrentDomain.BaseDirectory.IndexOf("bin")],
                "Data", "SavedFiles", "services.json"
            );
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                var reloaded = System.Text.Json.JsonSerializer.Deserialize<List<ServiceEntry>>(json) ?? new();
                foreach (var s in reloaded)
                    _viewProjectViewModel.Services.Add(s);
            }
        }


        private void NavigateBackToProjectMain()
        {
            var projektMainWindow = new ProjektMainWindow(_frame);
            _frame.Navigated += Frame_Navigated;
            _frame.Navigate(projektMainWindow);
        }

        private void Frame_Navigated(object sender, NavigationEventArgs e)
        {
            if (e.Content is ProjektMainWindow projektMainWindow &&
                projektMainWindow.DataContext is ProjektMainPageViewModel vm)
            {
                vm.ReloadProjects();
            }

            ((Frame)sender).Navigated -= Frame_Navigated;
        }

    }
}