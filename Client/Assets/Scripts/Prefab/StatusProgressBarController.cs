using UnityEngine;

public class StatusProgressBarController : MonoBehaviour
{
    Transform hpSquare;
    float startHpScale;
    int cur;
    int max;

    public GameObject aaa;

    public float XScale = 0.74f;
    private void Start()
    {
        hpSquare = transform.GetChild(1);
        startHpScale = hpSquare.transform.localScale.x;
    }
    public void UpdateHp(int cur, int max)
    {
        if (this.cur == cur && this.max == max) return;
        this.cur = cur;
        this.max = max;

        var scale = hpSquare.transform.localScale;
        scale.x = (float)cur / max * startHpScale;
        hpSquare.transform.localScale = scale;

        var pos = hpSquare.transform.localPosition;
        pos.x = -(startHpScale - scale.x) / 2;
        hpSquare.transform.localPosition = pos;
    }
}
