public class GameNode
{
    // 当状态自然结束
    public void OnBuffAdd() { }
    // 当状态自然结束
    public void OnBuffEnd() { }

    // 当Buff被驱散
    public void OnBuffClear() { }

    // 当被攻击前
    public void OnPreBeAttack(Damage damage) { }

    // 当被攻击后
    public void OnAfterBeAttack(Damage damage) { }
}

public enum GameNodeEvent
{
    // 当状态自然结束
    OnBuffAdd,
    // 当状态自然结束
    OnBuffEnd,

    // 当Buff被驱散
    OnBuffClear,

    // 当被攻击前
    OnPreBeAttack,
    // 当被攻击后
    OnAfterBeAttack
}