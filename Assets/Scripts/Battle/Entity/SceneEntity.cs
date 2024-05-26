
using System.Numerics;

public class SceneEntity : Entity
{
    // 位置
    public Vector2 position;

    // 碰撞体
    public CollisionBox collisionBox;

    public int userId;

    public SceneEntity(int userId) {
        this.userId = userId;
    }
}