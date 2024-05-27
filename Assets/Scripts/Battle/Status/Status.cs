public abstract class Status
{
    public StatusEnum Type;
    protected RoleEntity entity;

    public Status(RoleEntity entity)
    {
        this.entity = entity;
    }
    public abstract void FixedUpdate(int curFrame);
    public abstract string GetName();
}

public enum StatusEnum
{
    Idle,
    Move,
    Attack,
    Stun,
    Dead,
    Spell
}