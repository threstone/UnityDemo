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
    public bool UpdateHp(int cur, int max)
    {
        if (curHp == cur && maxHp == max) return false;

        curHp = cur;
        maxHp = max;

        var scale = hpTransform.transform.localScale;
        scale.x = (float)cur / max * startHpScale;
        hpTransform.transform.localScale = scale;

        var pos = hpTransform.transform.localPosition;
        pos.x = -(startHpScale - scale.x) / 2;
        hpTransform.transform.localPosition = pos;
        return true;
    }

    public bool UpdateMana(int cur, int max)
    {
        if (curMana == cur && maxMana == max) return false;

        curMana = cur;
        maxMana = max;

        var scale = manaTransform.transform.localScale;
        scale.x = (float)cur / max * startManaScale;
        manaTransform.transform.localScale = scale;

        var pos = manaTransform.transform.localPosition;
        pos.x = -(startManaScale - scale.x) / 2;
        manaTransform.transform.localPosition = pos;
        return true;
    }

    public void UpdateProgress(RoleEntity entityInfo)
    {
        bool isUpdateHp = UpdateHp(entityInfo.AttrComponent.Hp.Current, entityInfo.AttrComponent.Hp.Maximum);
        bool isUpdateMana = UpdateMana(entityInfo.AttrComponent.Mana.Current, entityInfo.AttrComponent.Mana.Maximum);
        if (isUpdateHp || isUpdateMana)
        {
            if (!isInit) isInit = true;
            else gameObject.transform.localScale = Vector3.one;
        }
    }

    public void SetGreen()
    {
        hpTransform.gameObject.GetComponent<SpriteRenderer>().color = Color.green;
    }
}
