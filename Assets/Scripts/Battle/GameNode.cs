public class GameNode
{
    // 当被攻击前
    public void OnPreBeAttack(Damage damage) { }

    // 当被攻击后
    public void OnAfterBeAttack(Damage damage) { }
}

public enum GameNodeEvent
{
    // 当被攻击前
    OnPreBeAttack,
    // 当被攻击后
    OnAfterBeAttack
}