using System.Numerics;
/*
 * 弹道类
 * 实现接近目标,定义撞击虚函数
 */
public abstract class Projectile : SceneEntity
{
    public RoleEntity Source;
    /// <summary> 目标位置 </summary>
    protected Vector2 TargetPosition;
    /// <summary> 完成击中的最大距离 </summary>
    protected int TriggerDistance;
    /// <summary> 飞行速度 </summary>
    protected int Speed;

    protected Projectile(RoleEntity source) : base(source.PlayerId)
    {
        Source = source;
        /// <summary> 加入simulator </summary>
        source.Simulator.AddEntity(this);
    }

    /// <summary> 接近目标 </summary>
    public void CloseTarget()
    {
        var move = GetMovePos(TargetPosition, Speed);
        Position += move;
    }
}