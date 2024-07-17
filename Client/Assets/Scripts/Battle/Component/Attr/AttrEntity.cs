using System;
public class AttrEntity
{
    readonly Role role;
    public RoleConfig BaseAttr;
    public AttrObject AddAttr;
    public AttrEntity(Role role)
    {
        this.role = role;
        BaseAttr = ConfigMgr.CloneRoleInfoById(role.RoleId);
        AddAttr = new AttrObject();

        UpdateAttr();
    }

    /// <summary> 更新属性 </summary>
    public void UpdateAttr()
    {
        UpdateBaseAttr();
        UpdateAddAttr();
    }

    /// <summary> 更新基础属性 </summary>
    void UpdateBaseAttr()
    {
        /// <summary> 基础属性计算 </summary>
        BaseAttr.Strength += (role.Level - 1) * BaseAttr.StrengthGain;
        BaseAttr.Intelligence += (role.Level - 1) * BaseAttr.IntelligenceGain;
        BaseAttr.Agility += (role.Level - 1) * BaseAttr.AgilityGain;
    }

    /// <summary> 更新额外属性,来自装备或buff或技能 </summary>
    void UpdateAddAttr()
    {
        /// <summary> 装备属性增加 </summary>
        for (int i = 0; i < role.EquipmentIdList?.Count; i++)
        {
            var equipmentId = role.EquipmentIdList[i];
            var config = ConfigMgr.GetEquipmentAttr(equipmentId);
            AddAttr.AddAttrFromTarget(config);
        }
    }

    /// <summary> 根据类型获取指定属性的值 </summary>
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

    /// <summary> ================================================================== </summary>
    /// <summary> ======================= 以下为获取属性的方法 ======================= </summary>
    /// <summary> ================================================================== </summary>

    /// <summary> 生命上限 </summary>
    public int MaxHp
    {
        get
        {
            return (int)(BaseAttr.Hp + AddAttr.Hp + (long)GetAttrByType(MajorAttrEnum.Strength) * ConfigMgr.Common.StrengthAddHp / 10000);
        }
    }
    /// <summary> 魔法上限上限 </summary>
    public int MaxMana
    {
        get
        {
            return (int)(BaseAttr.Mana + AddAttr.Mana + (long)GetAttrByType(MajorAttrEnum.Intelligence) * ConfigMgr.Common.IntelligenceAddMana / 10000);
        }
    }

    /// <summary> 生命恢复速度 </summary>
    public int HpRecoverySpeed
    {
        get
        {
            return BaseAttr.HpRecoverySpeed + AddAttr.HpRecoverySpeed
            + GetAttrByType(MajorAttrEnum.Strength) * ConfigMgr.Common.StrengthAddHpRecover / 10000;
        }
    }

    /// <summary> 魔法恢复速度 </summary>
    public int ManaRecoverySpeed
    {
        get
        {
            return BaseAttr.ManaRecoverySpeed + AddAttr.ManaRecoverySpeed
            + GetAttrByType(MajorAttrEnum.Intelligence) * ConfigMgr.Common.IntelligenceAddManaRecovery / 10000;
        }
    }

    /// <summary> 移动速度 </summary>
    public int MoveSpeed { get { return BaseAttr.MoveSpeed + AddAttr.MoveSpeed; } }

    /// <summary> 碰撞半径 </summary>
    public int ColliderRadius { get { return BaseAttr.ColliderRadius + AddAttr.ColliderRadius; } }

    /// <summary> 攻击距离 </summary>
    public int AtkRange { get { return BaseAttr.AtkRange + AddAttr.AtkRange; } }

    /// <summary> 攻击力 </summary>
    public int Attack { get { return BaseAttr.Attack + AddAttr.Attack + RoleMajorAttr; } }

    /// <summary> 主属性 </summary>
    public int RoleMajorAttr { get { return GetAttrByType(BaseAttr.MajorAttr); } }

    /// <summary> 总攻速 todo 高攻速效果有点差,主要是动画的问题 </summary>
    public int AtkSpeed { get { return BaseAttr.AtkSpeed + AddAttr.AtkSpeed + GetAttrByType(MajorAttrEnum.Agility); } }

    /// <summary> 攻击间隔  限制最低攻击间隔 万分之1700秒 </summary>
    public int AttackInterval { get { return Math.Max(10000 * 10000 / AttackTimesPer10000Sec, 1700); } }

    /// <summary> 每一万秒攻击次数 (基础攻速+额外攻速)/(基础攻击间隔) </summary>
    public int AttackTimesPer10000Sec { get { return AtkSpeed * 100 / (BaseAttr.AtkInterval + AddAttr.AtkInterval); } }

    /// <summary> 护甲 </summary>
    public int Armor { get { return (int)((long)GetAttrByType(MajorAttrEnum.Agility) * ConfigMgr.Common.AgilityAddArmor / 10000 + BaseAttr.Armor + AddAttr.Armor); } }

    /// <summary> 护甲决定的物理伤害减免万分比 </summary>
    public int PhysicalDamageReduceRatio
    {
        get
        {
            var armor = Math.Abs(Armor);
            var result = (int)((long)130000 * armor / (2250000 + 12 * armor));
            return Armor < 0 ? -result : result;// 伤害减少万分比
        }
    }

    /// <summary> 技能魔法抗性 </summary>
    public int SkillMagicResistance { get; set; } = 0;

    /// <summary> 魔抗决定的魔法伤害减免百分比  </summary>
    public double MagicalDamageReduceRatio
    {
        get
        {
            /// <summary> (1-角色基础魔法抗性)*(1-物品魔法抗性)*(1-技能魔法抗性) </summary>
            return (1 - (double)BaseAttr.RoleMagicResistance / 10000)
                  * (1 - (double)AddAttr.ItemMagicResistance / 10000)
                  * (1 - (double)SkillMagicResistance / 10000);
        }
    }

    /// <summary> 技能提供闪避率 </summary>
    public int SkillMiss { get; set; } = 0;
}