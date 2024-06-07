using System;

public class AttrComponent
{
    public RoleConfig BaseAttr;
    public AttrObject AttrAdd;
    readonly EquipmentComponent equipmentComponent;
    readonly BuffComponent buffComponent;
    public Role Role;

    // 移动速度
    public float MoveSpeed { get { return BaseAttr.MoveSpeed + AttrAdd.MoveSpeed; } }
    // 碰撞半径
    public float ColliderRadius { get { return BaseAttr.ColliderRadius + AttrAdd.ColliderRadius; } }
    // 攻击距离
    public float AtkRange { get { return BaseAttr.AtkRange + AttrAdd.AtkRange; } }

    // 攻击力
    public float Attack { get { return BaseAttr.Attack + AttrAdd.Attack + RoleMajorAttr; } }

    // 主属性
    public float RoleMajorAttr { get { return GetAttrByType(BaseAttr.MajorAttr); } }

    // 总攻速 todo 高攻速效果有点差
    public float AtkSpeed { get { return BaseAttr.AtkSpeed + AttrAdd.AtkSpeed + GetAttrByType(MajorAttrEnum.Agility); } }

    // 攻击间隔  限制最低攻击间隔0.17
    public float AttackInterval { get { return MathF.Max(1 / AttackPerSecond, 0.17f); } }
    // 每秒攻击次数 (基础攻速+额外攻速)/(基础攻击间隔*100)
    public float AttackPerSecond { get { return AtkSpeed / ((BaseAttr.AtkInterval + AttrAdd.AtkInterval) * 100); } }

    public AttrComponent(Role role, EquipmentComponent equipmentComponent, BuffComponent buffComponent)
    {
        Role = role;
        BaseAttr = ConfigMgr.CloneRoleInfoById(Role.RoleId);
        AttrAdd = new AttrObject();
        this.equipmentComponent = equipmentComponent;
        this.buffComponent = buffComponent;
        UpdateAttr();
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
    public float GetAttrByType(MajorAttrEnum type)
    {
        return type switch
        {
            MajorAttrEnum.Strength => BaseAttr.Strength + AttrAdd.Strength,
            MajorAttrEnum.Intelligence => BaseAttr.Intelligence + AttrAdd.Intelligence,
            MajorAttrEnum.Agility => BaseAttr.Agility + AttrAdd.Agility,
            _ => 0,
        };
    }
}