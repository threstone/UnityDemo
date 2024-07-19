using System.Numerics;

/// <summary> 毁灭阴影 </summary>
public class SkillImpl11001001 : ActiveSkill
{
    public SkillImpl11001001(SkillConfig config, int lv, RoleEntity entity) : base(config, lv, entity)
    {
    }

    public override void DoUseSkill()
    {
        var config = ActiveConfig;
        var damageValue = config.Param3[Level - 1];
        Vector2 targetPos = new(entity.Face ? entity.Position.X + config.Param1[0] : entity.Position.X - config.Param1[0], entity.Position.Y);
        for (int i = 0; i < entity.Simulator.EntityList.Count; i++)
        {
            var tempEntity = entity.Simulator.EntityList[i];
            if (tempEntity is RoleEntity roleEntity &&
             roleEntity.PlayerId != entity.PlayerId &&
              Vector2.Distance(targetPos, roleEntity.Position) < config.Param2[0])
            {
                roleEntity.HandleDamage(Damage.GetDamage(entity, DamageTypeEnum.MagicalDamage, damageValue, true));
            }
        }

        Utils.Log("使用毁灭阴影");
    }

    public override bool WhetherToUse()
    {
        var config = ActiveConfig;
        Vector2 targetPos = new(entity.Face ? entity.Position.X + config.Param1[0] : entity.Position.X - config.Param1[0], entity.Position.Y);
        // 面朝方向一定范围内是否有敌人
        for (int i = 0; i < entity.Simulator.EntityList.Count; i++)
        {
            var tempEntity = entity.Simulator.EntityList[i];
            if (tempEntity is RoleEntity roleEntity &&
             roleEntity.PlayerId != entity.PlayerId &&
              Vector2.Distance(targetPos, roleEntity.Position) < config.Param2[0])
            {
                return true;
            }
        }

        return false;
    }

    public override void FixedUpdate()
    {
    }

    public override string GetAnimationName()
    {
        return "spell";
    }
}