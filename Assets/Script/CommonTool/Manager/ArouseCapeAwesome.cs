using System;
using System.Collections;
using com.adjust.sdk;
using LitJson;
using UnityEngine;
using Random = UnityEngine.Random;

public class ArouseCapeAwesome : MonoBehaviour
{
    public static ArouseCapeAwesome Instance;
[UnityEngine.Serialization.FormerlySerializedAs("adjustID")]
    public string ScriptID;     // 由遇总的打包工具统一修改，无需手动配置

    //用户adjust 状态KEY
    private string If_ADNearCapeSick= "If_ADNearCapeSick";

    //adjust 时间戳
    private string If_ADNearDuty= "sv_ADJustTime";

    //adjust行为计数器
    public int _ErosionTruck{ get; private set; }

    public double _ErosionScratch{ get; private set; }

    double ScriptCapeOfScratch= 0;


    private void Awake()
    {
        Instance = this;
        SpotGushAwesome.SetString(If_ADNearDuty, WildRide.Reliant().ToString());

#if UNITY_IOS
        SpotGushAwesome.SetString(If_ADNearCapeSick, AdjustStatus.OpenAsAct.ToString());
        ArouseCape();
#endif
    }

    private void Start()
    {
        _ErosionTruck = 0;
    }


    void ArouseCape()
    {
#if UNITY_EDITOR
        return;
#endif
        AdjustConfig adjustConfig = new AdjustConfig(ScriptID, AdjustEnvironment.Production, false);
        adjustConfig.setLogLevel(AdjustLogLevel.Verbose);
        adjustConfig.setSendInBackground(false);
        adjustConfig.setEventBufferingEnabled(false);
        adjustConfig.setLaunchDeferredDeeplink(true);
        Adjust.start(adjustConfig);

        StartCoroutine(SpotArouseAlga());
    }

    private IEnumerator SpotArouseAlga()
    {
        while (true)
        {
            string adjustAdid = Adjust.getAdid();
            if (string.IsNullOrEmpty(adjustAdid))
            {
                yield return new WaitForSeconds(5);
            }
            else
            {
                SpotGushAwesome.SetString(CMillet.If_ArouseAlga, adjustAdid);
                TedSlumElk.instance.HornArouseAlga();
                ZJT_Manager.AgeFletcher().ReportAdjustID();
                yield break;
            }
        }
    }

    public string AgeArouseAlga()
    {
        return SpotGushAwesome.GetString(CMillet.If_ArouseAlga);
    }

    /// <summary>
    /// 获取adjust初始化状态
    /// </summary>
    /// <returns></returns>
    public string AgeArouseImpose()
    {
        return SpotGushAwesome.GetString(If_ADNearCapeSick);
    }

    /*
     *  API
     *  Adjust 初始化
     */
    public void CapeArouseGush(bool isOldUser = false)
    {
        #if UNITY_IOS
            return;
        #endif
        // 如果后台配置的adjust_init_act_position <= 0，直接初始化
        if (string.IsNullOrEmpty(TedSlumElk.instance.MilletGush.adjust_init_act_position) || int.Parse(TedSlumElk.instance.MilletGush.adjust_init_act_position) <= 0)
        {
            SpotGushAwesome.SetString(If_ADNearCapeSick, AdjustStatus.OpenAsAct.ToString());
        }
        print(" user init adjust by status :" + SpotGushAwesome.GetString(If_ADNearCapeSick));
        //用户二次登录 根据标签初始化
        if (SpotGushAwesome.GetString(If_ADNearCapeSick) == AdjustStatus.OldUser.ToString() || SpotGushAwesome.GetString(If_ADNearCapeSick) == AdjustStatus.OpenAsAct.ToString())
        {
            print("second login  and  init adjust");
            ArouseCape();
        }
    }



