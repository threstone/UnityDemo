using UnityEngine;


public class RoleEntityController : EntityController
{
    public new RoleEntity EntityInfo { get; set; }

    public Animator Animator;
    protected SpriteRenderer spriteRenderer;

    protected void Awake()
    {
        Animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Update()
    {
        if (null == EntityInfo) return;
        UpdatePostion();
        UpdateFace();
        UpdateAnimation();
    }

    private void UpdatePostion()
    {
        var pos = transform.position;
        pos.x = EntityInfo.Position.X;
        pos.y = EntityInfo.Position.Y;
        transform.position = pos;
    }

    private void UpdateFace()
    {
        spriteRenderer.flipX = EntityInfo.Face;
    }

    private void UpdateAnimation()
    {
        Animator.speed = EntityInfo.StatusComponent.Status.GetAnimatorSpeed();
        Animator.SetTrigger(EntityInfo.StatusComponent.Status.GetName());
    }

    public void UpdateGray()
    {
        if (PlayerController.PlayerId != EntityInfo.PlayerId)
        {
            spriteRenderer.color = new Color(0.5f, 0.5f, 0.5f);
        }
    }

    public override void Destroy()
    {
        Destroy(gameObject);
    }
}
