/// <summary> 恐惧 </summary>
public class BuffImpl2 : Buff
{
    FearBehavior behavior;
    public BuffImpl2(
        BuffConfig buffConfig,
        int duration,
        RoleEntity entity,
        RoleEntity sourceEntity,
        params int[] args
    ) : base(buffConfig, duration, entity, sourceEntity)
    {
        /// <summary> 恐惧只要有其他控制技能,就没办法动,属于是硬控技能里最垃圾的 </summary>
    }

    public new void OnBuffAdd()
    {
        behavior = new FearBehavior(entity.BehaviorComponent)
        {
            Sort = BuffConfig.ControllSort
        };
        entity.BehaviorComponent.AddBehavior(behavior);
    }

    public new void OnBuffEnd()
    {
        entity.BehaviorComponent.RemoveBehavior(behavior);
    }

    public new void OnBuffClear()
    {
        entity.BehaviorComponent.RemoveBehavior(behavior);
    }

}