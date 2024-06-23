public class StatusComponent
{
    readonly RoleEntity entity;
    public Status Status { get; set; }
    bool beControlled = false;
    public bool BeControlled
    {
        get { return beControlled; }
        set
        {
            if (beControlled == false && value == true)
            {
                entity.Event.Emit(EventEnum.OnBeControll);
            }
            beControlled = value;
        }
    }

    public StatusComponent(RoleEntity entity)
    {
        this.entity = entity;
        Status = new IdleStatus(entity, 5000);
    }

    public void FixedUpdate(int curFrame)
    {
        if (beControlled) return;
        Status.FixedUpdate(curFrame);
    }

    public string GetAnimationName()
    {
        return entity.BuffComponent.GetAnimationName() ?? Status.GetAnimationName();
    }

    // 更新控制状态
    public void UpdateControllStatus()
    {
        BeControlled = entity.BuffComponent.IsControlled();
    }
}