// 主动技能
public class ActiveSkill : Skill
{
    public new ActiveSkillConfig Config { get; }
    public ActiveSkill(SkillConfig config, int lv) : base(config, lv)
    {
    }
}