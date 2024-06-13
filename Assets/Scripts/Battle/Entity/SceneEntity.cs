
using System.Numerics;

public abstract class SceneEntity : Entity
{
    // 位置
    public Vector2 Position;

    // 碰撞体
    public BaseCollider Collider;

    public int PlayerId { get; set; }

    public SceneEntity(int playerId)
    {
        PlayerId = playerId;
    }

    public Vector2 GetMovePos(Vector2 target, int speed)
    {
        var dir = target - Position;
        var moveLen = Simulator.FrameInterval * speed / Simulator.TimeUnitRatioBySecond;
        return dir * (moveLen / dir.Length());
    }
}