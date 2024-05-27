using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    BattleSimulator simulator;
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
        simulator = new BattleSimulator();
        simulator.DoTest();
    }
}
