public class IdleStatus : Status
{
    int idleFrame;
    public IdleStatus(RoleEntity entity, int idleTime = 10000) : base(entity)
    {
        Type = StatusEnum.Idle;
        idleFrame = idleTime / Simulator.FrameInterval;
    }


    public override void FixedUpdate(int curFrame)
    {
        idleFrame--;
        if (idleFrame < 0)
        {
            entity.StatusComponent.Status = new MoveStatus(entity);
        }
    }

    public override string GetAnimationName()
    {
        return "idle";
    }

    public override int GetAnimatorSpeed (){
        return 10000;
    }
}