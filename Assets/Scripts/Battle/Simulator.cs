using System;
using System.Collections.Generic;

public class Simulator
{

    public static float FrameInterval = 0.12f;

    // 随机数种子
    int randomSeed;
    Random random;

    // 当前帧数
    public int CurFrame { get; set; }

    // 场景实体
    readonly List<Entity> entityList;

    // 帧数据
    readonly Dictionary<int, Frame> frameDic;

    public Simulator(int randomSeed, List<Role> roleList, Dictionary<int, Frame> frameDic)
    {
        this.randomSeed = randomSeed;
        random = new Random(randomSeed);
        this.frameDic = frameDic;

        entityList = new();
        roleList.ForEach((role) => {
            var roleEntity = new RoleEntity(role.RoleId, role.UserId);
            roleEntity.Simulator = this;
            entityList.Add(roleEntity);
        });
    }

    public void FixedUpdate()
    {
        CurFrame++;

        HandleUserInput();

        entityList?.ForEach(e => e.FixedUpdate(CurFrame));
    }

    public void OnUserInput(string key)
    {

        int f = CurFrame + 1;
        if (!frameDic.TryGetValue(f, out Frame frame))
        {
            frame = new Frame(f);
            frameDic.TryAdd(f, frame);
        }
        frame.UserInput.Add(key);

    }

    void HandleUserInput()
    {
        // 处理用户输入
        if (frameDic.TryGetValue(CurFrame, out Frame frame))
        {
            frame.UserInput.ForEach(key =>
            {
                // do something
            });
        }
    }

    public int RandomNext(int minValue = 0, int maxValue = int.MaxValue)
    {
        return random.Next(minValue, maxValue);
    }

    public object GetSimulatorInfo()
    {
        // todo
        return randomSeed;
    }
}

