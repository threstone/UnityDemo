using System;

/// <summary> 近战模型格挡 </summary>
public class SkillImpl20000001 : PassiveSkill
{
    public SkillImpl20000001(PassiveSkillConfig config, int lv, RoleEntity entity) : base(config, lv, entity)
    {
        On(EventEnum.OnPreBeAttacked, new Action<Damage>(OnPreBeAttacked));
    }

    public override void FixedUpdate()
    {
    }

    public void OnPreBeAttacked(Damage damage)
    {
        if (AllowToUse() == false) return;
        
        var blockDamage = Config.Param2[0];
        if (damage.IsMiss || damage.BlockDamage >= blockDamage)
        {
            return;
        }

        var rate = Config.Param1[0];
        /// <summary> 判断概率 </summary>
        if (entity.Simulator.RandomNext(0, 10000) < rate)
        {
            damage.BlockDamage = blockDamage;
            OnSkillUsed();
        }
    }
}