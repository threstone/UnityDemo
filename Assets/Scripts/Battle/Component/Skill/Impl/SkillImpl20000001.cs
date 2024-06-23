// 近战模型格挡
using System;

public class SkillImpl20000001 : PassiveSkill
{
    public SkillImpl20000001(PassiveSkillConfig config, int lv, RoleEntity entity) : base(config, lv, entity)
    {
        On(EventEnum.OnPreBeAttack, new Action<Damage>(OnPreBeAttack));
    }


    public void OnPreBeAttack(Damage damage)
    {
        if (damage.BlockDamage >= Config.Param1[1])
        {
            return;
        }

        var rate = Config.Param1[0];
        // 判断概率
        if (entity.Simulator.RandomNext(0, 10000) < rate)
        {
            damage.BlockDamage = Config.Param1[1];
            OnSkillUsed();
        }
    }
}