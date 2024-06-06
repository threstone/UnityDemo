public class AttrComponent
{
    readonly RoleEntity entity;
    public RoleConfig BaseAttr;
    public AttrObject AttrAdd;

    public float MoveSpeed { get { return BaseAttr.MoveSpeed + AttrAdd.MoveSpeed; } }
    public float ColliderRadius { get { return BaseAttr.ColliderRadius + AttrAdd.ColliderRadius; } }
    public float AtkRange { get { return BaseAttr.AtkRange + AttrAdd.AtkRange; } }
    // 攻击力
    public float Attack { get; set; }
    // 攻击间隔
    public float AttackInterval { get; set; }
    public AttrComponent(RoleEntity entity)
    {
        this.entity = entity;
        BaseAttr = ConfigMgr.CloneRoleInfoById(entity.Role.RoleId);
        InitBaseAttr();
        InitEquipments();
    }

    void InitBaseAttr()
    {
        // 基础属性计算
        BaseAttr.Strength += (entity.Role.Level - 1) * BaseAttr.StrengthGain;
        BaseAttr.Intelligence += (entity.Role.Level - 1) * BaseAttr.IntelligenceGain;
        BaseAttr.Agility += (entity.Role.Level - 1) * BaseAttr.AgilityGain;
    }

    void InitEquipments()
    {
        AttrAdd = new AttrObject();
        // 装备逻辑
        for (int i = 0; i < entity.Equipments.Length; i++)
        {
            var equipment = entity.Equipments[i];
        }
        //// 额外属性计算
        //StrengthAdd = 0;
        //IntelligenceAdd = 0;
        //AgilityAdd = 0;
    }
}