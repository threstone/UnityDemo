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

        Timer.UnityTimer.Register(5f, () => Debug.Log("Hello World"));
        Timer.UnityTimer.Register(3f, () =>
        {
            Debug.Log("123");
        });
    }
}
