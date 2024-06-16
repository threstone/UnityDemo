using UnityEngine;

public class Main : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(this);

        ConfigMgr.Init();

        BuffMgr.Init();

        SkillMgr.Init();
    }
}
