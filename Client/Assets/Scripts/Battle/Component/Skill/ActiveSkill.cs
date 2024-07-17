/// <summary> 主动技能 </summary>
public abstract class ActiveSkill : Skill
{
    public ActiveSkill(SkillConfig config, int lv, RoleEntity entity) : base(config, lv, entity)
    {
    }
}