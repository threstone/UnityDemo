using System;
using System.Collections.Generic;
using System.Reflection;

public static class BuffMgr
{
    static Dictionary<int, Type> buffTypeMap;

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
            var className = type.FullName;
            if (className != "Buff" && className.StartsWith("BuffImpl"))
            {
                var id = Convert.ToInt32(className[8..]);
                Utils.Log("add buff :" + className + "   " + id);
                buffTypeMap.Add(id, type);
            }
        }

        Utils.Log("buff init end use ms: " + (((DateTimeOffset)DateTime.UtcNow).ToUnixTimeMilliseconds() - now));
    }

    public static Buff GetBuffByType(int buffId, int duration, RoleEntity entity, RoleEntity sourceEntity, params int[] args)
    {
        if (buffTypeMap.TryGetValue(buffId, out var t))
        {
            var config = ConfigMgr.GetBuffConfig(buffId);
            return Activator.CreateInstance(t, config, duration, entity, sourceEntity, args) as Buff;
        }
        return null;
    }
}