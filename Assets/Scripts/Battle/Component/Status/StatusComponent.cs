public class StatusComponent {
    readonly RoleEntity entity;
    public Status Status { get; set; }
    public StatusComponent(RoleEntity entity)
    {
        this.entity = entity;
        Status = new IdleStatus(entity, 5000);
    }

    public void FixedUpdate(int curFrame)
    {
        Status.FixedUpdate(curFrame);
    }
}