public class IdleBahavior : Behavior
{
    public IdleBahavior(int duration, BehaviorComponent behaviorComponent) : base(duration / Simulator.FrameInterval, behaviorComponent)
    {
    }

    public override void FixedUpdate(int curFrame)
    {
    }

    public override string GetAnimationName()
    {
        return "idle";
    }
    public override int GetAnimatorSpeed()
    {
        return 10000;
    }

    public override void OnBehaviorEnd()
    {
        behaviorComponent.AddBehavior(new MoveBehavior(behaviorComponent));
    }
}