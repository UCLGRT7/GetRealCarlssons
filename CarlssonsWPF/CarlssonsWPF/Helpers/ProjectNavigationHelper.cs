
using System.Windows;
using System.Windows.Input;
using GetRealCarlssons.Models;
using GetRealCarlssons.Views;
using GetRealCarlssons.ViewModel;
using System.Windows.Controls;

namespace GetRealCarlssons.Helpers
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
