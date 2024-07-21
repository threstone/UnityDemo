using System.Numerics;

public class SkillDisplayEntity : SceneEntity
{
    public readonly string PrafabPath;

    public int DisplayFrame;

    public SkillDisplayEntity(RoleEntity source, Vector2 position, string prafabPath, int displayFrame = 50) : base(source.PlayerId)
    {
        PrafabPath = prafabPath;
        DisplayFrame = displayFrame;
        Position = position;
        source.Simulator.AddEntity(this);
    }

    public override void FixedUpdate(int curFrame)
    {
        DisplayFrame--;
        if (DisplayFrame <= 0)
        {
            IsDestroy = true;
        }
    }
}