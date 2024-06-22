using System.Numerics;
/*
 * 弹道类
 * 实现接近目标,定义撞击虚函数
 */
public abstract class Projectile : SceneEntity
{
    protected RoleEntity Source;
    // 目标位置
    protected Vector2 TargetPosition;
    // 完成击中的最大距离
    protected int TriggerDistance;
    // 飞行速度
    protected int Speed;

    protected Projectile(RoleEntity source) : base(source.PlayerId)
    {
        Source = source;
        // 加入simulator
        source.Simulator.AddEntity(this);
    }

    // 接近目标
    public void CloseTarget()
    {
        var move = GetMovePos(TargetPosition, Speed);
        Position += move;
    }
}