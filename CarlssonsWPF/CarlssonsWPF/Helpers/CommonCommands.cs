using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using CarlssonsWPF.ViewModel;
using CarlssonsWPF.Model;
using CarlssonsWPF.Helpers;

namespace CarlssonsWPF.Helpers
{



    public static class CommonCommands
    {
        private static readonly ICommand cancelAndGoBackCommand = new RelayCommand(ExecuteCancel);

        public static ICommand CancelAndGoBackCommand => cancelAndGoBackCommand;
        public static ICommand GoBack => new RelayCommand(ExecuteGoBack);


        private static void ExecuteGoBack(object? parameter)
        {

            if (parameter is Frame frame)
            {

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
                
            }
        }

        private static void ExecuteCancel(object? parameter)
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
}