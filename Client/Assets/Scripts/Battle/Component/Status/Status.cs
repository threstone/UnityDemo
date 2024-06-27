public abstract class Status
{
    public StatusEnum Type;
    protected RoleEntity entity;
    protected Simulator simulator { get { return entity.Simulator; } }

    public Status(RoleEntity entity)
    {
        this.entity = entity;
    }
    public abstract void FixedUpdate(int curFrame);
    public abstract string GetAnimationName();
    public abstract int GetAnimatorSpeed ();
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