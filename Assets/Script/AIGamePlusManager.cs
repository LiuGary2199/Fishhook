using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class AIGamePlusManager : TireStability<AIGamePlusManager>
{
    //获取IOS函数声明
#if UNITY_IOS && !UNITY_EDITOR
    [DllImport("__Internal")]
    internal extern static void onGameEvent(string eventToken);

    [DllImport("__Internal")]
    internal extern static void onGameLevelChanged(int level);
#endif

    public void SendEvent(string eventToken)
    {
#if UNITY_IOS && !UNITY_EDITOR
        onGameEvent(eventToken);
        print("AIGamePlus 尝试调用原生方法打点 事件：" + eventToken);
#endif
    }

    public void SendLevelChanged(int level)
    {
#if UNITY_IOS && !UNITY_EDITOR
        onGameLevelChanged(level);
        print($"AIGamePlus 尝试调用原生方法：等级： {level}");
#endif
    }
}

