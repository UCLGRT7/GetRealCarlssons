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
        private readonly ReminderViewService _reminderViewService;
        private ObservableCollection<RemindersData> _reminders14DaysExeededData;

        public StartPageViewModel()
        {
            _reminderViewService = new ReminderViewService();
            Reminders14DaysExeededDatas = _reminderViewService.GetExeededby14Days();
        }

        public ObservableCollection<RemindersData> Reminders14DaysExeededDatas
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
