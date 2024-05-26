[System.Serializable]
public class ConfigClass
{
    public RoleConfig[] roles;
}


[System.Serializable]
public class RoleConfig : System.Object
{
    public int id;
    public string heroName;
    /* 初始力量 */
    public float baseStrength;
    /* 初始智力 */
    public float baseIntelligence;
    /* 初始敏捷 */
    public float baseAgility;
    /* 力量成长 */
    public float strengthGain;
    /* 智力成长 */
    public float intelligenceGain;
    /* 敏捷成长 */
    public float agilityGain;
    /* 初始攻击 */
    public float baseAtk;
    /* 初始攻击速度 */
    public float baseAtkSpeed;
    /* 初始护甲 */
    public float baseArmor;
    /* 初始魔法抗性 */
    public float baseMagicResistance;
    /* 初始血量 */
    public float baseHp;
    /* 初始魔法 */
    public float baseMana;
    /* 血量恢复速率 */
    public float hpRecoverySpeed;
    /* 魔法恢复速率 */
    public float manaRecoverySpeed;
    /* 攻击类型 */
    public AtkType atkType;
    /* 攻击前摇时间 */
    public float preAtkTime;
    /* 攻击距离 */
    public float atkRange;
    /* 移动速度 */
    public float moveSpeed;
    /* 攻击弹道速度 */
    public float atkProjectileSpeed;
    /* 攻击弹道初始位置 */
    public float[] atkProjectilePos;

    public RoleConfig Clone() {

        return (RoleConfig)MemberwiseClone();
    }
}

public enum AtkType {
    MeleeHero,
    RangedHero
}