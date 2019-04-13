using System;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Thinkpower.Core.App.Commands
{
    /// <summary>
    /// 避免 Command 連續執行
    /// </summary>
    public class DelayCommand : ICommand
    {
        protected readonly Delegate _canExecute;
        protected readonly Delegate _execute;

        public DelayCommand(Action<object> execute)
        {
            if (execute == null)
                throw new ArgumentNullException(nameof(execute));
            _execute = execute;
        }

        protected DelayCommand(Delegate execute)
        {
            if (execute == null)
                throw new ArgumentNullException(nameof(execute));
            _execute = execute;
        }

        public DelayCommand(Action execute) : this(o => execute())
        {
            if (execute == null)
                throw new ArgumentNullException(nameof(execute));
        }

        public DelayCommand(Action<object> execute, Func<object, bool> canExecute) : this(execute)
        {
            if (canExecute == null)
                throw new ArgumentNullException(nameof(canExecute));
            _canExecute = canExecute;
        }

        public DelayCommand(Delegate execute, Delegate canExecute) : this(execute)
        {
            if (canExecute == null)
                throw new ArgumentNullException(nameof(canExecute));
            _canExecute = canExecute;
        }

        public DelayCommand(Action execute, Func<bool> canExecute) : this(o => execute(), o => canExecute())
        {
            if (execute == null)
                throw new ArgumentNullException(nameof(execute));
            if (canExecute == null)
                throw new ArgumentNullException(nameof(canExecute));
        }

        public bool CanExecute(object parameter)
        {
            if (_canExecute != null)
                return (bool)_canExecute.DynamicInvoke(parameter);
            return true;
        }
        public event EventHandler CanExecuteChanged;
        public virtual void Execute(object parameter)
        {
            lock (DelayFlag.DelayLockObject)
            {
                if (DelayFlag.Flag)
                {
                    DelayFlag.StartDelay();
                    _execute.DynamicInvoke(parameter);
                }
            }
        }
        public void ChangeCanExecute()
        {
            EventHandler changed = CanExecuteChanged;
            changed?.Invoke(this, EventArgs.Empty);
        }
    }

    public class DelayCommand<T> : DelayCommand
    {

        public DelayCommand(Action<T> execute) : base(execute)
        {

        }

        public DelayCommand(Action<T> execute, Func<T, bool> canExecute)
            : base(execute, canExecute)
        {
        }

        public override void Execute(object parameter)
        {
            if (this.IsValidParameter(parameter))
                base.Execute(parameter);
        }

        private bool IsValidParameter(object o)
        {
            if (o != null)
            {
                return o is T;
            }
            var t = typeof(T);
            if (Nullable.GetUnderlyingType(t) != null)
            {
                return true;
            }
            return !t.GetTypeInfo().IsValueType;
        }
    }

    public static class DelayFlag
    {
        public static object DelayLockObject = new object();
        public static volatile bool Flag = true;
        public async static void StartDelay()
        {
            DelayFlag.Flag = false;
            await Task.Delay(delaySec);
            DelayFlag.Flag = true;
        }

        private static int delaySec = 500;
    }
}
