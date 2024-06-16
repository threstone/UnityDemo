using System.Numerics;

public class AttackStatus : Status
{
    public readonly RoleEntity LockEnemy;

    public AttackStatus(RoleEntity entity, RoleEntity lockEnemy) : base(entity)
    {
        Type = StatusEnum.Attack;
        LockEnemy = lockEnemy;
        entity.AttackComponent.Reset();
    }

    public override void FixedUpdate(int curFrame)
    {
        TryAttack(curFrame);
    }

    void TryAttack(int curFrame)
    {
        // 是否死亡
        if (LockEnemy.IsDestroy)
        {
            entity.StatusComponent.Status = new IdleStatus(entity);
            return;
        }

        // 攻击距离检测
        if (AttackRangeCheck() == false)
        {
            TryClosedEnemy();
            return;
        }

        // 始终朝向敌方
        entity.Face = entity.Position.X < LockEnemy.Position.X;
        entity.AttackComponent.FixedUpdate(curFrame);
    }

    bool AttackRangeCheck()
    {
        if (LockEnemy.IsDestroy)
        {
            return false;
        }

        return Vector2.Distance(LockEnemy.Position, entity.Position) <= entity.AttrComponent.AtkRange;
    }

    void TryClosedEnemy()
    {
        entity.StatusComponent.Status = new MoveStatus(entity, LockEnemy);
    }

    public override string GetAnimationName()
    {
        return entity.AttackComponent.IsAttacking() ? "attack" : "idle";
    }

    public override int GetAnimatorSpeed()
    {
        return entity.AttackComponent.SpeedUpRate;
    }
}