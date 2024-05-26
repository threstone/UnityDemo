
using System.Numerics;

public class SceneEntity : Entity
{
    // 位置
    public Vector2 Position;

    // 碰撞体
    public CollisionBox CollisionBox;

    public int UserId;

    public SceneEntity(int userId) {
        UserId = userId;
    }
}