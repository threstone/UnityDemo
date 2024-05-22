using System.Numerics;

public class CollisionBox
{
    readonly SceneEntity entity;
    public Vector2 offset;
    public float Width { get; set; }
    public float Height { get; set; }

    public float X { get { return entity.position.X + offset.X; } }
    public float Y { get { return entity.position.Y + offset.Y; } }

    public CollisionBox(SceneEntity entity)
    {
        this.entity = entity;
    }

    // 检测与另一个矩形碰撞体的碰撞
    public  bool CheckCollision(CollisionBox other)
    {
        if (other == null) return false;

        return (X < other.X + other.Width) &&
               (X + Width > other.X) &&
               (Y < other.Y + other.Height) &&
               (Y + Height > other.Y);
    }


    // 检测与另一个场景实体的碰撞
    public bool CheckCollision(SceneEntity entity)
    {
        return CheckCollision(entity?.collisionBox);
    }
}