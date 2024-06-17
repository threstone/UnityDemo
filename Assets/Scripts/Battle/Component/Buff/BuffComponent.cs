using System;
using System.Collections.Generic;
using System.Reflection;
public class BuffComponent
{
    readonly Dictionary<BuffEnum, Buff> buffMap;
    readonly RoleEntity entity;
    public BuffComponent(RoleEntity entity)
    {
        this.entity = entity;
        buffMap = new();
    }

    public void OnPreAttack(Damage damage)
    {
        foreach (var statusType in buffMap.Keys)
        {
            buffMap[statusType].OnPreAttack(damage);
        }
    }

    public void FixedUpdate(int curFrame)
    {
        // 状态更新
        foreach (var statusType in buffMap.Keys)
        {
            buffMap[statusType].FixedUpdate(curFrame);
        };
    }

    public void RemoveBuff(BuffEnum buffType)
    {
        buffMap.Remove(buffType);
    }

    public void AddBuff(BuffEnum buffType, int duration)
    {
        if (buffMap.TryGetValue(buffType, out var buff))
        {
            buff.UpdateDuration(duration);
        }
        else
        {
            var newBuff = BuffMgr.GetBuffByType(buffType, entity, duration);
            newBuff.OnBuffAdd();
            buffMap.Add(buffType, newBuff);
        }
    }

    // 消费伤害中的buff
    public void HandleDamage(Damage damage)
    {
        damage.BuffList?.ForEach((b) => AddBuff(b.BuffType, b.Duration));
    }

    // 获取当前是否被控制,如果有飓风、眩晕、恐惧等状态时,则表示被控制
    public bool IsControlled()
    {
        return false;//todo
    }

    // 获取当前动画，如果有飓风、眩晕、恐惧等状态时，需要返回对应的动画
    public string GetAnimationName()
    {
        //todo 
        return null;
    }

    //  驱散  DispelType=>驱散类型 强驱散,弱驱散
    public void Dispel(RoleEntity from, int DispelType)
    {
        // 状态更新
        foreach (var statusType in buffMap.Keys)
        {
            var buff = buffMap[statusType];
            // todo 是否可以被驱散
            // 来自敌人的话 驱散有益buff 
            // 来自友方的话 驱散debuff
            if (true)
            {
                buff.OnBuffClear();
                RemoveBuff(buff.BuffType);
            }
        };
    }
}