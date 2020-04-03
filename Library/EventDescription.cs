using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Dexterity.Bridge.Extended
{
    public interface IEventSource<T>
    {
        void Add(T handler);

        void RegisterWithDexterity();

        void Remove(T handler);

        void RemoveAll();
    }

    internal abstract class DelegatedEventSource<T> : IEventSource<T> where T : Delegate
    {
        private readonly Action<T> registerWithDexterity;

        protected abstract T Delegate { get; }

        protected DelegatedEventSource(Action<T> registerWithDexterity)
        {
            this.registerWithDexterity = registerWithDexterity ?? throw new ArgumentNullException(nameof(registerWithDexterity));
        }

        public abstract void Add(T handler);

        public void RegisterWithDexterity() => registerWithDexterity(Delegate);

        public abstract void Remove(T handler);

        public abstract void RemoveAll();
    }

    internal abstract class ReflectedEventSource<T> : IEventSource<T> where T : Delegate
    {
        private readonly EventInfo eventInfo;

        private readonly object target;

        protected abstract T Delegate { get; }

        protected ReflectedEventSource(EventInfo eventInfo, object target)
        {
            this.target = target;
            this.eventInfo = eventInfo ?? throw new ArgumentNullException(nameof(eventInfo));
        }

        public abstract void Add(T handler);

        public void RegisterWithDexterity()
        {
            eventInfo.AddEventHandler(target, Delegate);
        }

        public abstract void Remove(T handler);

        public abstract void RemoveAll();
    }

    internal class EventHandlerDelegatedEventSource : DelegatedEventSource<EventHandler>
    {
        private event EventHandler baseEvent;

        protected override EventHandler Delegate { get; }

        public EventHandlerDelegatedEventSource(Action<EventHandler> registerWithDexterity) : base(registerWithDexterity) { }

        public override void Add(EventHandler handler) => baseEvent += handler;

        public override void Remove(EventHandler handler) => baseEvent -= handler;

        public override void RemoveAll()
        {
            foreach (Delegate d in baseEvent.GetInvocationList())
                baseEvent -= (EventHandler)d;
        }

        private void HandleEvent(object sender, EventArgs e) => baseEvent?.Invoke(sender, e);
    }

    internal class EventHandlerDelegatedEventSource<T> : DelegatedEventSource<EventHandler<T>>
    {
        private event EventHandler<T> baseEvent;

        protected override EventHandler<T> Delegate { get; }

        public EventHandlerDelegatedEventSource(Action<EventHandler<T>> registerWithDexterity) : base(registerWithDexterity)
        {
            Delegate = HandleEvent;
        }

        public override void Add(EventHandler<T> handler) => baseEvent += handler;

        public override void Remove(EventHandler<T> handler) => baseEvent -= handler;

        public override void RemoveAll()
        {
            foreach (Delegate d in baseEvent.GetInvocationList())
                baseEvent -= (EventHandler<T>)d;
        }

        private void HandleEvent(object sender, T e) => baseEvent?.Invoke(sender, e);
    }

    internal class CancelEventHandlerDelegatedEventSource : DelegatedEventSource<CancelEventHandler>
    {
        private event CancelEventHandler baseEvent;

        protected override CancelEventHandler Delegate { get; }

        public CancelEventHandlerDelegatedEventSource(Action<CancelEventHandler> registerWithDexterity) : base(registerWithDexterity)
        {
            Delegate = HandleEvent;
        }

        public override void Add(CancelEventHandler handler) => baseEvent += handler;

        public override void Remove(CancelEventHandler handler) => baseEvent -= handler;

        public override void RemoveAll()
        {
            foreach (Delegate d in baseEvent.GetInvocationList())
                baseEvent -= (CancelEventHandler)d;
        }

        private void HandleEvent(object sender, CancelEventArgs e) => baseEvent?.Invoke(sender, e);
    }

    internal class EventHandlerReflectedEventSource : ReflectedEventSource<EventHandler>
    {
        private event EventHandler baseEvent;

        protected override EventHandler Delegate { get; }

        public EventHandlerReflectedEventSource(EventInfo eventInfo, object target) : base(eventInfo, target)
        {
            Delegate = HandleEvent;
        }

        public override void Add(EventHandler handler) => baseEvent += handler;

        public override void Remove(EventHandler handler) => baseEvent -= handler;

        public override void RemoveAll()
        {
            foreach (Delegate d in baseEvent.GetInvocationList())
                baseEvent -= (EventHandler)d;
        }

        private void HandleEvent(object sender, EventArgs e) => baseEvent?.Invoke(sender, e);
    }

    internal class EventHandlerReflectedEventSource<T> : ReflectedEventSource<EventHandler<T>>
    {
        private event EventHandler<T> baseEvent;

        protected override EventHandler<T> Delegate { get; }

        public EventHandlerReflectedEventSource(EventInfo eventInfo, object target) : base(eventInfo, target)
        {
            Delegate = HandleEvent;
        }

        public override void Add(EventHandler<T> handler) => baseEvent += handler;

        public override void Remove(EventHandler<T> handler) => baseEvent -= handler;

        public override void RemoveAll()
        {
            foreach (Delegate d in baseEvent.GetInvocationList())
                baseEvent -= (EventHandler<T>)d;
        }

        private void HandleEvent(object sender, T e) => baseEvent?.Invoke(sender, e);
    }

    internal class CancelEventHandlerReflectedEventSource : ReflectedEventSource<CancelEventHandler>
    {
        private event CancelEventHandler baseEvent;

        protected override CancelEventHandler Delegate { get; }

        public CancelEventHandlerReflectedEventSource(EventInfo eventInfo, object target) : base(eventInfo, target)
        {
            Delegate = HandleEvent;
        }

        public override void Add(CancelEventHandler handler) => baseEvent += handler;

        public override void Remove(CancelEventHandler handler) => baseEvent -= handler;

        public override void RemoveAll()
        {
            foreach (Delegate d in baseEvent.GetInvocationList())
                baseEvent -= (CancelEventHandler)d;
        }

        private void HandleEvent(object sender, CancelEventArgs e) => baseEvent?.Invoke(sender, e);
    }

    public static class EventDescription
    {
        public static CancelEventHandlerDescription CancelEventHandler(EventInfo i, DictionaryElement target)
        {
            if (i is null || target is null)
                return null;

            return new CancelEventHandlerDescription(new CancelEventHandlerReflectedEventSource(i, target), target.Dictionary);
        }

        public static CancelEventHandlerDescription CancelEventHandler(Action<CancelEventHandler> action, DictionaryElement target)
        {
            if (action is null)
                return null;

            return new CancelEventHandlerDescription(new CancelEventHandlerDelegatedEventSource(action), target.Dictionary);
        }

        public static EventHandlerDescription EventHandler(EventInfo i, DictionaryElement target)
        {
            if (i is null || target is null)
                return null;

            return new EventHandlerDescription(new EventHandlerReflectedEventSource(i, target), target.Dictionary);
        }

        public static EventHandlerDescription EventHandler(Action<EventHandler> action, DictionaryElement target)
        {
            if (action is null)
                return null;

            return new EventHandlerDescription(new EventHandlerDelegatedEventSource(action), target.Dictionary);
        }

        public static EventHandlerDescription<T> EventHandler<T>(EventInfo i, DictionaryElement target)
        {
            if (i is null || target is null)
                return null;

            return new EventHandlerDescription<T>(new EventHandlerReflectedEventSource<T>(i, target), target.Dictionary);
        }

        public static EventHandlerDescription<T> EventHandler<T>(Action<EventHandler<T>> action, DictionaryElement target)
        {
            if (action is null)
                return null;

            return new EventHandlerDescription<T>(new EventHandlerDelegatedEventSource<T>(action), target.Dictionary);
        }
    }

    public abstract class EventDescription<T> : IEventDescription<T>
    {
        public bool Registered { get; private set; }

        public short TagId { get; private set; }

        protected readonly DictionaryRootExtended dictionary;

        protected readonly IEventSource<T> source;

        private bool registrationAttempted;

        internal EventDescription(IEventSource<T> source, DictionaryRoot dictionary)
        {
            this.source = source ?? throw new ArgumentNullException(nameof(source));

            this.dictionary = dictionary.Extended();
        }

        public void Register()
        {
            if (Registered || registrationAttempted)
                return;

            registrationAttempted = true;

            try
            {
                source.RegisterWithDexterity();
                TagId = dictionary.LastRegisteredEvent();
                Registered = true;
            }
            catch { }
        }

        public void Subscribe(T subscriber)
        {
            if (!Registered)
                Register();

            if (Registered)
                source.Add(subscriber);
        }

        public void Unsubscribe(T subscriber) => source.Remove(subscriber);

        public void UnsubscribeAll() => source.RemoveAll();
    }

    public sealed class EventHandlerDescription : EventDescription<EventHandler>
    {
        public EventHandlerDescription(IEventSource<EventHandler> source, DictionaryRoot dictionary) : base(source, dictionary) { }
    }

    public sealed class EventHandlerDescription<T> : EventDescription<EventHandler<T>>
    {
        public EventHandlerDescription(IEventSource<EventHandler<T>> source, DictionaryRoot dictionary) : base(source, dictionary){ }
    }

    public sealed class CancelEventHandlerDescription : EventDescription<CancelEventHandler>
    {
        public CancelEventHandlerDescription(IEventSource<CancelEventHandler> source, DictionaryRoot dictionary) : base(source, dictionary) { }
    }
}
