using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Dexterity.Bridge.Extended
{
    public class WindowExtended
    {
        private class WindowWrapper : WindowProxy
        {
            public WindowWrapper(Window window) : base(window) { }
        }

        public event EventHandler ActivateAfterOriginal { add => EventDescriptions.ActivateAfterOriginal?.Subscribe(value); remove => EventDescriptions.ActivateAfterOriginal?.Unsubscribe(value); }
        public event CancelEventHandler ActivateBeforeOriginal { add => EventDescriptions.ActivateBeforeOriginal?.Subscribe(value); remove => EventDescriptions.ActivateBeforeOriginal?.Unsubscribe(value); }
        public event EventHandler<AfterModalDialogEventArgs> AfterModalDialog { add => EventDescriptions.AfterModalDialog?.Subscribe(value); remove => EventDescriptions.AfterModalDialog?.Unsubscribe(value); }
        public event EventHandler<BeforeModalDialogEventArgs> BeforeModalDialog { add => EventDescriptions.BeforeModalDialog?.Subscribe(value); remove => EventDescriptions.BeforeModalDialog?.Unsubscribe(value); }
        public event EventHandler CloseAfterOriginal { add => EventDescriptions.CloseAfterOriginal?.Subscribe(value); remove => EventDescriptions.CloseAfterOriginal?.Unsubscribe(value); }
        public event CancelEventHandler CloseBeforeOriginal { add => EventDescriptions.CloseBeforeOriginal?.Subscribe(value); remove => EventDescriptions.CloseBeforeOriginal?.Unsubscribe(value); }
        public event EventHandler OpenAfterOriginal { add => EventDescriptions.OpenAfterOriginal?.Subscribe(value); remove => EventDescriptions.OpenAfterOriginal?.Unsubscribe(value); }
        public event CancelEventHandler OpenBeforeOriginal { add => EventDescriptions.OpenBeforeOriginal?.Subscribe(value); remove => EventDescriptions.OpenBeforeOriginal?.Unsubscribe(value); }
        public event EventHandler PrintAfterOriginal { add => EventDescriptions.PrintAfterOriginal?.Subscribe(value); remove => EventDescriptions.PrintAfterOriginal?.Unsubscribe(value); }
        public event CancelEventHandler PrintBeforeOriginal { add => EventDescriptions.PrintBeforeOriginal?.Subscribe(value); remove => EventDescriptions.PrintBeforeOriginal?.Unsubscribe(value); }


        public Window Window { get; }

        public EventDescriptions.Window EventDescriptions { get; }

        public bool IsChanged => Window.IsChanged;

        public bool IsOpen => Window.IsOpen;

        public readonly bool CanClose;

        private readonly Action doClose;

        internal WindowExtended(Window window)
        {
            Window = window;

            CanClose = (doClose = TryMethod("Close")) != null;

            EventDescriptions = new EventDescriptions.Window(
                EventDescription.EventHandler(o => window.ActivateAfterOriginal += o, window),
                EventDescription.CancelEventHandler(o => window.ActivateBeforeOriginal += o, window),
                EventDescription.EventHandler<AfterModalDialogEventArgs>(o=>TriggerManager.DialogTriggers.RegisterTrigger(new WindowWrapper(window), AttachType.After, o), window),
                EventDescription.EventHandler<BeforeModalDialogEventArgs>(o => TriggerManager.DialogTriggers.RegisterTrigger(new WindowWrapper(window), AttachType.Before, o), window),
                EventDescription.EventHandler(o => window.CloseAfterOriginal += o, window),
                EventDescription.CancelEventHandler(o => window.CloseBeforeOriginal += o, window),
                EventDescription.EventHandler(o => window.OpenAfterOriginal += o, window),
                EventDescription.CancelEventHandler(o => window.OpenBeforeOriginal += o, window),
                EventDescription.EventHandler(o => window.PrintAfterOriginal += o, window),
                EventDescription.CancelEventHandler(o => window.PrintBeforeOriginal += o, window)
            );
        }

        public void Open() => Window.Open();

        public void PullFocus() => Window.PullFocus();

        public bool TryClose(out Result results) => TryAction(doClose, out results);

        private bool TryAction(Action action, out Result results, [CallerMemberName]string callerMember = default)
        {
            if (action is null)
                return (results = new Result($"Field does have a {callerMember} method"));

            try
            {
                action.Invoke();
                return (results = Result.SUCCESS);
            }
            catch (Exception ex) { return (results = new Result(ex.Message)); }
        }

        private Action TryMethod(string method)
        {
            try
            {
                var meth = Window.GetType().GetMethod(method);
                if (meth != null)
                    return (Action)meth.CreateDelegate(typeof(Action), Window);
            }
            catch { }

            return null;
        }
    }
}
