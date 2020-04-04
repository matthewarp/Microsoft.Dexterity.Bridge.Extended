using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Dexterity.Bridge.Extended.Events
{
    public class WindowsEventDescriptions
    {
        internal static readonly WindowsEventDescriptions Empty = new WindowsEventDescriptions(null, null, null, null, null, null, null, null, null, null);

        public EventHandlerDescription ActivateAfterOriginal { get; }

        public CancelEventHandlerDescription ActivateBeforeOriginal { get; }

        public EventHandlerDescription<AfterModalDialogEventArgs> AfterModalDialog { get; }

        public EventHandlerDescription<BeforeModalDialogEventArgs> BeforeModalDialog { get; }

        public EventHandlerDescription CloseAfterOriginal { get; }

        public CancelEventHandlerDescription CloseBeforeOriginal { get; }

        public EventHandlerDescription OpenAfterOriginal { get; }

        public CancelEventHandlerDescription OpenBeforeOriginal { get; }

        public EventHandlerDescription PrintAfterOriginal { get; }

        public CancelEventHandlerDescription PrintBeforeOriginal { get; }

        public WindowsEventDescriptions(EventHandlerDescription activateAfterOriginal, CancelEventHandlerDescription activateBeforeOriginal, EventHandlerDescription<AfterModalDialogEventArgs> afterModalDialog, EventHandlerDescription<BeforeModalDialogEventArgs> beforeModalDialog, EventHandlerDescription closeAfterOriginal, CancelEventHandlerDescription closeBeforeOriginal, EventHandlerDescription openAfterOriginal, CancelEventHandlerDescription openBeforeOriginal, EventHandlerDescription printAfterOriginal, CancelEventHandlerDescription printBeforeOriginal)
        {
            ActivateAfterOriginal = activateAfterOriginal ?? EventHandlerDescription.Empty;
            ActivateBeforeOriginal = activateBeforeOriginal ?? CancelEventHandlerDescription.Empty;
            AfterModalDialog = afterModalDialog ?? EventHandlerDescription<AfterModalDialogEventArgs>.Empty;
            BeforeModalDialog = beforeModalDialog ?? EventHandlerDescription<BeforeModalDialogEventArgs>.Empty;
            CloseAfterOriginal = closeAfterOriginal ?? EventHandlerDescription.Empty;
            CloseBeforeOriginal = closeBeforeOriginal ?? CancelEventHandlerDescription.Empty;
            OpenAfterOriginal = openAfterOriginal ?? EventHandlerDescription.Empty;
            OpenBeforeOriginal = openBeforeOriginal ?? CancelEventHandlerDescription.Empty;
            PrintAfterOriginal = printAfterOriginal ?? EventHandlerDescription.Empty;
            PrintBeforeOriginal = printBeforeOriginal ?? CancelEventHandlerDescription.Empty;
        }
    }
}
