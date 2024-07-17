/// <summary> 被动技能 </summary>
public abstract class PassiveSkill : Skill
{
    public PassiveSkill(PassiveSkillConfig config, int lv, RoleEntity entity) : base(config, lv, entity)
    {
    }
}

/// <summary> 被动技能类型 </summary>
public enum PassiveSkillTypeEnum
{
    /// <summary> 普通被动 非默认类型即为概率型被动，概率性被动需要确定优先级 </summary>
    Normal,
    /// <summary> 物理攻击闪避 </summary>
    AttackMiss,
    /// <summary> 物理攻击格挡 </summary>
    PhysicalAttackBlock,
    /// <summary> 暴击 </summary>
    CriticalHit,
}