using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Dexterity.Bridge.Extended.Events
{
    public interface IEventDescription
    {
        IEnumerable<short> TagIds { get; }

        void Unsubscribe(short tagId);
    }
}
