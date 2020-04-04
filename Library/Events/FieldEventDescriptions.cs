using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Dexterity.Bridge.Extended.Events
{
    public class FieldEventDescriptions
    {
        internal static readonly FieldEventDescriptions Empty = new FieldEventDescriptions(null, null, null, null, null, null, null, null, null);

        public EventHandlerDescription ClickAfterOriginal { get; }

        public CancelEventHandlerDescription ClickBeforeOriginal { get; }

        public EventHandlerDescription Change { get; }

        public EventHandlerDescription EnterAfterOriginal { get; }

        public CancelEventHandlerDescription EnterBeforeOriginal { get; }

        public EventHandlerDescription LeaveAfterOriginal { get; }

        public CancelEventHandlerDescription LeaveBeforeOriginal { get; }

        public EventHandlerDescription ValidateAfterOriginal { get; }

        public CancelEventHandlerDescription ValidateBeforeOriginal { get; }

        internal FieldEventDescriptions(EventHandlerDescription clickAfterOriginal, CancelEventHandlerDescription clickBeforeOriginal, EventHandlerDescription change, EventHandlerDescription enterAfterOriginal, CancelEventHandlerDescription enterBeforeOriginal, EventHandlerDescription leaveAfterOriginal, CancelEventHandlerDescription leaveBeforeOriginal, EventHandlerDescription validateAfterOriginal, CancelEventHandlerDescription validateBeforeOriginal)
        {
            ClickAfterOriginal = clickAfterOriginal ?? EventHandlerDescription.Empty;
            ClickBeforeOriginal = clickBeforeOriginal ?? CancelEventHandlerDescription.Empty;
            Change = change ?? EventHandlerDescription.Empty;
            EnterAfterOriginal = enterAfterOriginal ?? EventHandlerDescription.Empty;
            EnterBeforeOriginal = enterBeforeOriginal ?? CancelEventHandlerDescription.Empty;
            LeaveAfterOriginal = leaveAfterOriginal ?? EventHandlerDescription.Empty;
            LeaveBeforeOriginal = leaveBeforeOriginal ?? CancelEventHandlerDescription.Empty;
            ValidateAfterOriginal = validateAfterOriginal ?? EventHandlerDescription.Empty;
            ValidateBeforeOriginal = validateBeforeOriginal ?? CancelEventHandlerDescription.Empty;
        }
    }
}
