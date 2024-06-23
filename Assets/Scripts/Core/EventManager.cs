using System;
using System.Collections.Generic;

public class EventManager
{

    public static EventManager instance = new EventManager();

    private readonly Dictionary<EventEnum, List<EventObject>> eventDictionary = new();

    public EventObject On(EventEnum eventName, Delegate cb)
    {
        if (!eventDictionary.ContainsKey(eventName))
        {
            eventDictionary[eventName] = new();
        }
        var eventObj = new EventObject() { Fun = cb }; // todo优化点 池化
        eventDictionary[eventName].Add(eventObj);
        return eventObj;
    }

    public void Off(EventEnum eventName, Delegate cb)
    {
        if (eventDictionary.ContainsKey(eventName))
        {
            var list = eventDictionary[eventName];
            for (int i = list.Count - 1; i >= 0; i--)
            {
                if (list[i].Fun == cb)
                {
                    list.RemoveAt(i);
                }
            }
        }
    }

    public void Off(EventEnum eventName, EventObject obj)
    {
        if (eventDictionary.ContainsKey(eventName))
        {
            eventDictionary[eventName].Remove(obj);
        }
    }

    public void Emit(EventEnum eventName, params object[] args)
    {
        if (eventDictionary.ContainsKey(eventName))
        {
            var list = eventDictionary[eventName];
            for (int i = 0; i < list.Count; i++)
            {
                var eventObj = list[i];
                eventObj.Fun.DynamicInvoke(args);
                if (eventObj.IsOnce)
                {
                    list.RemoveAt(i);
                    i--;
                }
            }
        }
    }

    public EventObject Once(EventEnum eventName, Delegate cb)
    {
        if (!eventDictionary.ContainsKey(eventName))
        {
            eventDictionary[eventName] = new();
        }
        var eventObj = new EventObject() { IsOnce = true, Fun = cb };
        eventDictionary[eventName].Add(eventObj);
        return eventObj;
    }
}

public class EventObject
{
    public bool IsOnce = false;
    public Delegate Fun;
}
