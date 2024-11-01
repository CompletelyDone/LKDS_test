using System.Windows.Input;

namespace ViewModels.Base
{
    public class RelayCommand : ICommand
    {
        private readonly Action action;
        private readonly Func<bool> canExecute;
        public RelayCommand(Action action, Func<bool> canExecute = null!)
        {
            this.action = action;
            this.canExecute = canExecute;
        }
        public event EventHandler? CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        public bool CanExecute(object? parameter)
        {
            return canExecute == null ? true : canExecute();
        }

        public void Execute(object? param)
        {
            if (CanExecute(param))
            {
                action.Invoke();
            }
        }
    }
    public class RelayCommand<T> : ICommand
    {
        private readonly Func<T?, Task> executeTask;
        private readonly Func<bool>? canExecute;
        public RelayCommand(Func<T?, Task> execute, Func<bool>? canExecute = null)
        {
            this.executeTask = execute;
            this.canExecute = canExecute;
        }
        public event EventHandler? CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        public async void Execute(object? parameter)
        {
            if (parameter is T typedParameter)
            {
                await executeTask(typedParameter);
            }
        }
        public bool CanExecute(object? parameter)
        {
            return canExecute?.Invoke() ?? true;
        }
    }
}
