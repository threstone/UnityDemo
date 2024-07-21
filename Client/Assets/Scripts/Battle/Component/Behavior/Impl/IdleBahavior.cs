public class IdleBahavior : Behavior
{
    public IdleBahavior(int timeUnit, BehaviorComponent behaviorComponent) : base(timeUnit / Simulator.FrameInterval, behaviorComponent)
    {
    }

    public override void FixedUpdate(int curFrame)
    {
    }

    public override string GetAnimationName()
    {
        return "Idle";
    }

    public override void OnBehaviorEnd()
    {
        behaviorComponent.AddBehavior(new MoveBehavior(behaviorComponent));
    }

    public override void OnLogicBehaviorChangeToOther()
    {
    }
}