using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class ConfigMgr
{
    public static ConfigClass AllConfig { get; set; }
    static Dictionary<int, RoleConfig> roleMap;
    static Dictionary<int, EquipmentConfig> equipmentMap;

    public static CommonConfig Common { get { return AllConfig.common; } }

    public static void Init()
    {
        Debug.Log("配置初始化");
        string filePath = Path.Combine(Application.dataPath, "Config/config.json");
        string json = File.ReadAllText(filePath);
        AllConfig = JsonUtility.FromJson<ConfigClass>(json);
        Debug.Log("配置初始化" + JsonUtility.ToJson(AllConfig));
        InitRoleMap();
        InitEquipmentMap();
    }

    public static RoleConfig CloneRoleInfoById(int id)
    {
        return roleMap[id].Clone();
    }

    public static RoleConfig GetRoleInfoById(int id)
    {
        return roleMap[id];
    }

    public static AttrObject GetEquipmentAttr(int id)
    {
        return equipmentMap[id];
    }

    static void InitRoleMap()
    {
        roleMap = new();
        foreach (var role in AllConfig.roles)
        {
            roleMap.TryAdd(role.Id, role);
        }
    }

    static void InitEquipmentMap()
    {
        equipmentMap = new();
        foreach (var equipment in AllConfig.equipments)
        {
            equipmentMap.TryAdd(equipment.Id, equipment);
        }
    }
}