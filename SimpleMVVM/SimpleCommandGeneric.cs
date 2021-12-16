using System.Windows.Input;

namespace SimpleMVVM
{
    public class SimpleCommand<T> : ICommand
    {
        private readonly Action<T> _execute;

        private readonly Predicate<T> _canExecute;

        private readonly List<WeakReference> _eventHandlers = new();

        private readonly object _syncRoot = new();

        public SimpleCommand(Action<T> execute) : this(execute, null)
        {
        }

        public SimpleCommand(Action<T> execute, Predicate<T>? canExecute)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute ?? (x => true);
        }

        public event EventHandler? CanExecuteChanged
        {
            add
            {
                if (_canExecute != null)
                {
                    lock (_syncRoot)
                    {
                        _eventHandlers.Add(new WeakReference(value));
                    }
                }
            }

            remove
            {
                if (_canExecute != null)
                {
                    lock (_syncRoot)
                    {
                        foreach (var thing in _eventHandlers)
                        {
                            var target = thing.Target;
                            if (target != null && (EventHandler)target == value)
                            {
                                _eventHandlers.Remove(thing);
                                break;
                            }
                        }
                    }
                }
            }
        }

        private IEnumerable<EventHandler> SafeCopyEventHandlerList()
        {
            lock (_syncRoot)
            {
                var toReturn = new List<EventHandler>();
                var deadEntries = new List<WeakReference>();

                foreach (var thing in _eventHandlers)
                {
                    if (!thing.IsAlive)
                    {
                        deadEntries.Add(thing);
                        continue;
                    }

                    if (thing.Target is EventHandler eventHandler)
                    {
                        toReturn.Add(eventHandler);
                    }
                }

                foreach (var weakReference in deadEntries)
                {
                    _eventHandlers.Remove(weakReference);
                }

                return toReturn;
            }
        }

        private void RaiseCanExecuteChanged(object sender)
        {
            var list = SafeCopyEventHandlerList();
            foreach (var eventHandler in list)
            {
                eventHandler(sender, EventArgs.Empty);
            }
        }

        public void Refresh(object sender) => RaiseCanExecuteChanged(sender);

        public bool CanExecute(object parameter) => _canExecute((T)parameter);

        public void Execute(object parameter) => _execute((T)parameter);
    }
}
