
using System;
using System.Windows.Input;

namespace WpfTreeView
{
    public class RelayCommand : ICommand
    {
        private Action mAction;

        public RelayCommand(Action action)
        {
            mAction = action;
        }
        /// <summary>
        /// The event fired when the <see cref="CanExecute(object)"/> value has changed
        /// </summary>
        public event EventHandler CanExecuteChanged = (sender, e) => { };

        public bool CanExecute(object parameter)
        {
            return true;
        }

                /// <summary>
        /// Execute the commands Action
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object parameter)
        {
            mAction();
        }
    }
}
