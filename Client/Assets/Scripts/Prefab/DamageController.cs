using UnityEngine;

public class DamageController : MonoBehaviour
{
    static readonly Pool<GameObject> damageObjectPool = new();
    public static GameObject GetDamage(GameObject damagePrefab)
    {
        if (damageObjectPool.Count == 0)
        {
            GameObject damageObj = Instantiate(damagePrefab, BattleRender.Canvas.transform);
            return damageObj;
        }
        else
        {
            var res = damageObjectPool.Get();
            DamageController damageController = res.GetComponent<DamageController>();
            damageController.Init();
            res.SetActive(true);
            return res;
        }
    }

    public static void DamageBackToPool(GameObject damage)
    {
        damageObjectPool.Back(damage);
        damage.SetActive(false);
    }

    public float Speed = 2f;
    public float ShowTimes = 0.7f;
    float curTimes = 0;

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        curTimes = 0;
        var pos = transform.position;
        pos.x = pos.x * (100 + BattleRender.Random.Next(21) - 10) / 100;
        transform.position = pos;
    }

    void Update()
    {
        curTimes += Time.deltaTime;
        if (curTimes > ShowTimes)
        {
            DamageBackToPool(gameObject);
            return;
        }

        var pos = transform.position;
        pos.y += Speed * Time.deltaTime;
        transform.position = pos;
    }
}
