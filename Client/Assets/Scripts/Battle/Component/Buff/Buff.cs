using System;

public abstract class Buff : GameNode /// <summary> buff池化 </summary>
{
    /// <summary> buff的所有者 </summary>
    protected readonly RoleEntity entity;
    /// <summary> buff的创建者 </summary>
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

    public void UpdateBuff(int duration, params int[] args)
    {
        Duration = Math.Max(duration, Duration);
    }

    /// <summary> 当状态自然结束 </summary>
    public void OnBuffAdd() { }
    /// <summary> 当状态自然结束 </summary>
    public void OnBuffEnd() { }

    /// <summary> 当Buff被驱散 </summary>
    public void OnBuffClear() { }
}
