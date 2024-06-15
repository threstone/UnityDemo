using System;
using System.Collections.Generic;

public class AttrComponent
{
    readonly RoleEntity entity;

    public List<Damage> CurFranmeDamages;

    public RoleConfig BaseAttr;
    public AttrObject AttrAdd;
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
        AttrAdd = new AttrObject();
        CurFranmeDamages = new();

        UpdateAttr();
        InitHPAndMana();
    }

    // 消费伤害，消费后存入已处理伤害List中
    public void HandleDamage(Damage damage)
    {
        // 伤害
        // todo

        damage.ExtraDamage?.ForEach((d) =>
        {
            HandleDamage(d);
        });
        CurFranmeDamages.Add(damage);
    }

    public void BeforeUpdate()
    {
        ClearDamageList();
    }

    public void FixedUpdate()
    {
        Recover();
    }

    // 检查是否死亡
    public void AfterUpdate()
    {
        if (Hp.Current <= 0) entity.IsDestroy = true;
    }

    // 清除已处理伤害列表
    public void ClearDamageList()
    {
        CurFranmeDamages.ForEach((damage) =>
        {
            Damage.DestroyDamage(damage);
        });
        CurFranmeDamages.Clear();
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

    // 更新额外属性,来自装备或buff
    void UpdateAddAttr()
    {
        AttrAdd.Reset();
        // 装备属性增加
        for (int i = 0; i < entity.EquipmentComponent.Equipments.Length; i++)
        {
            var equipment = entity.EquipmentComponent.Equipments[i];
            AttrAdd.AddAttrFromTarget(equipment?.Config);
        }

        // todo buff属性计算 

        // todo 来自技能的属性计算
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
        var attack = Attack;
        // 百分之5波动,未来角色可能引入波动值
        var limit = attack * 5 / 100;
        attack += entity.Simulator.RandomNext(-limit, limit);
        var damage = Damage.GetDamage(DamageTypeEnum.PhysicalDamage, attack);
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
            return (int)(BaseAttr.Hp + AttrAdd.Hp + (long)GetAttrByType(MajorAttrEnum.Strength) * ConfigMgr.Common.StrengthAddHp / 10000);
        }
    }
    // 魔法上限上限
    public int MaxMana
    {
        get
        {
            return (int)(BaseAttr.Mana + AttrAdd.Mana + (long)GetAttrByType(MajorAttrEnum.Intelligence) * ConfigMgr.Common.IntelligenceAddMana / 10000);
        }
    }

    // 生命恢复速度
    public int HpRecoverySpeed
    {
        get
        {
            return BaseAttr.HpRecoverySpeed + AttrAdd.HpRecoverySpeed
            + GetAttrByType(MajorAttrEnum.Strength) * ConfigMgr.Common.StrengthAddHpRecover / 10000;
        }
    }

    // 魔法恢复速度
    public int ManaRecoverySpeed
    {
        get
        {
            return BaseAttr.ManaRecoverySpeed + AttrAdd.ManaRecoverySpeed
            + GetAttrByType(MajorAttrEnum.Intelligence) * ConfigMgr.Common.IntelligenceAddManaRecovery / 10000;
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