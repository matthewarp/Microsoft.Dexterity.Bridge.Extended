using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Dexterity.Bridge.Extended
{
    public class EventDescriptions
    {
        public static EventDescriptions Empty = new EventDescriptions();

        public EventDescription ClickAfterOriginal { get; }

        public CancelEventDescription ClickBeforeOriginal { get; }

        public EventDescription Change { get; }

        public EventDescription EnterAfterOriginal { get; }

        public CancelEventDescription EnterBeforeOriginal { get; }

        public EventDescription LeaveAfterOriginal { get; }

        public CancelEventDescription LeaveBeforeOriginal { get; }

        public EventDescription ValidateAfterOriginal { get; }

        public CancelEventDescription ValidateBeforeOriginal { get; }

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

        private EventDescriptions() { }

        internal EventDescriptions(EventDescription clickAfterOriginal, CancelEventDescription clickBeforeOriginal, EventDescription change, EventDescription enterAfterOriginal, CancelEventDescription enterBeforeOriginal, EventDescription leaveAfterOriginal, CancelEventDescription leaveBeforeOriginal, EventDescription validateAfterOriginal, CancelEventDescription validateBeforeOriginal)
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
