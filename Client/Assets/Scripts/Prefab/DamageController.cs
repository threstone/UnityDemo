using UnityEngine;

public class DamageController : MonoBehaviour
{
    public float Speed = 2f;
    public float ShowTimes = 0.7f;
    float curTimes = 0;

    private void Start()
    {
        var pos = transform.position;
        pos.x = pos.x * (100 + BattleRender.Random.Next(21) - 10) / 100;
        transform.position = pos;
    }

    void Update()
    {
        curTimes += Time.deltaTime;
        if (curTimes > ShowTimes)
        {
            Destroy(gameObject);
            return;
        }

        var pos = transform.position;
        pos.y += Speed * Time.deltaTime;
        transform.position = pos;
    }
}
