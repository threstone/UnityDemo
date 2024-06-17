public class StunBuff : Buff
{
    public StunBuff(RoleEntity entity, int duration) : base(entity, duration)
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