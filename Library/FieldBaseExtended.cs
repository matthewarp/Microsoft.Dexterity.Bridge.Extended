using System;
using System.ComponentModel;
using System.Reflection;

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

        public readonly bool CanGetValue, CanSetValue;

        public EventDescriptions EventDescriptions { get; }

        private readonly FieldBase field;

        private readonly Func<object> getValue;
        private readonly Action<object> setValue;


        internal FieldBaseExtended(FieldBase field, bool enableEvents = true)
        {
            this.field = field;

            CanGetValue = (getValue = TryRegisterGetProperty(nameof(Value))) != null;
            CanSetValue = (setValue = TryRegisterSetProperty(nameof(Value))) != null;

            EventDescriptions = EventDescriptions.Empty;

            if (!enableEvents)
                return;

            EventDescriptions = new EventDescriptions(
                    EventDescription.Create(TryLocateEvent(nameof(ClickAfterOriginal)), field),
                    CancelEventDescription.Create(TryLocateEvent(nameof(ClickBeforeOriginal)), field),
                    EventDescription.Create(TryLocateEvent(nameof(Change)), field),
                    EventDescription.Create(TryLocateEvent(nameof(EnterAfterOriginal)), field),
                    CancelEventDescription.Create(TryLocateEvent(nameof(EnterBeforeOriginal)), field),
                    EventDescription.Create(TryLocateEvent(nameof(LeaveAfterOriginal)), field),
                    CancelEventDescription.Create(TryLocateEvent(nameof(LeaveBeforeOriginal)), field),
                    EventDescription.Create(TryLocateEvent(nameof(ValidateAfterOriginal)), field),
                    CancelEventDescription.Create(TryLocateEvent(nameof(ValidateBeforeOriginal)), field)
                );
        }

        public bool TryGetValue(out object value)
        {
            value = null;
            if (getValue is null)
                return false;

            value = getValue();
            return true;
        }

        public bool TryGetValue<T>(out T value)
        {
            value = default;
            if (getValue is null)
                return false;

            object temp = getValue();

            if (!(temp is T))
                return false;

            value = (T)temp;
            return true;
        }

        public bool TrySetValue(object value)
        {
            if (setValue is null)
                return false;

            try
            {
                setValue(value);
                return true;
            }
            catch { return false; }
        }

        public static implicit operator FieldBase(FieldBaseExtended obj)
        {
            return obj.field;
        }

        private Func<object> TryRegisterGetProperty(string property)
        {
            try
            {
                var prop = field.GetType().GetProperty(property);
                if (prop != null)
                    return new Func<object>(() => prop.GetValue(field));
            }
            catch { }

            return null;
        }

        private Action<object> TryRegisterSetProperty(string property)
        {
            try
            {
                var prop = field.GetType().GetProperty(property);
                if (prop != null)
                    return new Action<object>(val => prop.SetValue(field, val));
            }
            catch { }

            return null;
        }

        private EventInfo TryLocateEvent(string eventName)
        {
            try
            {
                return field.GetType().GetEvent(eventName);
            }
            catch { return null; }
        }
    }
}
