using System;
using System.Collections;
using System.Collections.Generic;
using LitJson;
using UnityEngine;
using System.Security.Cryptography;
using System.Text;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;

public enum LoginPlatform { Android, IOS }

#if ZT
/// <summary> 提现功能管理 </summary>
public class CashOutManager : TireStability<CashOutManager>
{
    const string Version = "[1] 2026-01-22";
    [Header("登录平台")]
    public LoginPlatform _LoginPlatform = LoginPlatform.Android;
    [Header("真提现后台的产品id")]
    public string AppInfo = "4";
    string WithdrawPlatform = "PAYPAL";
    [Header("真提现后台地址 （http不带s + 域名）")]
    public string BaseUrl = "https://us.nicedramatv.com";
    [HideInInspector] public string Account;
    [HideInInspector] public CashOutResponseData Data;
    [HideInInspector] public long LeftTime; // 剩余时间
    [HideInInspector] public CashOutPanel _CashOutPanel;
    [HideInInspector] public CashOutEnter _CashOutEnter;
    [HideInInspector] public float Money;
    [HideInInspector] public bool Ready;
    float MinWithdrawCount; //最小提现金额
    int Event_1304Time;
    string ClientIP;
    string RealIP;
    [HideInInspector] public long StartTime;


    #region 游戏逻辑
    private void Start()
    {
        Account = SpotGushAwesome.GetString("CashOut_Account");
        Money = SpotGushAwesome.GetFloat("CashOut_Money");

        QuitCacheCandle.AgeFletcher().HornCache("3010", "真", Version);
    }

    private void OnApplicationPause(bool pauseStatus)
    {
#if ZT
        long Seconds = LeftTime / 10000000;
        if (pauseStatus && Seconds > 0)
        {
            string title = "Your reward is ready!";
            string info = $"All {TedSlumElk.instance.CashOut_Data.MoneyName} have been converted,Please check your rewards!";
            ReproductiveAwesome.Fletcher.ClearNotification();
            ReproductiveAwesome.Fletcher.ScheduleNotification(title, info, (int)Seconds);
            for (int i = 0; i < 10; i++) // 10次延时 10800秒 3小时
                ReproductiveAwesome.Fletcher.ScheduleNotification(title, info, (int)Seconds + (i * 10800));
        }

        if (pauseStatus)
            ReportEvent(1005);
        else
            ReportEvent(1006);
#endif
    }

    void TimeCount() //计时
    {
        if (Data != null)
        {
            //转化时间倒计时
            long nowTime = System.DateTime.UtcNow.Ticks;
            LeftTime = Data.ConvertTime - nowTime;
            //倒计时结束 更新用户信息 
            if (LeftTime <= 0)
            {
                LeftTime = 0;
                if (_CashOutPanel != null && _CashOutPanel.gameObject.activeSelf)
                    _CashOutPanel?.UpdateUserInfo(); //因为界面需要显示加载动画所以此处由_CashOutPanel调用
                else
                    UpdateUserInfo();
            }
            //更新剩余时间ui 
            string timeStr = "";
            long Seconds = LeftTime / 10000000;
            if (Seconds <= 0)
                timeStr = "00:00:00";
            else
            {
                int hour = (int)(Seconds / 3600);
                int minute = (int)((Seconds - hour * 3600) / 60);
                int second = (int)(Seconds - hour * 3600 - minute * 60);
                timeStr = string.Format("{0:D2}:{1:D2}:{2:D2}", hour, minute, second);
            }
            _CashOutPanel?.UpdateTime(timeStr);
            _CashOutEnter?.UpdateTime(timeStr);


            //任务
            if (TedSlumElk.instance.CashOut_Data != null && TedSlumElk.instance.CashOut_Data.TaskList.Count > 0)
            {
                //记录第一次登录日期utc
                if (!PlayerPrefs.HasKey("CashOut_FirstLoginTime"))
                {
                    DateTime firstLaunchUTC = DateTime.UtcNow.Date;
                    PlayerPrefs.SetString("CashOut_FirstLoginTime", firstLaunchUTC.ToString());
                }
                // 判断是否进入新的一天
                bool isNewDay = false;
                if (PlayerPrefs.HasKey("CashOut_LastCheckDate"))
                {
                    if (DateTime.TryParse(PlayerPrefs.GetString("CashOut_LastCheckDate"), out DateTime lastDate))
                        isNewDay = !DateTime.UtcNow.Date.Equals(lastDate);
                }
                else
                    isNewDay = true;
                PlayerPrefs.SetString("CashOut_LastCheckDate", DateTime.UtcNow.Date.ToString());
                if (isNewDay)
                {
                    PlayerPrefs.SetInt("TaskEventReported", 0);
                    PlayerPrefs.SetFloat("CashOut_TaskValue", 0);
                    _CashOutPanel?.UpdateTask();
                }
                //计算今天距离第一次登录过了几天
                int Day = (DateTime.UtcNow.Date - DateTime.Parse(PlayerPrefs.GetString("CashOut_FirstLoginTime"))).Days;
                if (Day >= TedSlumElk.instance.CashOut_Data.TaskList.Count) //一天一任务 天数超出任务数量显示默认任务
                    Data.TaskData = TedSlumElk.instance.CashOut_Data.TaskList.FirstOrDefault(t => t.IsDefault);
                else
                    Data.TaskData = TedSlumElk.instance.CashOut_Data.TaskList[Day];
                Data.TaskData.NowValue = PlayerPrefs.GetFloat("CashOut_TaskValue");
            }
        }
    }

