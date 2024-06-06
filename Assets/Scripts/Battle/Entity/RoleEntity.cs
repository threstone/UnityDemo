/*
 * 角色实体
 */
using System.Collections.Generic;
using System.Data;

public class RoleEntity : SceneEntity
{
    public Role Role;
    public AttrComponent AttrComponent { get; set; }
    public Equipment[] Equipments;

    public bool Face { get; set; }

    public bool IsDead { get; set; }

    Dictionary<int, Buff> buffMap;
    public Status Status { get; set; }

    public RoleEntity(Role role, int id) : base(role.PlayerId, id)
    {
        Init(role);
    }

    private void Init(Role role)
    {
        IsDead = false;
        Role = role;
        Equipments = new Equipment[6];
        for (int i = 0; i < role.equipmentIdList?.Count; i++)
        {
            Equipments[i] = new Equipment(role.equipmentIdList[i]);
        }
        AttrComponent = new AttrComponent(this);

        buffMap = new();
        Status = new IdleStatus(this, 0.5f);
        Collider = new CircleCollider(this, AttrComponent.ColliderRadius);
    }

    public override void FixedUpdate(int curFrame)
    {
        base.FixedUpdate(curFrame);
        Status.FixedUpdate(curFrame);
        // 装备逻辑
        for (int i = 0; i < Equipments.Length; i++)
        {
            Equipments[i]?.FixedUpdate(curFrame);
        }
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
            buffMap.Add(buffType, new Buff(this, buffType, duration));
        }
    }

    public void Dead()
    {
        IsDead = true;
        Collider = null;
    }
}