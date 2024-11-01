using System.Windows.Input;

namespace ViewModels.Base
{
    public class RelayCommand<T> : ICommand
    {
        private readonly Func<T?, Task> execute;
        private readonly Func<bool>? canExecute;
        public RelayCommand(Func<T?, Task> execute, Func<bool>? canExecute = null)
        {
            this.execute = execute;
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
                await execute(typedParameter);
            }
        }
        public bool CanExecute(object? parameter)
        {
            return canExecute?.Invoke() ?? true;
        }
    }
}
