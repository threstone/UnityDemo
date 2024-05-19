using System;
using System.Collections.Generic;

public class Simulator
{
    // 随机数种子
    int randomSeed;
    Random random;
    // 当前帧数
    int curFrame = -1;
    public int CurFrame { get => curFrame; }

    // 场景实体
    readonly List<Entity> entityList;

    // 帧数据
    Dictionary<int, Frame> frameDic;

    Simulator(int randomSeed, List<Entity> entityList, Dictionary<int, Frame> frameList)
    {
        this.randomSeed = randomSeed;
        this.entityList = entityList;
        this.frameDic = frameList;
        random = new Random(randomSeed);
        entityList.ForEach(e => e.SetSimulator(this));
    }

    public void FixedUpdate()
    {
        curFrame++;
        // 处理用户输入
        if (frameDic.TryGetValue(curFrame, out Frame? frame))
        {
            HandleUserInput(frame);
        }
        entityList?.ForEach(e => e.FixedUpdate());
    }

    public void OnUserInput(string key)
    {

        int f = curFrame + 1;
        if (!frameDic.TryGetValue(f, out Frame? frame))
        {
            frame = new Frame(f);
            frameDic.TryAdd(f, frame);
        }
        frame.userInput.Add(key);

    }

    void HandleUserInput(Frame frame)
    {
        frame.userInput.ForEach(key =>
        {
            // do something
        });
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

