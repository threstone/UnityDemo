/*
 * ʵ�����
 */
public class Entity
{
    public Simulator Simulator { get; set; }

    // Ψһid
    public int Id { get; set; }

    public Entity(int id) {
        Id = id;
    }

    public void FixedUpdate(int curFrame)
    {
    }
}