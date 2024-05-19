using Assets.Scripts;
using UnityEngine;

public class RoleController : MonoBehaviour
{
    EntityController entityController;
    public GameObject enemy;
    private void Awake()
    {
        entityController = GetComponent<EntityController>();
    }

    // Update is called once per frame
    void Update()
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
        entityController.DoMove(horizontal, vertical);
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
        entityController.DoAttack(enemy);
    }


    private void Spell()
    {
        entityController.DoSpell();
    }
}
