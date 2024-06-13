using System;

public class LimitedStat
{
    public int Current { get; private set; }
    public int Maximum { get; private set; }

    public LimitedStat(int max)
    {
        Utils.Log("max" + max);
        Maximum = max;
        Current = max; // 通常初始化时属性是满的
    }

    public void Add(int amount)
    {
        Set(Current + amount);
    }

    public void Set(int value)
    {
        Utils.Log("value" + value + "    Maximum" + Maximum);
        Current = Math.Clamp(value, 0, Maximum);
    }

    /*
     * 增加上限值
     * isSync 是否按比例改变当前值
     */
    public void AddMaximum(int amount, bool isSync = true)
    {
        if (isSync)
        {
            Current = Current * (10000 + amount * 10000 / Maximum) / 10000;
        }
        Maximum += amount;
        Set(Current);
    }
}