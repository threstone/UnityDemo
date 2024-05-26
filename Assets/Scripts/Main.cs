using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    Simulator simulator;
    void Awake()
    {
        DontDestroyOnLoad(this);

        ConfigMgr.Init();

        DoTest();
    }

    private void FixedUpdate()
    {
        simulator?.FixedUpdate();
    }


    private void DoTest()
    {
        List<Entity> entityList = new()
        {
            new RoleEntity(1001,1),
            new RoleEntity(1001,2)
        };
        Dictionary<int, Frame> frameDic = new();
        simulator = new Simulator(1, entityList, frameDic);
    }
}
