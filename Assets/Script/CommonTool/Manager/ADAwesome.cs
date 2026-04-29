using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using com.adjust.sdk;
using LitJson;

public class ADAwesome : MonoBehaviour
{
[UnityEngine.Serialization.FormerlySerializedAs("MAX_SDK_KEY")]    public string MAX_SDK_KEY= "";
[UnityEngine.Serialization.FormerlySerializedAs("MAX_REWARD_ID")]    public string MAX_REWARD_ID= "";
[UnityEngine.Serialization.FormerlySerializedAs("MAX_INTER_ID")]    public string MAX_INTER_ID= "";
[UnityEngine.Serialization.FormerlySerializedAs("isTest")]
    public bool ToLean= false;
    public static ADAwesome Fletcher{ get; private set; }
    public bool IsShowingAd { get; private set; } = false;
    private int BrinyFlipper;   // 广告加载失败后，重新加载广告次数
    private bool ToBreakupOf;     // 是否正在播放广告，用于判断切换前后台时是否增加计数

    public int RendWifeDutyImagine{ get; private set; }   // 距离上次广告的时间间隔
    public int Leisure101{ get; private set; }     // 定时插屏(101)计数器
    public int Leisure102{ get; private set; }     // NoThanks插屏(102)计数器
    public int Leisure103{ get; private set; }     // 后台回前台插屏(103)计数器

    private string PoorlyEntwineLust;
    private Action<bool> PoorlyVoltRuinExcite;    // 激励视频回调
    private bool PoorlyImpetus;     // 激励视频是否成功收到奖励
    private string PoorlySmile;     // 激励视频的打点

    private string ManufacturerEntwineLust;
    private int ManufacturerSick;      // 当前播放的插屏类型，101/102/103
    private string ManufacturerSmile;     // 插屏广告的的打点
    public bool PupilDutyRenunciation{ get; private set; } // 定时插屏暂停播放

    private List<Action<ADType>> OrPrinterFolkloric;    // 广告播放完成回调列表，用于其他系统广告计数（例如商店看广告任务）

    private long OxfordshireBladeHusbandry;     // 切后台时的时间戳
    private Ad_CustomData LessonOfWisdomGush; //激励视频自定义数据
    private Ad_CustomData RenunciationOfWisdomGush; //插屏自定义数据

    private void Awake()
    {
        Fletcher = this;
    }

    private void OnEnable()
    {
        PupilDutyRenunciation = false;
        ToBreakupOf = false;
        RendWifeDutyImagine = 999;  // 初始时设置一个较大的值，不阻塞插屏广告
        PoorlyImpetus = false;

        // Android平台将Adjust的adid传给Max；iOS将randomKey传给Max
//#if UNITY_ANDROID
        MaxSdk.SetSdkKey(AgeBureauGush.CleanlyDES(MAX_SDK_KEY));
        // 将adjust id 传给Max
        string adjustId = SpotGushAwesome.GetString(CMillet.If_ArouseAlga);
        if (string.IsNullOrEmpty(adjustId))
        {
            adjustId = Adjust.getAdid();
        }
        if (!string.IsNullOrEmpty(adjustId))
        {
            MaxSdk.SetUserId(adjustId);
            MaxSdk.InitializeSdk();
            SpotGushAwesome.SetString(CMillet.If_ArouseAlga, adjustId);
        }
        else
        {
            StartCoroutine(OreArouseAlga());
        }
/*
#else
        MaxSdk.SetSdkKey(AgeBureauGush.DecryptDES(MAX_SDK_KEY));
        MaxSdk.SetUserId(SpotGushAwesome.GetString(CMillet.sv_LocalUserId));
        MaxSdk.InitializeSdk();
#endif
*/

        MaxSdkCallbacks.OnSdkInitializedEvent += (MaxSdkBase.SdkConfiguration sdkConfiguration) =>
        {
            // 打开调试模式
            //MaxSdk.ShowMediationDebugger();

            GlassmakerCatalyzeVie();
            MaxSdk.SetCreativeDebuggerEnabled(true);

            // 每秒执行一次计数
            InvokeRepeating(nameof(KernelCrisis), 1, 1);
        };
    }

