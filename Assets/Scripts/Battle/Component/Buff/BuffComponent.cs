using System.Collections.Generic;
public class BuffComponent
{
    readonly Dictionary<BuffEnum, Buff> buffMap;
    readonly RoleEntity entity;
    public BuffComponent(RoleEntity entity)
    {
        this.entity = entity;
        buffMap = new();
    }


    public void FixedUpdate(int curFrame)
    {

        // 状态更新
        foreach (var buff in buffMap.Values) buff.FixedUpdate(curFrame);
    }

    public void RemoveBuff(BuffEnum buffType)
    {
        buffMap.Remove(buffType);
    }

    public void AddBuff(BuffEnum buffType, int duration, RoleEntity sourceEntity)
    {
        if (buffMap.TryGetValue(buffType, out var buff))
        {
            buff.UpdateDuration(duration);
        }
        else
        {
            var newBuff = BuffMgr.GetBuffByType(buffType, duration, entity, sourceEntity);
            newBuff.OnBuffAdd();
            buffMap.Add(buffType, newBuff);
        }
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
        foreach (var buff in buffMap.Values)
        {
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

    public void OnPreBeAttack(Damage damage)
    {
        foreach (var buff in buffMap.Values) buff.OnPreBeAttack(damage);
    }

    public void OnAfterBeAttack(Damage damage)
    {
        foreach (var buff in buffMap.Values) buff.OnAfterBeAttack(damage);
    }


    public void OnHandleDamage(Damage damage)
    {

        foreach (var buff in buffMap.Values) buff.OnHandleDamage(damage);
        // 消费伤害中的buff
        damage.BuffList?.ForEach((b) => AddBuff(b.BuffType, b.Duration, damage.Entity));
    }

    public void OnPreHandleDamage(Damage damage)
    {
        foreach (var buff in buffMap.Values) buff.OnPreHandleDamage(damage);
    }

    public void OnAfterHandleDamage(Damage damage)
    {
        foreach (var buff in buffMap.Values) buff.OnAfterHandleDamage(damage);
    }
}