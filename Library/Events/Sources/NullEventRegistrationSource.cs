using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Dexterity.Bridge.Extended.Events.Sources
{
    internal class NullEventRegistrationSource<T> : IEventRegistrationSource<T> where T : Delegate
    {
        public bool Available => false;

        public EventRegistration<T> Register(T handler) => null;

        public void Unregister(EventRegistration<T> registration) { }
    }
}
