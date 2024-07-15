using System;
using System.Collections.Generic;
public class BuffComponent
{
    readonly Dictionary<int, Buff> buffMap;
    readonly Dictionary<int, Buff> controllBuffMap;

    readonly RoleEntity entity;

    bool beControlled = false;
    public bool BeControlled
    {
        get { return beControlled; }
        set
        {
            if (beControlled == false && value == true)
            {
                entity.Event.Emit(EventEnum.OnBeControlled);
            }
            beControlled = value;
        }
    }

    public BuffComponent(RoleEntity entity)
    {
        this.entity = entity;
        buffMap = new();
        controllBuffMap = new();
        entity.Event.On(EventEnum.OnHandleDamage, new Action<Damage>(OnHandleDamage));
    }

    public void FixedUpdate(int curFrame)
    {
        // 状态更新
        foreach (var buff in buffMap.Values) buff.FixedUpdate(curFrame);
        foreach (var buff in controllBuffMap.Values) buff.FixedUpdate(curFrame);
    }

    public void RemoveBuff(int buffId)
    {
        var config = ConfigMgr.GetBuffConfig(buffId);
        Dictionary<int, Buff> map = config.IsControll ? controllBuffMap : buffMap;
        map.Remove(buffId);
    }

    public void AddBuff(int buffId, int duration, RoleEntity sourceEntity)
    {
        var config = ConfigMgr.GetBuffConfig(buffId);
        Dictionary<int, Buff> map = config.IsControll ? controllBuffMap : buffMap;
        if (map.TryGetValue(buffId, out var buff))
        {
            buff.UpdateDuration(duration);
        }
        else
        {
            var newBuff = BuffMgr.GetBuffByType(buffId, duration, entity, sourceEntity);
            newBuff.OnBuffAdd();
            map.Add(buffId, newBuff);
        }
    }

    /// <summary> 获取当前是否被控制,如果有飓风、眩晕、恐惧等状态时,则表示被控制 </summary>
    public bool IsControlled()
    {
        return controllBuffMap.Count != 0;
    }

    /// <summary> 更新控制状态 </summary>
    public void UpdateControllStatus()
    {
        BeControlled = IsControlled();
    }

    /// <summary> 驱散  DispelType=>驱散类型 强驱散,弱驱散 </summary>
    public void Dispel(RoleEntity from, DispelTypeEnum dispelType)
    {
        if (dispelType == DispelTypeEnum.No) return;

        foreach (var buff in buffMap.Values)
        {
            /// <summary> 是否可以被驱散 </summary>
            /// <summary> 来自敌人的话 驱散有益buff </summary>
            /// <summary> 来自友方的话 驱散debuff </summary>
            if (buff.SourceEntity.PlayerId != from.PlayerId && dispelType <= buff.BuffConfig.DispelType)
            {
                buff.OnBuffClear();
                RemoveBuff(buff.BuffConfig.Id);
            }
        }
    }

    private void OnHandleDamage(Damage damage)
    {
        /// <summary> 消费伤害中的buff </summary>
        damage.BuffList.ForEach((b) => AddBuff(b.BuffId, b.Duration, damage.Entity));
    }
}