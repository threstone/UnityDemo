﻿public abstract class Status
{
    public StatusEnum type;
    protected RoleEntity entity;

    public Status(RoleEntity entity)
    {
        this.entity = entity;
    }
    public abstract void FixedUpdate(int curFrame);
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