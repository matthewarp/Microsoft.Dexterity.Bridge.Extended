using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Dexterity.Bridge.Extended
{
    public interface IEventDescription<T> : IEventDescription
    {
        void Subscribe(T subscriber);

        void Unsubscribe(T subscriber);
    }
}
