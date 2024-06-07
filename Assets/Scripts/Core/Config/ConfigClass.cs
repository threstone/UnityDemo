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
    public float StrengthGain;
    /* 智力成长 */
    public float IntelligenceGain;
    /* 敏捷成长 */
    public float AgilityGain;
    /* 攻击类型 */
    public AtkTypeEnum AtkType;
    /* 攻击前摇时间 */
    public float PreAtkTime;
    /* 攻击弹道初始位置 */
    public float[] AtkProjectilePos;

    public RoleConfig Clone()
    {

        return (RoleConfig)MemberwiseClone();
    }
}

public class AttrObject : object
{
    /* 力量 */
    public float Strength;
    /* 智力 */
    public float Intelligence;
    /* 敏捷 */
    public float Agility;
    /* 攻击 */
    public float Attack;
    /* 攻击速度 */
    public float AtkSpeed;
    /* 攻击间隔 */
    public float AtkInterval;
    /* 护甲 */
    public float Armor;
    /* 魔法抗性 */
    public float MagicResistance;
    /* 血量 */
    public float Hp;
    /* 魔法 */
    public float Mana;
    /* 血量恢复速率 */
    public float HpRecoverySpeed;
    /* 魔法恢复速率 */
    public float ManaRecoverySpeed;
    /* 攻击距离 */
    public float AtkRange;
    /* 移动速度 */
    public float MoveSpeed;
    /* 攻击弹道速度 */
    public float AtkProjectileSpeed;
    /* 碰撞体积半径 */
    public float ColliderRadius;

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
        Strength = 0.0f;
        Intelligence = 0.0f;
        Agility = 0.0f;
        Attack = 0.0f;
        AtkSpeed = 0.0f;
        AtkInterval = 0.0f;
        Armor = 0.0f;
        MagicResistance = 0.0f;
        Hp = 0.0f;
        Mana = 0.0f;
        HpRecoverySpeed = 0.0f;
        ManaRecoverySpeed = 0.0f;
        AtkRange = 0.0f;
        MoveSpeed = 0.0f;
        AtkProjectileSpeed = 0.0f;
        ColliderRadius = 0.0f;
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