using System;
using System.Reflection;

namespace Microsoft.Dexterity.Bridge.Extended
{
    public sealed class EventDescription : IEventDescription<EventHandler>
    {
        public bool Registered { get; private set; }

        public short TagId { get; private set; }

        private readonly DictionaryRootExtended dictionary;

        private readonly FieldBase field;
        private readonly EventInfo eventInfo;
        private readonly EventHandler handler;

        private event EventHandler baseEvent;

        private bool registrationAttempted;

        private EventDescription(EventInfo eventInfo, FieldBase field)
        {
            this.eventInfo = eventInfo ?? throw new ArgumentNullException(nameof(eventInfo));
            this.field = field ?? throw new ArgumentNullException(nameof(field));

            dictionary = field.Dictionary.Extended();

            handler = HandleEvent;
        }

        public void Register()
        {
            if (Registered || registrationAttempted)
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

        public void Subscribe(EventHandler subscriber)
        {
            if (!Registered)
                Register();

            if (Registered)
                baseEvent += subscriber;
        }

        public void Unsubscribe(EventHandler subscriber)
        {
            baseEvent -= subscriber;
        }

        public void UnsubscribeAll()
        {
            foreach (Delegate d in baseEvent.GetInvocationList())
                baseEvent -= (EventHandler)d;
        }

        private void HandleEvent(object sender, EventArgs e) => baseEvent?.Invoke(sender, e);

        public static EventDescription Create(EventInfo eventInfo, FieldBase field)
        {
            if (eventInfo != null)
                return new EventDescription(eventInfo, field);

            return null;
        }
    }
}
