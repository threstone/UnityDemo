[System.Serializable]
public class ConfigClass
{
    public RoleConfig[] roles;
    public EquipmentConfig[] equipments;
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
    /* 魔法抗性 */
    public int MagicResistance;
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
        MagicResistance += target.MagicResistance;
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
        MagicResistance = 0;
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
    public string name;
}

public enum AtkTypeEnum
{
    MeleeHero,
    RangedHero
}

public enum MajorAttrEnum
{
    Strength,
    Intelligence,
    Agility
}