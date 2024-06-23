using System;
using System.Collections.Generic;
using UnityEngine.Diagnostics;

public class Simulator
{

    // 每帧间隔多久(单位万分之一秒)
    public static int FrameInterval = 200;
    // 一秒有多少个时间单位,因为时间单位用的是万分比,所以一秒有10000个时间单位
    public static int TimeUnitRatioBySecond = 10000;

    // 随机数种子
    readonly int randomSeed;
    readonly Random random;

    // 当前帧
    public int CurFrame { get; set; } = 0;
    public int IncrId { get => incrId++; }

    private int incrId = 0;

    // 场景实体
    public readonly List<Entity> EntityList;

    // 帧数据
    readonly Dictionary<int, Frame> frameDic;

    public Simulator(int randomSeed, List<Role> roleList, Dictionary<int, Frame> frameDic)
    {
        this.randomSeed = randomSeed;
        random = new Random(randomSeed);
        this.frameDic = frameDic;

        EntityList = new();
        InitEntity(roleList);
    }

    // 初始化位置
    private void InitEntity(List<Role> roleList)
    {
        float[] postion = { 0, 17600f, -21600f, 35000f, -39000f };
        int leftUserId = roleList[0].PlayerId;
        byte leftIndex = 0;
        byte rightIndex = 0;
        for (int i = 0; i < roleList.Count; i++)
        {
            var role = roleList[i];
            bool isLeft = leftUserId == role.PlayerId;
            byte index = isLeft ? leftIndex : rightIndex;
            var roleEntity = new RoleEntity(role)
            {
                Simulator = this,
                Position = { X = isLeft ? -70000 : 70000, Y = postion[index] },
                Face = isLeft
            };
            AddEntity(roleEntity);

            if (isLeft) leftIndex++; else rightIndex++;

        }
    }

    public void AddEntity(Entity entity)
    {
        entity.Id = IncrId;
        EntityList.Add(entity);
    }

    public void FixedUpdate()
    {
        CurFrame++;

        // 逻辑帧执行前的逻辑
        for (int i = EntityList.Count - 1; i >= 0; i--)
        {
            var entity = EntityList[i];
            if (entity.IsDestroy)
            {
                EntityList.RemoveAt(i);
                continue;
            }
            entity.BeforeUpdate(CurFrame);
        }

        // 处理用户输入
        HandleUserInput();

        // 执行帧逻辑
        for (int i = EntityList.Count - 1; i >= 0; i--) EntityList[i].FixedUpdate(CurFrame);

        // 逻辑帧执行后的逻辑
        for (int i = EntityList.Count - 1; i >= 0; i--) EntityList[i].AfterUpdate(CurFrame);
    }

    public void OnUserInput(string key)
    {

        int handleTime = CurFrame + 1;
        if (!frameDic.TryGetValue(handleTime, out Frame frame))
        {
            frame = new Frame(handleTime);
            frameDic.TryAdd(handleTime, frame);
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

    // 不包含maxValue
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

