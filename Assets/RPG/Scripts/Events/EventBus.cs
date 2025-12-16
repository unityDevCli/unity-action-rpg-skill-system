using System;
using System.Collections.Generic;

namespace RPG.Scripts.Events
{
    public static class EventBus
    {
        private static readonly Dictionary<Type, Delegate> events = new();

        public static void Subscribe<T>(Action<T> callback)
        {
            if (!events.ContainsKey(typeof(T))) events.Add(typeof(T), null);
            events[typeof(T)] = (Action<T>)events[typeof(T)] + callback;
        }

        public static void Publish<T>(T data)
        {
            if (events.TryGetValue(typeof(T), out var callback))
            {
                ((Action<T>)callback)?.Invoke(data);
            }
        }
    }
}