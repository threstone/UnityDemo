using System.Collections.Generic;

public class Damage
{
    // 伤害类型
    public DamageTypeEnum Type;
    // 伤害值
    public int DamageValue;
    // 是否暴击
    public bool IsCriticalHit;

    public List<BuffData> BuffList;

    // 额外伤害 例如攻击触发的金箍棒特效  火女魔镜带来的技能额外伤害    
    public List<Damage> ExtraDamage;

    public Damage(DamageTypeEnum type, int damageValue, bool isCriticalHit)
    {
        Type = type;
        DamageValue = damageValue;
        IsCriticalHit = isCriticalHit;
    }

    public static Damage GetDamage(DamageTypeEnum type, int damageValue, bool isCriticalHit = false)
    {
        return new Damage(type, damageValue, isCriticalHit);
    }

    public static void DestroyDamage(Damage damage)
    {

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