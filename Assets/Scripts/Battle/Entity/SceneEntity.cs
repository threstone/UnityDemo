
using System.Numerics;

public class SceneEntity : Entity
{
    // 位置
    public Vector2 Position;

    // 碰撞体
    public CollisionBox CollisionBox;

    public int PlayerId { get; set; }

    public SceneEntity(int playerId, int id) : base(id)
    {
        PlayerId = playerId;
    }

    public override void FixedUpdate(int curFrame)
    {
    }

}