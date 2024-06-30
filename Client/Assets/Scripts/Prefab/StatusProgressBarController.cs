using UnityEngine;

public class StatusProgressBarController : MonoBehaviour
{
    Transform hpTransform;
    float startHpScale;
    int cur;
    int max;

    bool isInit = false;

    public float XScale = 0.74f;
    private void Awake()
    {
        hpTransform = transform.GetChild(1);
        startHpScale = hpTransform.localScale.x;
        gameObject.transform.localScale = Vector3.zero;
    }
    public void UpdateHp(int cur, int max)
    {
        if (this.cur == cur && this.max == max) return;

        if (!isInit) isInit = true;
        else gameObject.transform.localScale = Vector3.one;

        this.cur = cur;
        this.max = max;

        var scale = hpTransform.transform.localScale;
        scale.x = (float)cur / max * startHpScale;
        hpTransform.transform.localScale = scale;

        var pos = hpTransform.transform.localPosition;
        pos.x = -(startHpScale - scale.x) / 2;
        hpTransform.transform.localPosition = pos;
    }

    public void SetGreen()
    {
        hpTransform.gameObject.GetComponent<SpriteRenderer>().color = Color.green;
    }
}
