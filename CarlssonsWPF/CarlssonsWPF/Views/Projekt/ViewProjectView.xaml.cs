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
using CarlssonsWPF.Model;
using CarlssonsWPF.ViewModel;
using CarlssonsWPF.Views.Projekt;

namespace CarlssonsWPF.Views.Projekt
{
    /// <summary>
    /// Interaction logic for ViewProjectView.xaml
    /// </summary>
    public partial class ViewProjectView : Page
    {

        private Frame _frame;

        public ViewProjectView(Frame frame, Project selectedProject)
        {
            InitializeComponent();
            _frame = frame;
            DataContext = new ViewProjectViewModel(selectedProject);
        }
        public ViewProjectView(Project project)
        {
            InitializeComponent();
            DataContext = new ViewProjectViewModel(project);
        }
    }
}
