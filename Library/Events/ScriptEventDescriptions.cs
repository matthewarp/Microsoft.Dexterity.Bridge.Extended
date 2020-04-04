using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Dexterity.Bridge.Extended.Events
{
    public class ScriptEventDescriptions
    {
        internal static readonly ScriptEventDescriptions Empty = new ScriptEventDescriptions(null, null);

        public EventHandlerDescription<ScriptEventArgs> AfterInvoke { get; }

        public EventHandlerDescription<ScriptEventArgs> BeforeInvoke { get; }

        internal ScriptEventDescriptions(EventHandlerDescription<ScriptEventArgs> afterInvoke, EventHandlerDescription<ScriptEventArgs> beforeInvoke)
        {
            AfterInvoke = afterInvoke ?? EventHandlerDescription<ScriptEventArgs>.Empty;
            BeforeInvoke = beforeInvoke ?? EventHandlerDescription<ScriptEventArgs>.Empty;
        }
    }
}
