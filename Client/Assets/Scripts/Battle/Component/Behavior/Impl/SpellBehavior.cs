public class SpellBehavior : Behavior
{
    readonly ActiveSkill skill;
    int curPreTime = 0;
    bool isUse = false;
    readonly int totalSpellTime;

    public SpellBehavior(ActiveSkill skill, BehaviorComponent behaviorComponent) : base(-1, behaviorComponent)
    {
        Sort = 0;
        this.skill = skill;
        var config = skill.ActiveConfig;
        totalSpellTime = config.PreSpellTime + config.AfterSpellTime;
    }

    public override void FixedUpdate(int curFrame)
    {
        if (skill.AllowToUse() == false)
        {
            behaviorComponent.RemoveBehavior(this);
            return;
        }

        curPreTime += Simulator.FrameInterval;
        var config = skill.ActiveConfig;
        if (!isUse && curPreTime >= config.PreSpellTime)
        {
            skill.UseSkill();
            isUse = true;
        }

        if (isUse && curPreTime >= totalSpellTime)
        {
            behaviorComponent.RemoveBehavior(this);
            return;
        }
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
        if (isUse)
        {
            behaviorComponent.RemoveBehavior(this);
        }
        else
        {
            curPreTime = 0;
        }
    }
}