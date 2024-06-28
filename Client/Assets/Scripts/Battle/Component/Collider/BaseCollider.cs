public abstract class BaseCollider
{
    public readonly SceneEntity Entity;
    public BaseCollider(SceneEntity entity)
    {
        Entity = entity;
    }
    
    /// <summary> 检测与另一个圆形碰撞体的碰撞 </summary>
    public abstract bool CheckCollision(CircleCollider other);

    public bool CheckCollision(SceneEntity other)
    {
        if (other == null || other.Collider == null) { return false; }

        /// <summary> 圆形检测 </summary>
        if (other.Collider is CircleCollider c)
        {
            return CheckCollision(c);
        }

        return false;
    }
}