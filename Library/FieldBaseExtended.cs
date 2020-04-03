using System;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Microsoft.Dexterity.Bridge.Extended
{
    public class FieldBaseExtended
    {
        public object Value { get => getValue?.Invoke() ?? throw new InvalidOperationException(); set { if (setValue is null) throw new InvalidOperationException(); setValue(value); } }

        public event EventHandler ClickAfterOriginal { add => EventDescriptions.ClickAfterOriginal?.Subscribe(value); remove => EventDescriptions.ClickAfterOriginal?.Unsubscribe(value); }
        public event CancelEventHandler ClickBeforeOriginal { add => EventDescriptions.ClickBeforeOriginal?.Subscribe(value); remove => EventDescriptions.ClickBeforeOriginal?.Unsubscribe(value); }

        public event EventHandler Change { add => EventDescriptions.Change?.Subscribe(value); remove => EventDescriptions.Change?.Unsubscribe(value); }
        public event EventHandler EnterAfterOriginal { add => EventDescriptions.EnterAfterOriginal?.Subscribe(value); remove => EventDescriptions.EnterAfterOriginal?.Unsubscribe(value); }
        public event CancelEventHandler EnterBeforeOriginal { add => EventDescriptions.EnterBeforeOriginal?.Subscribe(value); remove => EventDescriptions.EnterBeforeOriginal?.Unsubscribe(value); }
        public event EventHandler LeaveAfterOriginal { add => EventDescriptions.LeaveAfterOriginal?.Subscribe(value); remove => EventDescriptions.LeaveAfterOriginal?.Unsubscribe(value); }
        public event CancelEventHandler LeaveBeforeOriginal { add => EventDescriptions.LeaveBeforeOriginal?.Subscribe(value); remove => EventDescriptions.LeaveBeforeOriginal?.Unsubscribe(value); }
        public event EventHandler ValidateAfterOriginal { add => EventDescriptions.ValidateAfterOriginal?.Subscribe(value); remove => EventDescriptions.ValidateAfterOriginal?.Unsubscribe(value); }
        public event CancelEventHandler ValidateBeforeOriginal { add => EventDescriptions.ValidateBeforeOriginal?.Subscribe(value); remove => EventDescriptions.ValidateBeforeOriginal?.Unsubscribe(value); }

        public readonly bool CanGetValue, CanSetValue, CanRunValidate, CanShow, CanHide, CanLock, CanUnlock, CanEnable, CanDisable, CanFocus;

        public FieldBase Field { get; }

        public EventDescriptions.Field EventDescriptions { get; }

        private readonly Func<object> getValue;
        private readonly Action<object> setValue;

        private readonly Action doRunValidate, doShow, doHide, doLock, doUnlock, doEnable, doDisable, doFocus;


        internal FieldBaseExtended(FieldBase field, bool enableEvents = true)
        {
            Field = field;

            CanGetValue = (getValue = TryRegisterGetProperty(nameof(Value))) != null;
            CanSetValue = (setValue = TryRegisterSetProperty(nameof(Value))) != null;

            CanRunValidate = (doRunValidate = TryMethod("RunValidate")) != null;
            CanShow = (doShow = TryMethod("Show")) != null;
            CanHide = (doHide = TryMethod("Hide")) != null;
            CanLock = (doLock = TryMethod("Lock")) != null;
            CanUnlock = (doUnlock = TryMethod("Unlock")) != null;
            CanEnable = (doEnable = TryMethod("Enable")) != null;
            CanDisable = (doDisable = TryMethod("Disable")) != null;
            CanFocus = (doFocus = TryMethod("Focus")) != null;

            EventDescriptions = Extended.EventDescriptions.Field.Empty;

            if (!enableEvents)
                return;

            EventDescriptions = new EventDescriptions.Field(
                    EventDescription.EventHandler(TryLocateEvent(nameof(ClickAfterOriginal)), field),
                    EventDescription.CancelEventHandler(TryLocateEvent(nameof(ClickBeforeOriginal)), field),
                    EventDescription.EventHandler(TryLocateEvent(nameof(Change)), field),
                    EventDescription.EventHandler(TryLocateEvent(nameof(EnterAfterOriginal)), field),
                    EventDescription.CancelEventHandler(TryLocateEvent(nameof(EnterBeforeOriginal)), field),
                    EventDescription.EventHandler(TryLocateEvent(nameof(LeaveAfterOriginal)), field),
                    EventDescription.CancelEventHandler(TryLocateEvent(nameof(LeaveBeforeOriginal)), field),
                    EventDescription.EventHandler(TryLocateEvent(nameof(ValidateAfterOriginal)), field),
                    EventDescription.CancelEventHandler(TryLocateEvent(nameof(ValidateBeforeOriginal)), field)
                );
        }

        public bool TryGetValue(out object value, out Result results)
        {
            value = null;
            if (getValue is null)
                return (results = new Result("Field does not have a property named Value with a getter"));

            try
            {
                value = getValue();
                return (results = Result.SUCCESS);
            }
            catch(Exception ex) { return (results = new Result(ex.Message)); }
        }

        public bool TryGetValue<T>(out T value, out Result results)
        {
            value = default;
            if (getValue is null)
                return (results = new Result("Field does not have a property named Value with a getter"));

            try
            {
                object temp = getValue();

                if (!(temp is T))
                    return (results = new Result($"Requested a value of type {typeof(T)} from the Field, received {temp?.GetType()} instead"));

                value = (T)temp;
                return (results = Result.SUCCESS);
            }
            catch(Exception ex) { return (results = new Result(ex.Message)); }
        }

        public bool TrySetValue(object value, bool runValidate, out Result results)
        {
            if (setValue is null)
                return (results = new Result("Field does not have a property named Value with a setter"));

            try
            {
                setValue(value);

                if (runValidate)
                    return TryAction(doRunValidate, out results);
                else
                    return (results = Result.SUCCESS);
            }
            catch(Exception ex) { return (results = new Result(ex.Message)); }
        }

        public bool TryRunValidate(out Result results) => TryAction(doRunValidate, out results);

        public bool TryShow(out Result results) => TryAction(doShow, out results);

        public bool TryHide(out Result results) => TryAction(doHide, out results);

        public bool TryLock(out Result results) => TryAction(doLock, out results);

        public bool TryUnlock(out Result results) => TryAction(doUnlock, out results);

        public bool TryEnable(out Result results) => TryAction(doEnable, out results);

        public bool TryDisable(out Result results) => TryAction(doDisable, out results);

        public bool TryFocus(out Result results) => TryAction(doFocus, out results);

        public static implicit operator FieldBase(FieldBaseExtended obj) => obj.Field;

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
                var meth = Field.GetType().GetMethod(method);
                if (meth != null)
                    return (Action)meth.CreateDelegate(typeof(Action), Field);
            }
            catch { }

            return null;
        }

        private Func<object> TryRegisterGetProperty(string property)
        {
            try
            {
                var prop = Field.GetType().GetProperty(property);
                if (prop != null)
                    return new Func<object>(() => prop.GetValue(Field));
            }
            catch { }

            return null;
        }

        private Action<object> TryRegisterSetProperty(string property)
        {
            try
            {
                var prop = Field.GetType().GetProperty(property);
                if (prop != null)
                    return new Action<object>(val => prop.SetValue(Field, val));
            }
            catch { }

            return null;
        }

        private EventInfo TryLocateEvent(string eventName)
        {
            try
            {
                return Field.GetType().GetEvent(eventName);
            }
            catch { return null; }
        }
    }
}
