/*
 * 角色实体
 */
using System.Collections.Generic;

public class RoleEntity : SceneEntity
{
    // 属性组件
    public AttrComponent AttrComponent { get; set; }
    // 装备组件
    public EquipmentComponent EquipmentComponent { get; set; }
    // Buff组件
    public BuffComponent BuffComponent { get; set; }
    // 状态组件
    public StatusComponent StatusComponent { get; set; }
    // 攻击组件
    public AttackComponent AttackComponent { get; set; }
    // 技能组件
    public SkillComponent SkillComponent { get; set; }

    public bool Face { get; set; }

    public Role Role { get; set; }

    public List<Damage> CurFranmeDamages = new();

    public EventManager Event = new();

    public RoleEntity(Role role) : base(role.PlayerId)
    {
        Role = role;
        InitComponent();
    }

    void InitComponent()
    {
        EquipmentComponent = new EquipmentComponent(Role);
        BuffComponent = new BuffComponent(this);
        StatusComponent = new StatusComponent(this);
        AttackComponent = new AttackComponent(this);

        SkillComponent = new SkillComponent(this);// 依赖装备组件,装备拥有技能
        AttrComponent = new AttrComponent(this);  // 最后初始化，依赖其他组件

        Collider = new CircleCollider(this, AttrComponent.ColliderRadius);
    }

    public new void BeforeUpdate(int curFrame)
    {
        ClearDamageList();
    }

    public override void FixedUpdate(int curFrame)
    {
        EquipmentComponent.FixedUpdate(curFrame);
        BuffComponent.FixedUpdate(curFrame);
        AttrComponent.FixedUpdate();
        StatusComponent.FixedUpdate(curFrame);
        SkillComponent.FixedUpdate();
    }

    public new void AfterUpdate(int curFrame)
    {
        AttrComponent.AfterUpdate();
    }

    // 消费伤害
    public void HandleDamage(Damage damage)
    {
        CurFranmeDamages.Add(damage);
        // 消费伤害前
        SkillComponent.OnPreHandleDamage(damage);
        BuffComponent.OnPreHandleDamage(damage);

        // 伤害消费
        AttrComponent.OnHandleDamage(damage);
        BuffComponent.OnHandleDamage(damage);

        // 消费伤害后
        SkillComponent.OnAfterHandleDamage(damage);
        BuffComponent.OnAfterHandleDamage(damage);

        // 消费伤害中的额外伤害
        damage.ExtraDamage?.ForEach((d) => HandleDamage(d));
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
}