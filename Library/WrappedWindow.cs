using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Dexterity.Bridge.Extended
{
    internal class WrappedWindow : WindowProxy
    {
        public WrappedWindow(Window window) : base(window) { }
    }
}
