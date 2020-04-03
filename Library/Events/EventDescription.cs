using Microsoft.Dexterity.Bridge.Extended.Events.Sources;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Dexterity.Bridge.Extended.Events
{
    public static class EventDescription
    {
        internal static readonly Dictionary<short, IEventDescription> Registry = new Dictionary<short, IEventDescription>();

        public static EventHandlerDescription EventHandler(EventInfo eventInfo, DictionaryElement target)
        {
            if (eventInfo is null || target is null)
                return null;

            return new EventHandlerDescription(new ReflectedEventRegistrationSource<EventHandler>(eventInfo, null, target));
        }

        public static EventHandlerDescription EventHandler(EventInfo eventInfo, Action<EventRegistration<EventHandler>> unregisterWithDexterity, DictionaryElement target)
        {
            if (eventInfo is null || target is null)
                return null;

            return new EventHandlerDescription(new ReflectedEventRegistrationSource<EventHandler>(eventInfo, unregisterWithDexterity, target));
        }

        public static EventHandlerDescription EventHandler(Action<EventHandler> registerWithDexterity, DictionaryElement target)
            => new EventHandlerDescription(new DelegatedEventRegistrationSource<EventHandler>(registerWithDexterity, null, target));

        public static EventHandlerDescription EventHandler(Action<EventHandler> registerWithDexterity, Action<EventRegistration<EventHandler>> unregisterWithDexterity, DictionaryElement target)
            => new EventHandlerDescription(new DelegatedEventRegistrationSource<EventHandler>(registerWithDexterity, unregisterWithDexterity, target));

        public static EventHandlerDescription<T> EventHandler<T>(EventInfo eventInfo, DictionaryElement target)
        {
            if (eventInfo is null || target is null)
                return null;

            return new EventHandlerDescription<T>(new ReflectedEventRegistrationSource<EventHandler<T>>(eventInfo, null, target));
        }

        public static EventHandlerDescription<T> EventHandler<T>(EventInfo eventInfo, Action<EventRegistration<EventHandler<T>>> unregisterWithDexterity, DictionaryElement target)
        {
            if (eventInfo is null || target is null)
                return null;

            return new EventHandlerDescription<T>(new ReflectedEventRegistrationSource<EventHandler<T>>(eventInfo, unregisterWithDexterity, target));
        }

        public static EventHandlerDescription<T> EventHandler<T>(Action<EventHandler<T>> registerWithDexterity, DictionaryElement target)
            => new EventHandlerDescription<T>(new DelegatedEventRegistrationSource<EventHandler<T>>(registerWithDexterity, null, target));

        public static EventHandlerDescription<T> EventHandler<T>(Action<EventHandler<T>> registerWithDexterity, Action<EventRegistration<EventHandler<T>>> unregisterWithDexterity, DictionaryElement target)
            => new EventHandlerDescription<T>(new DelegatedEventRegistrationSource<EventHandler<T>>(registerWithDexterity, unregisterWithDexterity, target));

        public static CancelEventHandlerDescription CancelEventHandler(EventInfo eventInfo, DictionaryElement target)
        {
            if (eventInfo is null || target is null)
                return null;

            return new CancelEventHandlerDescription(new ReflectedEventRegistrationSource<CancelEventHandler>(eventInfo, null, target));
        }

        public static CancelEventHandlerDescription CancelEventHandler(EventInfo eventInfo, Action<EventRegistration<CancelEventHandler>> unregisterWithDexterity, DictionaryElement target)
        {
            if (eventInfo is null || target is null)
                return null;
        
            return new CancelEventHandlerDescription(new ReflectedEventRegistrationSource<CancelEventHandler>(eventInfo, unregisterWithDexterity, target));
        }

        public static CancelEventHandlerDescription CancelEventHandler(Action<CancelEventHandler> registerWithDexterity, DictionaryElement target)
            => new CancelEventHandlerDescription(new DelegatedEventRegistrationSource<CancelEventHandler>(registerWithDexterity, null, target));

        public static CancelEventHandlerDescription CancelEventHandler(Action<CancelEventHandler> registerWithDexterity, Action<EventRegistration<CancelEventHandler>> unregisterWithDexterity, DictionaryElement target)
            => new CancelEventHandlerDescription(new DelegatedEventRegistrationSource<CancelEventHandler>(registerWithDexterity, unregisterWithDexterity, target));

        public static void UnregisterByTagId(short tagId)
        {
            if (!Registry.ContainsKey(tagId))
                return;

            Registry[tagId].Unsubscribe(tagId);
        }
    }
}
