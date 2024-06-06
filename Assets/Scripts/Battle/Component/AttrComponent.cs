public class AttrComponent
{
    readonly RoleEntity entity;
    public RoleConfig BaseInfo;

    // 碰撞体积
    public float ColliderRadius { get; set; }
    // 攻击距离
    public float AtkRange { get; set; }
    // 移动速度
    public float MoveSpeed { get; set; }

    // 基础力量(由等级提升获得的) 白字
    public float StrengthBase { get; set; }
    // 基础智力(由等级提升获得的) 白字
    public float IntelligenceBase { get; set; }
    // 基础敏捷(由等级提升获得的) 白字
    public float AgilityBase { get; set; }

    // 额外力量(由装备、buff等提供的) 绿字
    public float StrengthAdd { get; set; }
    // 额外智力(由装备、buff等提供的) 绿字
    public float IntelligenceAdd { get; set; }
    // 额外敏捷(由装备、buff等提供的) 绿字
    public float AgilityAdd { get; set; }

    // 攻击力
    public float Attack { get; set; }
    // 攻击间隔
    public float AttackInterval { get; set; }
    public AttrComponent( RoleEntity entity)
    {
        this.entity = entity;
        BaseInfo = ConfigMgr.CloneRoleInfoById(entity.Role.RoleId);
        InitBaseAttr();
        InitEquipments();
    }

    void InitBaseAttr()
    {
        ColliderRadius = BaseInfo.ColliderRadius;
        AtkRange = BaseInfo.AtkRange;
        MoveSpeed = BaseInfo.MoveSpeed;

        // 基础属性计算
        StrengthBase = (entity.Role.Level - 1) * BaseInfo.StrengthGain + BaseInfo.BaseStrength;
        IntelligenceBase = (entity.Role.Level - 1) * BaseInfo.IntelligenceGain + BaseInfo.BaseIntelligence;
        AgilityBase = (entity.Role.Level - 1) * BaseInfo.AgilityGain + BaseInfo.BaseAgility;

    }

    void InitEquipments()
    {
        // 装备逻辑
        for (int i = 0; i < entity.Equipments.Length; i++)
        {
            var equipment = entity.Equipments[i];
        }
        // 额外属性计算
        StrengthAdd = 0;
        IntelligenceAdd = 0;
        AgilityAdd = 0;
    }
}