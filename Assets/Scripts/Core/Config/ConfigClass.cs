[System.Serializable]
public class ConfigClass
{
    public RoleConfig[] Roles;
    public EquipmentConfig[] Equipments;
    public CommonConfig Common;
    public ActiveSkillConfig[] ActiveSkills;
    public PassiveSkillConfig[] PassiveSkills;
}

[System.Serializable]
public class CommonConfig
{
    // 每点力量增加的生命值
    public int StrengthAddHp;
    // 每点力量增加的生命恢复速度
    public int StrengthAddHpRecover;
    // 每点智力增加的魔法值
    public int IntelligenceAddMana;
    // 每点智力增加的魔法恢复速度
    public int IntelligenceAddManaRecovery;
    // 每点敏捷增加的护甲值
    public int AgilityAddArmor;
    // 每点敏捷增加的攻击速度
    public int AgilityAddAtkSpeed;
}

[System.Serializable]
public class RoleConfig : AttrObject
{
    public int Id;
    public string PrefabName;
    public string HeroName;
    // 主属性
    public MajorAttrEnum MajorAttr;
    /* 力量成长 */
    public int StrengthGain;
    /* 智力成长 */
    public int IntelligenceGain;
    /* 敏捷成长 */
    public int AgilityGain;
    /* 攻击类型 */
    public AtkTypeEnum AtkType;
    /* 攻击前摇时间 */
    public int PreAtkTime;
    /* 攻击弹道初始位置 */
    public int[] AtkProjectilePos;
    // 弹道击中偏移
    public int ProjectileOffset;
    // 技能数组
    public int[] Skills;

    public RoleConfig Clone()
    {

        return (RoleConfig)MemberwiseClone();
    }
}

public class AttrObject : object
{
    /* 力量 */
    public int Strength;
    /* 智力 */
    public int Intelligence;
    /* 敏捷 */
    public int Agility;
    /* 攻击 */
    public int Attack;
    /* 攻击速度 */
    public int AtkSpeed;
    /* 攻击间隔 */
    public int AtkInterval;
    /* 护甲 */
    public int Armor;
    /* 角色基础魔法抗性 */
    public int RoleMagicResistance;
    /* 物品魔法抗性 */
    public int ItemMagicResistance;
    /* 技能魔法抗性 */
    public int SkillMagicResistance;
    /* 血量 */
    public int Hp;
    /* 魔法 */
    public int Mana;
    /* 血量恢复速率 */
    public int HpRecoverySpeed;
    /* 魔法恢复速率 */
    public int ManaRecoverySpeed;
    /* 攻击距离 */
    public int AtkRange;
    /* 移动速度 */
    public int MoveSpeed;
    /* 攻击弹道速度 */
    public int AtkProjectileSpeed;
    /* 碰撞体积半径 */
    public int ColliderRadius;

    public void AddAttrFromTarget(AttrObject target)
    {
        if (target == null)
        {
            return;
        }
        Strength += target.Strength;
        Intelligence += target.Intelligence;
        Agility += target.Agility;
        Attack += target.Attack;
        AtkSpeed += target.AtkSpeed;
        AtkInterval += target.AtkInterval;
        Armor += target.Armor;
        RoleMagicResistance += target.RoleMagicResistance;
        ItemMagicResistance += target.ItemMagicResistance;
        SkillMagicResistance += target.SkillMagicResistance;
        Hp += target.Hp;
        Mana += target.Mana;
        HpRecoverySpeed += target.HpRecoverySpeed;
        ManaRecoverySpeed += target.ManaRecoverySpeed;
        AtkRange += target.AtkRange;
        MoveSpeed += target.MoveSpeed;
        AtkProjectileSpeed += target.AtkProjectileSpeed;
        ColliderRadius += target.ColliderRadius;
    }

    public void Reset()
    {
        Strength = 0;
        Intelligence = 0;
        Agility = 0;
        Attack = 0;
        AtkSpeed = 0;
        AtkInterval = 0;
        Armor = 0;
        RoleMagicResistance = 0;
        ItemMagicResistance = 0;
        SkillMagicResistance = 0;
        Hp = 0;
        Mana = 0;
        HpRecoverySpeed = 0;
        ManaRecoverySpeed = 0;
        AtkRange = 0;
        MoveSpeed = 0;
        AtkProjectileSpeed = 0;
        ColliderRadius = 0;
    }
}

[System.Serializable]
public class EquipmentConfig : AttrObject
{
    public int Id;
    public string Name;
    // 技能数组
    public int[] Skills;
}

[System.Serializable]
public class ActiveSkillConfig : SkillConfig// 主动技能
{
    // 生命消耗
    public int[] Hp;
    // 魔法消耗
    public int[] Mana;
    // 携带的被动技能ID,有些主动同时也会携带被动技能
    public int[] PassiveSkills;
}

[System.Serializable]
public class PassiveSkillConfig : SkillConfig// 被动技能
{
    // 被动技能类型
    public PassiveSkillTypeEnum PassiveSkillType;
    public int Sort;
    // 决定同类型被动技能的执行顺寻,  普通被动技能不关注
    public bool CanDestroy;
}

public abstract class SkillConfig
{
    public int Id;
    public string SkillName;
    public int[] CD;
    public int[] Param1;
    public int[] Param2;
    public int[] Param3;
}

public enum SkillTypeEnum
{
    ActiveSkill,
    PassiveSkill
}

public enum AtkTypeEnum
{
    MeleeHero,
    RangedHero
}

public enum MajorAttrEnum
{
    // 力量
    Strength,
    // 智力
    Intelligence,
    // 敏捷
    Agility
}