
using System.Numerics;

/// <summary> 恶魔降临 </summary>
public class SkillImpl21001001 : PassiveSkill
{
    public SkillImpl21001001(PassiveSkillConfig config, int lv, RoleEntity entity) : base(config, lv, entity)
    {
    }

    public override void FixedUpdate()
    {
        if (Level < 1 || Level > 4)
        {
            return;
        }

        var simulator = entity.Simulator;
        var distance = Config.Param2[0];
        var reduceArmor = Config.Param1[Level - 1];
        var duration = Config.Param3[0];
        // 碰撞检测
        for (int i = 0; i < simulator.EntityList.Count; i++)
        {
            var tempEntity = simulator.EntityList[i];
            if (tempEntity is RoleEntity roleEntity &&
             roleEntity.PlayerId != entity.PlayerId &&
             Vector2.Distance(roleEntity.Position, entity.Position) <= distance)
            {
                roleEntity.BuffComponent.AddBuff(Config.Id, duration, entity, reduceArmor);
            }
        }
    }
}