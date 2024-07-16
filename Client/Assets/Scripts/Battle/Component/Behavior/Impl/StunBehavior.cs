public class StunBehavior : Behavior
{
    public StunBehavior(BehaviorComponent behaviorComponent) : base(-1, behaviorComponent)
    {
    }

    public override void FixedUpdate(int curFrame)
    {
    }

    public override string GetAnimationName()
    {
        return "stun";
    }

    public override void OnBehaviorEnd()
    {
    }

    public override void OnLogicBehaviorChangeToOther()
    {
    }
}