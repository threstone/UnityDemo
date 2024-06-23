using System;
using System.Collections.Generic;

public class GameNode
{
    protected EventManager e;

    public Dictionary<EventEnum, EventObject> listenerMap;

    public GameNode(EventManager e)
    {
        this.e = e;
    }
    public void On(EventEnum eventTag, Delegate f)
    {
        listenerMap ??= new();
        EventObject result = e.On(eventTag, f);
        listenerMap.Add(eventTag, result);
    }

    public void Off(EventEnum eventTag)
    {
        if (listenerMap.TryGetValue(eventTag, out var result))
        {
            e.Off(eventTag, result);
        }
    }
}