using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Dexterity.Bridge.Extended.Events
{
    public class FieldEventDescriptions
    {
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

        internal FieldEventDescriptions(EventHandlerDescription clickAfterOriginal, CancelEventHandlerDescription clickBeforeOriginal, EventHandlerDescription change, EventHandlerDescription enterAfterOriginal, CancelEventHandlerDescription enterBeforeOriginal, EventHandlerDescription leaveAfterOriginal, CancelEventHandlerDescription leaveBeforeOriginal, EventHandlerDescription validateAfterOriginal, CancelEventHandlerDescription validateBeforeOriginal)
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
}
