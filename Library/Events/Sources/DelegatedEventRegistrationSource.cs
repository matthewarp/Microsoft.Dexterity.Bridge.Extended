using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Dexterity.Bridge.Extended.Events.Sources
{
    internal class DelegatedEventRegistrationSource<T> : IEventRegistrationSource<T> where T : Delegate
    {
        private readonly Action<T> registerWithDexterity;

        private readonly Action<EventRegistration<T>> unregisterWithDexterity;

        private readonly DictionaryRootExtended dictionary;

        public DelegatedEventRegistrationSource(Action<T> registerWithDexterity, Action<EventRegistration<T>> unregisterWithDexterity, DictionaryElement target)
        {
            this.registerWithDexterity = registerWithDexterity ?? throw new ArgumentNullException(nameof(registerWithDexterity));
            this.unregisterWithDexterity = unregisterWithDexterity ?? UnregisterViaDictionary;
            dictionary = target?.Dictionary.Extended() ?? throw new ArgumentNullException(nameof(target));
        }

        public EventRegistration<T> Register(T handler)
        {
            registerWithDexterity(handler);
            return new EventRegistration<T>(dictionary.LastRegisteredEvent(), handler);
        }

        public void Unregister(EventRegistration<T> registration) => unregisterWithDexterity(registration);

        private void UnregisterViaDictionary(EventRegistration<T> registration) => dictionary.UnregisterTagId(registration.TagId);
    }
}
