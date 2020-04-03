using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Dexterity.Bridge.Extended.Events
{
    public class ScriptEventDescriptions
    {

        public EventHandlerDescription<ScriptEventArgs> AfterInvoke { get; }

        public EventHandlerDescription<ScriptEventArgs> BeforeInvoke { get; }

        public ScriptEventDescriptions(EventHandlerDescription<ScriptEventArgs> afterInvoke, EventHandlerDescription<ScriptEventArgs> beforeInvoke)
        {
            AfterInvoke = afterInvoke;
            BeforeInvoke = beforeInvoke;
        }
    }
}
