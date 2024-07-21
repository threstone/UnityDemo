using UnityEngine;

public class InputController : MonoBehaviour
{
    RoleEntityController controller;
    private void Start()
    {
        controller = gameObject.GetComponent<RoleEntityController>();
        controller.EntityInfo = new RoleEntity(new Role(1001, 1, PlayerModel.PlayerId));
    }

    private void Update()
    {
        InputBehavior();
    }

    void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        var pos = new Vector2(horizontal, vertical);
        if (Mathf.Approximately(pos.magnitude, 0f))
        {
            return;
        }
        pos.Normalize();
        controller.EntityInfo.Position.X += pos.x * controller.EntityInfo.AttrComponent.MoveSpeed * Time.fixedDeltaTime;
        controller.EntityInfo.Position.Y += pos.y * controller.EntityInfo.AttrComponent.MoveSpeed * Time.fixedDeltaTime;
    }

    private void InputBehavior()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            Attack();
        }
        else if (Input.GetKeyDown(KeyCode.K))
        {
            Spell();
        }
    }
    private void Attack()
    {
        controller.Animator.SetTrigger("Attack");
    }

    private void Spell()
    {
        controller.Animator.SetTrigger("Spell");
    }
}