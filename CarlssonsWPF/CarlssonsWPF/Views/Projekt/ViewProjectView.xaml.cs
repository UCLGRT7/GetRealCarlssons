using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
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
using CarlssonsWPF.Helpers;
using CarlssonsWPF.ViewModel;
using CarlssonsWPF.Views.Projekt;
using CarlssonsWPF.Views.Dialogs;

namespace CarlssonsWPF.Views.Projekt
{
    /// <summary>
    /// Interaction logic for ViewProjectView.xaml
    /// </summary>
    public partial class ViewProjectView : Page
    {

        private ViewProjectVM _viewProjectViewModel;


        private Frame _frame;

        public ViewProjectView(Frame frame, Project selectedProject, ServiceEntry selectedService = null)
        {
            InitializeComponent();
            _frame = frame;
            _viewProjectViewModel = new ViewProjectVM(selectedProject, selectedService);
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
            var projektMainWindow = new ProjectMainWindow(_frame);
            _frame.Navigated += Frame_Navigated;
            _frame.Navigate(projektMainWindow);
        }

        private void Frame_Navigated(object sender, NavigationEventArgs e)
        {
            if (e.Content is ProjectMainWindow projektMainWindow &&
                projektMainWindow.DataContext is ProjectMainPageVM vm)
            {
                vm.ReloadProjects();
            }

            ((Frame)sender).Navigated -= Frame_Navigated;
        }





    }
}
