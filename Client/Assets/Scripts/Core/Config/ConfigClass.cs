[System.Serializable]
public class ConfigClass
{
    public RoleConfig[] Roles;
    public EquipmentConfig[] Equipments;
    public CommonConfig Common;
    public ActiveSkillConfig[] ActiveSkills;
    public PassiveSkillConfig[] PassiveSkills;
    public BuffConfig[] Buffs;
}

[System.Serializable]
public class CommonConfig
{
    /// <summary> 每点力量增加的生命值</summary>
    public int StrengthAddHp;
    /// <summary> 每点力量增加的生命恢复速度</summary>
    public int StrengthAddHpRecover;
    /// <summary> 每点智力增加的魔法值</summary>
    public int IntelligenceAddMana;
    /// <summary> 每点智力增加的魔法恢复速度</summary>
    public int IntelligenceAddManaRecovery;
    /// <summary> 每点敏捷增加的护甲值</summary>
    public int AgilityAddArmor;
    /// <summary> 每点敏捷增加的攻击速度</summary>
    public int AgilityAddAtkSpeed;
}

[System.Serializable]
public class RoleConfig : AttrObject
{
    public int Id;
    public string PrefabName;
    public string HeroName;
    /// <summary> 主属性</summary>
    public MajorAttrEnum MajorAttr;
    /// <summary>  力量成长  </summary> 
    public int StrengthGain;
    /// <summary>  智力成长  </summary> 
    public int IntelligenceGain;
    /// <summary>  敏捷成长  </summary> 
    public int AgilityGain;
    /// <summary>  攻击类型  </summary> 
    public AtkTypeEnum AtkType;
    /// <summary>  攻击前摇时间  </summary> 
    public int PreAtkTime;
    /// <summary>  攻击弹道初始位置  </summary> 
    public int[] AtkProjectilePos;
    /// <summary> 弹道击中偏移 </summary>
    public int ProjectileOffset;
    /// <summary> 技能数组 </summary>
    public int[] Skills;

    public RoleConfig Clone()
    {

        return (RoleConfig)MemberwiseClone();
    }
}

public class AttrObject : object
{
    /// <summary>  力量  </summary> 
    public int Strength;
    /// <summary>  智力  </summary> 
    public int Intelligence;
    /// <summary>  敏捷  </summary> 
    public int Agility;
    /// <summary>  攻击  </summary> 
    public int Attack;
    /// <summary>  攻击速度  </summary> 
    public int AtkSpeed;
    /// <summary>  攻击间隔  </summary> 
    public int AtkInterval;
    /// <summary>  护甲  </summary> 
    public int Armor;
    /// <summary>  角色基础魔法抗性  </summary> 
    public int RoleMagicResistance;
    /// <summary>  物品魔法抗性  </summary> 
    public int ItemMagicResistance;
    /// <summary>  角色基础闪避  </summary> 
    public int RoleMiss;
    /// <summary>  物品提供闪避  </summary> 
    public int ItemMiss;
    /// <summary>  血量  </summary> 
    public int Hp;
    /// <summary>  魔法  </summary> 
    public int Mana;
    /// <summary>  血量恢复速率  </summary> 
    public int HpRecoverySpeed;
    /// <summary>  魔法恢复速率  </summary> 
    public int ManaRecoverySpeed;
    /// <summary>  攻击距离  </summary> 
    public int AtkRange;
    /// <summary>  移动速度  </summary> 
    public int MoveSpeed;
    /// <summary>  攻击弹道速度  </summary> 
    public int AtkProjectileSpeed;
    /// <summary>  碰撞体积半径  </summary> 
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
        Hp += target.Hp;
        Mana += target.Mana;
        HpRecoverySpeed += target.HpRecoverySpeed;
        ManaRecoverySpeed += target.ManaRecoverySpeed;
        AtkRange += target.AtkRange;
        MoveSpeed += target.MoveSpeed;
        AtkProjectileSpeed += target.AtkProjectileSpeed;
        ColliderRadius += target.ColliderRadius;
    }

    //public void Reset()
    //{
    //    Strength = 0;
    //    Intelligence = 0;
    //    Agility = 0;
    //    Attack = 0;
    //    AtkSpeed = 0;
    //    AtkInterval = 0;
    //    Armor = 0;
    //    RoleMagicResistance = 0;
    //    ItemMagicResistance = 0;
    //    Hp = 0;
    //    Mana = 0;
    //    HpRecoverySpeed = 0;
    //    ManaRecoverySpeed = 0;
    //    AtkRange = 0;
    //    MoveSpeed = 0;
    //    AtkProjectileSpeed = 0;
    //    ColliderRadius = 0;
    //}
}

[System.Serializable]
public class EquipmentConfig : AttrObject
{
    public int Id;
    public string Name;
    /// <summary> 技能数组 </summary> 
    public int[] Skills;
}

/// <summary> 主动技能 </summary> 
[System.Serializable]
public class ActiveSkillConfig : SkillConfig
{
    /// <summary> 生命消耗 </summary> 
    public int[] Hp;
    /// <summary> 魔法消耗 </summary> 
    public int[] Mana;
    /// <summary> 携带的被动技能ID,有些主动同时也会携带被动技能 </summary> 
    public int[] PassiveSkills;
    /// <summary> 施法前摇 </summary> 
    public int PreSpellTime;
    /// <summary> 施法后摇 </summary> 
    public int AfterSpellTime;
}

/// <summary> 被动技能 </summary> 
[System.Serializable]
public class PassiveSkillConfig : SkillConfig
{
    /// <summary> 被动技能类型 </summary> 
    public PassiveSkillTypeEnum PassiveSkillType;
    public int Sort;
    /// <summary> 决定同类型被动技能的执行顺寻,  普通被动技能不关注 </summary> 
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

[System.Serializable]
public class BuffConfig
{
    /// <summary> 唯一Id </summary> 
    public int Id;
    /// <summary> 定义 </summary> 
    public string Define;
    /// <summary> 名称 </summary> 
    public int Name;
    /// <summary> 驱散类型 </summary> 
    public DispelTypeEnum DispelType;
    /// <summary> 是否属于硬控技能 </summary> 
    public bool IsControll;
    /// <summary> 仅硬控技能有效,控制Buff权重,决定使用哪个动画 </summary> 
    public int ControllSort;
}

public enum DispelTypeEnum
{
    /// <summary> 不可驱散 </summary> 
    No,
    /// <summary> 仅强驱散 </summary> 
    Strong,
    /// <summary> 弱驱散 </summary> 
    Weak 
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
    /// <summary> 力量 </summary> 
    Strength,
    /// <summary> 智力 </summary> 
    Intelligence,
    /// <summary> 敏捷 </summary> 
    Agility
}