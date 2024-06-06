public class AttrComponent
{
    public RoleConfig BaseAttr;
    public AttrObject AttrAdd;
    readonly EquipmentComponent equipmentComponent;
    BuffComponent buffComponent;
    public Role Role;

    public float MoveSpeed { get { return BaseAttr.MoveSpeed + AttrAdd.MoveSpeed; } }
    public float ColliderRadius { get { return BaseAttr.ColliderRadius + AttrAdd.ColliderRadius; } }
    public float AtkRange { get { return BaseAttr.AtkRange + AttrAdd.AtkRange; } }
    // 攻击力
    public float Attack { get; set; }
    // 攻击间隔
    public float AttackInterval { get; set; }
    public AttrComponent(Role role, EquipmentComponent equipmentComponent, BuffComponent buffComponent)
    {
        Role = role;
        BaseAttr = ConfigMgr.CloneRoleInfoById(Role.RoleId);
        this.equipmentComponent = equipmentComponent;
        this.buffComponent = buffComponent;
        UpdateAttr();
    }

    // 更新属性
    public void UpdateAttr()
    {
        UpdateBaseAttr();
        UpdateByEquipment();
    }

    // 更新基础属性
    void UpdateBaseAttr()
    {
        // 基础属性计算
        BaseAttr.Strength += (Role.Level - 1) * BaseAttr.StrengthGain;
        BaseAttr.Intelligence += (Role.Level - 1) * BaseAttr.IntelligenceGain;
        BaseAttr.Agility += (Role.Level - 1) * BaseAttr.AgilityGain;
    }

    // 更新来自装备的属性
    void UpdateByEquipment()
    {
        AttrAdd = new AttrObject();
        // 装备属性增加
        //for (int i = 0; i < Equipments.Length; i++)
        //{
        //    var equipment = Equipments[i];
        //}
        //// 额外属性计算
        //StrengthAdd = 0;
        //IntelligenceAdd = 0;
        //AgilityAdd = 0;

        // buff属性计算
        if (buffComponent == null)
        {
            return;
        }
    }
}