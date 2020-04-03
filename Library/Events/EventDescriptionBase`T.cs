using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Dexterity.Bridge.Extended.Events
{
    public abstract class EventDescriptionBase<T> : IEventDescription where T : Delegate
    {
        public IEventRegistrationSource<T> Source { get; }

        public IEnumerable<short> TagIds => registrations.Select(o => o.TagId);

        private readonly List<EventRegistration<T>> registrations = new List<EventRegistration<T>>();

        protected EventDescriptionBase(IEventRegistrationSource<T> source)
        {
            Source = source ?? throw new ArgumentNullException(nameof(source));
        }

        public void Subscribe(T handler) => Subscribe(Source.Register(handler));

        private void Subscribe(EventRegistration<T> registration)
        {
            registrations.Add(registration);
            EventDescription.Registry.Add(registration.TagId, this);
        }

        public void Unsubscribe(T handler)
        {
            var results = registrations.Where(o => o.Handler == handler).ToArray();

            foreach (var registration in registrations.ToArray())
            {
                if (results.Contains(registration))
                    Unsubscribe(registration);
            }
        }

        public void Unsubscribe(short tagId)
        {
            var result = registrations.Where(o => o.TagId == tagId).FirstOrDefault();

            if (result is null)
                return;

            Unsubscribe(result);
        }

        private void Unsubscribe(EventRegistration<T> registration)
        {
            registrations.Remove(registration);

            Source.Unregister(registration);
            EventDescription.Registry.Remove(registration.TagId);
        }
    }

    public sealed class EventHandlerDescription : EventDescriptionBase<EventHandler>
    {
        public EventHandlerDescription(IEventRegistrationSource<EventHandler> source) : base(source) { }
    }

    public sealed class EventHandlerDescription<T> : EventDescriptionBase<EventHandler<T>>
    {
        public EventHandlerDescription(IEventRegistrationSource<EventHandler<T>> source) : base(source) { }
    }

    public sealed class CancelEventHandlerDescription : EventDescriptionBase<CancelEventHandler>
    {
        public CancelEventHandlerDescription(IEventRegistrationSource<CancelEventHandler> source) : base(source) { }
    }
}
