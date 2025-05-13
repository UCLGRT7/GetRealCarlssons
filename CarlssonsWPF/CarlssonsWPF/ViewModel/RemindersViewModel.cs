using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarlssonsWPF.ViewModel
{
    public class RemindersViewModel
    {
        private readonly ReminderViewService _reminderViewService;
        private ObservableCollection<RemindersData> _remindersData;

        public RemindersViewModel()
        {
            _reminderViewService = new ReminderViewService(); // Initialize ReminderViewService
            RemindersDatas = _reminderViewService.GetRemindersData(); // Fetch data from ReminderViewService
        }

        public ObservableCollection<RemindersData> RemindersDatas
        {
            get => _remindersData;
            set
            {
                _remindersData = value;
                OnPropertyChanged(nameof(RemindersDatas));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
