public class StunBuff : Buff
{
    public StunBuff(int duration, RoleEntity entity, RoleEntity sourceEntity) : base(duration, entity, sourceEntity)
    {
        BuffType = BuffEnum.Stun;
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