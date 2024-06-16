using System.Collections.Generic;

public class Role
{
    public int RoleId { get; set; }
    public int PlayerId { get; set; }

    public int Level { get; set; }

    public List<int> EquipmentIdList;
    public List<SkillData> SkillList;

    public Role(int roleId, int level, int userId)
    {
        RoleId = roleId;
        Level = level;
        PlayerId = userId;
    }
}

public class SkillData
{
    public int Id;
    public int level;
}

public enum EventEnum
{
    BeControlled//被控制
}

public class BuffData
{
    public int BuffType { get; set; }
    public int Duration { get; set; }
}