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
using CarlssonsWPF.Helpers;
using System.IO;
using CarlssonsWPF.Model;
using CarlssonsWPF.ViewModel;
using CarlssonsWPF.Views.Dialogs;

namespace CarlssonsWPF.Views.Projekt
{
    /// <summary>  
    /// Interaction logic for TestPage.xaml  
    /// </summary>  
    public partial class CreateProjectView : Page
    {
        private CreateProjectVM _createProjectViewModel;
        private Frame _frame;

        public CreateProjectView(Frame frame)
        {
            InitializeComponent();
            _createProjectViewModel = new CreateProjectVM();
            DataContext = _createProjectViewModel;
            _frame = frame;

            // Navigation callback fra ViewModel  
            _createProjectViewModel.NavigateToViewProject = project =>
            {
                project.CustomerName = _createProjectViewModel.SelectedCustomer?.Name;
                project.Deadline = _createProjectViewModel.Deadline;
                project.OfferSent = _createProjectViewModel.OfferSent;
                project.OfferApproved = _createProjectViewModel.OfferApproved;
                project.Paid = _createProjectViewModel.Paid;

                var viewPage = new ViewProjectView(_frame, project);
                NavigationService?.Navigate(viewPage);
            };
        }

        private void AfsendtFelt_TextChanged(object sender, TextChangedEventArgs e)
        {
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

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
        }

        private void TilbudGodkendtDato_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
        }

        private void TilbudGodkendtDato_TextChanged(object sender, TextChangedEventArgs e)
        {
        }

        private void GoBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationHelper.ExecuteGoBack();
        }

        private void CancelCreate_Click(object sender, RoutedEventArgs e)
        {
            if (this.NavigationService != null && this.NavigationService.CanGoBack)
            {
                this.NavigationService.GoBack(); // Går tilbage til forrige visning  
            }
            else
            {
                // Hvis der ikke er navigation, lukker vi hele vinduet (f.eks. hvis det blev vist i Window)  
                Window.GetWindow(this)?.Close();
            }

        }

        private void ServiceMenuButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new ServiceMenuDialog();
            dialog.ShowDialog();

            // Genindlæs ydelser fra fil
            _createProjectViewModel.Services.Clear();
            var path = System.IO.Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory[..AppDomain.CurrentDomain.BaseDirectory.IndexOf("bin")],
                "Data", "SavedFiles", "services.json"
            );
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                var reloaded = System.Text.Json.JsonSerializer.Deserialize<List<ServiceEntry>>(json) ?? new();
                foreach (var s in reloaded)
                    _createProjectViewModel.Services.Add(s);
            }
        }



    }
}

