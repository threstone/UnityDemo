public class FearBehavior : Behavior
{
    public FearBehavior(BehaviorComponent behaviorComponent) : base(-1, behaviorComponent)
    {
    }

    public override void FixedUpdate(int curFrame)
    {
        // todo random move
    }

    public override string GetAnimationName()
    {
        return "Walk";
    }

    public override void OnBehaviorEnd()
    {
    }

    public override void OnLogicBehaviorChangeToOther()
    {
    }
}