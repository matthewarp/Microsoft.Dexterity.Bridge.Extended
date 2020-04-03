using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Dexterity.Bridge.Extended
{
    public class ScriptEventArgs : EventArgs
    {
        public Argument[] Arguments { get; }

        public ScriptEventArgs(Argument[] arguments)
        {
            Arguments = arguments ?? throw new ArgumentNullException(nameof(arguments));
        }
    }
}
