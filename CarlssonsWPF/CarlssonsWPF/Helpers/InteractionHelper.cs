
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using CarlssonsWPF.ViewModel;

namespace GetRealCarlssons.Helpers
{
    public static class InteractionHelper
    {
        public static void OnDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender is ListBox listBox && listBox.SelectedItem != null)
            {
                if (listBox.DataContext is SearchProjectViewModel vm && vm.OpenProjectCommand.CanExecute(null))
                {
                    vm.OpenProjectCommand.Execute(null);
                }
            }
        }
    }
}
