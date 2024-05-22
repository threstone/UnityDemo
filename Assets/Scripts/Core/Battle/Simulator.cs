using System;
using System.Collections.Generic;

public class Simulator
{
    // 随机数种子
    int randomSeed;
    Random random;

    public float FrameInterval { get; set; }

    // 当前帧数
    public int CurFrame { get; set; }

    // 场景实体
    readonly List<Entity> entityList;

    // 帧数据
    readonly Dictionary<int, Frame> frameDic;

    public Simulator(int randomSeed, List<Entity> entityList, Dictionary<int, Frame> frameDic, float frameInterval)
    {
        this.randomSeed = randomSeed;
        this.entityList = entityList;
        this.frameDic = frameDic;
        FrameInterval = frameInterval;
        random = new Random(randomSeed);
        entityList.ForEach(e => e.SetSimulator(this));
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
        frame.userInput.Add(key);

    }

    void HandleUserInput()
    {
        // 处理用户输入
        if (frameDic.TryGetValue(CurFrame, out Frame frame))
        {
            frame.userInput.ForEach(key =>
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

