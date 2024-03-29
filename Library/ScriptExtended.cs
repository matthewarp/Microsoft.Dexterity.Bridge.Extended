﻿using Microsoft.Dexterity.Bridge.Extended.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Dexterity.Bridge.Extended
{
    public class ScriptExtended
    {
        public event EventHandler<ScriptEventArgs> AfterInvoke { add => EventDescriptions.AfterInvoke.Subscribe(value); remove => EventDescriptions.AfterInvoke.Unsubscribe(value); }
        public event EventHandler<ScriptEventArgs> BeforeInvoke { add => EventDescriptions.BeforeInvoke.Subscribe(value); remove => EventDescriptions.BeforeInvoke.Unsubscribe(value); }

        public Script Script { get; }

        public bool Supported { get; }

        public ScriptEventDescriptions EventDescriptions { get; }

        private static readonly PropertyInfo supportedPropertyInfo;

        static ScriptExtended()
        {
            supportedPropertyInfo = typeof(Script).GetProperty("Supported", BindingFlags.Instance | BindingFlags.NonPublic);
        }

        public ScriptExtended(Script script)
        {
            Script = script;
            Supported = (bool)supportedPropertyInfo.GetValue(script);

            if (!Supported)
                EventDescriptions = ScriptEventDescriptions.Empty;
            else if (script.IsFunction)
            {
                var func = new WrappedFunction(script);

                EventDescriptions = new ScriptEventDescriptions(
                    EventDescription.EventHandler<ScriptEventArgs>(
                        o => TriggerManager.FunctionTriggers.RegisterTrigger(func, script, AttachType.After, o),
                        o => TriggerManager.FunctionTriggers.UnregisterTrigger(func, script, o.Handler),
                        script
                    ),
                    EventDescription.EventHandler<ScriptEventArgs>(
                        o => TriggerManager.FunctionTriggers.RegisterTrigger(func, script, AttachType.Before, o),
                        o => TriggerManager.FunctionTriggers.UnregisterTrigger(func, script, o.Handler),
                        script
                    )
                );
            }
            else
            {
                var func = new WrappedProcedure(script);

                EventDescriptions = new ScriptEventDescriptions(
                    EventDescription.EventHandler<ScriptEventArgs>(
                        o => TriggerManager.ProcedureTriggers.RegisterTrigger(func, script, AttachType.After, o),
                        o => TriggerManager.ProcedureTriggers.UnregisterTrigger(func, script, o.Handler),
                        script
                    ),
                    EventDescription.EventHandler<ScriptEventArgs>(
                        o => TriggerManager.ProcedureTriggers.RegisterTrigger(func, script, AttachType.Before, o),
                        o => TriggerManager.ProcedureTriggers.UnregisterTrigger(func, script, o.Handler),
                        script
                    )
                );
            }
        }
    }
}
