/*
 * 实体基类
 */
public abstract class Entity
{
    public Simulator Simulator { get; set; }

    // 唯一id
    public int Id { get; set; }

    public Entity(int id) {
        Id = id;
    }

    public abstract void FixedUpdate(int curFrame);
}