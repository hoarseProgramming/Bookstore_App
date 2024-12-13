using System.Windows.Input;

namespace Bookstore_App.Presentation.Commands
{
    internal class AsyncDelegateCommand : ICommand
    {
        private readonly Func<object, Task> execute;
        private readonly Func<object?, bool>? canExecute;

        public event EventHandler? CanExecuteChanged;

        public AsyncDelegateCommand(Func<object, Task> execute, Func<object?, bool>? canExecute = null)
        {
            ArgumentNullException.ThrowIfNull(execute);
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public async void Execute(object? parameter)
        {
            await execute(parameter);
        }

        public bool CanExecute(object? parameter) => canExecute is null ? true : canExecute(parameter);

        public void RaiseCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);

    }
}
