using System;

public class AttackComponent
{
    readonly RoleEntity roleEntity;
    int lastAtkFrame = -1000;
    int atkFrame = -1;
    bool isAtk = false;
    // 当攻击间隔低于1时需要加速攻击,否则无法成功实现高攻速
    public float SpeedUpRate { get; set; } = 1f;
    public AttackComponent(RoleEntity roleEntity)
    {
        this.roleEntity = roleEntity;
    }

    public void FixedUpdate(int curFrame)
    {
        if (roleEntity.PlayerId != PlayerController.PlayerId)
        {
            return;
        }
        // 攻击状态下
        if (atkFrame != -1f)
        {
            atkFrame++;
            if (!isAtk && IsPreAtkEnd())
            {
                lastAtkFrame = curFrame - atkFrame + 1;
                DoAttack();
            }
            else if (IsAtkEnd())
            {
                Reset();
                FixedUpdate(curFrame);
            }
            return;
        }

        // 未在攻击状态下 检查是否可以攻击
        if (AllowAtk(curFrame))
        {
            StartAtk();
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
        return curFrame >= MathF.Floor(lastAtkFrame + roleEntity.AttrComponent.AttackInterval / Simulator.FrameInterval);
    }

    // 开始攻击
    public void StartAtk()
    {
        atkFrame = 1;
        float AttackPerSecond = roleEntity.AttrComponent.AttackPerSecond;
        // 计算加速
        SpeedUpRate = MathF.Max(1f, AttackPerSecond);
    }

    // 前摇是否执行完毕
    public bool IsPreAtkEnd()
    {
        return atkFrame >= MathF.Floor(roleEntity.AttrComponent.BaseAttr.PreAtkTime / Simulator.FrameInterval / SpeedUpRate);//
    }

    public bool IsAtkEnd()
    {
        return atkFrame >= MathF.Floor(1f / Simulator.FrameInterval / SpeedUpRate);// 
    }

    // 执行攻击,远程生成攻击弹道   近战直接执行攻击
    void DoAttack()
    {
        isAtk = true;
        // todo   
    }
}