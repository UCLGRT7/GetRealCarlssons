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
            _frame = frame;
            _createProjectViewModel = new CreateProjectViewModel();
            DataContext = _createProjectViewModel;
        }

        private void AfsendtFelt_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Opret_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
