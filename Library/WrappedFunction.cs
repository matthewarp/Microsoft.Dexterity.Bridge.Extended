using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Dexterity.Bridge.Extended
{
    internal class WrappedFunction : FunctionBase
    {
        protected override Script WrappedScript { get; }

        public WrappedFunction(Script wrappedScript)
        {
            WrappedScript = wrappedScript ?? throw new ArgumentNullException(nameof(wrappedScript));
        }

        protected override void FireInvokeEvent(Argument[] arguments, Argument returnValue, Delegate handler)
        {
            Argument[] args = new Argument[arguments.Length + 1];
            args[0] = returnValue;
            Array.Copy(arguments, 0, args, 1, arguments.Length);

            handler.DynamicInvoke(this, new ScriptEventArgs(args));
        }
    }
}
