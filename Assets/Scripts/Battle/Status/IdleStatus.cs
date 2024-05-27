﻿public class IdleStatus : Status
{
    int idleFrame;
    public IdleStatus(RoleEntity entity) : base(entity)
    {
        Type = StatusEnum.Idle;
        idleFrame = (int)(1 / Simulator.FrameInterval);
    }


    public override void FixedUpdate(int curFrame)
    {
        idleFrame--;
        if (idleFrame < 0)
        {
            entity.Status = new MoveStatus(entity);
        }
    }

    public override string GetName()
    {
        return "idle";
    }
}