    public void AddMoney(float Value)
    {
        Money += Value;
        SpotGushAwesome.SetFloat("CashOut_Money", Money);
        _CashOutPanel?.UpdateMoney();
        _CashOutEnter?.UpdateMoney();
    }

    public void WaitToSendEvent1304() //等待 发送关闭商店后行为1304事件
    {
        InvokeRepeating(nameof(Count1304Time), 0, 1);
    }
    void Count1304Time() //计时器
    {
        Event_1304Time++;
    }
    public void SendEvent1304() ////打点 关闭商店后行为
    {
        CancelInvoke(nameof(Count1304Time));
        if (Event_1304Time <= 0)
            return;
        QuitCacheCandle.AgeFletcher().HornCache("1304", Event_1304Time.ToString());
        Event_1304Time = 0;
    }

    void CashOutLog(string log, bool IsError = false, bool IsOk = false) //提现相关功能日志
    {
        if (IsError)
            Debug.LogError("<color=red><b>+++++   " + log + "</b></color>");
        else
        {
            if (IsOk)
                print("<color=cyan><b>+++++   " + log + "</b></color>");
            else
                print("<color=yellow><b>+++++   " + log + "</b></color>");
        }
    }

    // //测试
    // void Update()
    // {
    //     if (Input.GetKeyDown(KeyCode.Space))
    //     {
    //         _CashOutPanel?.MoneyToCashAnim();
    //         _CashOutEnter?.MoneyToCashAnim(true);
    //     }
    // }
    #endregion

