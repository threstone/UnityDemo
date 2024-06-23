﻿using UnityEngine;

public class RoleEntityController : EntityController
{
    public new RoleEntity EntityInfo { get; set; }

    public Animator Animator;

    protected new void Awake()
    {
        base.Awake();
        Animator = GetComponent<Animator>();
    }

    public void Update()
    {
        if (null == EntityInfo) return;
        UpdatePostion();
        UpdateFace();
        UpdateAnimation();
        ShowDamages();
    }

    // 在元素上弹出伤害提示
    private void ShowDamages()
    {
        EntityInfo.CurFranmeDamages.ForEach((damage) =>
        {
            // todo
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
        if (PlayerController.PlayerId != EntityInfo.PlayerId) spriteRenderer.color = new Color(0.5f, 0.5f, 0.5f);
    }

    public override void Destroy()
    {
        Destroy(gameObject);
    }
}