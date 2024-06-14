// 被动技能
public class PassiveSkill : Skill
{
    public new PassiveSkillConfig Config { get; }


    public PassiveSkill(PassiveSkillConfig config, int lv) : base(config, lv)
    {
    }
}

// 被动技能类型
public enum PassiveSkillTypeEnum
{
    Normal,// 普通被动 非默认类型即为概率型被动，概率性被动需要确定优先级
    AttackMiss,// 物理攻击闪避
    PhysicalAttackBlock,//物理攻击格挡
    CriticalHit,// 暴击
}