    #region 短剧后台各类接口
    Dictionary<string, string> Headers() // 请求头
    {
#if UNITY_EDITOR //编译器不传设备信息
        return new Dictionary<string, string>
        {
            {"app-version", Application.version},
            {"lang", I2.Loc.LocalizationManager.CurrentLanguageCode},
            {"Authorization", SpotGushAwesome.GetString("CashOut_Token")},
            {"platform", WithdrawPlatform},
            {"os-version", ""},
            {"device-name", ""},
        };
#endif

        return new Dictionary<string, string>
        {
            {"app-version", Application.version},
            {"lang", I2.Loc.LocalizationManager.CurrentLanguageCode},
            {"Authorization", SpotGushAwesome.GetString("CashOut_Token")},
            {"platform", WithdrawPlatform},
            {"os-version", GetOperatingSystem()},
            {"device-name", SystemInfo.deviceName},
        };
    }
    string GetOperatingSystem() // 获取操作系统版本
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            try
            {
                using (AndroidJavaClass versionClass = new AndroidJavaClass("android.os.Build$VERSION"))
                {
                    string release = versionClass.GetStatic<string>("RELEASE");  // 获取系统版本号，如 "14"
                    int sdkInt = versionClass.GetStatic<int>("SDK_INT"); // 获取 SDK 等级，如 34
                    return release + "_" + sdkInt;
                }
            }
            catch (System.Exception e)
            {
                Debug.LogError("获取 Android 系统版本失败：" + e.Message);
                return "unknown";
            }
        }
        else if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            try
            {
                string osString = SystemInfo.operatingSystem; // 示例："iPhone OS 17.5.1"
                System.Text.RegularExpressions.Match match = System.Text.RegularExpressions.Regex.Match(osString, @"\d+(\.\d+)*"); // 用正则表达式提取版本号中的纯数字部分（例如 17.5.1）
                if (match.Success)
                    return match.Value;
                else
                    return "unknown";
            }
            catch (System.Exception e)
            {
                Debug.LogError("获取 iOS 系统版本失败：" + e.Message);
                return "unknown";
            }
        }
        return "unknown";
    }

    public void Login() // 登录
    {
        string Platform = "Editor";
        string Manufacturer = "Editor";
        string DeviceAdId = "";
        if (_LoginPlatform == LoginPlatform.Android)
        {
            Platform = "Android";
            DeviceAdId = SpotGushAwesome.GetString("gaid");
            if (Application.platform == RuntimePlatform.Android)
            {
                AndroidJavaClass aj = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
                AndroidJavaObject p = aj.GetStatic<AndroidJavaObject>("currentActivity");
                Manufacturer = p.CallStatic<string>("getManufacturer");
            }
        }
        else
        {
            Platform = "iOS";
            Manufacturer = "Apple";
            DeviceAdId = SpotGushAwesome.GetString("idfv");
        }
        StringBuilder uuidsb = new StringBuilder();
        uuidsb.Append(SystemInfo.deviceUniqueIdentifier);
        //UUID存在不同应用相同ID的情况 用SystemInfo.deviceUniqueIdentifier + AppInfo 
        bool isNewPlayer = !PlayerPrefs.HasKey(CMillet.If_AxEarSailor + "Bool") || SpotGushAwesome.GetBool(CMillet.If_AxEarSailor);
        bool hasuuidAndAppid = SpotGushAwesome.GetBool("UuidAndAPPid");
        if (isNewPlayer || hasuuidAndAppid) //新老用户兼容
            uuidsb.Append(AppInfo);
        SpotGushAwesome.SetString("uuid", uuidsb.ToString());
        var loginRequest = new Request_Login
        {
            platform = Platform,
            bundle_id = Application.identifier,
            uuid = uuidsb.ToString(),
            device_ad_id = DeviceAdId,
            device_lang = CultureInfo.CurrentCulture.Name,
            model = SystemInfo.deviceModel,
            manufacturer = Manufacturer,
            screen_size = Mathf.RoundToInt(Screen.width) + "*" + Mathf.RoundToInt(Screen.height),
            screen_pixel = Mathf.RoundToInt(Screen.currentResolution.width) + "*" + Mathf.RoundToInt(Screen.currentResolution.height),
        };

        string jsonBody = JsonMapper.ToJson(loginRequest);
        string loginUrl = $"{BaseUrl}/login";
        CashOutLog($"请求登录  请求体: {jsonBody}", false);

        TedYearAwesome.AgeFletcher().TestQuitMode(
            url: loginUrl,
            jsonData: jsonBody,
            success: (result) =>
            {
                try
                {
                    var response = JsonMapper.ToObject<Response_User>(result.downloadHandler.text);
                    if (response.code == 0)
                    {
                        CashOutLog("登录成功 数据： " + result.downloadHandler.text, false, true);
                        //UUID 新老用户兼容
                        bool isNewPlayer = !PlayerPrefs.HasKey(CMillet.If_AxEarSailor + "Bool") || SpotGushAwesome.GetBool(CMillet.If_AxEarSailor);
                        if (isNewPlayer)
                            SpotGushAwesome.SetBool("UuidAndAPPid", true);
                        //刷新token 获取提现规则
                        SpotGushAwesome.SetString("CashOut_Token", response.data.token);
                        GetWithdrawRule();
                        //整理数据
                        Data = new CashOutResponseData();
                        Data.UserID = response.data.id.ToString();
                        Data.Cash = float.Parse(response.data.cash, CultureInfo.InvariantCulture);
                        DateTime ConvertTime = DateTime.Parse(response.data.convert_time);
                        if (PlayerPrefs.HasKey("CashOut_ConvertTime"))
                            Data.ConvertTime = long.Parse(SpotGushAwesome.GetString("CashOut_ConvertTime"));
                        if (Data.ConvertTime < ConvertTime.Ticks)
                        {
                            Money = 0;
                            SpotGushAwesome.SetFloat("CashOut_Money", Money);
                        }
                        Data.ConvertTime = ConvertTime.Ticks;
                        SpotGushAwesome.SetString("CashOut_ConvertTime", Data.ConvertTime.ToString());
                        InvokeRepeating(nameof(TimeCount), 1, 1);

                        // 更新UI
                        _CashOutPanel?.UpdateMoney();
                        _CashOutEnter?.UpdateMoney();

                        Ready = true;

                        //上报ip
                        GetClientIP();
                        GetRealIP_Step1();

                        //游戏启动打点 需要先登录成功才能打点 这里的时间戳参数由ReportEvent方法内部特殊处理
                        ReportEvent(1000);

                        //上报设备信息
                        ReportEvent_DeviceInfo();
                    }
                    else
                    {
                        BrinyAwesome.AgeFletcher().DaleBriny("Login fail :" + response.msg);

                        CashOutLog($"登录失败: {response.msg}", true);
                        CashOutLog("1. 如果报错是 app not found，检查包名和真提现后台ID是否填对，如果都对 联系乔梁删后台错误数据", true);
                        CashOutLog("2. 如果报错是 user block，是被真提现后台反作弊策略命中了， 联系乔梁删把你加白名单", true);
                        CashOutLog("3. 如果报错是 app version not found，是真提现后台没创建对应版本， 检查版本是否填对 或联系乔梁创建对应版本后台", true);
                        CashOutLog("4. 如果报错是 server not xxx，一般在虚拟机出现，是真提现不在这个地区提供服务， 换个网或者vpn", true);
                    }
                }
                catch (Exception e)
                {
                    CashOutLog($"解析登录响应数据失败: {e.Message}", true);
                }
            },
            fail: () =>
            {
                CashOutLog("登录请求失败", true);
                BrinyAwesome.AgeFletcher().DaleBriny("Login fail");
                Ready = false;
            },
            timeout: 3f,
            headers: Headers()
        );
    }

    public void UpdateUserInfo() // 更新用户信息
    {
        CancelInvoke(nameof(TimeCount));
        string url = $"{BaseUrl}/user";
        TedYearAwesome.AgeFletcher().TestAge(
            url: url,
            success: (result) =>
            {
                try
                {
                    var response = JsonMapper.ToObject<Response_User>(result.downloadHandler.text);
                    if (response.code == 0)
                    {
                        CashOutLog("用户信息数据： " + result.downloadHandler.text, false, true);
                        string Event_Money = Money.ToString();

                        double OldCash = SpotGushAwesome.GetDouble("CashOut_Cash");
                        float NewCash = float.Parse(response.data.cash, CultureInfo.InvariantCulture);
                        DateTime ConvertTime = DateTime.Parse(response.data.convert_time);
                        //当前时间小于后台时间 代表新一轮转换开始 清空Money
                        if (Data.ConvertTime < ConvertTime.Ticks)
                        {
                            Money = 0;
                            SpotGushAwesome.SetFloat("CashOut_Money", Money);
                        }
                        if (Money == 0)
                        {
                            //如果转换后Cash增加 播飞钱动画 否则播进度归零动画
                            bool IsIconFly = NewCash > 0 && NewCash > OldCash;
                            _CashOutPanel?.MoneyToCashAnim();
                            _CashOutEnter?.MoneyToCashAnim(IsIconFly);

                            //打点 如果钱转化了 上报转化信息
                            if (IsIconFly)
                                QuitCacheCandle.AgeFletcher().HornCache("1302", Event_Money, NewCash.ToString());
                        }
                        else
                        {
                            Data.Cash = NewCash;
                            _CashOutPanel?.UpdateMoney();
                            _CashOutEnter?.UpdateMoney();
                        }
                        Data.ConvertTime = ConvertTime.Ticks;
                        SpotGushAwesome.SetString("CashOut_ConvertTime", Data.ConvertTime.ToString());
                        Data.Cash = NewCash;

                        InvokeRepeating(nameof(TimeCount), 0, 1);
                        SpotGushAwesome.SetDouble("CashOut_Cash", Data.Cash);
                        _CashOutPanel?.CloseLoading_UpdateUI();
                    }
                    else
                    {
                        CashOutLog($"获取用户信息失败: {response.msg}", true);
                    }
                }
                catch (Exception e)
                {
                    CashOutLog($"解析用户信息响应数据失败: {e.Message}", true);
                }
            },
            fail: () =>
            {
                CashOutLog("获取用户信息请求失败", true);
            },
            timeout: 3f,
            headers: Headers()
        );
    }

    public void GetWithdrawRule() // 获取提现规则（规则获取不到不影响流程）
    {
        string url = $"{BaseUrl}/withdraw/rule?platform={WithdrawPlatform}";

        TedYearAwesome.AgeFletcher().TestAge(
            url: url,
            success: (result) =>
            {
                try
                {
                    var response = JsonMapper.ToObject<Response_WithdrawRule>(result.downloadHandler.text);
                    if (response.code == 0) // 成功状态码
                    {
                        CashOutLog("提现规则数据： " + result.downloadHandler.text, false, true);
                        MinWithdrawCount = float.Parse(response.data.min_amount, CultureInfo.InvariantCulture);
                    }
                    else
                    {
                        CashOutLog($"提现规则获取失败: {response.msg}", true);
                    }
                }
                catch (Exception e)
                {
                    CashOutLog($"解析提现规则响应数据失败: {e.Message}", true);
                }
            },
            fail: () =>
            {
                CashOutLog("获取最小提现金额请求失败", true);
            },
            timeout: 3f,
            headers: Headers()
        );
    }

    public void Withdraw() // 提现
    {
        if (Data.Cash < MinWithdrawCount)
        {
            BrinyAwesome.AgeFletcher().DaleBriny($"Minimum withdrawal amount {MinWithdrawCount}");
            _CashOutPanel?.CloseLoading_Withdraw(true);
            string Amount = Data.Cash.ToString("F2", System.Globalization.CultureInfo.InvariantCulture);
            SendWithdrawEvent(Amount, false);
            return;
        }

        var withdrawRequest = new RequestData_Withdraw
        {
            //测试
            //amount = MinWithdrawCount.ToString("F2",System.Globalization.CultureInfo.InvariantCulture),

            amount = Data.Cash.ToString("F2", System.Globalization.CultureInfo.InvariantCulture),
            receiver_value = Account
        };

        string jsonBody = JsonMapper.ToJson(withdrawRequest);
        string url = $"{BaseUrl}/withdraw";

        TedYearAwesome.AgeFletcher().TestQuitMode(
            url: url,
            jsonData: jsonBody,
            success: (result) =>
            {
                try
                {
                    var response = JsonMapper.ToObject<Response_Withdraw>(result.downloadHandler.text);
                    if (response.code == 0)
                    {
                        CashOutLog("提现数据： " + result.downloadHandler.text, false, true);
                        _CashOutPanel?.CloseLoading_Withdraw(true);
                        _CashOutPanel?.UpdateUserInfo();

                        SendWithdrawEvent(withdrawRequest.amount, true);
                    }
                    else
                    {
                        CashOutLog($"提现失败: {response.msg}", true);
                        _CashOutPanel?.CloseLoading_Withdraw();

                        SendWithdrawEvent(withdrawRequest.amount, false);
                    }
                }
                catch (Exception e)
                {
                    CashOutLog($"解析提现响应数据失败: {e.Message}", true);
                    _CashOutPanel?.CloseLoading_Withdraw();
                    BrinyAwesome.AgeFletcher().DaleBriny("Withdraw fail :" + e.Message);

                    SendWithdrawEvent(withdrawRequest.amount, false);
                }
            },
            fail: () =>
            {
                CashOutLog("提现请求失败", true);
                _CashOutPanel?.CloseLoading_Withdraw();

                SendWithdrawEvent(withdrawRequest.amount, false);
            },
            timeout: 3f,
            headers: Headers()
        );
    }
    void SendWithdrawEvent(string Event_Cash, bool IsSuccess) //打点 提现成功或失败
    {
        QuitCacheCandle.AgeFletcher().HornCache("1303", Event_Cash, IsSuccess ? "1" : "0");
    }

    public void GetWithdrawRecord() // 获取提现记录
    {
        string url = $"{BaseUrl}/withdraw";
        TedYearAwesome.AgeFletcher().TestAge(
            url: url,
            success: (result) =>
            {
                try
                {
                    var response = JsonMapper.ToObject<Response_WithdrawRecord>(result.downloadHandler.text);
                    if (response.code == 0)
                    {
                        CashOutLog("提现记录数据： " + result.downloadHandler.text, false, true);
                        Data.Record = response.data.data;
                        _CashOutPanel?.CloseLoading_Record();
                        _CashOutPanel?.UpdateRecord();
                    }
                    else
                    {
                        CashOutLog($"提现记录获取失败: {response.msg}", true);
                    }
                }
                catch (Exception e)
                {
                    CashOutLog($"解析提现记录响应数据失败: {e.Message}", true);
                }
            },
            fail: () =>
            {
                CashOutLog("提现记录请求失败", true);
            },
            timeout: 3f,
            headers: Headers()
        );
    }

    public void ReportEcpm(MaxSdkBase.AdInfo info, string RequestID, string AdFormat) // 上报ecpm
    {
        return; //暂时不需要上报ecpm

        if (Application.isEditor)
        {
            CashOutLog("假装上报ecpm，当前为编辑器模式 RequestID： " + RequestID);
            return;
        }
        string url = $"{BaseUrl}/ecpm";
        RequestData_ReportEcpm requestData = new RequestData_ReportEcpm();
        requestData.type = AdFormat;
        requestData.request_id = RequestID;
        requestData.amount = info.Revenue.ToString("G17", System.Globalization.CultureInfo.InvariantCulture);
        requestData.vendor = info.NetworkName;
        requestData.placement_id = info.Placement;
        requestData.timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
        requestData.signature = EcpmSignatureMD5(requestData.request_id, requestData.amount, Data.UserID, requestData.timestamp);
        string jsonBody = JsonMapper.ToJson(requestData);
        CashOutLog($"上报ecpmURL: {url}  请求体: {jsonBody}", false);

        TedYearAwesome.AgeFletcher().TestQuitMode(
            url: url,
            jsonData: jsonBody,
            success: (result) =>
            {
                CashOutLog("上报ecpm数据： " + result.downloadHandler.text, false);
                try
                {
                    var response = JsonMapper.ToObject<Response_Ecpm>(result.downloadHandler.text);
                    if (response.code == 0) // 成功状态码
                    {
                        CashOutLog("上报ecpm成功", false);
                    }
                    else
                    {
                        CashOutLog($"上报ecpm失败: {response.msg}", true);
                    }
                }
                catch (Exception e)
                {
                    CashOutLog($"解析上报ecpm响应数据失败: {e.Message}", true);
                }
            },
            fail: () =>
            {
                CashOutLog("上报ecpm请求失败", true);
            },
            timeout: 3f,
            headers: Headers()
        );
    }
    public string EcpmRequestID() // 获取上报ecpm的request_id 广告加载时生成
    {
        string uuid = Guid.NewGuid().ToString();
        string formattedUuid = uuid.ToLowerInvariant().Replace("-", "");
        return formattedUuid;
    }
    string EcpmSignatureMD5(string requestId, string amount, string userId, long timestamp) // 上报ecpm生成签名
    {
        string Input = $"{requestId}-{amount}-{userId}-{timestamp}";
        using (MD5 md5 = MD5.Create())
        {
            byte[] inputBytes = Encoding.UTF8.GetBytes(Input);
            byte[] hashBytes = md5.ComputeHash(inputBytes);
            return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
        }
    }

    public void ReportAdjustID() // 上报adjust_id
    {
        string url = $"{BaseUrl}/user/ad";
        RequestData_ReportAdjustID requestData = new RequestData_ReportAdjustID();
        requestData.id = ArouseCapeAwesome.Instance.AgeArouseAlga();
        string jsonBody = JsonMapper.ToJson(requestData);
        CashOutLog($"上报adjust_idURL: {url}  请求体: {jsonBody}", false);
        TedYearAwesome.AgeFletcher().TestQuitMode(
            url: url,
            jsonData: jsonBody,
            success: (result) =>
            {
                CashOutLog("上报adjust_id数据： " + result.downloadHandler.text, false);
                try
                {
                    var response = JsonMapper.ToObject<BaseResponse>(result.downloadHandler.text);
                    if (response.code == 0) // 成功状态码
                    {
                        CashOutLog("上报adjust_id成功", false);
                    }
                    else
                    {
                        CashOutLog($"上报adjust_id失败: {response.msg}", true);
                    }
                }
                catch (Exception e)
                {
                    CashOutLog($"解析上报adjust_id响应数据失败: {e.Message}", true);
                }
            },
            fail: () =>
            {
                CashOutLog("上报adjust_id请求失败", true);
            },
            timeout: 3f,
            headers: Headers()
        );
    }

    void GetClientIP() // 获取客户端IP
    {
        string url = "http://ip-api.com/json/?key=NN3ExblXQt2Esoy";
        TedYearAwesome.AgeFletcher().TestAge(
            url: url,
            success: (result) =>
            {
                //CashOutLog("获取客户端IP数据： " + result.downloadHandler.text, false);
                try
                {
                    string json = result.downloadHandler.text;
                    JsonData data = JsonMapper.ToObject(json);
                    if (data.ContainsKey("query"))
                    {
                        ClientIP = data["query"].ToString();
                        ReportIDs();
                    }
                    else
                    {
                        CashOutLog("未找到IP地址", true);
                    }
                }
                catch (Exception e)
                {
                    CashOutLog($"解析获取客户端IP响应数据失败: {e.Message}", true);
                }
            },
            fail: () =>
            {
                CashOutLog("获取客户端IP请求失败", true);
            },
            timeout: 3f,
            headers: null
        );
    }
    void GetRealIP_Step1() // 获取真实IP网址
    {
        string url = "https://nstool.netease.com/";
        TedYearAwesome.AgeFletcher().TestAge(
            url: url,
            success: (result) =>
            {
                //CashOutLog("获取真实IP网址： " + result.downloadHandler.text, false);
                try
                {
                    // 使用正则表达式匹配iframe的src属性
                    var match = System.Text.RegularExpressions.Regex.Match(result.downloadHandler.text, @"<iframe\s+[^>]*src=['""]([^'""]+)['""][^>]*>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                    if (match.Success)
                    {
                        GetRealIP_Step2(match.Groups[1].Value);
                        //RealIP = match.Groups[1].Value;
                    }
                }
                catch (Exception e)
                {
                    CashOutLog($"解析获取真实IP网址响应数据失败: {e.Message}", true);
                }
            },
            fail: () =>
            {
                CashOutLog("获取真实IP网址请求失败", true);
            },
            timeout: 3f,
            headers: null
        );
    }
    void GetRealIP_Step2(string url) // 获取真实IP
    {
        TedYearAwesome.AgeFletcher().TestAge(
           url: url,
           success: (result) =>
           {
               //CashOutLog("获取真实IP数据： " + result.downloadHandler.text, false);
               try
               {
                   string ipPattern = @"(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)";
                   var match = System.Text.RegularExpressions.Regex.Match(result.downloadHandler.text, ipPattern);
                   if (match.Success)
                   {
                       RealIP = match.Groups[0].Value;
                       ReportIDs();
                   }
               }
               catch (Exception e)
               {
                   CashOutLog($"解析获取真实IP响应数据失败: {e.Message}", true);
               }
           },
           fail: () =>
           {
               CashOutLog("获取真实IP请求失败", true);
           },
           timeout: 3f,
           headers: null
       );
    }
    void ReportIDs() // 上报各种ID
    {
        CashOutLog("上报ID  客户端Ip：" + ClientIP + "  真实Ip：" + RealIP);
        string url = $"{BaseUrl}/user/meta";
        TedYearAwesome.AgeFletcher().TestQuitMode(
            url: url,
            jsonData: GetReportIDsBody(),
            success: (result) =>
            {
                try
                {
                    var response = JsonMapper.ToObject<BaseResponse>(result.downloadHandler.text);
                    if (response.code == 0) // 成功状态码
                    {
                        CashOutLog("上报ID成功 数据： " + result.downloadHandler.text, false, true);
                    }
                    else
                    {
                        CashOutLog($"上报各种ID失败: {response.msg}", true);
                    }
                }
                catch (Exception e)
                {
                    CashOutLog($"解析上报各种ID响应数据失败: {e.Message}", true);
                }
            },
            fail: () =>
            {
                CashOutLog("上报各种ID请求失败", true);
            },
            timeout: 3f,
            headers: Headers()
        );
    }
    string GetReportIDsBody() // 获取上报各种ID的请求体(处理某个ip为空的情况)
    {
        if (string.IsNullOrEmpty(ClientIP))
            return JsonMapper.ToJson(new { REAL_IP = RealIP });
        if (string.IsNullOrEmpty(RealIP))
            return JsonMapper.ToJson(new { CLIENT_IP = ClientIP });
        return JsonMapper.ToJson(new { CLIENT_IP = ClientIP, REAL_IP = RealIP });
    }

    public void ReportEvent(int type, string string_0 = null, string string_1 = null, long? big_int_0 = null, int? big_int_1 = null, int? big_int_2 = null) // 上报事件
    {
        if (string.IsNullOrEmpty(SpotGushAwesome.GetString("CashOut_Token")))
        {
            CashOutLog($"没Token不上报事件{type}", true);
            return;
        }

        long EventTimestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(); //毫秒时间戳
        if (type == 1000)  //游戏启动时间戳 由于要先等登录成功获取token 所以特殊处理
            EventTimestamp = StartTime;
        RequestData_ReportEvent EventRequest = new RequestData_ReportEvent();
        EventRequest.network = GetNetworkInt();
        EventRequest.time_zone = GetTimeZone();
        EventRequest.timestamp = EventTimestamp;
        EventRequest.events = new List<RequestData_ReportEvent_Event>();
        RequestData_ReportEvent_Event Event = new RequestData_ReportEvent_Event();
        Event.type = type;
        Event.timestamp = EventTimestamp;
        Event.string_0 = string_0;
        Event.string_1 = string_1;
        Event.big_int_0 = big_int_0;
        Event.big_int_1 = big_int_1;
        Event.big_int_2 = big_int_2;
        EventRequest.events.Add(Event);

        string url = $"{BaseUrl}/event";
        CashOutLog($"上报事件{type}  请求体: {JsonMapper.ToJson(EventRequest)}", false);
        TedYearAwesome.AgeFletcher().TestQuitMode(
            url: url,
            jsonData: JsonMapper.ToJson(EventRequest),
            success: (result) =>
            {
                //CashOutLog("上报事件数据： " + result.downloadHandler.text, false);
                try
                {
                    var response = JsonMapper.ToObject<BaseResponse>(result.downloadHandler.text);
                    if (response.code == 0) // 成功状态码
                    {
                        CashOutLog($"上报事件{type}成功", false, true);
                    }
                    else
                    {
                        CashOutLog($"上报事件{type}失败: {response.msg}", true);
                    }
                }
                catch (Exception e)
                {
                    CashOutLog($"解析上报事件响应数据失败: {e.Message}", true);
                }
            },
            fail: () =>
            {
                CashOutLog("上报事件请求失败", true);
            },
            timeout: 3f,
            headers: Headers()
        );
    }
    int GetNetworkInt() //根据网络类型获取对应的int值 
    {
        //安卓调原生方法 获取更详细的网络类型
        if (Application.platform == RuntimePlatform.Android)
        {
            AndroidJavaClass aj = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject p = aj.GetStatic<AndroidJavaObject>("currentActivity");
            return p.CallStatic<int>("getNetwork");
        }

        //苹果端暂时这样处理
        NetworkReachability reachability = Application.internetReachability;
        if (reachability == NetworkReachability.ReachableViaLocalAreaNetwork)
            return 1; // WIFI
        else
            return 0; // 其他网络
    }
    int GetTimeZone() //获取当前时区相对于UTC0时区的差值，单位：秒
    {
        // 获取当前本地时间
        DateTime localTime = DateTime.Now;
        // 获取当前时间对应的UTC时间
        DateTime utcTime = localTime.ToUniversalTime();
        // 计算时间差（本地时间 - UTC时间）
        TimeSpan offset = localTime - utcTime;
        // 将时间差转换为秒
        return (int)offset.TotalSeconds;
    }
    public void ReportEvent_LoadingTime() //上报loading时间
    {
        long LoadingTime = System.DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() - StartTime;
        ReportEvent(1004, null, null, (int)LoadingTime);
    }
    void ReportEvent_DeviceInfo() //上报设备信息
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            AndroidJavaClass unityPlayerClass = null;
            AndroidJavaObject currentActivity = null;
            try
            {
                unityPlayerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
                currentActivity = unityPlayerClass.GetStatic<AndroidJavaObject>("currentActivity");
                if (currentActivity == null)
                {
                    Debug.LogError("获取Android Activity失败，设备信息上报中断");
                    return;
                }
                // 是否开启VPN
                bool isVpn = CallAndroidStaticBool(currentActivity, "isVpn");
                if (isVpn) ReportEvent(1007);
                // 是否模拟器
                bool isSimulator = CallAndroidStaticBool(currentActivity, "isSimulator");
                if (isSimulator) ReportEvent(1008);
                // 是否ROOT
                bool isRoot = CallAndroidStaticBool(currentActivity, "isRoot");
                if (isRoot) ReportEvent(1009);
                // 是否开发者模式
                bool isDeveloper = CallAndroidStaticBool(currentActivity, "isDeveloper");
                if (isDeveloper) ReportEvent(1010);
                // 是否开启USB调试
                bool isUsb = CallAndroidStaticBool(currentActivity, "isUsb");
                if (isUsb) ReportEvent(1011);
                // 是否有SIM卡
                bool isSimCard = CallAndroidStaticBool(currentActivity, "isSimcard");
                ReportEvent(1012, null, null, isSimCard ? 1 : 0);
                // 是否安装Google Play服务
                bool isHaveGMS = CallAndroidStaticBool(currentActivity, "isHaveGMS");
                ReportEvent(1014, null, null, isHaveGMS ? 1 : 0);
                // 是否在Google Play商店能查询到
                currentActivity.Call("isInStore");
                // 是否安装Paypal
                bool isHavePaypal = CallAndroidStaticBool(currentActivity, "isHavePaypal");
                ReportEvent(1018, null, null, isHavePaypal ? 1 : 0);
                //安装来源
                string installerPackageName = "unknown";
                using (AndroidJavaObject packageManager = currentActivity.Call<AndroidJavaObject>("getPackageManager"))
                {
                    string packageName = currentActivity.Call<string>("getPackageName");
                    installerPackageName = packageManager.Call<string>("getInstallerPackageName", packageName) ?? "unknown";
                    ReportEvent(1019, installerPackageName);
                }
                //开机时间
                long bootTime = 0;
                using (AndroidJavaClass systemClock = new AndroidJavaClass("android.os.SystemClock"))
                {
                    bootTime = systemClock.CallStatic<long>("elapsedRealtime"); // uptimeMillis不含休眠；elapsedRealtime含休眠
                    ReportEvent(1020, null, null, bootTime);
                }
                //硬件信息
                int cpuNum = SystemInfo.processorCount; // CPU数量
                long totalRAMBytes = (long)SystemInfo.systemMemorySize * 1024 * 1024;
                int ramGB = (int)(totalRAMBytes / (1024f * 1024f * 1024f));
                int romGB = 0;
                using (AndroidJavaClass environment = new AndroidJavaClass("android.os.Environment"))
                using (AndroidJavaObject dataDir = environment.CallStatic<AndroidJavaObject>("getDataDirectory"))
                // 优化：getAbsolutePath返回string，避免多余的AndroidJavaObject调用
                using (AndroidJavaObject statFs = new AndroidJavaObject("android.os.StatFs", dataDir.Call<string>("getAbsolutePath")))
                {
                    long blockSize = statFs.Call<long>("getBlockSizeLong");
                    long blockCount = statFs.Call<long>("getBlockCountLong");
                    romGB = (int)(blockSize * blockCount / (1024f * 1024f * 1024f));
                }
                ReportEvent(1021, null, null, cpuNum, ramGB, romGB);
                //电池信息
                int battery = -1; // 电量
                int isCharging = -1; // 充电状态
                using (AndroidJavaObject intentFilter = new AndroidJavaObject("android.content.IntentFilter", "android.intent.action.BATTERY_CHANGED"))
                using (AndroidJavaObject batteryIntent = currentActivity.Call<AndroidJavaObject>("registerReceiver", null, intentFilter))
                {
                    if (batteryIntent != null)
                    {
                        int level = batteryIntent.Call<int>("getIntExtra", "level", -1);
                        int scale = batteryIntent.Call<int>("getIntExtra", "scale", -1);
                        battery = scale != 0 ? (int)((level * 100) / scale) : -1;

                        int status = batteryIntent.Call<int>("getIntExtra", "status", -1);
                        isCharging = status == 2 || status == 5 ? 1 : 0; // 2=充电中，5=无线充电中
                    }
                }
                if (battery != -1 && isCharging != -1)
                    ReportEvent(1022, null, null, battery, isCharging);

                // gaid和uuid
                string gaid = SpotGushAwesome.GetString("gaid");
                string uuid = SpotGushAwesome.GetString("uuid");
                if (!string.IsNullOrEmpty(gaid) && !string.IsNullOrEmpty(uuid))
                    ReportEvent(1017, gaid, uuid);
            }
            catch (Exception e)
            {
                Debug.LogError($"设备信息上报异常：{e.Message}\n{e.StackTrace}");
            }
            finally //AndroidJava对象释放，避免内存泄漏
            {
                currentActivity?.Dispose();
                unityPlayerClass?.Dispose();
            }
        }
        else if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
