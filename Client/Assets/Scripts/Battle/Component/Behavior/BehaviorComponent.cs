using System.Collections.Generic;

public class BehaviorComponent
{
    public List<Behavior> Behaviors = new();

    public Behavior LogicBehavior { get; set; }
    public readonly RoleEntity Entity;

    public RoleEntity TargetEntity { get; set; }

    public BehaviorComponent(RoleEntity entity)
    {
        Entity = entity;
        AddBehavior(new IdleBahavior(10000, this));
    }

    public void FixedUpdate(int curFrame)
    {
        if (TargetEntity?.IsDestroy == true)
        {
            TargetEntity = null;
        }

        LogicBehavior?.FixedUpdate(curFrame);

        bool isModifyed = false;
        for (int i = Behaviors.Count - 1; i >= 0; i--)
        {
            var behavior = Behaviors[i];
            if (behavior.ReduceDuration())
            {
                behavior.OnBehaviorEnd();
                isModifyed = true;
                Behaviors.RemoveAt(i);
            }
        }

        if (isModifyed) UpdateLogicBehavior();
    }

    public void AddBehavior(Behavior behavior)
    {
        Behaviors.Add(behavior);
        UpdateLogicBehavior();
    }

    public void RemoveBehavior(Behavior behavior)
    {
        Behaviors.Remove(behavior);
        UpdateLogicBehavior();
    }

    void UpdateLogicBehavior()
    {
        if (Behaviors.Count == 1)
        {
            LogicBehavior = Behaviors[0];
            return;
        }

        int max = -1;
        LogicBehavior = null;
        for (int i = Behaviors.Count - 1; i >= 0; i--)
        {
            var tempBehavior = Behaviors[i];
            if (tempBehavior.Sort > max)
            {
                max = tempBehavior.Sort;
                LogicBehavior = tempBehavior;
            }
        }

        for (int i = Behaviors.Count - 1; i >= 0; i--)
        {
            var tempBehavior = Behaviors[i];
            if (tempBehavior != LogicBehavior)
            {
                tempBehavior.OnLogicBehaviorChangeToOther();
            }
        }
    }

}