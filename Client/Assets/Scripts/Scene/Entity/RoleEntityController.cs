﻿using UnityEngine;
using UnityEngine.UI;

public class RoleEntityController : EntityController
{
    public GameObject DamagePrefab;
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
            if (damage.IsShow) return;

            damage.IsShow = true;

            if (damage.IsMiss)
            {
                Utils.Log("Miss!");
                return;
            }

            // todo优化点 池化
            GameObject damageObj = Instantiate(DamagePrefab, BattleRender.Canvas.transform);
            var p = transform.position;
            p.y -= (float)EntityInfo.AttrComponent.BaseAttr.ProjectileOffset / 10000;
            damageObj.transform.position = p;
            var uiTextComponent = damageObj.GetComponent<Text>();
            uiTextComponent.text = "" + damage.DamageValue / 10000;
            if (PlayerModel.PlayerId == EntityInfo.PlayerId) uiTextComponent.color = Color.gray;
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
        if (PlayerModel.PlayerId != EntityInfo.PlayerId) spriteRenderer.color = Color.gray;
        else statusProgressBarController.SetGreen();
    }

    public override void Destroy()
    {
        Destroy(gameObject);
    }
}
