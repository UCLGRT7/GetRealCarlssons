using System.Windows;
using CarlssonsWPF.Views;

namespace CarlssonsWPF.Helpers
{
    public static class NavigationHelper
    {
        public static void ExecuteGoBack()
        {
            var frame = ((MainWindow)Application.Current.MainWindow).AppFrame;

            if (CommonCommands.GoBack.CanExecute(frame))
                CommonCommands.GoBack.Execute(frame);
        }
    }
}
