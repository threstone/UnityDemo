public class SpellBehavior : Behavior
{
    public SpellBehavior(BehaviorComponent behaviorComponent) : base(-1, behaviorComponent)
    {
    }

    public override void FixedUpdate(int curFrame)
    {
    }

    public override string GetAnimationName()
    {
        return "spell";
    }

    public override void OnBehaviorEnd()
    {
    }

    public override void OnLogicBehaviorChangeToOther()
    {
    }
}