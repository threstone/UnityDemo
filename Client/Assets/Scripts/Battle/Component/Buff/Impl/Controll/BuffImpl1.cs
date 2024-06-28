public class BuffImpl1 : Buff
{
    /// <summary> 眩晕 </summary>
    public BuffImpl1(BuffConfig buffConfig, int duration, RoleEntity entity, RoleEntity sourceEntity) : base(buffConfig, duration, entity, sourceEntity)
    {
    }

    public new void OnBuffAdd()
    {
        entity.StatusComponent.UpdateControllStatus();
    }

    public new void OnBuffEnd()
    {
        entity.StatusComponent.UpdateControllStatus();
    }

    public new void OnBuffClear()
    {
        entity.StatusComponent.UpdateControllStatus();
    }
}