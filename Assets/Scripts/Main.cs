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
        List<Role> roleList = new() {
            new Role(1001,1),
            new Role(1001,2)
        };
        Dictionary<int, Frame> frameDic = new();
        int randSeed = 1;
        simulator = new Simulator(randSeed, roleList, frameDic);
    }
}
