﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class EntityController : MonoBehaviour
    {
        public float moveSpeed;

        // 攻击范围
        public float atkRange;

        // 攻击前摇时间
        public float preAttackTime = 0;

        // 攻击投射物相关信息
        public GameObject atkProjectile;
        public float atkProjectileSpeed;
        public float atkProjectileStartX;
        public float atkProjectileStartY;

        Animator animator;
        SpriteRenderer spriteRenderer;
        Rigidbody2D rigidbody2;

        EventManager eventManager = new EventManager();

        // 动作执行回调，如果遇到眩晕等控制用来打断动作执行回调
        private readonly List<Timer> actionCbList = new();
        private long stopStunTicks = -1;

        protected void Awake()
        {
            animator = GetComponent<Animator>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            rigidbody2 = GetComponent<Rigidbody2D>();

        }

        public void DoMove(float horizontal, float vertical)
        {
            Vector2 move = new Vector2(horizontal, vertical);
            animator.SetFloat("speed", move.magnitude);
            if (CanOprate() == false)
            {
                return;
            }
            Vector2 position = rigidbody2.position;
            position.x += moveSpeed * horizontal * Time.fixedDeltaTime;
            position.y += moveSpeed * vertical * Time.fixedDeltaTime;
            rigidbody2.MovePosition(position);
            if (horizontal == 0f)
            {
                return;
            }

            spriteRenderer.flipX = horizontal < 0;
        }
        private bool CanOprate()
        {
            AnimatorStateInfo minfo = animator.GetCurrentAnimatorStateInfo(0);
            return minfo.IsTag("1");
        }

        public void DoAttack(GameObject target)
        {
            if (CanOprate() == false)
            {
                return;
            }

            // 攻击距离判断
            Vector2 selfPos = rigidbody2.position;
            Transform targetPos = target.GetComponent<Transform>();
            Vector2 difPos = (Vector2)targetPos.position - selfPos;
            if (difPos.magnitude > atkRange)
            {
                return;
            }
            // 朝向改变
            if (difPos.x < 0)
            {
                spriteRenderer.flipX = true;
            }
            else if (difPos.x > 0)
            {
                spriteRenderer.flipX = false;
            }
            // 执行攻击动画
            animator.SetTrigger("attack");
            actionCbList.Add(Timer.Register(preAttackTime, () => OnPreAtkEnd(target)));
            Timer.Register(0.2f, () => DoStun(2f));
            Timer.Register(1.2f, () => DoStun(2f));
        }

       
        // 使眩晕、将打断
        public void DoStun(float stunTime)// todo 眩晕时间实现
        {
            long tempTicks = DateTime.Now.Ticks + (long)(stunTime * 10000000);
            if (tempTicks < stopStunTicks)
            {
                return;
            }
            stopStunTicks = tempTicks;
            Debug.Log("Stun" + stunTime + "  DateTime.Now" + DateTime.Now + "   DateTime.UtcNow" + DateTime.UtcNow);
            animator.SetTrigger("stun");
            actionCbList.ForEach((timer) =>
            {
                Timer.Cancel(timer);
            });

            actionCbList.Add(Timer.Register(stunTime, () => animator.SetTrigger("stopStun")));
            actionCbList.Add(Timer.Register(stunTime, () => Debug.Log("stun end")));
        }

        // 攻击前摇执行完毕
        public void OnPreAtkEnd(GameObject target = null)
        {
            Debug.Log("OnPreAtkEnd");
            // 远程不会有攻击飞弹特效
            if (target != null && atkProjectile != null)
            {
                Vector2 selfPos = rigidbody2.position;
                // 攻击对象起始位置
                selfPos.x += atkProjectileStartX * (spriteRenderer.flipX ? -1 : 1);
                selfPos.y += atkProjectileStartY;
                GameObject projectileObject = Instantiate(atkProjectile, selfPos, Quaternion.identity);
                RangeAttack atkEntity = projectileObject.GetComponent<RangeAttack>();
                atkEntity.Launch(this, target);
            }
        }

        public void DoSpell(int spellIndex)
        {
            if (CanOprate() == false)
            {
                return;
            }
            animator.SetTrigger("spell" + spellIndex);
        }

        public void DoSpell()
        {
            if (CanOprate() == false)
            {
                return;
            }
            animator.SetTrigger("spell");
        }
    }
}