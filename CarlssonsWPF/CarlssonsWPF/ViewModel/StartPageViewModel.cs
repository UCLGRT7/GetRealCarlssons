using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarlssonsWPF.Data.FileRepositories;
using CarlssonsWPF.Model;
using CarlssonsWPF.ViewModel.IRepositories;

namespace CarlssonsWPF.ViewModel
{
    public class StartPageViewModel
    {
        private readonly Reminders14DaysExeededViewService _reminders14DaysExeededViewService;
        private ObservableCollection<Reminders14DaysExeededData> _reminders14DaysExeededData;

        public StartPageViewModel()
        {
            _reminders14DaysExeededViewService = new Reminders14DaysExeededViewService();
            Reminders14DaysExeededDatas = _reminders14DaysExeededViewService.GetExeededby14Days();
        }

        public ObservableCollection<Reminders14DaysExeededData> Reminders14DaysExeededDatas
        {
            get => _reminders14DaysExeededData;
            set
            {
                _reminders14DaysExeededData = value;
                OnPropertyChanged(nameof(Reminders14DaysExeededDatas));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
