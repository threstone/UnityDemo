using UnityEngine;

public class StatusProgressBarController : MonoBehaviour
{
    Transform hpTransform;
    float startHpScale;
    int curHp;
    int maxHp;

    Transform manaTransform;
    float startManaScale;
    int curMana;
    int maxMana;

    bool isInit = false;
    private void Awake()
    {
        hpTransform = transform.GetChild(1);
        startHpScale = hpTransform.localScale.x;

        manaTransform = transform.GetChild(3);
        startManaScale = manaTransform.localScale.x;

        gameObject.transform.localScale = Vector3.zero;
    }
    public void UpdateHp(int cur, int max)
    {
        if (curHp == cur && maxHp == max) return;

        if (!isInit) isInit = true;
        else gameObject.transform.localScale = Vector3.one;

        curHp = cur;
        maxHp = max;

        var scale = hpTransform.transform.localScale;
        scale.x = (float)cur / max * startHpScale;
        hpTransform.transform.localScale = scale;

        var pos = hpTransform.transform.localPosition;
        pos.x = -(startHpScale - scale.x) / 2;
        hpTransform.transform.localPosition = pos;
    }

    public void UpdateMana(int cur, int max)
    {
        if (curMana == cur && maxMana == max) return;

        if (!isInit) isInit = true;
        else gameObject.transform.localScale = Vector3.one;

        curMana = cur;
        maxMana = max;

        var scale = manaTransform.transform.localScale;
        scale.x = (float)cur / max * startManaScale;
        manaTransform.transform.localScale = scale;

        var pos = manaTransform.transform.localPosition;
        pos.x = -(startManaScale - scale.x) / 2;
        manaTransform.transform.localPosition = pos;
    }

    public void SetGreen()
    {
        hpTransform.gameObject.GetComponent<SpriteRenderer>().color = Color.green;
    }
}
