using System.Collections.Generic;

public class Damage
{
    // 伤害类型
    public DamageTypeEnum Type;
    // 伤害值
    public int DamageValue;
    // 是否暴击
    public bool IsCriticalHit;

    public List<Buff> DebuffList;

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