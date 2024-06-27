using System;
using System.Collections.Generic;
public class SkillComponent
{
    readonly RoleEntity entity;
    // 主动技能
    public List<ActiveSkill> ActiveSkillList;
    // 被动技能
    public Dictionary<PassiveSkillTypeEnum, List<PassiveSkill>> PassiveSkillMap;

    public SkillComponent(RoleEntity entity)
    {
        this.entity = entity;
        InitSkill(entity);
    }

    public void FixedUpdate()
    {
        var interval = Simulator.FrameInterval;
        ForEachAllSkill((Skill s) => s.ReduceCD(interval));
    }

    public void ForEachAllSkill(Action<Skill> action)
    {
        // 主动技能CD
        ActiveSkillList?.ForEach((s) => action(s));

        if (PassiveSkillMap != null)
        {
            // 被动技能CD
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
        // 增加主动技能附带的被动技能
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
        if (PassiveSkillMap.TryGetValue(skill.Config.PassiveSkillType, out var list) == false)
        {
            list = new();
            PassiveSkillMap.Add(skill.Config.PassiveSkillType, list);
        }

        list.Add(skill);
        // 非默认类型即为概率型被动，有些概率性被动需要确定优先级
        if (skill.Config.PassiveSkillType != PassiveSkillTypeEnum.Normal)
        {
            list.Sort((skill1, skill2) =>
            {
                if (skill1.Config.Sort > skill2.Config.Sort) return 1;
                if (skill1.Config.Sort < skill2.Config.Sort) return -1;
                return 0;
            });
        }
    }

    public void InitSkill(RoleEntity entity)
    {
        // 角色技能初始化
        for (int i = 0; i < entity.Role.SkillList?.Count; i++)
        {
            var skillInfo = entity.Role.SkillList[i];
            AddSkill(skillInfo);
        }

        // 装备技能初始化
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

        // 近战增加模型格挡
        if (ConfigMgr.GetRoleInfoById(entity.Role.RoleId).AtkType == AtkTypeEnum.MeleeHero)
        {
            AddSkill(20000001, 1);
        }
    }
}
