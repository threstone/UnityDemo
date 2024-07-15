public class BuffImpl1 : Buff
{
    /// <summary> 眩晕 </summary>
    public BuffImpl1(BuffConfig buffConfig, int duration, RoleEntity entity, RoleEntity sourceEntity) : base(buffConfig, duration, entity, sourceEntity)
    {
    }

    public new void OnBuffAdd()
    {
        entity.BuffComponent.UpdateControllStatus();
    }

    public new void OnBuffEnd()
    {
        entity.BuffComponent.UpdateControllStatus();
    }

    public new void OnBuffClear()
    {
        entity.BuffComponent.UpdateControllStatus();
    }
}