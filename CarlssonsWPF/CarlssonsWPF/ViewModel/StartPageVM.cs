using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using CarlssonsWPF.Model;

namespace CarlssonsWPF.ViewModel
{
    public class StartPageVM : INotifyPropertyChanged
    {
        private readonly ReminderViewService _reminderViewService;
        private ObservableCollection<RemindersData> _reminders14DaysExeededData;

        public StartPageVM()
        {
            _reminderViewService = new ReminderViewService();
            var reminders = _reminderViewService.GetExeededby14Days();
            Reminders14DaysExeededDatasView = CollectionViewSource.GetDefaultView(reminders);

            // Sorter efter DaysPassed ved opstart
            Reminders14DaysExeededDatasView.SortDescriptions.Clear();
            Reminders14DaysExeededDatasView.SortDescriptions.Add(
                new SortDescription(nameof(RemindersData.DaysPassed), ListSortDirection.Descending));

        }
        private ICollectionView _reminders14DaysExeededDatasView;
        public ICollectionView Reminders14DaysExeededDatasView
        {
            get => _reminders14DaysExeededDatasView;
            set
            {
                _reminders14DaysExeededDatasView = value;
                OnPropertyChanged(nameof(Reminders14DaysExeededDatasView));
            }
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
