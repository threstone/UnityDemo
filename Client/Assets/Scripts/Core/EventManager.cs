using System;
using System.Collections.Generic;

public class EventManager
{

    public static EventManager instance = new();
    static readonly Pool<EventObject> pool = new();

    private readonly Dictionary<EventEnum, List<EventObject>> eventDictionary = new();

    public EventObject On(EventEnum eventName, Delegate cb)
    {
        if (!eventDictionary.ContainsKey(eventName))
        {
            eventDictionary[eventName] = new();
        }
        var eventObj = pool.Get();
        eventObj.IsOnce = false;
        eventObj.Fun = cb;
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
                var e = list[i];
                if (e.Fun == cb)
                {
                    pool.Back(e.Reset());
                    list.RemoveAt(i);
                }
            }
        }
    }

    public void Off(EventEnum eventName, EventObject obj)
    {
        if (eventDictionary.ContainsKey(eventName))
        {
            pool.Back(obj.Reset());
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
                    pool.Back(eventObj.Reset());
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
        var eventObj = pool.Get();
        eventObj.IsOnce = true;
        eventObj.Fun = cb;
        eventDictionary[eventName].Add(eventObj);
        return eventObj;
    }
}

public class EventObject
{
    public bool IsOnce = false;
    public Delegate Fun;

    public EventObject Reset()
    {
        Fun = null;
        return this;
    }
}
