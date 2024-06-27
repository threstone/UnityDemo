using UnityEngine;

public class SimulatorTest : MonoBehaviour
{

    BattleSimulator simulator;
    void Start()
    {
        simulator = new BattleSimulator();
        simulator.DoTest();
    }

    private void FixedUpdate()
    {
        simulator.FixedUpdate();
    }
}
