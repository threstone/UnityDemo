using System;
using UnityEngine;

public class EventManagerTest : MonoBehaviour
{
    private void Awake()
    {
        Test();
    }

    private void Test()
    {

        UnityTimer.Timer.Register(5f, () => Debug.Log("Hello World"));
        UnityTimer.Timer.Register(3f, () =>
        {
            Debug.Log("123");
        });
    }
}
