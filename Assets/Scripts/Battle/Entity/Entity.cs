/*
 * 实体基类
 */
public abstract class Entity
{
    public Simulator Simulator { get; set; }

    public EventManager Event = new();

    // 唯一id
    public int Id { get; set; }

    public bool IsDestroy { get; set; } = false;

    public void BeforeUpdate(int curFrame) { }
    public abstract void FixedUpdate(int curFrame);
    public void AfterUpdate(int curFrame) { }
}