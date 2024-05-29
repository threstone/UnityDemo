using System;
using System.Collections.Generic;

public class Simulator
{

    public static float FrameInterval = 0.02f;

    // 随机数种子
    int randomSeed;
    Random random;

    // 当前帧数
    public int CurFrame { get; set; }
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
        float[] postion = { 0, 1.96f, -1.96f, 3.7f, -3.7f };
        int leftUserId = roleList[0].PlayerId;
        byte leftIndex = 0;
        byte rightIndex = 0;
        for (int i = 0; i < roleList.Count; i++)
        {
            var role = roleList[i];
            bool isLeft = leftUserId == role.PlayerId;
            byte index = isLeft ? leftIndex : rightIndex;
            var roleEntity = new RoleEntity(role.RoleId, role.PlayerId, IncrId)
            {
                Simulator = this,
                Position = { X = isLeft ? -7 : 7, Y = postion[index] },
                Face = !isLeft
            };
            EntityList.Add(roleEntity);
            if (isLeft)
            {
                leftIndex++;
            }
            else
            {
                rightIndex++;
            }
        }
    }

    public void FixedUpdate()
    {
        CurFrame++;

        HandleUserInput();

        for (int i = EntityList.Count-1; i >=0; i--)
        {
            EntityList[i].FixedUpdate(CurFrame);
        }
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

