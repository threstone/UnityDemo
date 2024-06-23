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
    OnBeControll,//被控制
    OnPreBeAttack,// 当被攻击前
    OnAfterBeAttack,// 当被攻击后
    OnPreHandleDamage,// 消费伤害前
    OnHandleDamage,// 当消费伤害
    OnAfterHandleDamage,// 消费伤害后
}

public class BuffData
{
    public BuffEnum BuffType { get; set; }
    public int Duration { get; set; }
}