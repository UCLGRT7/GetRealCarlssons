using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace GetRealCarlssons.ViewModel
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private void Cancel()
        {
            // Navigation tilbage til start
        }
    }
}
