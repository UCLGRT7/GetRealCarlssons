using System.ComponentModel;
using System.Runtime.CompilerServices;

public class ComplexityEntry : INotifyPropertyChanged
{
    private int _value;
    public int Value
    {
        get => _value;
        set
        {
            if (_value != value)
            {
                _value = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Value));
            }
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string? name = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}