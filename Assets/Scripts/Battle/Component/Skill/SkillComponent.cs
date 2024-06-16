using System;
using System.Collections.Generic;
using System.Reflection;
public class SkillComponent
{
    // 主动技能
    public List<ActiveSkill> ActiveSkillList;
    // 被动技能
    public Dictionary<PassiveSkillTypeEnum, List<PassiveSkill>> PassiveSkillMap;

    public SkillComponent(RoleEntity entity)
    {
        InitSkill(entity);
    }

    public void FixedUpdate()
    {
        var interval = Simulator.FrameInterval;
        // 主动技能CD
        ActiveSkillList?.ForEach((s) =>
        {
            s.ReduceCD(interval);
        });

        if (PassiveSkillMap != null)
        {
            // 被动技能CD
            foreach (var pair in PassiveSkillMap)
            {
                pair.Value.ForEach((s) =>
                {
                    s.ReduceCD(interval);
                });
            }
        }
    }

    public void AddSkill(SkillData skill)
    {
        var skillConfig = ConfigMgr.GetSkillConfig(skill.Id);
        if (skillConfig is PassiveSkillConfig pSkill)
        {
            AddSkill(new PassiveSkill(pSkill, skill.level));
        }
        else if (skillConfig is ActiveSkillConfig aSkill)
        {
            AddSkill(new ActiveSkill(aSkill, skill.level));
            // 增加主动技能附带的被动技能
            foreach (var pSkillId in aSkill.PassiveSkills)
            {
                var config = ConfigMgr.GetSkillConfig<PassiveSkillConfig>(pSkillId);
                AddSkill(new PassiveSkill(config, skill.level));
            }
        }
    }

    public void AddSkill(ActiveSkill skill)
    {
        ActiveSkillList ??= new();
        ActiveSkillList.Add(skill);
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
                AddSkill(new SkillData
                {
                    Id = skillId,
                    level = 1
                });
            }
        }

        // 近战增加模型格挡
        if (ConfigMgr.GetRoleInfoById(entity.Role.RoleId).AtkType == AtkTypeEnum.MeleeHero)
        {
            AddSkill(new SkillData
            {
                Id = 20000001,
                level = 1
            });
        }

    }
}
