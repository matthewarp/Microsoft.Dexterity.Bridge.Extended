using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Dexterity.Bridge.Extended.Events
{
    public class EventRegistration<T> where T : Delegate
    {
        public short TagId { get; }

        public T Handler { get; }

        public EventRegistration(short tagId, T handler)
        {
            TagId = tagId;
            Handler = handler ?? throw new ArgumentNullException(nameof(handler));
        }
    }
}
