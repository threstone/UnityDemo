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
            var className = type.FullName;
            if (className != "Skill" && className.StartsWith("SkillImpl"))
            {
                var id = Convert.ToInt32(className[9..]);
                Utils.Log("add skill :" + className + "   " + id);
                skillTypeMap.Add(id, type);
            }
        }
        Utils.Log("skill init end use ms: " + (((DateTimeOffset)DateTime.UtcNow).ToUnixTimeMilliseconds() - now));
    }

    public static Skill GetSkillById(SkillConfig skillConfig, int level, RoleEntity entity)
    {
        if (skillTypeMap.TryGetValue(skillConfig.Id, out var t))
        {
            return Activator.CreateInstance(t, skillConfig, level, entity) as Skill;
        }
        return null;
    }
}