using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Dexterity.Bridge.Extended
{
    internal class WrappedProcedure : ProcedureBase
    {
        protected override Script WrappedScript { get; }

        public WrappedProcedure(Script wrappedScript)
        {
            WrappedScript = wrappedScript ?? throw new ArgumentNullException(nameof(wrappedScript));
        }

        protected override void FireInvokeEvent(Argument[] arguments, Delegate handler)
        {
            handler.DynamicInvoke(this, new ScriptEventArgs(arguments));
        }
    }
}
