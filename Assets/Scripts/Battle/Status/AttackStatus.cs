public class AttackStatus : Status
{
    RoleEntity lockEnemy;
    public AttackStatus(RoleEntity entity) : base(entity)
    {
        type = StatusEnum.Attack;
    }

    public override void FixedUpdate(int curFrame)
    {
        if (lockEnemy == null)
        {
            TryGetEnemy();
            return;
        }

        TryAttack();
    }

    void TryGetEnemy()
    {
        // todo
    }

    void TryAttack()
    {
        if (AttackRangeCheck() == false)
        {
            TryClosedEnemy();
            return;
        }

        // todo
    }

    bool AttackRangeCheck()
    {
        return false;// todo
    }

    void TryClosedEnemy()
    {
        // todo
    }
}