/*
 * 角色实体
 */

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

    public bool Face { get; set; }

    public RoleEntity(Role role) : base(role.PlayerId)
    {
        EquipmentComponent = new EquipmentComponent(role);
        BuffComponent = new BuffComponent(this);
        AttrComponent = new AttrComponent(role, EquipmentComponent, BuffComponent);
        StatusComponent = new StatusComponent(this);
        Collider = new CircleCollider(this, AttrComponent.ColliderRadius);
        AttackComponent = new AttackComponent(this);
    }

    public override void FixedUpdate(int curFrame)
    {
        EquipmentComponent.FixedUpdate(curFrame);
        BuffComponent.FixedUpdate(curFrame);
        AttrComponent.FixedUpdate();
        StatusComponent.FixedUpdate(curFrame);
    }
}