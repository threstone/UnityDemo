/*
 * 角色实体
 */

public class RoleEntity : SceneEntity
{
    public AttrComponent AttrComponent { get; set; }
    public EquipmentComponent EquipmentComponent { get; set; }
    public BuffComponent BuffComponent { get; set; }
    public StatusComponent StatusComponent { get; set; }

    public bool Face { get; set; }

    public bool IsDead { get; set; }


    public RoleEntity(Role role, int id) : base(role.PlayerId, id)
    {
        IsDead = false;
        EquipmentComponent = new EquipmentComponent(role);
        BuffComponent = new BuffComponent(this);
        AttrComponent = new AttrComponent(role, EquipmentComponent, BuffComponent);
        StatusComponent = new StatusComponent(this);
        Collider = new CircleCollider(this, AttrComponent.ColliderRadius);
    }

    public override void FixedUpdate(int curFrame)
    {
        base.FixedUpdate(curFrame);
        EquipmentComponent.FixedUpdate(curFrame);
        BuffComponent.FixedUpdate(curFrame);
        StatusComponent.FixedUpdate(curFrame);
    }

    public void Dead()
    {
        IsDead = true;
        Collider = null;
    }
}