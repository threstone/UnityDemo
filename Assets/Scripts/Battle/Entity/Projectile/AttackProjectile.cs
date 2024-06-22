using System.Numerics;

/*
 * 攻击弹道类
 * 实现撞击虚函数
 */
public class AttackProjectile : Projectile
{
    readonly RoleEntity target;
    public AttackProjectile(RoleEntity source) : base(source)
    {
        target = (source.StatusComponent.Status as AttackStatus).LockEnemy;
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
            TriggerDistance = 5000;
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
        target.AttackComponent.BeAttack(Source);
        IsDestroy = true;
    }
}