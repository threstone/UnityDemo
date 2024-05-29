using System.Numerics;

public class MoveStatus : Status
{
    RoleEntity lockEnemy;

    float duration = 1.0f;
    bool isUp = true;
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
        if (lockEnemy == null)
        {
            TryGetEnemy();
            return;
        }

        if (AttackRangeCheck() == false)
        {
            TryClosedEnemy();
            return;
        }
        entity.Status = new AttackStatus(entity, lockEnemy);
    }

    void TryGetEnemy()
    {
        var playerId = entity.PlayerId;
        var enemyList = simulator.EntityList.FindAll((entity) =>
        {
            return entity is RoleEntity roleEntity && roleEntity.PlayerId != playerId;
        });
        if (enemyList.Count == 0)
        {
            return;
        }
        RoleEntity closedEntity = null;
        float minDistance = float.MaxValue;
        enemyList.ForEach((entity) =>
        {
            var roleEntity = entity as RoleEntity;
            var distance = Vector2.Distance(roleEntity.Position, this.entity.Position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closedEntity = roleEntity;
            }
        });
        if (closedEntity == null)
        {
            // 找不到敌人就idel一会儿
            entity.Status = new IdleStatus(entity);
        }
        else
        {
            lockEnemy = closedEntity;
        }
    }


    bool AttackRangeCheck()
    {
        if (lockEnemy == null)
        {
            return false;
        }

        return Vector2.Distance(lockEnemy.Position, entity.Position) <= entity.RoleInfo.AtkRange;
    }

    void TryClosedEnemy()
    {
        // todo
        // test code 
        float len = Simulator.FrameInterval * entity.RoleInfo.MoveSpeed;
        duration -= Simulator.FrameInterval;
        var pos = entity.Position;
        pos.Y += isUp ? len : -len;
        entity.Position = pos;
        if (duration < 0)
        {
            isUp = !isUp;
            duration = 1.0f;
        }
    }

    public override string GetName()
    {
        return "walk";
    }
}