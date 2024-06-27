using System.Numerics;
public class MoveStatus : Status
{
    RoleEntity lockEnemy;

    public MoveStatus(RoleEntity entity) : base(entity)
    {
        Type = StatusEnum.Move;
    }

    public MoveStatus(RoleEntity entity, RoleEntity lockEnemy) : this(entity)
    {
        this.lockEnemy = lockEnemy;
    }

    public override void FixedUpdate(int curFrame)
    {
        if (lockEnemy == null || lockEnemy.IsDestroy)
        {
            TryGetEnemy();
            return;
        }

        if (AttackRangeCheck() == false)
        {
            TryClosedEnemy();
            return;
        }
        entity.StatusComponent.Status = new AttackStatus(entity, lockEnemy);
    }

    void TryGetEnemy()
    {
        var playerId = entity.PlayerId;
        var enemyList = simulator.EntityList.FindAll((entity) =>
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
            var distance = Vector2.Distance(roleEntity.Position, this.entity.Position);
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
            entity.StatusComponent.Status = new IdleStatus(entity);
        }
        else
        {
            lockEnemy = closedEntity;
        }
    }
    bool AttackRangeCheck()
    {
        if (lockEnemy == null || lockEnemy.IsDestroy)
        {
            return false;
        }

        return Vector2.Distance(lockEnemy.Position, entity.Position) <= entity.AttrComponent.AtkRange;
    }

    void TryClosedEnemy()
    {
        if (lockEnemy == null || lockEnemy.IsDestroy)
        {
            return;
        }
        var move = entity.GetMovePos(lockEnemy.Position, entity.AttrComponent.MoveSpeed);
        entity.Position += move;
        if (MoveCheck() == false)
        {
            entity.Position -= move;
        }
    }

    // 检查移动是否允许
    bool MoveCheck()
    {
        // 碰撞检测
        for (int i = 0; i < simulator.EntityList.Count; i++)
        {
            var entity = simulator.EntityList[i];
            if (entity is SceneEntity sceneEntity && entity.Id != this.entity.Id && this.entity.Collider.CheckCollision(sceneEntity))
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

    public override int GetAnimatorSpeed()
    {
        return 10000;
    }
}