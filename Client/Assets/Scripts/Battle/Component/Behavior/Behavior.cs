public abstract class Behavior
{
    public int Duration { get; set; }
    public int Sort { get; set; } = 0;
    protected readonly BehaviorComponent behaviorComponent;

    public Behavior(int duration, BehaviorComponent behaviorComponent)
    {
        Duration = duration;
        this.behaviorComponent = behaviorComponent;
    }

    public abstract void FixedUpdate(int curFrame);
    /// <summary> 当行为持续时间结束  </summary> 
    public abstract void OnBehaviorEnd();
    /// <summary> 当逻辑行为切换为其他行为,逻辑行为本身不会触发  </summary> 
    public abstract void OnLogicBehaviorChangeToOther();
    /// <summary> 动画播放名称  </summary> 
    public abstract string GetAnimationName();
    /// <summary> 动画播放速度万分比  </summary> 
    public int GetAnimatorSpeed()
    {
        return 10000;
    }

    /// <summary> 返回行为是否结束  </summary> 
    public bool ReduceDuration()
    {
        if (Duration == -1)
        {
            return false;
        }

        Duration--;
        return Duration <= 0;
    }
}