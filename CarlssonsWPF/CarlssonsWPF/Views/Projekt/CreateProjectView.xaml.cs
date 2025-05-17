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
using CarlssonsWPF.ViewModel;

namespace CarlssonsWPF.Views.Projekt
{
    /// <summary>
    /// Interaction logic for CreateProjectView.xaml
    /// </summary>
    public partial class CreateProjectView : Page
    {
        private CreateProjectViewModel _createProjectViewModel;
        private Frame _frame;

        public CreateProjectView(Frame frame)
        {
            InitializeComponent();
            _createProjectViewModel = new CreateProjectViewModel();
            DataContext = _createProjectViewModel;
            _frame = frame;

            // Navigation callback fra ViewModel
            _createProjectViewModel.NavigateToViewProject = project =>
            {
                var viewPage = new ViewProjectView(project);
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
    }
}
