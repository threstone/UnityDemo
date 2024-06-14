public class StatusComponent {
    public Status Status { get; set; }
    public StatusComponent(RoleEntity entity)
    {
        Status = new IdleStatus(entity, 5000);
    }

    public void FixedUpdate(int curFrame)
    {
        Status.FixedUpdate(curFrame);
    }
}