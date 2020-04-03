using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Dexterity.Bridge.Extended
{
    public static class EventDescriptions
    {
        public class Field
        {
            public static Field Empty = new Field();

            public EventHandlerDescription ClickAfterOriginal { get; }

            public CancelEventHandlerDescription ClickBeforeOriginal { get; }

            public EventHandlerDescription Change { get; }

            public EventHandlerDescription EnterAfterOriginal { get; }

            public CancelEventHandlerDescription EnterBeforeOriginal { get; }

            public EventHandlerDescription LeaveAfterOriginal { get; }

            public CancelEventHandlerDescription LeaveBeforeOriginal { get; }

            public EventHandlerDescription ValidateAfterOriginal { get; }

            public CancelEventHandlerDescription ValidateBeforeOriginal { get; }

            public bool CanGetValue { get; }

            public bool CanSetValue { get; }

            public bool CanClickAfterOriginal { get; }

            public bool CanClickBeforeOriginal { get; }

            public bool CanChange { get; }

            public bool CanEnterAfterOriginal { get; }

            public bool CanEnterBeforeOriginal { get; }

            public bool CanLeaveAfterOriginal { get; }

            public bool CanLeaveBeforeOriginal { get; }

            public bool CanValidateAfterOriginal { get; }

            public bool CanValidateBeforeOriginal { get; }

            private Field() { }

            internal Field(EventHandlerDescription clickAfterOriginal, CancelEventHandlerDescription clickBeforeOriginal, EventHandlerDescription change, EventHandlerDescription enterAfterOriginal, CancelEventHandlerDescription enterBeforeOriginal, EventHandlerDescription leaveAfterOriginal, CancelEventHandlerDescription leaveBeforeOriginal, EventHandlerDescription validateAfterOriginal, CancelEventHandlerDescription validateBeforeOriginal)
            {
                CanClickAfterOriginal = (ClickAfterOriginal = clickAfterOriginal) != null;
                CanClickBeforeOriginal = (ClickBeforeOriginal = clickBeforeOriginal) != null;
                CanChange = (Change = change) != null;
                CanEnterAfterOriginal = (EnterAfterOriginal = enterAfterOriginal) != null;
                CanEnterBeforeOriginal = (EnterBeforeOriginal = enterBeforeOriginal) != null;
                CanLeaveAfterOriginal = (LeaveAfterOriginal = leaveAfterOriginal) != null;
                CanLeaveBeforeOriginal = (LeaveBeforeOriginal = leaveBeforeOriginal) != null;
                CanValidateAfterOriginal = (ValidateAfterOriginal = validateAfterOriginal) != null;
                CanValidateBeforeOriginal = (ValidateBeforeOriginal = validateBeforeOriginal) != null;
            }
        }

        public class Window
        {
            public static readonly Window Empty = new Window();

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

            private Window() { }

            public Window(EventHandlerDescription activateAfterOriginal, CancelEventHandlerDescription activateBeforeOriginal, EventHandlerDescription<AfterModalDialogEventArgs> afterModalDialog, EventHandlerDescription<BeforeModalDialogEventArgs> beforeModalDialog, EventHandlerDescription closeAfterOriginal, CancelEventHandlerDescription closeBeforeOriginal, EventHandlerDescription openAfterOriginal, CancelEventHandlerDescription openBeforeOriginal, EventHandlerDescription printAfterOriginal, CancelEventHandlerDescription printBeforeOriginal)
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
}
