using System.Numerics;

public class CircleCollider : BaseCollider
{
    // 半径
    public float Radius;
    public CircleCollider(SceneEntity entity, float radius) : base(entity)
    {
        Radius = radius;
    }

    // 检测与另一个圆形碰撞体的碰撞
    public override bool CheckCollision(CircleCollider other)
    {
        return Vector2.Distance(Entity.Position, other.Entity.Position) <= Radius + other.Radius;
    }

}