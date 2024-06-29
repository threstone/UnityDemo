using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class ConfigMgr
{
    public static ConfigClass AllConfig { get; set; }
    static Dictionary<int, RoleConfig> roleMap;
    static Dictionary<int, EquipmentConfig> equipmentMap;
    static Dictionary<int, SkillConfig> skillMap;
    static Dictionary<int, BuffConfig> buffMap;

    public static CommonConfig Common { get { return AllConfig.Common; } }

    public static void Init()
    {
        Debug.Log("配置初始化");
        TextAsset jsonText = Resources.Load<TextAsset>("Config/config");
        AllConfig = JsonUtility.FromJson<ConfigClass>(jsonText.text);
        Debug.Log("配置初始化" + JsonUtility.ToJson(AllConfig));

        InitRoleMap();
        InitEquipmentMap();
        InitSkill();
        InitBuff();
    }

    public static RoleConfig CloneRoleInfoById(int id)
    {
        return roleMap[id].Clone();
    }

    public static RoleConfig GetRoleInfoById(int id)
    {
        return roleMap[id];
    }

    public static EquipmentConfig GetEquipmentAttr(int id)
    {
        return equipmentMap[id];
    }

    public static SkillConfig GetSkillConfig(int id)
    {
        return skillMap[id];
    }

    public static T GetSkillConfig<T>(int id)
    {
        return (T)(object)skillMap[id];
    }

    public static BuffConfig GetBuffConfig(int id)
    {
        return buffMap[id];
    }

    static void InitRoleMap()
    {
        roleMap = new();
        foreach (var role in AllConfig.Roles)
        {
            roleMap.TryAdd(role.Id, role);
        }
    }

    static void InitEquipmentMap()
    {
        equipmentMap = new();
        foreach (var equipment in AllConfig.Equipments)
        {
            equipmentMap.TryAdd(equipment.Id, equipment);
        }
    }

    static void InitSkill()
    {
        skillMap = new();
        foreach (var skill in AllConfig.ActiveSkills)
        {
            skillMap.TryAdd(skill.Id, skill);
        }
        foreach (var skill in AllConfig.PassiveSkills)
        {
            skillMap.TryAdd(skill.Id, skill);
        }
    }

    static void InitBuff()
    {
        buffMap = new();
        foreach (var buff in AllConfig.Buffs)
        {
            buffMap.TryAdd(buff.Id, buff);
        }
    }
}