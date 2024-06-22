using System;
using System.Collections.Generic;

public class EventManager
{

    public static EventManager instance = new EventManager();

    private readonly Dictionary<EventEnum, List<Action<object[]>>> eventDictionary = new();

    public void On(EventEnum eventName, Action<object[]> cb)
    {
        if (!eventDictionary.ContainsKey(eventName))
        {
            eventDictionary[eventName] = new List<Action<object[]>>();
        }
        eventDictionary[eventName].Add(cb);
    }

    public void Off(EventEnum eventName, Action<object[]> cb)
    {
        if (eventDictionary.ContainsKey(eventName))
        {
            eventDictionary[eventName].Remove(cb);
        }
    }

    public void Emit(EventEnum eventName, object[] p = null)
    {
        if (eventDictionary.ContainsKey(eventName))
        {
            foreach (var action in eventDictionary[eventName].ToArray())
            {
                action.Invoke(p);
            }
        }
    }

    public void Once(EventEnum eventName, Action<object[]> cb)
    {
        void fun(params object[] args)
        {
            cb(args);
            Off(eventName, fun);
        }

        On(eventName, fun);
    }
}
