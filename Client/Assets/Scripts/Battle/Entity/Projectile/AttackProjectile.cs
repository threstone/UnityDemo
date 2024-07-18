using System.Numerics;

/*
 * 攻击弹道类
 * 实现撞击虚函数
 */
public class AttackProjectile : Projectile
{
    readonly RoleEntity target;
    readonly Damage damage;

    public AttackProjectile(RoleEntity source, Damage damage) : base(source)
    {
        this.damage = damage;
        target = source.BehaviorComponent.TargetEntity;
        Speed = source.AttrComponent.BaseAttr.AtkProjectileSpeed;

        // 生成位置
        var offset = new Vector2(source.AttrComponent.BaseAttr.AtkProjectilePos[0], source.AttrComponent.BaseAttr.AtkProjectilePos[1]);
        if (!source.Face)
        {
            offset.X = -offset.X;
        }
        Position = source.Position + offset;
    }

    public override void FixedUpdate(int curFrame)
    {
        if (target != null && target.IsDestroy == false)
        {
            TargetPosition = target.Position;
            TargetPosition.Y -= target.AttrComponent.BaseAttr.ProjectileOffset;
            TriggerDistance = 3000;
        }
        else
        {
            TriggerDistance = 500;
        }
        CloseTarget();
        if (Vector2.Distance(Position, TargetPosition) <= TriggerDistance)
        {
            OnTrigger();
            IsDestroy = true;
        }
    }

    // 当弹道到达目标
    public void OnTrigger()
    {
        target.AttackComponent.BeAttack(damage);
        IsDestroy = true;
    }
}