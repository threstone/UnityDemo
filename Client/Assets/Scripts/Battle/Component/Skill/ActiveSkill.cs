/// <summary> 主动技能 </summary>
public abstract class ActiveSkill : Skill
{
    public ActiveSkillConfig ActiveConfig { get { return Config as ActiveSkillConfig; } }
    public ActiveSkill(SkillConfig config, int lv, RoleEntity entity) : base(config, lv, entity)
    {
    }

    /// <summary> 执行主动技能 </summary>
    public void UseSkill()
    {
        DoUseSkill();
        OnSkillUsed();
    }

    /// <summary> 是否使用，用以给技能增加使用条件检查 </summary>
    public abstract bool WhetherToUse();

    /// <summary> 执行使用技能逻辑 </summary>
    public abstract void DoUseSkill();

    public new bool AllowToUse()
    {
        return base.AllowToUse() && ConsumeCheck();
    }

    /// <summary> 消耗检查 </summary>
    private bool ConsumeCheck()
    {
        var config = ActiveConfig;
        if (config.Hp != null && entity.AttrComponent.Hp.Current <= config.Hp[Level - 1]) return false;
        if (config.Mana != null && entity.AttrComponent.Mana.Current < config.Mana[Level - 1]) return false;
        return true;
    }

    public new void OnSkillUsed()
    {
        base.OnSkillUsed();
        var config = ActiveConfig;
        if (config.Hp != null) entity.AttrComponent.Hp.Add(-config.Hp[Level - 1]);
        if (config.Mana != null) entity.AttrComponent.Mana.Add(-config.Mana[Level - 1]);
    }
}