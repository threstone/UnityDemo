public class AttackStatus : Status
{
    RoleEntity lockEnemy;

    float duration = 1.0f;
    bool isUp = true;
    public AttackStatus(RoleEntity entity,RoleEntity lockEnemy) : base(entity)
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
        // todo是否死亡


        // 攻击距离检测
        if (AttackRangeCheck() == false)
        {
            TryClosedEnemy();
            return;
        }

        // todo攻击间隔


        DoAttack();
    }

    void DoAttack() { 
        // todo
    }


    bool AttackRangeCheck()
    {
        return false;// todo
    }

    void TryClosedEnemy()
    {
        entity.Status = new MoveStatus(entity,lockEnemy);
    }

    public override string GetName()
    {
        return "attack";
    }
}