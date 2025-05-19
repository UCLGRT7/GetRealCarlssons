using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using CarlssonsWPF.ViewModel;
using CarlssonsWPF.Model;

namespace CarlssonsWPF.Helpers
{
    public static class CommonCommands
    {
        public static ICommand GoBack { get; } = new RelayCommand(ExecuteGoBack);
        public static ICommand Create { get; } = new RelayCommand(ExecuteCreate);

        private static void ExecuteGoBack(object parameter)
        {
            if (parameter is FrameworkElement fe)
            {
                var navService = NavigationService.GetNavigationService(fe);

                if (navService != null && navService.CanGoBack)
                {
                    navService.GoBack();
                }
            }
        }

        private static void ExecuteCreate(object parameter)
        {
            var frame = Application.Current.MainWindow?.FindName("MainFrame") as Frame;

            switch (parameter)
            {
                case CreateProjectViewModel projectVm:
                    {
                        projectVm.CreateProject();
                        var latestProject = projectVm.projects.LastOrDefault();
                        if (latestProject == null)
                        {
                            MessageBox.Show("Projekt blev ikke oprettet korrekt.");
                            return;
                        }

                        if (frame != null)
                        {
                            var view = new Views.Projekt.ViewProjectView(frame, latestProject);
                            frame.Navigate(view);
                        }

                        break;
                    }

                case AddCustomerViewModel customerVm:
                    {
                        customerVm.AddCustomer();
                        var latestCustomer = customerVm.customers.LastOrDefault();
                        if (latestCustomer == null)
                        {
                            MessageBox.Show("Kunde blev ikke oprettet korrekt.");
                            return;
                        }

                        if (frame != null)
                        {
                            var view = new Views.Kunde.CustomerSpecPage(frame, latestCustomer);
                            frame.Navigate(view);
                        }

                        break;
                    }

                default:
                    MessageBox.Show("Ugyldig ViewModel – kun CreateProjectViewModel eller AddCustomerViewModel understøttes.");
                    break;
            }
        }

    }
}