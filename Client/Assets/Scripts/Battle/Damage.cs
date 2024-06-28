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

    // 伤害值
    public int DamageValue { get; set; }
    // 真实造成的伤害
    public int RealValue { get; set; }
    // 格挡伤害
    public int BlockDamage { get; set; }
    // 伤害类型
    public DamageTypeEnum DamageType { get; set; }

    public bool IsShow;

    // 是否是技能伤害
    public bool IsSkill { get; set; }
    // 是否暴击
    public bool IsCriticalHit { get; set; }
    // 无视闪避
    public bool NoMiss { get; set; }
    // 是否闪避
    public bool IsMiss { get; set; }

    public List<BuffData> BuffList { get; } = new();

    // 额外伤害 例如攻击触发的金箍棒特效  火女魔镜带来的技能额外伤害    
    public List<Damage> ExtraDamage { get; } = new();

    public Damage(RoleEntity entity, DamageTypeEnum type, int damageValue, bool isSkill, bool isCriticalHit)
    {

        InitData(entity, type, damageValue, isSkill, isCriticalHit);
    }

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

    // 把引用去掉
    private void Reset()
    {
        Entity = null;
        BuffList.Clear();
        ExtraDamage.Clear();
    }

    // 设置不可闪避
    public void SetNoMiss()
    {
        NoMiss = true;
        IsMiss = false;
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