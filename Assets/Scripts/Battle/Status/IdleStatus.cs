public class IdleStatus : Status
{
    int idleFrame;
    public IdleStatus(RoleEntity entity) : base(entity)
    {
        type = StatusEnum.Idle;
        idleFrame = (int)(1 / Simulator.FrameInterval);
    }


    public override void FixedUpdate(int curFrame)
    {
        idleFrame--;
        if (idleFrame < 0)
        {
            entity.Status = new AttackStatus(entity);
        }
    }
}