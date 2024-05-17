using System;
using System.Collections.Generic;


public class EventManager
{

    public static EventManager instance = new EventManager();

    private Dictionary<string, List<Action<Object>>> eventDictionary = new Dictionary<string, List<Action<Object>>>();

    public void On(string eventName, Action<Object> cb)
    {
        if (!eventDictionary.ContainsKey(eventName))
        {
            eventDictionary[eventName] = new List<Action<Object>>();
        }
        eventDictionary[eventName].Add(cb);
    }

    public void Off(string eventName, Action<Object> cb)
    {
        if (eventDictionary.ContainsKey(eventName))
        {
            eventDictionary[eventName].Remove(cb);
        }
    }

    public void Emit(string eventName, Object p = null)
    {
        if (eventDictionary.ContainsKey(eventName))
        {
            foreach (var action in eventDictionary[eventName].ToArray())
            {
                action.Invoke(p);
            }
        }
    }

    public void Once(string eventName, Action<Object> cb)
    {
        Action<Object> fun = null;
        fun = (data) =>
        {
            cb(data);
            Off(eventName, fun);
        };

        On(eventName, fun);
    }
}
