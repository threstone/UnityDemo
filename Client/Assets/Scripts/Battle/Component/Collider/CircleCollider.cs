using System.Numerics;

public class CircleCollider : BaseCollider
{
    /// <summary> 半径 </summary>
    public float Radius;
    public CircleCollider(SceneEntity entity, float radius) : base(entity)
    {
        Radius = radius;
    }

    /// <summary> 检测与另一个圆形碰撞体的碰撞 </summary>
    public override bool CheckCollision(CircleCollider other)
    {
        return Vector2.Distance(Entity.Position, other.Entity.Position) <= Radius + other.Radius;
    }

}