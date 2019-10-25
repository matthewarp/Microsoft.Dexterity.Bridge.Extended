using System;
using System.ComponentModel;
using System.Reflection;

namespace Microsoft.Dexterity.Bridge.Extended
{


    public class FieldBaseWithEvents
    {
        public object Value { get => getValue?.DynamicInvoke() ?? throw new InvalidOperationException(); set { if (setValue is null) throw new InvalidOperationException(); setValue.DynamicInvoke(value); } }

        public event EventHandler ClickAfterOriginal { add => clickAfter?.Subscribe(value); remove => clickAfter?.Unsubscribe(value); }
        public event CancelEventHandler ClickBeforeOriginal { add => clickBefore?.Subscribe(value); remove => clickBefore?.Unsubscribe(value); }

        public event EventHandler Change { add => change?.Subscribe(value); remove => change?.Unsubscribe(value); }
        public event EventHandler EnterAfterOriginal { add => enterAfter?.Subscribe(value); remove => enterAfter?.Unsubscribe(value); }
        public event CancelEventHandler EnterBeforeOriginal { add => enterBefore?.Subscribe(value); remove => enterBefore?.Unsubscribe(value); }
        public event EventHandler LeaveAfterOriginal { add => leaveAfter?.Subscribe(value); remove => leaveAfter?.Unsubscribe(value); }
        public event CancelEventHandler LeaveBeforeOriginal { add => leaveBefore?.Subscribe(value); remove => leaveBefore?.Unsubscribe(value); }
        public event EventHandler ValidateAfterOriginal { add => validateAfter?.Subscribe(value); remove => validateAfter?.Unsubscribe(value); }
        public event CancelEventHandler ValidateBeforeOriginal { add => validateBefore?.Subscribe(value); remove => validateBefore?.Unsubscribe(value); }

        public readonly bool CanGetValue, CanSetValue, CanClickAfterOriginal, CanClickBeforeOriginal, CanChange, CanEnterAfterOriginal,
            CanEnterBeforeOriginal, CanLeaveAfterOriginal, CanLeaveBeforeOriginal, CanValidateAfterOriginal, CanValidateBeforeOriginal;

        private readonly FieldBase field;

        private readonly Delegate setValue, getValue;

        private readonly EventDescription clickAfter, change, enterAfter, leaveAfter, validateAfter;
        private readonly CancelEventDescription clickBefore, enterBefore, leaveBefore, validateBefore;


        internal FieldBaseWithEvents(FieldBase field, bool enableEvents = true)
        {
            this.field = field;

            CanGetValue = (getValue = TryRegisterGetProperty(nameof(Value))) != null;
            CanSetValue = (setValue = TryRegisterSetProperty(nameof(Value))) != null;

            if (!enableEvents)
                return;

            CanClickAfterOriginal = (clickAfter = EventDescription.Create(TryLocateEvent(nameof(ClickAfterOriginal)), field)) != null;
            CanClickBeforeOriginal = (clickBefore = CancelEventDescription.Create(TryLocateEvent(nameof(ClickBeforeOriginal)), field)) != null;

            CanChange = (change = EventDescription.Create(TryLocateEvent(nameof(Change)), field)) != null;
            CanEnterAfterOriginal = (enterAfter = EventDescription.Create(TryLocateEvent(nameof(EnterAfterOriginal)), field)) != null;
            CanEnterAfterOriginal = (enterBefore = CancelEventDescription.Create(TryLocateEvent(nameof(EnterBeforeOriginal)), field)) != null;
            CanLeaveAfterOriginal = (leaveAfter = EventDescription.Create(TryLocateEvent(nameof(LeaveAfterOriginal)), field)) != null;
            CanLeaveBeforeOriginal = (leaveBefore = CancelEventDescription.Create(TryLocateEvent(nameof(LeaveBeforeOriginal)), field)) != null;
            CanValidateAfterOriginal = (validateAfter = EventDescription.Create(TryLocateEvent(nameof(ValidateAfterOriginal)), field)) != null;
            CanValidateBeforeOriginal = (validateBefore = CancelEventDescription.Create(TryLocateEvent(nameof(ValidateBeforeOriginal)), field)) != null;
        }

        public bool TryGetValue(out object value)
        {
            value = null;
            if (getValue is null)
                return false;

            value = getValue.DynamicInvoke();
            return true;
        }

        public bool TryGetValue<T>(out T value)
        {
            value = default;
            if (getValue is null)
                return false;

            object temp = getValue.DynamicInvoke();

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
                setValue.DynamicInvoke(new object[] { value });
                return true;
            }
            catch { return false; }
        }

        private Delegate TryRegisterGetProperty(string property)
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

        private Delegate TryRegisterSetProperty(string property)
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
