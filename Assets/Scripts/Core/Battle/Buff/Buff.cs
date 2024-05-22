using System;

public class Buff
{
    public int BuffType { get; set; }

    int duration;

    RoleEntity entity;
    public Buff(RoleEntity entity, int statusType, int duration)
    {
        this.entity = entity;
        BuffType = statusType;
        this.duration = duration;
    }

    public void FixedUpdate(int curFrame)
    {
        duration--;
        if (duration < 0)
        {
            Stop();
            return;
        }

        // do something
    }

    public void UpdateDuration(int duration)
    {
        this.duration = Math.Max(duration, this.duration);
    }

    void Stop()
    {
        entity.RemoveBuff(BuffType);
    }
}
