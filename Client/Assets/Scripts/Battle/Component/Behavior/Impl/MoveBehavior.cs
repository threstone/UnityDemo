using System.Numerics;

public class MoveBehavior : Behavior
{
    public MoveBehavior(BehaviorComponent behaviorComponent) : base(-1, behaviorComponent)
    {
        behaviorComponent.ActiveSkillSpellCheck();
    }

    public override void FixedUpdate(int curFrame)
    {
        // 检查是否有主动技能要执行
        if (curFrame % 25 == 0 && behaviorComponent.ActiveSkillSpellCheck()) return;

        if (behaviorComponent.TargetEntity == null || behaviorComponent.TargetEntity.IsDestroy)
        {
            TryGetEnemy();
            return;
        }

        if (AttackRangeCheck() == false)
        {
            TryClosedEnemy();
            return;
        }
        behaviorComponent.AddBehavior(new AttackBehavior(behaviorComponent));
        behaviorComponent.RemoveBehavior(this);
    }

    void TryGetEnemy()
    {
        var entity = behaviorComponent.Entity;
        var playerId = entity.PlayerId;
        var enemyList = entity.Simulator.EntityList.FindAll((entity) =>
        {
            return entity is RoleEntity roleEntity && roleEntity.IsDestroy != true && roleEntity.PlayerId != playerId;
        });
        if (enemyList.Count == 0)
        {
            return;
        }
        RoleEntity closedEntity = null;
        float minDistance = float.MaxValue;
        for (int i = 0; i < enemyList.Count; i++)
        {
            var roleEntity = enemyList[i] as RoleEntity;
            var distance = Vector2.Distance(roleEntity.Position, entity.Position);
            if (distance < entity.AttrComponent.AtkRange)
            {
                closedEntity = roleEntity;
                break;
            }
            else if (distance < minDistance)
            {
                minDistance = distance;
                closedEntity = roleEntity;
            }
        }

        if (closedEntity == null)
        {
            // 找不到敌人就idel一会儿
            behaviorComponent.AddBehavior(new IdleBahavior(10000, behaviorComponent));
            behaviorComponent.RemoveBehavior(this);
        }
        else
        {
            behaviorComponent.TargetEntity = closedEntity;
        }
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
        var target = behaviorComponent.TargetEntity;
        if (target == null || target.IsDestroy)
        {
            return;
        }
        var entity = behaviorComponent.Entity;
        var move = entity.GetMovePos(target.Position, entity.AttrComponent.MoveSpeed);
        entity.Position += move;
        if (MoveCheck() == false)
        {
            entity.Position -= move;
        }
    }

    /// <summary> 检查移动是否允许 </summary>
    bool MoveCheck()
    {
        var entity = behaviorComponent.Entity;
        var simulator = behaviorComponent.Entity.Simulator;
        /// <summary> 碰撞检测 </summary>
        for (int i = 0; i < simulator.EntityList.Count; i++)
        {
            var tempEntity = simulator.EntityList[i];
            if (tempEntity is SceneEntity sceneEntity && tempEntity.Id != entity.Id && entity.Collider.CheckCollision(sceneEntity))
            {
                return false;
            }
        }

        return true;
    }

    public override string GetAnimationName()
    {
        return "walk";
    }

    public override void OnBehaviorEnd()
    {
    }

    public override void OnLogicBehaviorChangeToOther()
    {
    }
}