using System;
using System.Collections.Generic;
using System.Reflection;

public static class BuffMgr
{
    static Dictionary<BuffEnum, Type> buffTypeMap;

    public static void Init()
    {
        var now = ((DateTimeOffset)DateTime.UtcNow).ToUnixTimeMilliseconds();
        buffTypeMap = new();
        // 获取当前程序集
        Assembly assembly = Assembly.GetExecutingAssembly();

        // 获取程序集中的所有类型
        Type[] types = assembly.GetTypes();

        foreach (Type type in types)
        {
            if (type.FullName != "Buff" && type.FullName.EndsWith("Buff"))
            {
                Utils.Log("add buff :" + type.FullName);
                Buff instance = Activator.CreateInstance(type, null, 0) as Buff;
                buffTypeMap.Add(instance.BuffType, type);
            }
        }

        Utils.Log("buff init end use ms: " + (((DateTimeOffset)DateTime.UtcNow).ToUnixTimeMilliseconds() - now));
    }

    public static Buff GetBuffByType(BuffEnum buffType, RoleEntity entity, int duration)
    {
        if (buffTypeMap.TryGetValue(buffType, out var t))
        {
            return Activator.CreateInstance(t, entity, duration) as Buff;
        }
        return null;
    }
}