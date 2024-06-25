using System.Collections.Generic;

public class Damage
{
    static readonly Stack<Damage> damagePool = new();

    public static Damage GetDamage(RoleEntity entity, DamageTypeEnum type, int damageValue, bool isSkill, bool isCriticalHit = false)
    {
        if (damagePool.Count == 0)
        {
            return new Damage(entity, type, damageValue, isSkill, isCriticalHit);
        }
        var result = damagePool.Pop();
        result.InitData(entity, type, damageValue, isSkill, isCriticalHit);
        return result;
    }

    public static void DestroyDamage(Damage damage)
    {
        damage.Reset();
        damagePool.Push(damage);
    }

    public RoleEntity Entity;
    public bool IsShow ;
    // 伤害类型
    public DamageTypeEnum DamageType { get; set; }
    // 是否是技能伤害
    public bool IsSkill { get; set; }
    // 伤害值
    public int DamageValue { get; set; }
    // 真实造成的伤害
    public int RealValue { get; set; }
    // 是否暴击
    public bool IsCriticalHit { get; set; }
    // 格挡伤害
    public int BlockDamage { get; set; } = 0;
    // 是否闪避
    public bool IsMiss { get; set; }

    public List<BuffData> BuffList { get; set; }

    // 额外伤害 例如攻击触发的金箍棒特效  火女魔镜带来的技能额外伤害    
    public List<Damage> ExtraDamage { get; set; }

    public Damage(RoleEntity entity, DamageTypeEnum type, int damageValue, bool isSkill, bool isCriticalHit)
    {
        InitData(entity, type, damageValue, isSkill, isCriticalHit);
    }

    private void InitData(RoleEntity entity, DamageTypeEnum type, int damageValue, bool isSkill, bool isCriticalHit)
    {
        Entity = entity;
        DamageType = type;
        DamageValue = damageValue;
        RealValue = damageValue;
        IsSkill = isSkill;
        IsCriticalHit = isCriticalHit;
        IsShow = false;
    }

    private void Reset()
    {
        Entity = null;
        BuffList?.Clear();
        ExtraDamage?.Clear();
    }

    public bool IgnoreAttackMiss()
    {
        // todo 遍历Buff list查看是否拥有无视闪避的buff
        return false;
    }
}
public enum DamageTypeEnum
{
    // 物理伤害
    PhysicalDamage,
    // 魔法伤害
    MagicalDamage,
    // 纯粹伤害
    PureDamage
}