using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Dexterity.Bridge.Extended.Events
{
    public interface IEventRegistrationSource<T> where T : Delegate
    {
        EventRegistration<T> Register(T handler);

        void Unregister(EventRegistration<T> registration);

        bool Available { get; }
    }
}
