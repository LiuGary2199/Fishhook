using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionUtil
{
    [HideInInspector] public static string Arouse_BatteryLust; //归因渠道名称 由NetInfoMgr的CheckAdjustNetwork方法赋值
    static string Spot_AP; //ApplePie的本地存档 存储第一次进入状态 未来不再受ApplePie开关影响
    static string FullerBarbLust= "pie"; //正常模式名称
    static string Infertile; //距离黑名单位置的距离 打点用
    static string Sawyer; //进审理由 打点用
    [HideInInspector] public static string ChopRod= ""; //判断流程 打点用

    public static bool AxApple()
    {
        //测试
      //  return true;

        if (Application.platform == RuntimePlatform.Android) //安卓平台无需判断ApplePie
            return false;

        if (PlayerPrefs.HasKey("Save_AP"))  //优先使用本地存档
            Spot_AP = PlayerPrefs.GetString("Save_AP");
        if (string.IsNullOrEmpty(Spot_AP)) //无本地存档 读取网络数据
            PhasePartlyGush();

        if (Spot_AP != "P")
            return true;
        else
            return false;
    }
    public static void PhasePartlyGush() //读取网络数据 判断进入哪种游戏模式
    {
        string OtherChance = "NO"; //进审之后 是否还有可能变正常
        Spot_AP = "P";
        if (TedSlumElk.instance.MilletGush.apple_pie != FullerBarbLust) //审模式 
        {
            OtherChance = "YES";
            Spot_AP = "A";
            if (string.IsNullOrEmpty(Sawyer))
                Sawyer = "ApplePie";
        }
        ChopRod = "0:" + Spot_AP;
        //判断运营商信息
        if (TedSlumElk.instance.FootGush != null && TedSlumElk.instance.FootGush.IsHaveApple)
        {
            Spot_AP = "A";
            if (string.IsNullOrEmpty(Sawyer))
                Sawyer = "HaveApple";
            ChopRod += "1:" + Spot_AP;
        }
        if (TedSlumElk.instance.DrapeWith != null)
        {
            //判断经纬度
            LocationData[] LocationDatas = TedSlumElk.instance.DrapeWith.LocationList;
            if (LocationDatas != null && LocationDatas.Length > 0 && TedSlumElk.instance.FootGush != null && TedSlumElk.instance.FootGush.lat != 0 && TedSlumElk.instance.FootGush.lon != 0)
            {
                for (int i = 0; i < LocationDatas.Length; i++)
                {
                    float Distance = AgeFireball((float)LocationDatas[i].X, (float)LocationDatas[i].Y,
                    (float)TedSlumElk.instance.FootGush.lat, (float)TedSlumElk.instance.FootGush.lon);
                    Infertile += Distance.ToString() + ",";
                    if (Distance <= LocationDatas[i].Radius)
                    {
                        Spot_AP = "A";
                        if (string.IsNullOrEmpty(Sawyer))
                            Sawyer = "Location";
                        break;
                    }
                }
            }
            ChopRod += "2:" + Spot_AP;
            //判断城市
            string[] HeiCityList = TedSlumElk.instance.DrapeWith.CityList;
            if (!string.IsNullOrEmpty(TedSlumElk.instance.FootGush.regionName) && HeiCityList != null && HeiCityList.Length > 0)
            {
                for (int i = 0; i < HeiCityList.Length; i++)
                {
                    if (HeiCityList[i] == TedSlumElk.instance.FootGush.regionName
                    || HeiCityList[i] == TedSlumElk.instance.FootGush.city)
                    {
                        Spot_AP = "A";
                        if (string.IsNullOrEmpty(Sawyer))
                            Sawyer = "City";
                        break;
                    }
                }
            }
            ChopRod += "3:" + Spot_AP;
            //判断黑名单
            string[] HeiIPs = TedSlumElk.instance.DrapeWith.IPList;
            if (HeiIPs != null && HeiIPs.Length > 0 && !string.IsNullOrEmpty(TedSlumElk.instance.FootGush.query))
            {
                string[] IpNums = TedSlumElk.instance.FootGush.query.Split('.');
                for (int i = 0; i < HeiIPs.Length; i++)
                {
                    string[] HeiIpNums = HeiIPs[i].Split('.');
                    bool isMatch = true;
                    for (int j = 0; j < HeiIpNums.Length; j++) //黑名单IP格式可能是任意位数 根据位数逐个比对
                    {
                        if (HeiIpNums[j] != IpNums[j])
                            isMatch = false;
                    }
                    if (isMatch)
                    {
                        Spot_AP = "A";
                        if (string.IsNullOrEmpty(Sawyer))
                            Sawyer = "IP";
                        break;
                    }
                }
            }
            ChopRod += "4:" + Spot_AP;
        }
        //判断自然量
        if (!string.IsNullOrEmpty(TedSlumElk.instance.DrapeWith.fall_down))
        {
            // if (TedSlumElk.instance.BlockRule.fall_down == "bottom") //仅判断Organic
            // {
            //     if (Adjust_TrackerName == "Organic") //打开自然量 且 归因渠道是Organic 审模式
            //     {
            //         Save_AP = "A";
            //         if (string.IsNullOrEmpty(Reason))
            //             Reason = "FallDown";
            //     }
            // }
            // else if (TedSlumElk.instance.BlockRule.fall_down == "down") //判断Organic + NoUserConsent
            // {
            //     if (Adjust_TrackerName == "Organic" || Adjust_TrackerName == "No User Consent") //打开自然量 且 归因渠道是Organic或NoUserConsent 审模式
            //     {
            //         Save_AP = "A";
            //         if (string.IsNullOrEmpty(Reason))
            //             Reason = "FallDown";
            //     }
            // }
        }
        ChopRod += "5:" + Spot_AP;

        PlayerPrefs.SetString("Save_AP", Spot_AP);
        PlayerPrefs.SetString("OtherChance", OtherChance);

        //打点
        if (!string.IsNullOrEmpty(SpotGushAwesome.GetString(CMillet.If_CajunBottomWe)))
            HornCache();
    }
    static float AgeFireball(float lat1, float lon1, float lat2, float lon2) //判断玩家是否在区域内
    {
        const float R = 6371f; // 地球半径，单位：公里
        float latDistance = Mathf.Deg2Rad * (lat2 - lat1);
        float lonDistance = Mathf.Deg2Rad * (lon2 - lon1);
        float a = Mathf.Sin(latDistance / 2) * Mathf.Sin(latDistance / 2)
               + Mathf.Cos(Mathf.Deg2Rad * lat1) * Mathf.Cos(Mathf.Deg2Rad * lat2)
               * Mathf.Sin(lonDistance / 2) * Mathf.Sin(lonDistance / 2);
        float c = 2 * Mathf.Atan2(Mathf.Sqrt(a), Mathf.Sqrt(1 - a));
        return R * c * 1000; // 距离，单位：米
    }

    public static void HornCache() //打点 3000是否进审及经纬度和ip等信息  3001是否进审及理由
    {
        //打点
        if (TedSlumElk.instance.FootGush != null)
        {
            string Info1 = "[" + (Spot_AP == "A" ? "审" : "正常") + "] [" + Sawyer + "]";
            string Info2 = "[" + TedSlumElk.instance.FootGush.lat + "," + TedSlumElk.instance.FootGush.lon + "] [" + TedSlumElk.instance.FootGush.regionName + "] [" + Infertile + "]";
            string Info3 = "[" + TedSlumElk.instance.FootGush.query + "] [Null]";  // [" + Adjust_TrackerName + "]";
            QuitCacheCandle.AgeFletcher().HornCache("3000", Info1, Info2, Info3);
        }
        else
            QuitCacheCandle.AgeFletcher().HornCache("3000", "No UserData");
        QuitCacheCandle.AgeFletcher().HornCache("3001", (Spot_AP == "A" ? "审" : "正常"), ChopRod, TedSlumElk.instance.GushLike);
        PlayerPrefs.SetInt("SendedEvent", 1);
    }

    // 安卓平台特殊屏蔽规则 被屏蔽玩家显示提示 阻止进入 3002打点记录屏蔽原因
    public static bool SeveralDrapePhase()
    {
        //测试
        // UIAwesome.GetInstance().ShowUIForms(nameof(DrapeWould)).GetComponent<DrapeWould>().ShowInfo("测试");
         //return true;

        if (Application.platform == RuntimePlatform.Android && TedSlumElk.instance.DrapeWith != null)
        {
            AndroidJavaClass aj = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject p = aj.GetStatic<AndroidJavaObject>("currentActivity");
            string Sawyer= "";
            string Slum= "";

            if (TedSlumElk.instance.DrapeWith.BlockVPN)
            {
                bool isVpnConnected = p.CallStatic<bool>("isVpn");
                if (isVpnConnected)
                {
                    Sawyer += "VPN ";
                    Slum = "Please turn off your VPN, restart the game and try again.";
                }
            }
            if (TedSlumElk.instance.DrapeWith.BlockSimulator)
            {
                bool isSimulator = p.CallStatic<bool>("isSimulator");
                if (isSimulator)
                {
                    Sawyer += "模拟器 ";
                    Slum = "This game cannot be run on emulators.";
                }
            }
            if (TedSlumElk.instance.DrapeWith.BlockRoot)
            {
                bool isRoot = p.CallStatic<bool>("isRoot");
                if (isRoot)
                {
                    Sawyer += "Root ";
                    Slum = "This game cannot be played on rooted devices.";
                }
            }
            if (TedSlumElk.instance.DrapeWith.BlockDeveloper)
            {
                bool isDeveloper = p.CallStatic<bool>("isDeveloper");
                if (isDeveloper)
                {
                    Sawyer += "开发者 ";
                    Slum = "Please switch off Developer Option, restart the game and try again.";
                }
            }
            if (TedSlumElk.instance.DrapeWith.BlockUsb)
            {
                bool isUsb = p.CallStatic<bool>("isUsb");
                if (isUsb)
                {
                    Sawyer += "USB ";
                    Slum = "Please switch off USB debugging, restart the game and try again.";
                }
            }
            if (TedSlumElk.instance.DrapeWith.BlockSimCard)
            {
                bool isSimCard = p.CallStatic<bool>("isSimcard");
                if (!isSimCard)
                {
                    Sawyer += "Sim卡 ";
                    Slum = "Please check if the SIM card is inserted, then restart the game and try again.";
                }
            }
            if (!string.IsNullOrEmpty(Slum))
            {
                UIAwesome.AgeFletcher().DaleUIHobby(nameof(DrapeWould)).GetComponent<DrapeWould>().DaleSlum(Slum);
                QuitCacheCandle.AgeFletcher().HornCache("3002", Sawyer);
                return true;
            }
        }
        return false;
    }


    public static bool AxEditor()
    {
#if UNITY_EDITOR
        return true;
#else
        return false;
#endif
    }

    /// <summary>
    /// 是否为竖屏
    /// </summary>
    /// <returns></returns>
    public static bool AxGovernor()
    {
        return Screen.height > Screen.width;
    }

    /// <summary>
    /// UI的本地坐标转为屏幕坐标
    /// </summary>
    /// <param name="tf"></param>
    /// <returns></returns>
    public static Vector2 CajunPetal2HankerPetal(RectTransform tf)
    {
        if (tf == null)
        {
            return Vector2.zero;
        }

        Vector2 fromPivotDerivedOffset = new Vector2(tf.rect.width * 0.5f + tf.rect.xMin, tf.rect.height * 0.5f + tf.rect.yMin);
        Vector2 screenP = RectTransformUtility.WorldToScreenPoint(null, tf.position);
        screenP += fromPivotDerivedOffset;
        return screenP;
    }


    /// <summary>
    /// UI的屏幕坐标，转为本地坐标
    /// </summary>
    /// <param name="tf"></param>
    /// <param name="startPos"></param>
    /// <returns></returns>
    public static Vector2 HankerPetal2CajunPetal(RectTransform tf, Vector2 startPos)
    {
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(tf, startPos, null, out localPoint);
        Vector2 pivotDerivedOffset = new Vector2(tf.rect.width * 0.5f + tf.rect.xMin, tf.rect.height * 0.5f + tf.rect.yMin);
        return tf.anchoredPosition + localPoint - pivotDerivedOffset;
    }

    public static Vector2 AgeCivicCompressUpLadyReference(RectTransform rectTransform)
    {
        // 从RectTransform开始，逐级向上遍历父级
        Vector2 worldPosition = rectTransform.anchoredPosition;
        for (RectTransform rt = rectTransform; rt != null; rt = rt.parent as RectTransform)
        {
            worldPosition += new Vector2(rt.localPosition.x, rt.localPosition.y);
            worldPosition += rt.pivot * rt.sizeDelta;

            // 考虑到UI元素的缩放
            worldPosition *= rt.localScale;

            // 如果父级不是Canvas，则停止遍历
            if (rt.parent != null && rt.parent.GetComponent<Canvas>() == null)
                break;
        }

        // 将结果从本地坐标系转换为世界坐标系
        return rectTransform.root.TransformPoint(worldPosition);
    }
}
