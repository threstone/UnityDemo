using System;

public abstract class Skill
{

    public int Id { get; set; }
    public int Level { get; set; }

    public SkillConfig Config { get; set; }

    public int CD { get; set; } = 0;

    public Skill(SkillConfig config, int lv)
    {
        Level = lv;
        Config = config;
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
}