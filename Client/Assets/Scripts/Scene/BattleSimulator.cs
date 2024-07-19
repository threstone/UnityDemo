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
        var enemyPlayerId = PlayerModel.PlayerId + 1;
        List<Role> roleList = new() {
            new Role(1001,1,PlayerModel.PlayerId){
                SkillList= new (){
                    new SkillData(){Id=21001001,level=1},
                    new SkillData(){Id=11001001,level=1}
                }
            },
            // new Role(1001,1,PlayerController.PlayerId),
            // new Role(1001,1,PlayerController.PlayerId),
            // new Role(1001,1,PlayerController.PlayerId),
            // new Role(1001,1,PlayerController.PlayerId),
            // new Role(1001,1,enemyPlayerId),
            // new Role(1001,1,enemyPlayerId),
            // new Role(1001,1,enemyPlayerId),
            // new Role(1001,1,enemyPlayerId),
            new Role(1001,1,enemyPlayerId){
                SkillList= new (){new SkillData(){Id=21001001,level=0}}
            },
        };
        Dictionary<int, Frame> frameDic = new();
        int randSeed = 1;
        simulator = new Simulator(randSeed, roleList, frameDic);
        battleRender = new BattleRender();
    }
}