using System.Collections.ObjectModel;
using System.ComponentModel;
using CarlssonsWPF.Model;

namespace CarlssonsWPF.ViewModel
{
    public class RemindersViewModel
    {
        private readonly ReminderViewService _reminderViewService;
        private ObservableCollection<RemindersData> _remindersData;

        public RemindersViewModel()
        {
            _reminderViewService = new ReminderViewService();
            RemindersDatas = _reminderViewService.GetRemindersData();

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
