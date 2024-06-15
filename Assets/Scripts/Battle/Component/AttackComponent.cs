using System;

public class AttackComponent
{
    readonly RoleEntity entity;
    int lastAtkFrame = -int.MaxValue;
    int atkFrame = -1;
    bool isAtk = false;
    // 当攻击间隔低于1时需要加速攻击,否则无法成功实现高攻速
    public int SpeedUpRate { get; set; } = 10000;
    public AttackComponent(RoleEntity entity)
    {
        this.entity = entity;
    }

    public void FixedUpdate(int curFrame)
    {
        // 未在攻击状态下 检查是否可以攻击
        if (atkFrame == -1 && AllowAtk(curFrame))
        {
            StartAtk();
        }

        // 攻击状态下
        if (atkFrame != -1)
        {
            atkFrame++;
            if (!isAtk && IsPreAtkEnd())
            {
                lastAtkFrame = curFrame - atkFrame + 1;
                StartAttack();
            }
            else if (IsAtkEnd())
            {
                Reset();
                FixedUpdate(curFrame);
            }
            return;
        }
    }

    // 重置状态
    public void Reset()
    {
        atkFrame = -1;
        isAtk = false;
    }

    public bool IsAttacking()
    {
        return atkFrame != -1;
    }

    // 是否允许攻击,攻击间隔检测
    public bool AllowAtk(int curFrame)
    {
        return (curFrame - lastAtkFrame) * Simulator.FrameInterval >= entity.AttrComponent.AttackInterval;
    }

    // 开始攻击
    public void StartAtk()
    {
        atkFrame = 0;
        // 计算加速
        SpeedUpRate = Math.Max(10000, entity.AttrComponent.AttackTimesPer10000Sec);
    }

    // 前摇是否执行完毕
    public bool IsPreAtkEnd()
    {
        return atkFrame * Simulator.FrameInterval >= entity.AttrComponent.BaseAttr.PreAtkTime * 10000 / SpeedUpRate;
    }

    public bool IsAtkEnd()
    {
        return atkFrame * Simulator.FrameInterval >= 10000 * 10000 / SpeedUpRate;
    }

    // 开始攻击,远程生成攻击弹道   近战直接执行攻击
    void StartAttack()
    {
        isAtk = true;
        // 近战直接执行攻击
        if (entity.AttrComponent.BaseAttr.AtkType == AtkTypeEnum.MeleeHero)
        {
            var enemy = (entity.StatusComponent.Status as AttackStatus).LockEnemy;
            enemy.AttackComponent.BeAttack(entity);
        }
        // 远程生成攻击弹道
        else
        {
            new AttackProjectile(entity);
        }
    }

    // 被攻击
    public void BeAttack(RoleEntity attacker)
    {
        var selfAttr = entity.AttrComponent;
        var targetAttr = attacker.AttrComponent;
        var damage = targetAttr.GetAttack();
        // 是否miss
        if (!damage.IgnoreAttackMiss() && selfAttr.IsMiss()) return;
        // 消费伤害
        entity.AttrComponent.HandleDamage(damage);
    }

}