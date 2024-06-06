using System.Collections.Generic;

public class BuffComponent {
    Dictionary<int, Buff> buffMap;
    RoleEntity entity;
    public BuffComponent(RoleEntity entity) {
        this.entity = entity;
        buffMap = new Dictionary<int, Buff>();
    }

    public void FixedUpdate(int curFrame) {
        // 状态更新
        foreach (var statusType in buffMap.Keys)
        {
            buffMap[statusType].FixedUpdate(curFrame);
        };
    }

    public void RemoveBuff(int buffType)
    {
        buffMap.Remove(buffType);
    }

    public void AddBuff(int buffType, int duration)
    {
        if (buffMap.TryGetValue(buffType, out var buff))
        {
            buff.UpdateDuration(duration);
        }
        else
        {
            buffMap.Add(buffType, new Buff(entity, buffType, duration));
        }
    }
}