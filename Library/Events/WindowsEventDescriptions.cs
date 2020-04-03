using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Dexterity.Bridge.Extended.Events
{
    public class WindowsEventDescriptions
    {
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

        public bool CanAfterModalDialog { get; }

        public bool CanBeforeModalDialog { get; }

        public WindowsEventDescriptions(EventHandlerDescription activateAfterOriginal, CancelEventHandlerDescription activateBeforeOriginal, EventHandlerDescription<AfterModalDialogEventArgs> afterModalDialog, EventHandlerDescription<BeforeModalDialogEventArgs> beforeModalDialog, EventHandlerDescription closeAfterOriginal, CancelEventHandlerDescription closeBeforeOriginal, EventHandlerDescription openAfterOriginal, CancelEventHandlerDescription openBeforeOriginal, EventHandlerDescription printAfterOriginal, CancelEventHandlerDescription printBeforeOriginal)
        {
            ActivateAfterOriginal = activateAfterOriginal;
            ActivateBeforeOriginal = activateBeforeOriginal;
            CanAfterModalDialog = (AfterModalDialog = afterModalDialog) != null;
            CanBeforeModalDialog = (BeforeModalDialog = beforeModalDialog) != null;
            CloseAfterOriginal = closeAfterOriginal;
            CloseBeforeOriginal = closeBeforeOriginal;
            OpenAfterOriginal = openAfterOriginal;
            OpenBeforeOriginal = openBeforeOriginal;
            PrintAfterOriginal = printAfterOriginal;
            PrintBeforeOriginal = printBeforeOriginal;
        }
    }
}
