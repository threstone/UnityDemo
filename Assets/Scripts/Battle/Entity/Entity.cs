/*
 * 实体基类
 */
public class Entity
{
    public Simulator Simulator { get; set; }

    // 唯一id
    public int Id { get; set; }

    public Entity(int id) {
        Id = id;
    }

    public void FixedUpdate(int curFrame)
    {
    }
}