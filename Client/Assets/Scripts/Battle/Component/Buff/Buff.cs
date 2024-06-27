using System;

public abstract class Buff : GameNode
{
    // buff的所有者
    protected readonly RoleEntity entity;
    // buff的创建者
    public readonly RoleEntity SourceEntity;
 
    public readonly BuffConfig BuffConfig;
    public int Duration { get; set; }

    public Buff(BuffConfig buffConfig, int duration, RoleEntity entity, RoleEntity sourceEntity) : base(entity?.Event)
    {
        BuffConfig = buffConfig;
        Duration = duration;
        this.entity = entity;
        SourceEntity = sourceEntity;
    }

    public void FixedUpdate(int curFrame)
    {
        Duration--;
        if (Duration < 0)
        {
            entity.BuffComponent.RemoveBuff(BuffConfig.Id);
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