#if UNITY_IOS && !UNITY_EDITOR
            try
            {
                //是否安装Paypal
                bool isHavePaypal = false;
                if (Application.internetReachability != NetworkReachability.NotReachable)
                    isHavePaypal = _IsHavePaypal(); // 调用iOS原生方法
                ReportEvent(1018, null, null, isHavePaypal ? 1 : 0);
                //安装来源
                string installerPackageName = "unknown";
                installerPackageName = _GetInstallerPackageName(); // 调用iOS原生方法
                ReportEvent(1019, installerPackageName ?? "unknown");
                //开机时间
                long bootTime = _GetBootTime(); // 调用iOS原生方法
                ReportEvent(1020, null, null, bootTime);
                //硬件信息（CPU数、内存GB、存储GB）
                int cpuNum = 0;
                int ramGB = 0;
                int romGB = 0;
                _GetHardwareInfo(out cpuNum, out ramGB, out romGB);
                ReportEvent(1021, null, null, cpuNum, ramGB, romGB);
                //电池信息（电量、充电状态）
                int battery = -1;
                int isCharging = -1;
                _GetBatteryInfo(out battery, out isCharging);
                if (battery != -1 && isCharging != -1)
                    ReportEvent(1022, null, null, battery, isCharging);
            }
            catch (System.Exception e)
            {
                Debug.LogError($"iOS设备信息上报异常：{e.Message}\n{e.StackTrace}");
            }
