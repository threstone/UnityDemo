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
    /// <summary> 被控制 </summary>
    OnBeControlled,
    /// <summary> 当被攻击前 </summary>
    OnPreBeAttacked,
    /// <summary> 当被攻击后 </summary>
    OnAfterBeAttacked,
    /// <summary> 消费伤害前 </summary>
    OnPreHandleDamage,
    /// <summary> 当消费伤害 </summary>
    OnHandleDamage,
    /// <summary> 消费伤害后 </summary>
    OnAfterHandleDamage,
    /// <summary> 
    /// 当攻击前 
    /// 被动技能影响的暴击等特效   冰眼、暴击等
    /// 主动技能影响的攻击特效     小黑冰箭等
    /// buff 蓝猫超负荷  todo
    /// 致盲使IsMiss = true ; 克敌机先使IsMiss = false和NoMiss = true
    /// </summary>
    OnPreAttack,
    /// <summary> 当创造的伤害被消费,多用于吸血 </summary>
    OnDamageBeHandled,
    /// <summary> 角色死亡 </summary>
    OnRoleDead,
}

public class BuffData
{
    public int BuffId { get; set; }
    public int Duration { get; set; }
}