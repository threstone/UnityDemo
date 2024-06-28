public class BuffImpl2 : Buff
{
    // 恐惧
    public BuffImpl2(BuffConfig buffConfig, int duration, RoleEntity entity, RoleEntity sourceEntity) : base(buffConfig, duration, entity, sourceEntity)
    {
        // 恐惧只要有其他控制技能,就没办法动,属于是硬控技能里最垃圾的
    }

    public new void FixedUpdate(int curFrame)
    {
        base.FixedUpdate(curFrame);
        // 恐惧的具体逻辑...
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