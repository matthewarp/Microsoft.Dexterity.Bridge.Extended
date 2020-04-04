using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Dexterity.Bridge.Extended.Events.Sources
{
    internal class ReflectedEventRegistrationSource<T> : IEventRegistrationSource<T> where T : Delegate
    {
        public bool Available => true;

        private readonly EventInfo eventInfo;

        private readonly object target;

        private readonly DictionaryRootExtended dictionary;

        private readonly Action<EventRegistration<T>> unregisterWithDexterity;

        public ReflectedEventRegistrationSource(EventInfo eventInfo, Action<EventRegistration<T>> unregisterWithDexterity, DictionaryElement target)
        {
            this.eventInfo = eventInfo ?? throw new ArgumentNullException(nameof(eventInfo));
            this.unregisterWithDexterity = unregisterWithDexterity ?? UnregisterViaDictionary;

            this.target = target ?? throw new ArgumentNullException(nameof(target));

            dictionary = target.Dictionary.Extended();
        }

        public EventRegistration<T> Register(T handler)
        {
            eventInfo.AddEventHandler(target, handler);
            return new EventRegistration<T>(dictionary.LastRegisteredEvent(), handler);
        }

        public void Unregister(EventRegistration<T> registration) => unregisterWithDexterity(registration);

        private void UnregisterViaDictionary(EventRegistration<T> registration) => dictionary.UnregisterTagId(registration.TagId);
    }
}