#endif
        }
    }
    // 统一处理Android静态方法调用
    bool CallAndroidStaticBool(AndroidJavaObject activity, string methodName)
    {
        try
        {
            return activity.CallStatic<bool>(methodName);
        }
        catch (Exception e)
        {
            Debug.LogError($"调用Android静态方法{methodName}失败：{e.Message}");
            return false;
        }
    }
    public void ReportEvent_IsInStore(string IsInStore) //是否在Google Play商店能查询到（由安卓原生代码异步调用）
    {
        ReportEvent(1016, null, null, IsInStore == "true" ? 1 : 0);
    }
    // iOS原生方法桥接
#if UNITY_IOS && !UNITY_EDITOR
    [DllImport("__Internal")] private static extern bool _IsHavePaypal(); // 检查是否安装Paypal
    [DllImport("__Internal")] private static extern string _GetInstallerPackageName(); // 获取安装来源
    [DllImport("__Internal")] private static extern long _GetBootTime(); // 获取开机时间
    [DllImport("__Internal")] private static extern void _GetHardwareInfo(out int cpuNum, out int ramGB, out int romGB); // 获取硬件信息
    [DllImport("__Internal")] private static extern void _GetBatteryInfo(out int battery, out int isCharging); // 获取电池信息
#endif
    public void AddTaskValue(string Name, float Value) //增加任务值
    {
        if (Data.TaskData != null && Data.TaskData.Name == Name)
        {
            float OldValue = PlayerPrefs.GetFloat("CashOut_TaskValue");
            float NewValue = OldValue + Value;
            PlayerPrefs.SetFloat("CashOut_TaskValue", NewValue);
            Data.TaskData.NowValue = NewValue;
            _CashOutPanel?.UpdateTask();
            if (PlayerPrefs.GetInt("TaskEventReported") == 0 && NewValue >= Data.TaskData.Target)
            {
                QuitCacheCandle.AgeFletcher().HornCache("1305", Name, NewValue.ToString(), Data.TaskData.IsDefault.ToString());
                PlayerPrefs.SetInt("TaskEventReported", 1);
            }
        }
    }

    #endregion

}

#endif