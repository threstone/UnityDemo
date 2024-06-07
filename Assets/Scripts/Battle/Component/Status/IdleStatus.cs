public class IdleStatus : Status
{
    int idleFrame;
    public IdleStatus(RoleEntity entity, float idleSecond = 1.0f) : base(entity)
    {
        Type = StatusEnum.Idle;
        idleFrame = (int)(idleSecond / Simulator.FrameInterval);
    }


    public override void FixedUpdate(int curFrame)
    {
        idleFrame--;
        if (idleFrame < 0)
        {
            entity.StatusComponent.Status = new MoveStatus(entity);
        }
    }

    public override string GetName()
    {
        return "idle";
    }

    public override float GetAnimatorSpeed (){
        return 1f;
    }
}