    IEnumerator OreArouseAlga()
    {
        int i = 0;
        while (i < 5)
        {
            yield return new WaitForSeconds(1);
            if (PotionUtil.AxEditor())
            {
                MaxSdk.SetUserId(SpotGushAwesome.GetString(CMillet.If_CajunFootWe));
                MaxSdk.InitializeSdk();
                yield break;
            }
            else
            {
                string adjustId = Adjust.getAdid();
                if (!string.IsNullOrEmpty(adjustId))
                {
                    MaxSdk.SetUserId(adjustId);
                    MaxSdk.InitializeSdk();
                    SpotGushAwesome.SetString(CMillet.If_ArouseAlga, adjustId);
                    yield break;
                }
            }
            i++;
        }
        if (i == 5)
        {
            MaxSdk.SetUserId(SpotGushAwesome.GetString(CMillet.If_CajunFootWe));
            MaxSdk.InitializeSdk();
        }
    }

    public void GlassmakerCatalyzeVie()
    {
        // Attach callback
        MaxSdkCallbacks.Rewarded.OnAdLoadedEvent += OnRewardedAdLoadedEvent;
        MaxSdkCallbacks.Rewarded.OnAdLoadFailedEvent += OnRewardedAdLoadFailedEvent;
        MaxSdkCallbacks.Rewarded.OnAdDisplayedEvent += OnRewardedAdDisplayedEvent;
        MaxSdkCallbacks.Rewarded.OnAdClickedEvent += OnRewardedAdClickedEvent;
        MaxSdkCallbacks.Rewarded.OnAdRevenuePaidEvent += OnRewardedAdRevenuePaidEvent;
        MaxSdkCallbacks.Rewarded.OnAdHiddenEvent += OnRewardedAdHiddenEvent;
        MaxSdkCallbacks.Rewarded.OnAdDisplayFailedEvent += OnRewardedAdFailedToDisplayEvent;
        MaxSdkCallbacks.Rewarded.OnAdReceivedRewardEvent += OnRewardedAdReceivedRewardEvent;

        MaxSdkCallbacks.Interstitial.OnAdLoadedEvent += OnInterstitialLoadedEvent;
        MaxSdkCallbacks.Interstitial.OnAdLoadFailedEvent += OnInterstitialLoadFailedEvent;
        MaxSdkCallbacks.Interstitial.OnAdDisplayedEvent += OnInterstitialDisplayedEvent;
        MaxSdkCallbacks.Interstitial.OnAdClickedEvent += OnInterstitialClickedEvent;
        MaxSdkCallbacks.Interstitial.OnAdRevenuePaidEvent += OnInterstitialRevenuePaidEvent;
        MaxSdkCallbacks.Interstitial.OnAdHiddenEvent += OnInterstitialHiddenEvent;
        MaxSdkCallbacks.Interstitial.OnAdDisplayFailedEvent += OnInterstitialAdFailedToDisplayEvent;

        // Load the first rewarded ad
        FrogCatalyzeOf();

        // Load the first interstitial
        FrogRenunciation();
    }

    private void FrogCatalyzeOf()
    {
        MaxSdk.LoadRewardedAd(MAX_REWARD_ID);
    }

    private void FrogRenunciation()
    {
        MaxSdk.LoadInterstitial(MAX_INTER_ID);
    }

    private void OnRewardedAdLoadedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {
        // Rewarded ad is ready for you to show. MaxSdk.IsRewardedAdReady(adUnitId) now returns 'true'.

        // Reset retry attempt
        BrinyFlipper = 0;
        PoorlyEntwineLust = adInfo.NetworkName;

        LessonOfWisdomGush = new Ad_CustomData();
        LessonOfWisdomGush.user_id = ZJT_Manager.AgeFletcher().GetUserID();
        LessonOfWisdomGush.version = Application.version;
        LessonOfWisdomGush.request_id = ZJT_Manager.AgeFletcher().GetEcpmRequestID();
        LessonOfWisdomGush.vendor = adInfo.NetworkName;
    }

    private void OnRewardedAdLoadFailedEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo)
    {
        // Rewarded ad failed to load 
        // AppLovin recommends that you retry with exponentially higher delays, up to a maximum delay (in this case 64 seconds).

        BrinyFlipper++;
        double retryDelay = Math.Pow(2, Math.Min(6, BrinyFlipper));

        Invoke(nameof(FrogCatalyzeOf), (float)retryDelay);
    }

    private void OnRewardedAdDisplayedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {
#if UNITY_IOS
        ChileElk.AgeFletcher().NoChileOrange = !ChileElk.AgeFletcher().NoChileOrange;
        Time.timeScale = 0;
#endif
    }

    private void OnRewardedAdFailedToDisplayEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo, MaxSdkBase.AdInfo adInfo)
    {
        // Rewarded ad failed to display. AppLovin recommends that you load the next ad.
        FrogCatalyzeOf();
        ToBreakupOf = false;
    }

    private void OnRewardedAdClickedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {

    }

    private void OnRewardedAdHiddenEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {
        // Rewarded ad is hidden. Pre-load the next ad
#if UNITY_IOS
        Time.timeScale = 1;
        ChileElk.AgeFletcher().NoChileOrange = !ChileElk.AgeFletcher().NoChileOrange;
#endif

        ToBreakupOf = false;
        FrogCatalyzeOf();
        if (PoorlyImpetus)
        {
            PoorlyImpetus = false;
            PoorlyVoltRuinExcite?.Invoke(true);

            AmongOfWifeImpetus(ADType.Rewarded);
            QuitCacheCandle.AgeFletcher().HornCache("9007", PoorlySmile);
        }
        else
        {
            PoorlyVoltRuinExcite?.Invoke(false);
        }
    }

    private void OnRewardedAdReceivedRewardEvent(string adUnitId, MaxSdk.Reward reward, MaxSdkBase.AdInfo adInfo)
    {
        // The rewarded ad displayed and the user should receive the reward.
        PoorlyImpetus = true;
    }

    private void OnRewardedAdRevenuePaidEvent(string adUnitId, MaxSdkBase.AdInfo info)
    {
        // Ad revenue paid. Use this callback to track user revenue.
        //从MAX获取收入数据
        var adRevenue = new AdjustAdRevenue(AdjustConfig.AdjustAdRevenueSourceAppLovinMAX);
        adRevenue.setRevenue(info.Revenue, "USD");
        adRevenue.setAdRevenueNetwork(info.NetworkName);
        adRevenue.setAdRevenueUnit(info.AdUnitIdentifier);
        adRevenue.setAdRevenuePlacement(info.Placement);

        //发回收入数据给自己后台
        string countryCodeByMAX = MaxSdk.GetSdkConfiguration().CountryCode; // "US" for the United States, etc - Note: Do not confuse this with currency code which is "USD"
        QuitCacheCandle.AgeFletcher().HornCache("9008", info.Revenue.ToString(), countryCodeByMAX);

        //带广告收入的漏传策略
        //ArouseCapeAwesome.Instance.AddAdCount(countryCodeByMAX, info.Revenue);

        string adjustAdid = ArouseCapeAwesome.Instance.AgeArouseAlga();
        //发回收入数据给Adjust
        if (!string.IsNullOrEmpty(adjustAdid))
        {
            Adjust.trackAdRevenue(adRevenue);
            UnityEngine.Debug.Log("Max to Adjust (rewarded), adUnitId:" + adUnitId + ", revenue:" + info.Revenue + ", network:" + info.NetworkName + ", unit:" + info.AdUnitIdentifier + ", placement:" + info.Placement);
        }

        // 发回收入数据给Mintegral
        if (!string.IsNullOrEmpty(adjustAdid))
        {
#if UNITY_ANDROID || UNITY_IOS
            MBridgeRevenueParamsEntity mBridgeRevenueParamsEntity = new MBridgeRevenueParamsEntity(MBridgeRevenueParamsEntity.ATTRIBUTION_PLATFORM_ADJUST, adjustAdid);
            ///MaxSdkBase.AdInfo类型的adInfo
            mBridgeRevenueParamsEntity.SetMaxAdInfo(info);
            MBridgeRevenueManager.Track(mBridgeRevenueParamsEntity);
            UnityEngine.Debug.Log(nameof(MBridgeRevenueManager) + "~Rewarded revenue:" + info.Revenue);
#endif
        }
    }

    private void OnInterstitialLoadedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {
        // Interstitial ad is ready for you to show. MaxSdk.IsInterstitialReady(adUnitId) now returns 'true'

        // Reset retry attempt
        BrinyFlipper = 0;
        ManufacturerEntwineLust = adInfo.NetworkName;

        RenunciationOfWisdomGush = new Ad_CustomData();
        RenunciationOfWisdomGush.user_id = ZJT_Manager.AgeFletcher().GetUserID();
        RenunciationOfWisdomGush.version = Application.version;
        RenunciationOfWisdomGush.request_id = ZJT_Manager.AgeFletcher().GetEcpmRequestID();
        RenunciationOfWisdomGush.vendor = adInfo.NetworkName;
    }

    private void OnInterstitialLoadFailedEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo)
    {
        // Interstitial ad failed to load 
        // AppLovin recommends that you retry with exponentially higher delays, up to a maximum delay (in this case 64 seconds)

        BrinyFlipper++;
        double retryDelay = Math.Pow(2, Math.Min(6, BrinyFlipper));

        Invoke(nameof(FrogRenunciation), (float)retryDelay);
    }

    private void OnInterstitialDisplayedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {
#if UNITY_IOS
        ChileElk.AgeFletcher().NoChileOrange = !ChileElk.AgeFletcher().NoChileOrange;
        Time.timeScale = 0;
#endif
    }

    private void OnInterstitialAdFailedToDisplayEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo, MaxSdkBase.AdInfo adInfo)
    {
        // Interstitial ad failed to display. AppLovin recommends that you load the next ad.
        FrogRenunciation();
        ToBreakupOf = false;
    }

    private void OnInterstitialClickedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo) { }

    private void OnInterstitialRevenuePaidEvent(string adUnitId, MaxSdkBase.AdInfo info)
    {
        //从MAX获取收入数据
        var adRevenue = new AdjustAdRevenue(AdjustConfig.AdjustAdRevenueSourceAppLovinMAX);
        adRevenue.setRevenue(info.Revenue, "USD");
        adRevenue.setAdRevenueNetwork(info.NetworkName);
        adRevenue.setAdRevenueUnit(info.AdUnitIdentifier);
        adRevenue.setAdRevenuePlacement(info.Placement);

        //发回收入数据给自己后台
        string countryCodeByMAX = MaxSdk.GetSdkConfiguration().CountryCode; // "US" for the United States, etc - Note: Do not confuse this with currency code which is "USD"
        QuitCacheCandle.AgeFletcher().HornCache("9108", info.Revenue.ToString(), countryCodeByMAX);

        //带广告收入的漏传策略
        //ArouseCapeAwesome.Instance.AddAdCount(countryCodeByMAX, info.Revenue);

        //发回收入数据给Adjust
        if (!string.IsNullOrEmpty(ArouseCapeAwesome.Instance.AgeArouseAlga()))
        {
            Adjust.trackAdRevenue(adRevenue);
            UnityEngine.Debug.Log("Max to Adjust (interstitial), adUnitId:" + adUnitId + ", revenue:" + info.Revenue + ", network:" + info.NetworkName + ", unit:" + info.AdUnitIdentifier + ", placement:" + info.Placement);
        }

        // 发回收入数据给Mintegral
        string adjustAdid = ArouseCapeAwesome.Instance.AgeArouseAlga();
        if (!string.IsNullOrEmpty(adjustAdid))
        {
#if UNITY_ANDROID || UNITY_IOS
            MBridgeRevenueParamsEntity mBridgeRevenueParamsEntity = new MBridgeRevenueParamsEntity(MBridgeRevenueParamsEntity.ATTRIBUTION_PLATFORM_ADJUST, adjustAdid);
            ///MaxSdkBase.AdInfo类型的adInfo
            mBridgeRevenueParamsEntity.SetMaxAdInfo(info);
            MBridgeRevenueManager.Track(mBridgeRevenueParamsEntity);
            UnityEngine.Debug.Log(nameof(MBridgeRevenueManager) + "~Interstitial revenue:" + info.Revenue);
#endif
        }
    }

    private void OnInterstitialHiddenEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {
        // Interstitial ad is hidden. Pre-load the next ad.
#if UNITY_IOS
        Time.timeScale = 1;
        ChileElk.AgeFletcher().NoChileOrange = !ChileElk.AgeFletcher().NoChileOrange;
#endif
        FrogRenunciation();

        AmongOfWifeImpetus(ADType.Interstitial);
        QuitCacheCandle.AgeFletcher().HornCache("9107", ManufacturerSmile);
    }


    /// <summary>
    /// 播放激励视频广告
    /// </summary>
    /// <param name="callBack"></param>
    /// <param name="index"></param>
    public void WoadLessonMount(Action<bool> callBack, string index)
    {
        if (ToLean)
        {
            callBack(true);
            AmongOfWifeImpetus(ADType.Rewarded);
            return;
        }

        bool rewardVideoReady = MaxSdk.IsRewardedAdReady(MAX_REWARD_ID);
        PoorlyVoltRuinExcite = callBack;
        if (rewardVideoReady)
        {
            LoveManual.DewLoveCareless(LoveManual.LoveWe_3, 1);
            // 打点
            PoorlySmile = index;
            QuitCacheCandle.AgeFletcher().HornCache("9002", index);
            ToBreakupOf = true;
            PoorlyImpetus = false;
            string placement = index + "_" + PoorlyEntwineLust;
            LessonOfWisdomGush.placement_id = placement;
            MaxSdk.ShowRewardedAd(MAX_REWARD_ID, placement, JsonMapper.ToJson(LessonOfWisdomGush));
        }
        else
        {
            BrinyAwesome.AgeFletcher().DaleBriny("No ads right now, please try it later.");
            PoorlyVoltRuinExcite(false);
        }
    }

    /// <summary>
    /// 播放插屏广告
    /// </summary>
    /// <param name="index"></param>
    public void WoadRenunciationOf(int index)
    {
        if (index == 101 || index == 102 || index == 103)
        {
            UnityEngine.Debug.LogError("广告点位不允许为101、102、103");
            return;
        }

        WoadRenunciation(index);
    }

    /// <summary>
    /// 播放插屏广告
    /// </summary>
    /// <param name="index">101/102/103</param>
    /// <param name="customIndex">用户自定义点位</param>
    private void WoadRenunciation(int index, int customIndex = 0)
    {
        ManufacturerSick = index;

        if (ToBreakupOf)
        {
            return;
        }

        //这个参数很少有游戏会用 需要的时候自己再打开
        // 当用户船等级 < trial_MaxNum 时，不弹插屏广告
        int currentShipLevel = ClanGushAwesome.AgeFletcher() != null ? ClanGushAwesome.AgeFletcher().PermGripe : 0;
        int trial_MaxNum = int.Parse(TedSlumElk.instance.MilletGush.trial_MaxNum);
        if (currentShipLevel < trial_MaxNum)
        {
            return;
        }

        // 时间间隔低于阈值，不播放广告
        if (RendWifeDutyImagine < int.Parse(TedSlumElk.instance.MilletGush.inter_freq))
        {
            return;
        }

        if (ToLean)
        {
            AmongOfWifeImpetus(ADType.Interstitial);
            return;
        }

        bool interstitialVideoReady = MaxSdk.IsInterstitialReady(MAX_INTER_ID);
        if (interstitialVideoReady)
        {
            ToBreakupOf = true;
            // 打点
            string point = index.ToString();
            if (customIndex > 0)
            {
                point += customIndex.ToString().PadLeft(2, '0');
            }
            ManufacturerSmile = point;
            QuitCacheCandle.AgeFletcher().HornCache("9102", point);
            string placement = point + "_" + ManufacturerEntwineLust;
            RenunciationOfWisdomGush.placement_id = placement;
            MaxSdk.ShowInterstitial(MAX_INTER_ID, placement, JsonMapper.ToJson(RenunciationOfWisdomGush));
        }
    }

    /// <summary>
    /// 每秒更新一次计数器 - 101计数器 和 时间间隔计数器
    /// </summary>
    private void KernelCrisis()
    {
        RendWifeDutyImagine++;

        int relax_interval = int.Parse(TedSlumElk.instance.MilletGush.relax_interval);
        // 计时器阈值设置为0或负数时，关闭广告101；
        // 播放广告期间不计数；
        if (relax_interval <= 0 || ToBreakupOf)
        {
            return;
        }
        else
        {
            Leisure101++;
            if (Leisure101 >= relax_interval && !PupilDutyRenunciation)
            {
                int currentShipLevel = ClanGushAwesome.AgeFletcher() != null ? ClanGushAwesome.AgeFletcher().PermGripe : 0;
                int trial_MaxNum = int.Parse(TedSlumElk.instance.MilletGush.trial_MaxNum);
                if (currentShipLevel < trial_MaxNum)
                {
                    return;
                }
                if (ToBreakupOf)
                {
                    return;
                }
                // 时间间隔低于阈值，不播放广告
                if (RendWifeDutyImagine < int.Parse(TedSlumElk.instance.MilletGush.inter_freq))
                {
                    return;
                }
                bool isGamePanelTop = UIAwesome.AgeFletcher().AxMoteWouldFew();
                bool isFerverMode = ClanAwesome.Instance != null && ClanAwesome.Instance.ClanSick == GameType.FerverTime;

                //print("游戏面板是否在最上层： " + isGamePanelTop);
                if (!isGamePanelTop || isFerverMode)
                {
                    Leisure101 -= 5;
                   return;
               }
                //计时插屏 弹出提示
                bool interstitialVideoReady = MaxSdk.IsInterstitialReady(MAX_INTER_ID);
                if (!PotionUtil.AxApple() && interstitialVideoReady)
                {
                    Leisure101 = 0;

                    UIAwesome.AgeFletcher().DaleUIHobby(nameof(RenunciationOfRimWould));
                    DutyAwesome.AgeFletcher().Nomad_RageDuty(3, () =>
                    {
                        UIAwesome.AgeFletcher().BloodSoSolelyUIHobby(nameof(RenunciationOfRimWould));
                        WoadRenunciation(101);
                    });
                    return;
                }

                WoadRenunciation(101);
            }
        }
    }

    /// <summary>
    /// NoThanks插屏 - 102
    /// </summary>
    public void OxCrunchDewTruck(int customIndex = 0)
    {
        // 用户行为累计次数计数器阈值设置为0或负数时，关闭广告102
        int nextlevel_interval = int.Parse(TedSlumElk.instance.MilletGush.nextlevel_interval);
        if (nextlevel_interval <= 0)
        {
            return;
        }
        else
        {
            Leisure102 = SpotGushAwesome.GetInt("NoThanksCount") + 1;
            SpotGushAwesome.SetInt("NoThanksCount", Leisure102);
            if (Leisure102 >= nextlevel_interval)
            {
                WoadRenunciation(102, customIndex);
            }
        }
    }

    /// <summary>
    /// 前后台切换计数器 - 103
    /// </summary>
    /// <param name="pause"></param>
    private void OnApplicationPause(bool pause)
    {
        if (!pause)
        {
            // 切回前台
            if (!ToBreakupOf)
            {
                // 前后台切换时，播放间隔计数器需要累加切到后台的时间
                if (OxfordshireBladeHusbandry > 0)
                {
                    RendWifeDutyImagine += (int)(WildRide.Reliant() - OxfordshireBladeHusbandry);
                    OxfordshireBladeHusbandry = 0;
                }
                // 后台切回前台累计次数，后台配置为0或负数，关闭该广告
                int inter_b2f_count = int.Parse(TedSlumElk.instance.MilletGush.inter_b2f_count);
                if (inter_b2f_count <= 0)
                {
                    return;
                }
                else
                {
                    Leisure103++;
                    if (Leisure103 >= inter_b2f_count)
                    {
                        WoadRenunciation(103);
                    }
                }
            }
        }
        else
        {
            // 切到后台
            OxfordshireBladeHusbandry = WildRide.Reliant();
        }
    }

    /// <summary>
    /// 暂停定时插屏播放 - 101
    /// </summary>
    public void BladeDutyRenunciation()
    {
        PupilDutyRenunciation = true;
    }

    /// <summary>
    /// 恢复定时插屏播放 - 101
    /// </summary>
    public void SecureDutyRenunciation()
    {
        PupilDutyRenunciation = false;
    }

    /// <summary>
    /// 更新游戏的TrialNum
    /// </summary>
    /// <param name="num"></param>
    public void JobberTrialCud(int num)
    {
        SpotGushAwesome.SetInt(CMillet.If_Or_Spiny_Due, num);
    }

    /// <summary>
    /// 注册看广告的回调事件
    /// </summary>
    /// <param name="callback"></param>
    public void TreasuryWifeExercise(Action<ADType> callback)
    {
        if (OrPrinterFolkloric == null)
        {
            OrPrinterFolkloric = new List<Action<ADType>>();
        }

        if (!OrPrinterFolkloric.Contains(callback))
        {
            OrPrinterFolkloric.Add(callback);
        }
    }

    /// <summary>
    /// 广告播放成功后，执行看广告回调事件
    /// </summary>
    private void AmongOfWifeImpetus(ADType adType)
    {
        ToBreakupOf = false;
        // 播放间隔计数器清零
        RendWifeDutyImagine = 0;
        // 插屏计数器清零
        if (adType == ADType.Interstitial)
        {
            // 计数器清零
            if (ManufacturerSick == 101)
            {
                Leisure101 = 0;
            }
            else if (ManufacturerSick == 102)
            {
                Leisure102 = 0;
                SpotGushAwesome.SetInt("NoThanksCount", 0);
            }
            else if (ManufacturerSick == 103)
            {
                Leisure103 = 0;
            }
        }

        // 看广告总数+1
        SpotGushAwesome.SetInt(CMillet.If_Worry_Or_Due + adType.ToString(), SpotGushAwesome.GetInt(CMillet.If_Worry_Or_Due + adType.ToString()) + 1);
        // 提现任务 
        if (adType == ADType.Rewarded)
            ZJT_Manager.AgeFletcher().AddTaskValue("Ad",1);

        // 回调
        if (OrPrinterFolkloric != null && OrPrinterFolkloric.Count > 0)
        {
            foreach (Action<ADType> callback in OrPrinterFolkloric)
            {
                callback?.Invoke(adType);
            }
        }
    }

    /// <summary>
    /// 获取总的看广告次数
    /// </summary>
    /// <returns></returns>
    public int AgeRigidOfCud(ADType adType)
    {
        return SpotGushAwesome.GetInt(CMillet.If_Worry_Or_Due + adType.ToString());
    }
}

public enum ADType { Interstitial, Rewarded }

[System.Serializable]
public class Ad_CustomData //广告自定义数据
{
    public string user_id; //用户id
    public string version; //版本号
    public string request_id; //请求id
    public string vendor; //渠道
    public string placement_id; //广告位id
}