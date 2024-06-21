using System;

public abstract class Buff : GameNode
{
    protected readonly RoleEntity entity;

    public BuffEnum BuffType { get; set; }
    public int Duration { get; set; }

    public Buff(RoleEntity entity, int duration)
    {
        this.entity = entity;
        Duration = duration;
    }

    public void FixedUpdate(int curFrame)
    {
        Duration--;
        if (Duration < 0)
        {
            entity.BuffComponent.RemoveBuff(BuffType);
            OnBuffEnd();
            return;
        }
    }

    public void UpdateDuration(int duration)
    {
        Duration = Math.Max(duration, Duration);
    }

    // 当状态自然结束
    public void OnBuffAdd() { }
    // 当状态自然结束
    public void OnBuffEnd() { }

    // 当Buff被驱散
    public void OnBuffClear() { }
}
