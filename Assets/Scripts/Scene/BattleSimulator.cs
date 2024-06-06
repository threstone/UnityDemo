using System.Collections.Generic;

public class BattleSimulator
{

    Simulator simulator;
    BattleRender battleRender;

    public void FixedUpdate()
    {
        simulator?.FixedUpdate();
        battleRender?.FixedUpdate(simulator);
    }

    public void DoTest()
    {
        List<Role> roleList = new() {
            new Role(1001,1,PlayerController.PlayerId),
            new Role(1001,1,PlayerController.PlayerId + 1)
        };
        Dictionary<int, Frame> frameDic = new();
        int randSeed = 1;
        simulator = new Simulator(randSeed, roleList, frameDic);
        battleRender = new BattleRender();
    }
}