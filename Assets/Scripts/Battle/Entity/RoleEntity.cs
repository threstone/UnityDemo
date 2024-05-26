/*
 * 角色实体
 */
using System.Collections.Generic;

public class RoleEntity : SceneEntity
{
    public int RoleId { get; set; }
    public RoleConfig RoleInfo { get; set; }
    public bool Face { get; set; }


    Equipment[] equipmentArray;
    Dictionary<int, Buff> buffMap;
    public Status Status { get; set; }

    public RoleEntity(int roleId, int userId) : base(userId)
    {
        RoleId = roleId;
        Init();
    }

    public RoleEntity(Role role) : base(role.UserId)
    {
        RoleId = role.RoleId;
        Init();
    }

    private void Init()
    {
        RoleInfo = ConfigMgr.GetRoleInfoById(RoleId);
        equipmentArray = new Equipment[6];
        buffMap = new();
        Status = new IdleStatus(this);
    }

    public new void FixedUpdate(int curFrame)
    {
        base.FixedUpdate(curFrame);
        Status.FixedUpdate(curFrame);
        // 装备逻辑
        foreach (var item in equipmentArray)
        {
            item?.FixedUpdate(curFrame);
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
}