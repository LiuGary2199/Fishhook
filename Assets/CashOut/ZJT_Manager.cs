using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


//真假提 切换调用各种方法和获取数据
public class ZJT_Manager : TireStability<ZJT_Manager>
{
    /// <summary> 提现模块是否准备好 </summary>
    public bool CashOutReady()
    {
#if ZT
        return CashOutManager.AgeFletcher().Ready;
#endif

#if JT

#endif
        return true;
    }

    /// <summary> 登录 </summary>
    public void Login()
    {
#if ZT
        CashOutManager.AgeFletcher().Login();
#endif

#if JT
        JT_Manager.GetInstance().Login();
#endif
    }

    /// <summary> 初始化 </summary>
    public void Init()
    {
#if ZT
      
#endif

#if JT
        JT_Manager.GetInstance().Init();
#endif
    }

    /// <summary> 添加金额 </summary>
    public void AddMoney(float money, string Type = "JT_Money1")
    {
#if ZT
        CashOutManager.AgeFletcher().AddMoney(money);
#endif

#if JT
        JT_Manager.GetInstance().AddMoney(money, Type);
#endif
    }

    /// <summary> 获取金额 </summary>   
    public float GetMoney(string Type = "JT_Money1")
    {
#if ZT
        return CashOutManager.AgeFletcher().Money;
#endif

#if JT
        if (Type == "JT_Money1")
            return JT_Manager.GetInstance().JT_Money1;
        else if (Type == "JT_Money2")
            return JT_Manager.GetInstance().JT_Money2;
#endif
        return 0;
    }

    /// <summary> 金额减少后的事件 </summary>
    public UnityAction AfterReduceMoneyAction;
    public void AfterReduceMoney()
    {
        AfterReduceMoneyAction?.Invoke();
    }

    /// <summary> 获取汇率 </summary>
    public float GetRate()
    {
#if ZT
        
#endif

#if JT
        return JT_Manager.GetInstance().Rate;
#endif
        return 1;
    }

    /// <summary> 记录游戏开始时间 </summary>
    public void RecordStartTime()
    {
#if ZT
        CashOutManager.AgeFletcher().StartTime = System.DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
#endif

#if JT

#endif
    }

    /// <summary> 上报加载时间 </summary>
    public void ReportEvent_LoadingTime()
    {
#if ZT
        CashOutManager.AgeFletcher().ReportEvent_LoadingTime();
#endif

#if JT
        JT_Manager.GetInstance().ReportEvent_LoadingTime();
#endif
    }

    /// <summary> 获取用户ID </summary>
    public string GetUserID()
    {
#if ZT
        return CashOutManager.AgeFletcher().Data.UserID;
#endif

#if JT
       return JT_Manager.GetInstance().Data.UserID;
#endif
        return "";
    }

    /// <summary> 获取上报ecpm的request_id 广告加载时生成 </summary>
    public string GetEcpmRequestID()
    {
#if ZT
        return CashOutManager.AgeFletcher().EcpmRequestID();
#endif

#if JT
        return JT_Manager.GetInstance().EcpmRequestID();
#endif
        return "";
    }

    /// <summary> 上报adjust id </summary>
    public void ReportAdjustID()
    {
#if ZT
        CashOutManager.AgeFletcher().ReportAdjustID();
#endif

#if JT
        JT_Manager.GetInstance().ReportAdjustID();
#endif
    }

    /// <summary> 添加任务值 </summary>
    public void AddTaskValue(string Name, float Value)
    {
#if ZT
        CashOutManager.AgeFletcher().AddTaskValue(Name, Value);
#endif

#if JT
        JT_Manager.GetInstance().AddTastValue(Name, Value);
#endif
    }

    /// <summary> 跳转按钮事件 跳转到游戏某个位置 </summary>
    public UnityAction JumpBtnClickAction;
    public void JumpBtnClick()
    {
#if ZT
       
#endif

#if JT
        JumpBtnClickAction?.Invoke();
#endif
    }

    [HideInInspector] public Vector2 DisplayScale = Vector2.zero;
    [HideInInspector] public Vector2 HiddingScale = Vector2.zero;
    /// <summary> 重置Canvas的尺寸 </summary>
    public void ReSetCanvasScale(Vector2 DisplayScale, Vector2 HiddingScale)
    {
        this.DisplayScale = DisplayScale;
        this.HiddingScale = HiddingScale;
    }
}


#region 接口相关各类请求和响应数据结构

// 各种接口数据汇总 UI常用数据
[System.Serializable]
public class CashOutResponseData
{
    public string UserID;
    public long ConvertTime;     // Money转Cash时间戳
    public float Cash;           // 当前Cash
    public List<WithdrawRecordItem> Record; // 提现记录
    public CashOut_TaskData TaskData; //今日任务数据
}

// 基础响应模型
[System.Serializable]
public class BaseResponse
{
    public int code;
    public string msg;
}

// 用户相关数据模型
[System.Serializable]
public class Request_Login
{
    public string platform;
    public string bundle_id;
    public string uuid;
    public string device_ad_id;
    public string device_lang;
    public string model;
    public string manufacturer;
    public string screen_size;
    public string screen_pixel;
}
[System.Serializable]
public class Response_User : BaseResponse
{
    public UserData data;
}
[System.Serializable]
public class UserData
{
    public long id;
    public string nick_name;
    public string avatar;
    public string crt_time;
    public string coin;
    public string cash;
    public string channel;
    public string convert_time;
    public string token;
    public bool is_regist;
}

// 提现相关数据模型
[System.Serializable]
public class RequestData_Withdraw
{
    public string type = "TRANSFER";
    public string platform = "PAYPAL";
    public string amount;  // 改为string类型
    public string receiver_type = "EMAIL";
    public string receiver_value;
}
[System.Serializable]
public class Response_Withdraw : BaseResponse
{
    public int data;
}

// 提现规则数据模型
[System.Serializable]
public class Response_WithdrawRule : BaseResponse
{
    public WithdrawRuleData data;
}
[System.Serializable]
public class WithdrawRuleData
{
    public string min_amount;      // 最小提现金额
    public int day_count;        // 每日提现次数限制
    public string day_max_amount;  // 每日最大提现金额
    public int count;           // 总提现次数
    public string amount;         // 总提现金额
}

// 提现记录数据模型
[System.Serializable]
public class Response_WithdrawRecord : BaseResponse
{
    public WithdrawRecordData data;
}
[System.Serializable]
public class WithdrawRecordData
{
    public long count;
    public List<WithdrawRecordItem> data;
}
[System.Serializable]
public class WithdrawRecordItem
{
    public long id;
    public string amount;
    public string receiver_type;
    public string receiver_value;
    public string status;
    public string crt_time;
}

// ECPM相关数据模型
[System.Serializable]
public class RequestData_ReportEcpm
{
    public string type;
    public string request_id;
    public string amount;
    public string vendor;
    public string placement_id;
    public long timestamp;
    public string signature;
}
[System.Serializable]
public class Response_Ecpm : BaseResponse
{
    public int data;
}

// Adjust相关数据模型
[System.Serializable]
public class RequestData_ReportAdjustID
{
    public string id;
}

//打点相关数据模型
[System.Serializable]
public class RequestData_ReportEvent
{
    public int network;
    public int time_zone;
    public long timestamp;
    public List<RequestData_ReportEvent_Event> events;
}
[System.Serializable]
public class RequestData_ReportEvent_Event
{
    public int type;
    public long timestamp;
    public string string_0;
    public string string_1;
    public long? big_int_0;
    public int? big_int_1;
    public int? big_int_2;
}

#endregion
