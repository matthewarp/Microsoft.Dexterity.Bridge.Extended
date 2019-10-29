using System;
using System.ComponentModel;
using System.Reflection;

namespace Microsoft.Dexterity.Bridge.Extended
{
    public sealed class CancelEventDescription
    {
        public bool Registered { get; private set; }

        public short TagId { get; private set; }

        private readonly DictionaryRootExtended dictionary;

        private readonly FieldBase field;
        private readonly EventInfo eventInfo;
        private readonly CancelEventHandler handler;

        private event CancelEventHandler baseEvent;

        private bool registrationAttempted;

        private CancelEventDescription(EventInfo eventInfo, FieldBase field)
        {
            this.eventInfo = eventInfo ?? throw new ArgumentNullException(nameof(eventInfo));
            this.field = field ?? throw new ArgumentNullException(nameof(field));

            dictionary = field.Dictionary.Extended();

            handler = HandleEvent;
        }

        public void Register()
        {
            if (registrationAttempted || Registered)
                return;

            registrationAttempted = true;

            try
            {
                eventInfo.AddEventHandler(field, handler);
                TagId = dictionary.LastRegisteredEvent();
                Registered = true;
            }
            catch { }
        }

        public void Subscribe(CancelEventHandler subscriber)
        {
            if (!registrationAttempted && !Registered)
                Register();

            if (Registered)
                baseEvent += subscriber;
        }

        public void Unsubscribe(CancelEventHandler subscriber)
        {
            baseEvent -= subscriber;
        }

        private void HandleEvent(object sender, CancelEventArgs e) => baseEvent?.Invoke(sender, e);

        public static CancelEventDescription Create(EventInfo eventInfo, FieldBase field)
        {
            if (eventInfo != null)
                return new CancelEventDescription(eventInfo, field);

            return null;
        }
    }
}
