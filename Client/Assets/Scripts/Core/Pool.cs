using System.Collections.Generic;

public class Pool<T> where T : new()
{
    readonly Stack<T> damagePool = new();
    public int Count { get { return damagePool.Count; } }

    public T Get()
    {
        if (damagePool.Count != 0) { return damagePool.Pop(); };
        return new T();
    }

    public void Back(T item)
    {
        damagePool.Push(item);
    }
}