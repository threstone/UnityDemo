public class MoveStatus : Status
{
    RoleEntity lockEnemy;

    float duration = 1.0f;
    bool isUp = true;
    public MoveStatus(RoleEntity entity) : base(entity)
    {
        Type = StatusEnum.Move;
    }

    public MoveStatus(RoleEntity entity,RoleEntity lockEnemy) : this(entity)
    {
        this.lockEnemy = lockEnemy;
    }

    public override void FixedUpdate(int curFrame)
    {
        if (lockEnemy == null)
        {
            TryGetEnemy();
            return;
        }

        if (AttackRangeCheck() == false)
        {
            TryClosedEnemy();
            return;
        }
        entity.Status = new AttackStatus(entity, lockEnemy);
    }

    void TryGetEnemy()
    {
        // todo
        TryClosedEnemy();
    }


    bool AttackRangeCheck()
    {
        return false;// todo
    }

    void TryClosedEnemy()
    {
        // todo
        // test code 
        float len = Simulator.FrameInterval * entity.RoleInfo.MoveSpeed;
        duration -= Simulator.FrameInterval;
        var pos = entity.Position;
        pos.Y += isUp ? len : -len;
        entity.Position = pos;
        if (duration < 0)
        {
            isUp = !isUp;
            duration = 1.0f;
        }
    }

    public override string GetName()
    {
        return "walk";
    }
}