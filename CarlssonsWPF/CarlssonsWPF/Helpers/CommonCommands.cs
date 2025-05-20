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

    public static ICommand GoBackAndReload { get; } = new RelayCommand(ExecuteGoBackAndReload);

    private static void ExecuteGoBackAndReload(object parameter)
    {
        if (parameter is Frame frame)
        {
            if (frame.CanGoBack)
                frame.RemoveBackEntry(); // Fjern nuværende side hvis ønsket

            var currentPage = frame.Content as Page;
            var pageType = currentPage?.GetType();
            if (pageType != null)
            {
                var newPage = (Page)Activator.CreateInstance(pageType);

                if (newPage.DataContext is IReloadableViewModel vm)
                {
                    vm.LoadData(); // Kald på tværs af alle ViewModels
                }

                frame.Navigate(newPage);
            }
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