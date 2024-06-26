﻿/*
 * 角色实体
 */
using System;
using System.Collections.Generic;

public class RoleEntity : SceneEntity
{
    /// <summary> 属性组件 </summary>
    public AttrComponent AttrComponent { get; set; }
    /// <summary> 装备组件 </summary>
    public EquipmentComponent EquipmentComponent { get; set; }
    /// <summary> Buff组件 </summary>
    public BuffComponent BuffComponent { get; set; }
    /// <summary> 状态组件 </summary>
    public StatusComponent StatusComponent { get; set; }
    /// <summary> 攻击组件 </summary>
    public AttackComponent AttackComponent { get; set; }
    /// <summary> 技能组件 </summary>
    public SkillComponent SkillComponent { get; set; }

    public bool Face { get; set; }

    public Role Role { get; set; }

    public List<Damage> CurFranmeDamages = new();

    public EventManager Event = new();

    public RoleEntity(Role role) : base(role.PlayerId)
    {
        Role = role;
        InitEvent();
        InitComponent();
    }

    void InitComponent()
    {
        EquipmentComponent = new EquipmentComponent(Role);
        BuffComponent = new BuffComponent(this);
        StatusComponent = new StatusComponent(this);
        AttackComponent = new AttackComponent(this);

        SkillComponent = new SkillComponent(this);/// <summary> 依赖装备组件,装备拥有技能 </summary>
        AttrComponent = new AttrComponent(this);  /// <summary> 最后初始化，依赖其他组件 </summary>

        Collider = new CircleCollider(this, AttrComponent.ColliderRadius);
    }

    void InitEvent()
    {
        /// <summary> 死亡事件 </summary>
        Event.On(EventEnum.OnRoleDead, new Action(() => IsDestroy = true));
    }

    public new void BeforeUpdate(int curFrame)
    {
        ClearDamageList();
    }

    public override void FixedUpdate(int curFrame)
    {
        EquipmentComponent.FixedUpdate(curFrame);
        BuffComponent.FixedUpdate(curFrame);
        AttrComponent.FixedUpdate();
        StatusComponent.FixedUpdate(curFrame);
        SkillComponent.FixedUpdate();
    }

    /// <summary> 消费伤害 </summary>
    public void HandleDamage(Damage damage)
    {
        CurFranmeDamages.Add(damage);
        /// <summary> 消费伤害前 </summary>
        Event.Emit(EventEnum.OnPreHandleDamage, damage);

        /// <summary> 伤害消费 </summary>
        Event.Emit(EventEnum.OnHandleDamage, damage);

        /// <summary> 消费伤害后 </summary>
        Event.Emit(EventEnum.OnAfterHandleDamage, damage);

        /// <summary> 消费伤害中的额外伤害 </summary>
        damage.ExtraDamage.ForEach((d) => HandleDamage(d));
    }

    /// <summary> 清除已处理伤害列表 </summary>
    public void ClearDamageList()
    {
        CurFranmeDamages.ForEach((damage) =>
        {
            Damage.DestroyDamage(damage);
        });
        CurFranmeDamages.Clear();
    }
}