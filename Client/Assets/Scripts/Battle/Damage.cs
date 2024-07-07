using System.Collections.Generic;

public class Damage
{
    static readonly Pool<Damage> damagePool = new();

    public static Damage GetDamage(RoleEntity entity, DamageTypeEnum type, int damageValue, bool isSkill, bool isCriticalHit = false)
    {
        var result = damagePool.Get();
        result.InitData(entity, type, damageValue, isSkill, isCriticalHit);
        return result;
    }

    public static void DestroyDamage(Damage damage)
    {
        damage.Reset();
        damagePool.Back(damage);
    }

    public RoleEntity Entity;

    /// <summary> 伤害值 </summary>
    public int DamageValue { get; set; }
    /// <summary> 真实造成的伤害 </summary>
    public int RealValue { get; set; }
    /// <summary> 格挡伤害 </summary>
    public int BlockDamage { get; set; }
    /// <summary> 伤害类型 </summary>
    public DamageTypeEnum DamageType { get; set; }

    public bool IsShow;

    /// <summary> 是否是技能伤害 </summary>
    public bool IsSkill { get; set; }
    /// <summary> 是否暴击 </summary>
    public bool IsCriticalHit { get; set; }
    /// <summary> 无视闪避 </summary>
    public bool NoMiss { get; set; }
    /// <summary> 是否闪避 </summary>
    public bool IsMiss { get; set; }

    public List<BuffData> BuffList { get; } = new();

    /// <summary> 额外伤害 例如攻击触发的金箍棒特效  火女魔晶带来的技能额外伤害     </summary>
    public List<Damage> ExtraDamage { get; } = new();

    private void InitData(RoleEntity entity, DamageTypeEnum type, int damageValue, bool isSkill, bool isCriticalHit)
    {
        Entity = entity;
        DamageValue = damageValue;
        RealValue = damageValue;
        DamageType = type;
        IsSkill = isSkill;
        IsCriticalHit = isCriticalHit;
        BlockDamage = 0;
        IsShow = false;
        NoMiss = false;
        IsMiss = false;
    }

    /// <summary> 把引用去掉 </summary>
    private void Reset()
    {
        Entity = null;
        BuffList.Clear();
        ExtraDamage.Clear();
    }

    /// <summary> 设置不可闪避 </summary>
    public void SetNoMiss()
    {
        NoMiss = true;
        IsMiss = false;
    }
}

public enum DamageTypeEnum
{
    /// <summary> 物理伤害 </summary>
    PhysicalDamage,
    /// <summary> 魔法伤害 </summary>
    MagicalDamage,
    /// <summary> 纯粹伤害 </summary>
    PureDamage
}