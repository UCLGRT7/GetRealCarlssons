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

            var viewModel = new CreateProjectViewModel();

            // 🔁 Navigation fra ViewModel til View
            viewModel.NavigateToViewProject = project =>
            {
                var viewPage = new ViewProjectView(project);
                NavigationService?.Navigate(viewPage);
            };

            DataContext = viewModel;
            _frame = frame;
        }

        private void AfsendtFelt_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void DatoFormatter_TextChanged(object sender, TextChangedEventArgs e)
        {
            var tb = sender as TextBox;
            if (tb == null) return;

            var raw = new string(tb.Text.Where(char.IsDigit).ToArray());

            if (raw.Length >= 6)
            {
                string dag = raw.Substring(0, 2);
                string måned = raw.Substring(2, 2);
                string år = raw.Length == 8 ? raw.Substring(6, 2) : raw.Substring(4, 2);
                tb.Text = $"{dag}/{måned}/{år}";
                tb.CaretIndex = tb.Text.Length;
            }
        }


        private void Opret_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TilbudGodkendtDato_PreviewTextInput(object sender, TextCompositionEventArgs e)
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



    }
}
