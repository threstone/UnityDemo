using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManagerTest : MonoBehaviour
{
    private void Awake()
    {
        Test();
    }

    private void Test()
    {
        Action<object> f = (data) =>
        {
            Debug.Log("on test" + data);
        };

        EventManager.instance.On("test", f);

        EventManager.instance.Once("test", (data) =>
        {
            Debug.Log("Once test" + data);
        });

        EventManager.instance.Emit("test");
        EventManager.instance.Emit("test", 123);
        EventManager.instance.Off("test", f);
        EventManager.instance.Emit("test", 123);
    }
}
