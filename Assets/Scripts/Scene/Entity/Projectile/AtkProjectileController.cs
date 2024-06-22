using UnityEngine;

public class AtkProjectileController : EntityController
{
    public new AttackProjectile EntityInfo { get; set; }
    public void Update()
    {
        if (null == EntityInfo) return;
        UpdatePosAndRotate();
    }

    protected void UpdatePosAndRotate()
    {

        var curPos = transform.position;
        curPos.x = EntityInfo.Position.X / 10000;
        curPos.y = EntityInfo.Position.Y / 10000;

        // 计算角度
        Vector2 direction = curPos - transform.position;
        if (direction != Vector2.zero)
        {
            // 计算差分向量的角度，然后转换为相对于Z轴的角度
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // 将角度设置为Z轴的旋转角度
            // 确保角度在0到360度之间
            angle = (angle + 360) % 360;

            // 设置物体的旋转
            transform.eulerAngles = new Vector3(0, 0, angle);
        }

        transform.position = curPos;
    }

    public void UpdateGray()
    {
        if (PlayerController.PlayerId != EntityInfo.Source.PlayerId) spriteRenderer.color = new Color(0.5f, 0.5f, 0.5f);
    }

    public override void Destroy()
    {
        Destroy(gameObject);
    }
}
