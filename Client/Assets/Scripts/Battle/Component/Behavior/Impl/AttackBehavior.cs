using System.Numerics;

public class AttackBehavior : Behavior
{

    public AttackBehavior(BehaviorComponent behaviorComponent) : base(-1, behaviorComponent)
    {
        behaviorComponent.Entity.AttackComponent.Reset();
    }

    public override void FixedUpdate(int curFrame)
    {
        TryAttack(curFrame);
    }

    void TryAttack(int curFrame)
    {
        var entity = behaviorComponent.Entity;
        // 是否死亡
        var target = behaviorComponent.TargetEntity;
        if (target == null || target.IsDestroy)
        {
            behaviorComponent.AddBehavior(new IdleBahavior(10000, behaviorComponent));
            behaviorComponent.RemoveBehavior(this);
            return;
        }

        // 攻击距离检测
        if (AttackRangeCheck() == false)
        {
            TryClosedEnemy();
            return;
        }

        // 始终朝向敌方
        entity.Face = entity.Position.X < target.Position.X;
        entity.AttackComponent.FixedUpdate(curFrame);
    }

    bool AttackRangeCheck()
    {
        var target = behaviorComponent.TargetEntity;
        if (target == null || target.IsDestroy)
        {
            return false;
        }

        var entity = behaviorComponent.Entity;
        return Vector2.Distance(target.Position, entity.Position) <= entity.AttrComponent.AtkRange;
    }

    void TryClosedEnemy()
    {
        behaviorComponent.AddBehavior(new MoveBehavior(behaviorComponent));
        behaviorComponent.RemoveBehavior(this);
    }

    public override string GetAnimationName()
    {
        var attackComp = behaviorComponent.Entity.AttackComponent;
        return attackComp.IsAttacking() ? attackComp.AttackDamage.AttackAnimation : "Idle";
    }

    public new int GetAnimatorSpeed()
    {
        return behaviorComponent.Entity.AttackComponent.SpeedUpRate;
    }

    public override void OnBehaviorEnd()
    {
    }

    public override void OnLogicBehaviorChangeToOther()
    {
        behaviorComponent.Entity.AttackComponent.Reset();
    }
}