/*
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
    /// <summary> 行为组件 </summary>
    public BehaviorComponent BehaviorComponent { get; set; }
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
        BehaviorComponent = new BehaviorComponent(this);
        AttackComponent = new AttackComponent(this);

        SkillComponent = new SkillComponent(this);// 依赖装备组件,装备拥有技能 
        AttrComponent = new AttrComponent(this);  // 最后初始化，依赖其他组件 

        Collider = new CircleCollider(this, AttrComponent.ColliderRadius);
    }

    void InitEvent()
    {
        // 死亡事件 
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
        BehaviorComponent.FixedUpdate(curFrame);
        SkillComponent.FixedUpdate();
    }

    /// <summary> 消费伤害 </summary>
    public void HandleDamage(Damage damage)
    {
        CurFranmeDamages.Add(damage);
        // 消费伤害前 
        Event.Emit(EventEnum.OnPreHandleDamage, damage);

        // 伤害消费 
        Event.Emit(EventEnum.OnHandleDamage, damage);

        // 消费伤害后 
        Event.Emit(EventEnum.OnAfterHandleDamage, damage);

        // 消费伤害中的额外伤害
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