using System;
using System.Windows.Input;

namespace CarlssonsWPF.ViewModel
{
    public class RelayCommand : ICommand
    {
        private readonly Action? _executeNoParam;
        private readonly Action<object?>? _executeWithParam;
        private readonly Func<bool>? _canExecuteNoParam;
        private readonly Func<object?, bool>? _canExecuteWithParam;

        private readonly ICommand? _showWindowCommand;
        private readonly object? _canShowWindowCommand;
        private readonly Action? cancel; // Made nullable to resolve CS8618
        private Action<string> search;

        public object CanShowWindowCommand => _canShowWindowCommand;

        public event EventHandler? CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        // Brug til: new RelayCommand(() => ...)
        public RelayCommand(Action execute, Func<bool>? canExecute = null)
        {
            _executeNoParam = execute;
            _canExecuteNoParam = canExecute;
        }

        // Brug til: visning af vindue m.m.
        public RelayCommand(ICommand? showWindowCommand, object canShowWindowCommand)
        {
            _showWindowCommand = showWindowCommand;
            _canShowWindowCommand = canShowWindowCommand;
        }

        public RelayCommand(Action cancel)
        {
            this.cancel = cancel;
        }

        public RelayCommand(Action<string> search)
        {
            this.search = search;
        }

        public bool CanExecute(object? parameter)
        {
            if (_canExecuteWithParam != null)
                return _canExecuteWithParam(parameter);
            if (_canExecuteNoParam != null)
                return _canExecuteNoParam();
            return true;
        }

        public void Execute(object? parameter)
        {
            if (_executeWithParam != null)
                _executeWithParam(parameter);
            else
                _executeNoParam?.Invoke();
        }
    }
}


