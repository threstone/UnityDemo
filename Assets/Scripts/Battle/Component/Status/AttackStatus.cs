using System.Numerics;

public class AttackStatus : Status
{
    readonly RoleEntity lockEnemy;

    public AttackStatus(RoleEntity entity, RoleEntity lockEnemy) : base(entity)
    {
        Type = StatusEnum.Attack;
        this.lockEnemy = lockEnemy;
    }

    public override void FixedUpdate(int curFrame)
    {
        TryAttack();
    }

    void TryAttack()
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

        // todo攻击间隔


        DoAttack();
    }

    void DoAttack()
    {
        // 记录当时的攻速与攻击时间
        // 如果攻速没变，那么下一次攻击的时间是固定的，不需要重复算
        // 如果攻速变了，那么下一次攻击的时间就变了，重新计算下一次攻击的时间
        // 到达下次攻击时间，执行攻击
        // 攻击的时候是不可移动的
        // todo
        //Utils.Log("atk");
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
        return "attack";
    }
}