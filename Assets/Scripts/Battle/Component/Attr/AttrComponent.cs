using System;


public class AttrComponent
{
    public RoleConfig BaseAttr;
    public AttrObject AttrAdd;
    readonly EquipmentComponent equipmentComponent;
    readonly BuffComponent buffComponent;
    public Role Role;

    // 生命属性
    public LimitedStat Hp;
    // 魔法属性
    public LimitedStat Mana;

    public AttrComponent(Role role, EquipmentComponent equipmentComponent, BuffComponent buffComponent)
    {
        Role = role;
        BaseAttr = ConfigMgr.CloneRoleInfoById(Role.RoleId);
        AttrAdd = new AttrObject();
        this.equipmentComponent = equipmentComponent;
        this.buffComponent = buffComponent;

        UpdateAttr();
        InitHPAndMana();
    }

    public void FixedUpdate()
    {
        UpdateHPAndMana();
    }

    // 魔法、生命恢复
    void UpdateHPAndMana()
    {
        Hp.Add(HpRecoverySpeed);
        Mana.Add(ManaRecoverySpeed);
    }

    void InitHPAndMana()
    {
        Mana = new(MaxHp);
        Hp = new(MaxMana);
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
        BaseAttr.Strength += (Role.Level - 1) * BaseAttr.StrengthGain;
        BaseAttr.Intelligence += (Role.Level - 1) * BaseAttr.IntelligenceGain;
        BaseAttr.Agility += (Role.Level - 1) * BaseAttr.AgilityGain;
    }

    // 更新额外属性,来自装备或buff
    void UpdateAddAttr()
    {
        AttrAdd.Reset();
        // 装备属性增加
        for (int i = 0; i < equipmentComponent.Equipments.Length; i++)
        {
            var equipment = equipmentComponent.Equipments[i];
            AttrAdd.AddAttrFromTarget(equipment?.Attr);
        }

        // buff属性计算 
        if (buffComponent == null)
        {
            return;
        }
        // todo buff属性计算 
    }

    // 根据类型获取指定属性的值
    public int GetAttrByType(MajorAttrEnum type)
    {
        return type switch
        {
            MajorAttrEnum.Strength => BaseAttr.Strength + AttrAdd.Strength,
            MajorAttrEnum.Intelligence => BaseAttr.Intelligence + AttrAdd.Intelligence,
            MajorAttrEnum.Agility => BaseAttr.Agility + AttrAdd.Agility,
            _ => 0,
        };
    }

    // 获取普通攻击伤害对象
    public Damage GetAttack()
    {
        var attack = Attack;// todo
        return Damage.GetDamage(DamageTypeEnum.PhysicalDamage, 111);
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
            return BaseAttr.Hp + AttrAdd.Hp + GetAttrByType(MajorAttrEnum.Strength) * ConfigMgr.Common.StrengthAddHp;
        }
    }
    // 魔法上限上限
    public int MaxMana
    {
        get
        {
            return BaseAttr.Mana + AttrAdd.Mana + GetAttrByType(MajorAttrEnum.Intelligence) * ConfigMgr.Common.IntelligenceAddMana;
        }
    }

    // 生命恢复速度
    public int HpRecoverySpeed
    {
        get
        {
            return BaseAttr.HpRecoverySpeed + AttrAdd.HpRecoverySpeed + GetAttrByType(MajorAttrEnum.Strength) * ConfigMgr.Common.StrengthAddHpRecover;
        }
    }

    // 魔法恢复速度
    public int ManaRecoverySpeed
    {
        get
        {
            return BaseAttr.ManaRecoverySpeed + AttrAdd.ManaRecoverySpeed + GetAttrByType(MajorAttrEnum.Intelligence) * ConfigMgr.Common.IntelligenceAddManaRecovery;
        }
    }

    // 移动速度
    public int MoveSpeed { get { return BaseAttr.MoveSpeed + AttrAdd.MoveSpeed; } }
    // 碰撞半径
    public int ColliderRadius { get { return BaseAttr.ColliderRadius + AttrAdd.ColliderRadius; } }
    // 攻击距离
    public int AtkRange { get { return BaseAttr.AtkRange + AttrAdd.AtkRange; } }

    // 攻击力
    public int Attack { get { return BaseAttr.Attack + AttrAdd.Attack + RoleMajorAttr; } }

    // 主属性
    public int RoleMajorAttr { get { return GetAttrByType(BaseAttr.MajorAttr); } }

    // 总攻速 todo 高攻速效果有点差,主要是动画的问题
    public int AtkSpeed { get { return BaseAttr.AtkSpeed + AttrAdd.AtkSpeed + GetAttrByType(MajorAttrEnum.Agility); } }

    // 攻击间隔  限制最低攻击间隔 万分之1700秒
    public int AttackInterval { get { return Math.Max(10000 * 10000 / AttackTimesPer10000Sec, 1700); } }
    // 每一万秒攻击次数 (基础攻速+额外攻速)/(基础攻击间隔)
    public int AttackTimesPer10000Sec { get { return AtkSpeed * 100 / (BaseAttr.AtkInterval + AttrAdd.AtkInterval); } }
}