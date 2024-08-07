﻿using System;

public class AttrComponent : AttrEntity
{
    readonly RoleEntity entity;


    /// <summary> 生命属性 </summary>
    public LimitedStat Hp;
    /// <summary> 魔法属性 </summary>
    public LimitedStat Mana;

    int recoverProgress = 0;
    /// <summary> 恢复间隔 </summary>
    static readonly int recoverInterval = 10;

    public AttrComponent(RoleEntity entity) : base(entity.Role)
    {
        this.entity = entity;

        entity.Event.On(EventEnum.OnPreBeAttacked, new Action<Damage>(OnPreBeAttacked));
        entity.Event.On(EventEnum.OnHandleDamage, new Action<Damage>(OnHandleDamage));

        InitHPAndMana();
    }

    public void FixedUpdate()
    {
        Recover();
    }

    /// <summary> 被攻击前 </summary>
    public void OnPreBeAttacked(Damage damage)
    {
        if (damage.NoMiss == false && damage.IsMiss == false)
        {
            damage.IsMiss = IsMiss();
        }
    }

    /// <summary> 消费伤害 </summary>
    public void OnHandleDamage(Damage damage)
    {
        // 伤害 
        switch (damage.DamageType)
        {   // 物理伤害 
            case DamageTypeEnum.PhysicalDamage:
                HandlePhysicalDamage(damage);
                break;
            // 魔法伤害
            case DamageTypeEnum.MagicalDamage:
                HandleMagicalDamage(damage);
                break;
            // 纯粹伤害
            case DamageTypeEnum.PureDamage:
                HandlePureDamage(damage);
                break;
        }
        // 派发伤害成功被消费,多用于造成伤害者的吸血
        damage.SourceEntity.Event.Emit(EventEnum.OnDamageBeHandled, damage);
        // 死亡检查
        if (Hp.Current <= 0) entity.Event.Emit(EventEnum.OnRoleDead);
    }

    /// <summary> 消费物理伤害 </summary>
    void HandlePhysicalDamage(Damage damage)
    {
        damage.RealValue -= damage.BlockDamage;
        // 物理伤害计算护甲 
        damage.RealValue = (int)((long)damage.RealValue * (10000 - PhysicalDamageReduceRatio) / 10000);
        Hp.Add(-Math.Max(0, damage.RealValue));
    }

    /// <summary> 消费魔法伤害 </summary>
    void HandleMagicalDamage(Damage damage)
    {
        damage.RealValue -= damage.BlockDamage;
        damage.RealValue = Convert.ToInt32(damage.RealValue * MagicalDamageReduceRatio);
        Hp.Add(-Math.Max(0, damage.RealValue));
    }

    /// <summary> 消费纯粹伤害 </summary>
    void HandlePureDamage(Damage damage)
    {
        damage.RealValue -= damage.BlockDamage;
        Hp.Add(-Math.Max(0, damage.RealValue));
    }

    /// <summary> 魔法、生命恢复 </summary>
    void Recover()
    {
        if (recoverProgress >= recoverInterval)
        {
            // 每N帧恢复一次生命和血量,减少计算量 
            Hp.Add(recoverInterval * HpRecoverySpeed * Simulator.FrameInterval / Simulator.TimeUnitRatioBySecond);
            Mana.Add(recoverInterval * ManaRecoverySpeed * Simulator.FrameInterval / Simulator.TimeUnitRatioBySecond);
            recoverProgress = -1;
        }
        recoverProgress++;
    }

    /// <summary> 初始化生命、魔法属性  </summary>
    void InitHPAndMana()
    {
        Hp = new(MaxHp);
        Mana = new(MaxMana);
    }

    /// <summary> 获取普通攻击伤害对象 </summary>
    public Damage GetAttack()
    {
        var attack = Attack;
        // 百分之5波动,未来角色可能引入波动值
        var limit = attack * 5 / 100;
        attack += entity.Simulator.RandomNext(-limit, limit);
        var damage = Damage.GetDamage(entity, DamageTypeEnum.PhysicalDamage, attack, false);
        damage.AttackAnimation = "Attack";
        // 顺劈斩会修改攻击动画
        entity.Event.Emit(EventEnum.OnPreAttack, damage);
        return damage;
    }

    /// <summary> 是否闪避 </summary>
    public bool IsMiss()
    {
        if (BaseAttr.RoleMiss == 0 && BaseAttr.ItemMiss == 0 && SkillMiss == 0) return false;

        // (1-角色基础闪避)*(1-物品提供闪避)*(1-技能提供闪避) 
        var ratio = (1 - (double)BaseAttr.RoleMiss / 10000)
              * (1 - (double)AddAttr.ItemMiss / 10000)
              * (1 - (double)SkillMiss / 10000);

        return entity.Simulator.RandomNext(0, 10000) > Convert.ToInt32(ratio * 10000);
    }
}