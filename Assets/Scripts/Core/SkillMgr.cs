using System;
using System.Collections.Generic;
using System.Reflection;

public static class SkillMgr
{
    static Dictionary<int, Type> skillTypeMap;

    public static void Init()
    {
        var now = ((DateTimeOffset)DateTime.UtcNow).ToUnixTimeMilliseconds();
        skillTypeMap = new();
        // 获取当前程序集
        Assembly assembly = Assembly.GetExecutingAssembly();

        // 获取程序集中的所有类型
        Type[] types = assembly.GetTypes();

        foreach (Type type in types)
        {
            if (type.FullName != "Skill" && type.FullName.StartsWith("SkillImpl"))
            {
                Utils.Log("add skill :" + type.FullName);
                Skill instance = Activator.CreateInstance(type, null, 0) as Skill; // todo
                skillTypeMap.Add(instance.Id, type);
            }
        }
        Utils.Log("skill init end use ms: " + (((DateTimeOffset)DateTime.UtcNow).ToUnixTimeMilliseconds() - now));
    }

    public static Skill GetSkillById(int type, RoleEntity entity, int duration)
    {
        if (skillTypeMap.TryGetValue(type, out var t))
        {
            return Activator.CreateInstance(t, entity, duration) as Skill;
        }
        return null;
    }
}