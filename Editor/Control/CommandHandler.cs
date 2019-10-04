using System;
using System.Windows.Input;

namespace Editor.Control
{
    internal class CommandHandler : ICommand
    {
        Action Click;
        public CommandHandler(Action click)
        {
            Click = click;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            if (parameter is Tab)
                return true;
            else
                return false;
        }

        public void Execute(object parameter)
        {
            CanExecuteChanged?.Invoke(parameter,null);
            Click.Invoke();
        }
    }
}