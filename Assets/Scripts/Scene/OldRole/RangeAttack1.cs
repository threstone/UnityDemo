using Assets.Scripts;
using UnityEngine;

public class RangeAttack1 : MonoBehaviour
{
    Transform ts;
    GameObject target;
    EntityController1 sourceController;
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
        Vector2 newPostion =  Vector2.MoveTowards(ts.position, targetPosition, sourceController.AtkProjectileSpeed * Time.fixedDeltaTime);
        if (newPostion.Equals(ts.position)) {
            Destroy(gameObject);
            return;
        }
        ts.position = newPostion;
    }


    public void Launch(EntityController1 sourceController ,GameObject target)
    {
        this.sourceController = sourceController;
        this.target = target;
    }
}
