using System.Numerics;

public class AttackStatus : Status
{
    readonly RoleEntity lockEnemy;

    public AttackStatus(RoleEntity entity, RoleEntity lockEnemy) : base(entity)
    {
        Type = StatusEnum.Attack;
        this.lockEnemy = lockEnemy;
        entity.AttackComponent.Reset();
    }

    public override void FixedUpdate(int curFrame)
    {
        TryAttack(curFrame);
    }

    void TryAttack(int curFrame)
    {
        // 是否死亡
        if (lockEnemy.IsDead)
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

        entity.AttackComponent.FixedUpdate(curFrame);
    }

    bool AttackRangeCheck()
    {
        if (lockEnemy.IsDead)
        {
            return false;
        }

        return Vector2.Distance(lockEnemy.Position, entity.Position) <= entity.AttrComponent.AtkRange;
    }

    void TryClosedEnemy()
    {
        entity.StatusComponent.Status = new MoveStatus(entity, lockEnemy);
    }

    public override string GetName()
    {
        return entity.AttackComponent.IsAttacking() ? "attack" : "idle";
    }

    public override int GetAnimatorSpeed (){
        return entity.AttackComponent.SpeedUpRate;
    }
}