    /*
     * API
     *  记录行为累计次数
     *  @param2 打点参数
     */
    public void DewJetTruck(string param2 = "")
    {
#if UNITY_IOS
            return;
#endif
        if (SpotGushAwesome.GetString(If_ADNearCapeSick) != "") return;
        _ErosionTruck++;
        print(" add up to :" + _ErosionTruck);
        if (string.IsNullOrEmpty(TedSlumElk.instance.MilletGush.adjust_init_act_position) || _ErosionTruck == int.Parse(TedSlumElk.instance.MilletGush.adjust_init_act_position))
        {
            FrogArouseToJet(param2);
        }
    }

    /// <summary>
    /// 记录广告行为累计次数，带广告收入
    /// </summary>
    /// <param name="countryCode"></param>
    /// <param name="revenue"></param>
    public void DewOfTruck(string countryCode, double revenue)
    {
#if UNITY_IOS
            return;
#endif
        //if (SpotGushAwesome.GetString(If_ADNearCapeSick) != "") return;

        _ErosionTruck++;
        _ErosionScratch += revenue;
        print(" Ads count: " + _ErosionTruck + ", Revenue sum: " + _ErosionScratch);

        //如果后台有adjust_init_adrevenue数据 且 能找到匹配的countryCode，初始化adjustInitAdRevenue
        if (!string.IsNullOrEmpty(TedSlumElk.instance.MilletGush.adjust_init_adrevenue))
        {
            JsonData jd = JsonMapper.ToObject(TedSlumElk.instance.MilletGush.adjust_init_adrevenue);
            if (jd.ContainsKey(countryCode))
            {
                ScriptCapeOfScratch = double.Parse(jd[countryCode].ToString(), new System.Globalization.CultureInfo("en-US"));
            }
        }

        if (
            string.IsNullOrEmpty(TedSlumElk.instance.MilletGush.adjust_init_act_position)                   //后台没有配置限制条件，直接走LoadAdjust
            || (_ErosionTruck == int.Parse(TedSlumElk.instance.MilletGush.adjust_init_act_position)         //累计广告次数满足adjust_init_act_position条件，且累计广告收入满足adjust_init_adrevenue条件，走LoadAdjust
                && _ErosionScratch >= ScriptCapeOfScratch)
        )
        {
            FrogArouseToJet();
        }
    }

    /*
     * API
     * 根据行为 初始化 adjust
     *  @param2 打点参数 
     */
    public void FrogArouseToJet(string param2 = "")
    {
        if (SpotGushAwesome.GetString(If_ADNearCapeSick) != "") return;

        // 根据比例分流   adjust_init_rate_act  行为比例
        if (string.IsNullOrEmpty(TedSlumElk.instance.MilletGush.adjust_init_rate_act) || int.Parse(TedSlumElk.instance.MilletGush.adjust_init_rate_act) > Random.Range(0, 100))
        {
            print("user finish  act  and  init adjust");
            SpotGushAwesome.SetString(If_ADNearCapeSick, AdjustStatus.OpenAsAct.ToString());
            ArouseCape();

            // 上报点位 新用户达成 且 初始化
            QuitCacheCandle.AgeFletcher().HornCache("1091", AgeArouseDuty(), param2);
        }
        else
        {
            print("user finish  act  and  not init adjust");
            SpotGushAwesome.SetString(If_ADNearCapeSick, AdjustStatus.CloseAsAct.ToString());
            // 上报点位 新用户达成 且  不初始化
            QuitCacheCandle.AgeFletcher().HornCache("1092", AgeArouseDuty(), param2);
        }
    }

    
    /*
     * API
     *  重置当前次数
     */
    public void ChartJetTruck()
    {
        print("clear current ");
        _ErosionTruck = 0;
    }


    // 获取启动时间
    private string AgeArouseDuty()
    {
        return WildRide.Reliant() - long.Parse(SpotGushAwesome.GetString(If_ADNearDuty)) + "";
    }
}


/*
 *@param
 *  OldUser     老用户
 *  OpenAsAct   行为触发且初始化
 *  CloseAsAct  行为触发不初始化
 */
public enum AdjustStatus
{
    OldUser,
    OpenAsAct,
    CloseAsAct
}