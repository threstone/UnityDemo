using System;
using System.Collections.Generic;
public class SkillComponent
{
    readonly RoleEntity entity;
    /// <summary> 主动技能 </summary>
    public List<ActiveSkill> ActiveSkillList;
    /// <summary> 被动技能 </summary>
    public Dictionary<PassiveSkillTypeEnum, List<PassiveSkill>> PassiveSkillMap;

    public SkillComponent(RoleEntity entity)
    {
        this.entity = entity;
        InitSkill(entity);
    }

    public void FixedUpdate()
    {
        var interval = Simulator.FrameInterval;
        ForEachAllSkill((Skill s) =>
        {
            s.FixedUpdate();
            s.ReduceCD(interval);
        });
    }

    public void ForEachAllSkill(Action<Skill> action)
    {
        /// <summary> 主动技能CD </summary>
        ActiveSkillList?.ForEach((s) => action(s));

        if (PassiveSkillMap != null)
        {
            /// <summary> 被动技能CD </summary>
            foreach (var pair in PassiveSkillMap)
            {
                pair.Value.ForEach((s) => action(s));
            }
        }
    }

    public void AddSkill(SkillData data)
    {
        AddSkill(data.Id, data.level);
    }

    public void AddSkill(int skillId, int level)
    {
        var skillConfig = ConfigMgr.GetSkillConfig(skillId);
        var skill = SkillMgr.GetSkillById(skillConfig, level, entity);
        if (skill is PassiveSkill pSkill)
        {
            AddSkill(pSkill);
        }
        else if (skill is ActiveSkill aSkill)
        {
            AddSkill(aSkill, skillConfig as ActiveSkillConfig);
        }
    }

    public void AddSkill(ActiveSkill skill, ActiveSkillConfig skillConfig)
    {
        ActiveSkillList ??= new();
        ActiveSkillList.Add(skill);
        /// <summary> 增加主动技能附带的被动技能 </summary>
        foreach (var pSkillId in skillConfig.PassiveSkills)
        {
            var passiveSkillConfig = ConfigMgr.GetSkillConfig<PassiveSkillConfig>(pSkillId);
            var passiveSkill = SkillMgr.GetSkillById(passiveSkillConfig, skill.Level, entity) as PassiveSkill;
            AddSkill(passiveSkill);
        }
    }

    public void AddSkill(PassiveSkill skill)
    {
        PassiveSkillMap ??= new();
        var config = skill.Config as PassiveSkillConfig;
        if (PassiveSkillMap.TryGetValue(config.PassiveSkillType, out var list) == false)
        {
            list = new();
            PassiveSkillMap.Add(config.PassiveSkillType, list);
        }

        list.Add(skill);
        /// <summary> 非默认类型即为概率型被动，有些概率性被动需要确定优先级 </summary>
        if (config.PassiveSkillType != PassiveSkillTypeEnum.Normal)
        {
            list.Sort((skill1, skill2) =>
            {
                var config1 = skill1.Config as PassiveSkillConfig;
                var config2 = skill2.Config as PassiveSkillConfig;
                if (config1.Sort > config2.Sort) return 1;
                if (config1.Sort < config2.Sort) return -1;
                return 0;
            });
        }
    }

    public void InitSkill(RoleEntity entity)
    {
        /// <summary> 角色技能初始化 </summary>
        for (int i = 0; i < entity.Role.SkillList?.Count; i++)
        {
            var skillInfo = entity.Role.SkillList[i];
            AddSkill(skillInfo);
        }

        /// <summary> 装备技能初始化 </summary>
        for (int i = 0; i < entity.EquipmentComponent.Equipments.Length; i++)
        {
            var equipment = entity.EquipmentComponent.Equipments[i];
            if (equipment == null) continue;

            for (int z = 0; z < equipment.Config.Skills?.Length; z++)
            {
                var skillId = equipment.Config.Skills[z];
                AddSkill(skillId, 1);
            }
        }

        /// <summary> 近战增加模型格挡 </summary>
        if (ConfigMgr.GetRoleInfoById(entity.Role.RoleId).AtkType == AtkTypeEnum.MeleeHero)
        {
            AddSkill(20000001, 1);
        }
    }
}
