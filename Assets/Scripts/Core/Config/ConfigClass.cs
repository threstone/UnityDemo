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
    /* 力量成长 */
    public float StrengthGain;
    /* 智力成长 */
    public float IntelligenceGain;
    /* 敏捷成长 */
    public float AgilityGain;
    /* 攻击类型 */
    public AtkType AtkType;
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
}

[System.Serializable]
public class EquipmentConfig : AttrObject
{
    public int Id;
    public string name;
}

public enum AtkType
{
    MeleeHero,
    RangedHero
}