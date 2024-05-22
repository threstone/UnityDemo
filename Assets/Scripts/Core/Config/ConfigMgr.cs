using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class ConfigMgr
{
    public static ConfigClass AllConfig { get; set; }

    static Dictionary<int, Role> roleMap;

    public static void Init()
    {
        Debug.Log("配置初始化");
        string filePath = Path.Combine(Application.dataPath, "Config/config.json");
        string json = File.ReadAllText(filePath);
        AllConfig = JsonUtility.FromJson<ConfigClass>(json);
        Debug.Log(JsonUtility.ToJson(AllConfig));
        InitRoleMap();
    }

    public static Role GetRoleInfoById(int id)
    {
        return roleMap[id];
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