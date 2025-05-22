
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using CarlssonsWPF.Model;
using CarlssonsWPF.ViewModel;
using CarlssonsWPF.Views;
using CarlssonsWPF.Views.Projekt;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CarlssonsWPF.ViewModel
{
    public class SearchProjectViewModel : INotifyPropertyChanged
    {
        public string CaseNumber { get; set; }
        public string Address { get; set; }
        public string Scope { get; set; }
        public string Deadline { get; set; }

        public ObservableCollection<ServiceEntry> Services { get; set; } = [];
        public ObservableCollection<Project> SearchResults { get; set; } = [];

        public Project SelectedProject { get; set; }

        public ICommand SearchCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        public ICommand OpenProjectCommand { get; set; }

        public SearchProjectViewModel()
        {
            for (int i = 0; i < 5; i++)
            {
                Services.Add(new ServiceEntry());
            }

            //SearchCommand = new RelayCommand(Search);
            CancelCommand = new RelayCommand(Cancel);
            //OpenProjectCommand = new RelayCommand(OpenSelectedProject);
        }


        private void Cancel()
        {
            throw new NotImplementedException();
        }

        //private void OpenSelectedProject()
        //{
        //    if (SelectedProject != null)
        //    {
        //        var view = new ViewProjectView
        //        {
        //            DataContext = new ViewProjectViewModel(SelectedProject)
        //        };
        //        view.Show();
        //    }
        //}

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
