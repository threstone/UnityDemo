public class GameNode
{

    public void OnPreBeAttack(Damage damage) { }  // 当被攻击前

    public void OnAfterBeAttack(Damage damage) { }// 当被攻击后

    public void OnPreHandleDamage(Damage damage) { }// 消费伤害前
    public void OnHandleDamage(Damage damage){} // 当受到伤害
    public void OnAfterHandleDamage(Damage damage) { }// 消费伤害后


}

public enum GameNodeEvent
{
    // 当被攻击前
    OnPreBeAttack,
    // 当被攻击后
    OnAfterBeAttack
}