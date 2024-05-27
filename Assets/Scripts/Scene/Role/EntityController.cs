using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class EntityController : MonoBehaviour
    {
        public RoleEntity RoleEntity { get; set; }

        Animator animator;
        SpriteRenderer spriteRenderer;

        protected void Awake()
        {
            animator = GetComponent<Animator>();
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void Update()
        {
            if (null == RoleEntity) return;
            UpdatePostion();
            UpdateFace();
            UpdateAnimation();
        }

        private void UpdatePostion()
        {
            var pos = transform.position;
            pos.x = RoleEntity.Position.X;
            pos.y = RoleEntity.Position.Y;
            transform.position = pos;
        }

        private void UpdateFace() {
            spriteRenderer.flipX = RoleEntity.Face;
        }

        private void UpdateAnimation()
        {
            animator.SetTrigger(RoleEntity.Status.GetName());
        }

        public void UpdateGray() {
            if (PlayerController.PlayerId != RoleEntity.PlayerId) {
                spriteRenderer.color = new Color(0.5f, 0.5f, 0.5f);
            }
        }
    }
}