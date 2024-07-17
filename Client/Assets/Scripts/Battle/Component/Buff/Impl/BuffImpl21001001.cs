using System;

/// <summary> 眩晕 </summary>
public class BuffImpl21001001 : Buff
{

    int reduceArmor = 0;
    public BuffImpl21001001(
        BuffConfig buffConfig,
        int duration,
        RoleEntity entity,
        RoleEntity sourceEntity,
        params int[] args
    ) : base(buffConfig, duration, entity, sourceEntity)
    {
        reduceArmor = Math.Max(reduceArmor, args[0]);
    }

    public new void UpdateBuff(int duration, params int[] args)
    {
        var newReduceArmor = args[0];
        // 续费
        if (reduceArmor <= newReduceArmor) base.UpdateBuff(duration, args);

        // 更高级的光环
        if (reduceArmor < newReduceArmor)
        {
            reduceArmor = newReduceArmor;
            entity.AttrComponent.AddAttr.Armor -= newReduceArmor - reduceArmor;
        }
    }

    public override void OnBuffAdd()
    {
        entity.AttrComponent.AddAttr.Armor -= reduceArmor;
    }

    public override void OnBuffEnd()
    {
        entity.AttrComponent.AddAttr.Armor += reduceArmor;
    }

    public override void OnBuffClear()
    {
    }
}