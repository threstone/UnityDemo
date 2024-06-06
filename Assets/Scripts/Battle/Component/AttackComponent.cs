
public class AttackComponent
{
    // 最低攻击间隔0.17
    // 公式 ： (额外攻速+基础攻速)/(基础攻击间隔*100)
    readonly RoleEntity roleEntity;
    int lastAtkFrame = -1000;
    int atkFrame = 0;

    public AttackComponent(RoleEntity roleEntity)
    {
        this.roleEntity = roleEntity;
    }

    public void StartAtk()
    {
        atkFrame = 0;
    }

    public void FixedUpdate()
    {
    }

    public bool IsPreAtkEnd()
    {
        return false;
    }
}