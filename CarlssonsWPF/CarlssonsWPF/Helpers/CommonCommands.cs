using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;
using CarlssonsWPF.ViewModel;

public static class CommonCommands
{
    public static ICommand CancelAndGoBackCommand { get; } = new RelayCommand(ExecuteCancel);

    private static void ExecuteCancel(object parameter)
    {
        if (parameter is FrameworkElement fe)
        {
            var navService = NavigationService.GetNavigationService(fe);

            if (navService != null && navService.CanGoBack)
            {
                navService.GoBack();
            }
            else
            {
                Window.GetWindow(fe)?.Close();
            }
        }
    }
}