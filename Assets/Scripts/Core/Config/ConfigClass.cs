[System.Serializable]
public class ConfigClass
{
    public RoleConfig[] roles;
}


[System.Serializable]
public class RoleConfig : System.Object
{
    public int Id;
    public string PrefabName;
    public string HeroName;
    /* 初始力量 */
    public float BaseStrength;
    /* 初始智力 */
    public float BaseIntelligence;
    /* 初始敏捷 */
    public float BaseAgility;
    /* 力量成长 */
    public float StrengthGain;
    /* 智力成长 */
    public float IntelligenceGain;
    /* 敏捷成长 */
    public float AgilityGain;
    /* 初始攻击 */
    public float BaseAtk;
    /* 初始攻击速度 */
    public float BaseAtkSpeed;
    /* 初始护甲 */
    public float BaseArmor;
    /* 初始魔法抗性 */
    public float BaseMagicResistance;
    /* 初始血量 */
    public float BaseHp;
    /* 初始魔法 */
    public float BaseMana;
    /* 血量恢复速率 */
    public float HpRecoverySpeed;
    /* 魔法恢复速率 */
    public float ManaRecoverySpeed;
    /* 攻击类型 */
    public AtkType AtkType;
    /* 攻击前摇时间 */
    public float PreAtkTime;
    /* 攻击距离 */
    public float AtkRange;
    /* 移动速度 */
    public float MoveSpeed;
    /* 攻击弹道速度 */
    public float AtkProjectileSpeed;
    /* 攻击弹道初始位置 */
    public float[] AtkProjectilePos;

    public RoleConfig Clone() {

        return (RoleConfig)MemberwiseClone();
    }
}

public enum AtkType {
    MeleeHero,
    RangedHero
}