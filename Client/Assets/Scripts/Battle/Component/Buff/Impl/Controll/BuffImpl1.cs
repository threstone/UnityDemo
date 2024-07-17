/// <summary> 眩晕 </summary>
public class BuffImpl1 : Buff
{
    StunBehavior behavior;
    public BuffImpl1(
        BuffConfig buffConfig,
        int duration,
        RoleEntity entity,
        RoleEntity sourceEntity,
        params int[] args
    ) : base(buffConfig, duration, entity, sourceEntity)
    {
    }

    public override void OnBuffAdd()
    {
        behavior = new StunBehavior(entity.BehaviorComponent)
        {
            Sort = BuffConfig.ControllSort
        };
        entity.BehaviorComponent.AddBehavior(behavior);
    }

    public override void OnBuffEnd()
    {
        entity.BehaviorComponent.RemoveBehavior(behavior);
    }

    public override void OnBuffClear()
    {
        entity.BehaviorComponent.RemoveBehavior(behavior);
    }
}