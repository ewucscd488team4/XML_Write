using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace WpfApp1.ViewModels
{
    public class Command : ICommand
    {
        private Action Method { get; }

        public Command(Action method)
        {
            Method = method ?? throw new ArgumentNullException(nameof(method));
        }

        public bool CanExecute(object paremeter)
        {
            return true;
        }

        public void Execute(object paremeter) => Method?.Invoke();

        public event EventHandler CanExecuteChanged;
    }
}
