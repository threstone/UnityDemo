using Assets.Scripts;
using UnityEngine;

public class RangeAttack : MonoBehaviour
{
    Transform ts;
    GameObject target;
    EntityController sourceController;
    //float speed;

    void Awake()
    {
        ts = GetComponent<Transform>();
    }

    private void FixedUpdate()
    {
        if (target == null)
        {
            return;
        }

        Vector2 targetPosition = target.GetComponent<Transform>().position;
        Vector2 newPostion =  Vector2.MoveTowards(ts.position, targetPosition, sourceController.atkProjectileSpeed * Time.fixedDeltaTime);
        if (newPostion.Equals(ts.position)) {
            Destroy(gameObject);
            return;
        }
        ts.position = newPostion;
    }


    public void Launch(EntityController sourceController ,GameObject target)
    {
        this.sourceController = sourceController;
        this.target = target;
    }
}
