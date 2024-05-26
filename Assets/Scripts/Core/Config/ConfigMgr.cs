using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public static class ConfigMgr
{
    public static ConfigClass AllConfig { get; set; }

    static Dictionary<int, RoleConfig> roleMap;

    public static void Init()
    {
        Debug.Log("配置初始化");
        string filePath = Path.Combine(Application.dataPath, "Config/config.json");
        string json = File.ReadAllText(filePath);
        AllConfig = JsonUtility.FromJson<ConfigClass>(json);
        InitRoleMap();
    }

    public static RoleConfig GetRoleInfoById(int id)
    {
        return roleMap[id].Clone();
    }
    static void InitRoleMap()
    {
        roleMap = new();
        foreach (var role in AllConfig.roles)
        {
            roleMap.TryAdd(role.id, role);
        }
    }

}