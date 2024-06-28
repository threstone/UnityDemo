﻿using UnityEngine;

public class RoleEntityController : EntityController
{
    public new RoleEntity EntityInfo { get; set; }

    public Animator Animator;

    StatusProgressBarController statusProgressBarController;

    protected new void Awake()
    {
        base.Awake();
        Animator = GetComponent<Animator>();
        statusProgressBarController = transform.Find("StatusProgressBar").gameObject.GetComponent<StatusProgressBarController>();
    }

    public void Update()
    {
        if (null == EntityInfo) return;
        UpdatePostion();
        UpdateFace();
        UpdateAnimation();
        ShowDamages();
        UpdateStatusProgressBar();
    }

    /// <summary> 更新血量  </summary>
    private void UpdateStatusProgressBar()
    {
        statusProgressBarController.UpdateHp(EntityInfo.AttrComponent.Hp.Current, EntityInfo.AttrComponent.Hp.Maximum);
    }

    /// <summary> 在元素上弹出伤害提示  </summary>
    private void ShowDamages()
    {
        EntityInfo.CurFranmeDamages.ForEach((damage) =>
        {
            // todo
            if (damage.IsShow) return;

            damage.IsShow = true;

            if (damage.IsMiss)
            {
                Utils.Log("Miss!");
                return;
            }

            Utils.Log("[" + EntityInfo.Id + "]受到了来自[" + damage.Entity.Id + "]的" + damage.RealValue + "(" + damage.DamageValue + ")点伤害,当前生命值:"
            + EntityInfo.AttrComponent.Hp.Current);
        });
    }

    private void UpdatePostion()
    {
        var pos = transform.position;
        pos.x = EntityInfo.Position.X / 10000;
        pos.y = EntityInfo.Position.Y / 10000;
        transform.position = pos;
    }

    private void UpdateFace()
    {
        spriteRenderer.flipX = !EntityInfo.Face;
    }

    private void UpdateAnimation()
    {
        Animator.speed = (float)EntityInfo.StatusComponent.Status.GetAnimatorSpeed() / 10000;
        Animator.SetTrigger(EntityInfo.StatusComponent.GetAnimationName());
    }

    public void UpdateGray()
    {
        if (PlayerModel.PlayerId != EntityInfo.PlayerId) spriteRenderer.color = new Color(0.5f, 0.5f, 0.5f);
    }

    public override void Destroy()
    {
        Destroy(gameObject);
    }
}
