using System;

public class AttrComponent
{
    readonly RoleEntity entity;

    public RoleConfig BaseAttr;
    public AttrObject AddAttr;
    // 生命属性
    public LimitedStat Hp;
    // 魔法属性
    public LimitedStat Mana;

    int recoverProgress = 0;
    // 恢复间隔
    static readonly int recoverInterval = 10;

    public AttrComponent(RoleEntity entity)
    {
        this.entity = entity;
        BaseAttr = ConfigMgr.CloneRoleInfoById(entity.Role.RoleId);
        AddAttr = new AttrObject();

        entity.Event.On(EventEnum.OnHandleDamage, new Action<Damage>(OnHandleDamage));

        UpdateAttr();
        InitHPAndMana();
    }

    public void FixedUpdate()
    {
        Recover();
    }

    // 消费伤害
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
        damage.Entity.Event.Emit(EventEnum.OnDamageBeHandled, damage);
        // 死亡检查
        if (Hp.Current <= 0) entity.Event.Emit(EventEnum.OnRoleDead);
    }

    // 消费物理伤害
    void HandlePhysicalDamage(Damage damage)
    {
        damage.RealValue -= damage.BlockDamage;
        // 物理伤害计算护甲
        var armor = Armor;
        var reduceRadio = (long)130000 * armor / (2250000 + 12 * armor); // 伤害变化万分比
        damage.RealValue = (int)(damage.RealValue * (10000 - reduceRadio) / 10000);
        Hp.Add(-damage.RealValue);
    }

    // 消费魔法伤害
    void HandleMagicalDamage(Damage damage)
    {
        damage.RealValue -= damage.BlockDamage;
        // 实际伤害 = 预期伤害*(1-角色基础魔法抗性)*(1-物品魔法抗性)*(1-技能魔法抗性)
        damage.RealValue = Convert.ToInt32(
           damage.RealValue
           * (1 - (double)BaseAttr.RoleMagicResistance / 10000)
           * (1 - (double)AddAttr.ItemMagicResistance / 10000)
           * (1 - (double)SkillMagicResistance / 10000)
        );
        Hp.Add(-damage.RealValue);
    }

    // 消费纯粹伤害
    void HandlePureDamage(Damage damage)
    {
        damage.RealValue -= damage.BlockDamage;
        Hp.Add(-damage.RealValue);
    }

    // 魔法、生命恢复
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

    // 初始化生命、魔法属性 
    void InitHPAndMana()
    {
        Hp = new(MaxHp);
        Mana = new(MaxMana);
    }

    // 更新属性
    public void UpdateAttr()
    {
        UpdateBaseAttr();
        UpdateAddAttr();
    }

    // 更新基础属性
    void UpdateBaseAttr()
    {
        // 基础属性计算
        BaseAttr.Strength += (entity.Role.Level - 1) * BaseAttr.StrengthGain;
        BaseAttr.Intelligence += (entity.Role.Level - 1) * BaseAttr.IntelligenceGain;
        BaseAttr.Agility += (entity.Role.Level - 1) * BaseAttr.AgilityGain;
    }

    // 更新额外属性,来自装备或buff或技能
    void UpdateAddAttr()
    {
        // 装备属性增加
        for (int i = 0; i < entity.EquipmentComponent.Equipments.Length; i++)
        {
            var equipment = entity.EquipmentComponent.Equipments[i];
            AddAttr.AddAttrFromTarget(equipment?.Config);
        }
    }

    // 根据类型获取指定属性的值
    public int GetAttrByType(MajorAttrEnum type)
    {
        return type switch
        {
            MajorAttrEnum.Strength => BaseAttr.Strength + AddAttr.Strength,
            MajorAttrEnum.Intelligence => BaseAttr.Intelligence + AddAttr.Intelligence,
            MajorAttrEnum.Agility => BaseAttr.Agility + AddAttr.Agility,
            _ => 0,
        };
    }

    // 获取普通攻击伤害对象
    public Damage GetAttack()
    {
        var attack = Attack;
        // 百分之5波动,未来角色可能引入波动值
        var limit = attack * 5 / 100;
        attack += entity.Simulator.RandomNext(-limit, limit);
        var damage = Damage.GetDamage(entity, DamageTypeEnum.PhysicalDamage, attack, false);
        // OnPreAttack()
        // todo 被动技能影响的暴击等特效   冰眼、暴击等
        // todo 主动技能影响的攻击特效     小黑冰箭等
        // todo buff 蓝猫超负荷
        return damage;
    }

    //是否闪避
    public bool IsMiss()
    {
        // todo
        return false;
    }

    // ==================================================================
    // ======================= 以下为获取属性的方法 =======================
    // ==================================================================

    // 生命上限
    public int MaxHp
    {
        get
        {
            return (int)(BaseAttr.Hp + AddAttr.Hp + (long)GetAttrByType(MajorAttrEnum.Strength) * ConfigMgr.Common.StrengthAddHp / 10000);
        }
    }
    // 魔法上限上限
    public int MaxMana
    {
        get
        {
            return (int)(BaseAttr.Mana + AddAttr.Mana + (long)GetAttrByType(MajorAttrEnum.Intelligence) * ConfigMgr.Common.IntelligenceAddMana / 10000);
        }
    }

    // 生命恢复速度
    public int HpRecoverySpeed
    {
        get
        {
            return BaseAttr.HpRecoverySpeed + AddAttr.HpRecoverySpeed
            + GetAttrByType(MajorAttrEnum.Strength) * ConfigMgr.Common.StrengthAddHpRecover / 10000;
        }
    }

    // 魔法恢复速度
    public int ManaRecoverySpeed
    {
        get
        {
            return BaseAttr.ManaRecoverySpeed + AddAttr.ManaRecoverySpeed
            + GetAttrByType(MajorAttrEnum.Intelligence) * ConfigMgr.Common.IntelligenceAddManaRecovery / 10000;
        }
    }

    // 移动速度
    public int MoveSpeed { get { return BaseAttr.MoveSpeed + AddAttr.MoveSpeed; } }

    // 碰撞半径
    public int ColliderRadius { get { return BaseAttr.ColliderRadius + AddAttr.ColliderRadius; } }

    // 攻击距离
    public int AtkRange { get { return BaseAttr.AtkRange + AddAttr.AtkRange; } }

    // 攻击力
    public int Attack { get { return BaseAttr.Attack + AddAttr.Attack + RoleMajorAttr; } }

    // 主属性
    public int RoleMajorAttr { get { return GetAttrByType(BaseAttr.MajorAttr); } }

    // 总攻速 todo 高攻速效果有点差,主要是动画的问题
    public int AtkSpeed { get { return BaseAttr.AtkSpeed + AddAttr.AtkSpeed + GetAttrByType(MajorAttrEnum.Agility); } }

    // 攻击间隔  限制最低攻击间隔 万分之1700秒
    public int AttackInterval { get { return Math.Max(10000 * 10000 / AttackTimesPer10000Sec, 1700); } }

    // 每一万秒攻击次数 (基础攻速+额外攻速)/(基础攻击间隔)
    public int AttackTimesPer10000Sec { get { return AtkSpeed * 100 / (BaseAttr.AtkInterval + AddAttr.AtkInterval); } }

    // 护甲
    public int Armor { get { return (int)((long)GetAttrByType(MajorAttrEnum.Agility) * ConfigMgr.Common.AgilityAddArmor / 10000 + BaseAttr.Armor + AddAttr.Armor); } }

    // 技能魔法抗性
    public int SkillMagicResistance { get; set; } = 0;
}