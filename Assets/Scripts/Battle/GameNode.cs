public class GameNode
{
    // 当状态自然结束
    public void OnBuffAdd() { }
    // 当状态自然结束
    public void OnBuffEnd() { }

    // 当Buff被驱散
    public void OnBuffClear() { }

    // 当被攻击前
    public void OnPreAttack(Damage damage) { }
}