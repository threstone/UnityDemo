/// <summary> 主动技能 </summary>
public class ActiveSkill : Skill
{
    public new ActiveSkillConfig Config { get; }
    public ActiveSkill(SkillConfig config, int lv, RoleEntity entity) : base(config, lv, entity)
    {
    }
}