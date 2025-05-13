
using System.Windows;
using System.Windows.Input;
using CarlssonsWPF.Model;
using CarlssonsWPF.Views;
using CarlssonsWPF.ViewModel;
using System.Windows.Controls;

namespace CarlssonsWPF.Helpers
{
    public static class ProjectNavigationHelper
    {

        public static void AttachDoubleClick(ListBox listBox)
        {
            listBox.MouseDoubleClick += (s, e) =>
            {
                if (listBox.SelectedItem is Project project)
                {
                    var window = new ViewProjectView
                    {
                        DataContext = new ViewProjectViewModel(project)
                    };
                    window.Show();
                }
            };
        }
    }
}
