using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using CarlssonsWPF.ViewModel;

namespace CarlssonsWPF.Helpers;

public static class CommonCommands
{
    public static ICommand CancelAndGoBackCommand { get; } = new RelayCommand(ExecuteCancel);

    public static ICommand GoBack => new RelayCommand(ExecuteGoBack);


    private static void ExecuteGoBack(object parameter)
    {
        System.Diagnostics.Debug.WriteLine($"PARAM: {parameter?.GetType().Name}");

        if (parameter is Frame frame)
        {
            System.Diagnostics.Debug.WriteLine($"CanGoBack: {frame.CanGoBack}");

            if (frame.CanGoBack)
            {
                frame.GoBack();

                if (frame.Content is Page newPage && newPage.DataContext is IReloadableViewModel vm)
                {
                    vm.LoadData();
                }
            }
        }
        else
        {
            System.Diagnostics.Debug.WriteLine("Parameter is not a Frame");
        }
    }


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