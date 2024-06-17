using System;

public abstract class Skill : GameNode
{

    public int Id { get; set; }
    public int Level { get; set; }
    protected readonly RoleEntity entity;

    public SkillConfig Config { get; set; }

    public int CD { get; set; } = 0;

    public Skill(SkillConfig config, int lv, RoleEntity entity)
    {
        Level = lv;
        Config = config;
        this.entity = entity;
    }

    public void ReduceCD(int v)
    {
        if (CD == 0) return;
        CD = Math.Max(0, CD - v);
    }

    // 是否可以使用
    public bool IsUseful()
    {
        return CD == 0;
    }

    // 当技能被使用,增加冷却时间
    public void OnSkillUsed()
    {
        CD += Config.CD[Level];
    }
}