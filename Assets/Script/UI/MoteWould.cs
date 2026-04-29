using DG.Tweening;
using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;
using static MaxSdkBase;

public class MoteWould : ShedUIHobby
{
    public static MoteWould Instance;
    [HideInInspector] [UnityEngine.Serialization.FormerlySerializedAs("_GuidePanel")]public CruelWould _CruelWould;
    [HideInInspector] [UnityEngine.Serialization.FormerlySerializedAs("GuideIndex")]public int CruelSmile;
[UnityEngine.Serialization.FormerlySerializedAs("CashOutBtn")]    public Transform SeedMobLad;
[UnityEngine.Serialization.FormerlySerializedAs("ClickBtn")]    public Transform TheseLad;
[UnityEngine.Serialization.FormerlySerializedAs("ferverslider")]    public Transform Proficiently;
[UnityEngine.Serialization.FormerlySerializedAs("AutoBtn")]
    public Transform PikeLad;
[UnityEngine.Serialization.FormerlySerializedAs("m_SettingBtn")]

    public Button m_CentralLad;
[UnityEngine.Serialization.FormerlySerializedAs("m_DailyBtn")]    public Button m_MaserLad;
[UnityEngine.Serialization.FormerlySerializedAs("m_TaskBtn")]    public Button m_LoveLad;
[UnityEngine.Serialization.FormerlySerializedAs("m_CashTrans")]
    public RectTransform m_SeedVigor;

    [Header("钩子系统切换")]
    [Tooltip("true=新版穿刺钩子(预制体)，false=旧版收回钩子")]
[UnityEngine.Serialization.FormerlySerializedAs("m_UseNewHookSystem")]    public bool m_HueEarDownBureau= false;
    [Tooltip("旧版系统根节点（含 UIImageCrash + 旧 HitArea）")]
[UnityEngine.Serialization.FormerlySerializedAs("m_OldSwingRoot")]    public GameObject m_BoyCrashWest;
    [Tooltip("新版系统根节点（含 UIToughCrashEar + 新 HitArea）")]
[UnityEngine.Serialization.FormerlySerializedAs("m_NewSwingRoot")]    public GameObject m_EarCrashWest;
[UnityEngine.Serialization.FormerlySerializedAs("m_UIImageSwing")]
    public UIImageCrash m_UIImageCrash;
[UnityEngine.Serialization.FormerlySerializedAs("m_UIImageSwingNew")]    public UIToughCrashEar m_UIToughCrashEar;
[UnityEngine.Serialization.FormerlySerializedAs("m_DiamondImage")]    public Image m_LinkageTough;
[UnityEngine.Serialization.FormerlySerializedAs("m_GoldImage")]
    public Image m_TalkTough;
    [Tooltip("鱼金币动画专用图标（可选，空则回退 m_GoldImage；需要拖带 Image 组件的物体，而不是 Sprite 资源）")]
[UnityEngine.Serialization.FormerlySerializedAs("m_FishGoldImage")]    public Image m_EaseTalkTough;
    [Tooltip("鱼金币飞币的 Sprite 覆盖（可直接拖 Sprite 资源；空则不覆盖）")]
[UnityEngine.Serialization.FormerlySerializedAs("m_FishGoldCoinSprite")]    public Sprite m_EaseTalkViceMidway;
    [Tooltip("鱼钻石飞入动画用的 LetStar 模板（可选，空则用 m_DiamondImage 所在物体）")]
[UnityEngine.Serialization.FormerlySerializedAs("m_FishDiamondImage")]    public Image m_EaseLinkageTough;
[UnityEngine.Serialization.FormerlySerializedAs("m_GoldText")]    public TextMeshProUGUI m_TalkWelt;
[UnityEngine.Serialization.FormerlySerializedAs("m_HPImage")]
    public Image m_HPTough;
[UnityEngine.Serialization.FormerlySerializedAs("m_HPText")]    public TextMeshProUGUI m_HPWelt;
[UnityEngine.Serialization.FormerlySerializedAs("energyTextTMP")]    public TMP_Text IndoorWeltTMP; // TextMeshPro（二选一）
[UnityEngine.Serialization.FormerlySerializedAs("countdownTextTMP")]    public TMP_Text ZoologistWeltTMP; // 倒计时文本（TMP）
[UnityEngine.Serialization.FormerlySerializedAs("shipLevelView")]    public MotePermGripeHurt SortGripeHurt;
[UnityEngine.Serialization.FormerlySerializedAs("ferverTimeView")]    public MoteEntireDutyHurt DecadeDutyHurt;
[UnityEngine.Serialization.FormerlySerializedAs("fishDeathMoneyBurstFxView")]    public EaseAloftJuicyVideoOnMold VaseAloftJuicyVideoOnHurt;
[UnityEngine.Serialization.FormerlySerializedAs("homeRotWheel")]    public MoteSexStump MoltSexStump;
    [Header("鱼群生成控制")]
[UnityEngine.Serialization.FormerlySerializedAs("m_FishSwimSystem")]    public UIEaseBergBureau m_EaseBergBureau;
    [Tooltip("进入 MoteWould 时是否重置场上鱼并重启开场鱼潮流程")]
[UnityEngine.Serialization.FormerlySerializedAs("m_ResetFishOnHomeDisplay")]    public bool m_ChartEaseToMoteAcreage= true;

    [Header("LittleGame 调度器")]
    [Tooltip("把“定时随机小游戏/Boss鱼”逻辑从 MoteWould 抽离出来的组件")]
[UnityEngine.Serialization.FormerlySerializedAs("m_LittleGameScheduler")]    public MoteWouldSewageClanSituation m_SewageClanSituation;
    [Header("Ferver 结算UI")]
[UnityEngine.Serialization.FormerlySerializedAs("FeverMode_GroundMoney")]    public Transform RebelBarb_SludgeJuicy; //疯狂模式 地面堆积的钱
    List<Transform> RebelBarb_SludgeJuicyPloy= new List<Transform>(); //疯狂模式 地面堆积的钱
[UnityEngine.Serialization.FormerlySerializedAs("FX_Cash")]
    public GameObject FX_Seed; //金币收集父级
[UnityEngine.Serialization.FormerlySerializedAs("FX_Collect")]    public GameObject FX_Mermaid;
    [Tooltip("钻石飞入 HUD 时落地粒子（可选；空则复用 FX_Collect）")]
[UnityEngine.Serialization.FormerlySerializedAs("FX_DiamondCollect")]    public GameObject FX_LinkageMermaid;

    private Action<Transform, int> onHeGratifyDewJuicyDeviate;
    private Action<Transform, int> NoEaseMistSeedLessonDeviate;
    private Action<Transform, int> NoEaseMistLinkageLessonDeviate;
    private Action<Transform, Transform> NoKierEaseWorthGovernCruelDeviate;
    private Action NoEntireDutyDiscWalkerDeviate;
    private Action NoPermErectusSlimePromoteDeviate;
    private int m_EntireBicycleJuicy;
    private bool m_AxEntireBarb;
    private float m_EntireSludgeJuicyDiscCollapse;
[UnityEngine.Serialization.FormerlySerializedAs("m_UIShake")]    public UIPluto m_UIPluto;
    private bool m_EaseAloftOnAstute;
    private Coroutine m_ControlSexStumpReclaimEdifice;

    protected override void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        if (PotionUtil.AxApple())
        {
            m_SeedVigor.gameObject.SetActive(false);
        }

        m_CentralLad.onClick.AddListener(() =>
        {
            ChileElk.AgeFletcher().WifeMisery(ChileSick.UIMusic.Sound_UIButton);
            MarkCentralWould();
        });
        m_MaserLad.onClick.AddListener(() =>
        {
            ChileElk.AgeFletcher().WifeMisery(ChileSick.UIMusic.Sound_UIButton);
            MarkUIJazz(nameof(FoldItWould));
        });
        m_LoveLad.onClick.AddListener(() =>
        {
            ChileElk.AgeFletcher().WifeMisery(ChileSick.UIMusic.Sound_UIButton);
            MarkUIJazz(nameof(LoveWould));
        });
        CruelSmile = 0;
        onHeGratifyDewJuicyDeviate = SeduceHeGratifyDewJuicyLesson;
        NoEaseMistSeedLessonDeviate = SeduceEaseMistSeedLesson;
        NoEaseMistLinkageLessonDeviate = SeduceEaseMistLinkageLesson;
        BarelyIon.ToDewJuicy += onHeGratifyDewJuicyDeviate;
        BarelyIon.ToEaseDewJuicy += NoEaseMistSeedLessonDeviate;
        BarelyIon.ToEaseDewLinkage += NoEaseMistLinkageLessonDeviate;
        BarelyIon.ToSchoolPursuit += OnEnergyUpdate;
        BarelyIon.ToClanSickPursuit += OnGameTypeChanged;
        NoEntireDutyDiscWalkerDeviate = OnFerverTimeAnimFinish;
        BarelyIon.ToEntireDutyDiscWalker += NoEntireDutyDiscWalkerDeviate;
        NoPermErectusSlimePromoteDeviate = OnShipUpgradePopupRequest;
        BarelyIon.ToPermErectusSlimePromote += NoPermErectusSlimePromoteDeviate;
        NoKierEaseWorthGovernCruelDeviate = OnBossFishCrossCenterGuideRequest;
        BarelyIon.ToKierEaseWorthGovernCruelPromote += NoKierEaseWorthGovernCruelDeviate;
        SortGripeHurt?.Glassmaker();
        DecadeDutyHurt?.Glassmaker();
        if (VaseAloftJuicyVideoOnHurt == null)
        {
            VaseAloftJuicyVideoOnHurt = FindFirstObjectByType<EaseAloftJuicyVideoOnMold>();
        }
        // 爆钱特效改为懒初始化，避免首屏同帧大量 Instantiate。
        m_EaseAloftOnAstute = false;
        GlassmakerEntireLessonUI();
        GlassmakerEntireSludgeJuicy();
        bool IsGuideCashOut = SpotGushAwesome.GetBool("IsGuideCashOut");
        if (!PotionUtil.AxApple() && !IsGuideCashOut)
        {
            Cruel_1();
        }
        //  SpotGushAwesome.SetBool(CMillet.sv_guide_boss_center_done, false);

    }
    private void Cruel_1()
    {
        DutyAwesome.AgeFletcher().Nomad(1, () =>
        {
            CruelSmile = 1;
            _CruelWould = MarkUIJazz(nameof(CruelWould)).GetComponent<CruelWould>();
            _CruelWould.DaleIron(SeedMobLad);
            _CruelWould.DaleSlum("Help you understand how to withdraw cash", 220);
            _CruelWould.DaleCore(new Vector2[] { SeedMobLad.position });
            SpotGushAwesome.SetBool("IsGuideCashOut", true);
        });
    }
    public void Cruel_2()
    {
        CruelSmile = 2;
        _CruelWould.Hone(true);
        DutyAwesome.AgeFletcher().Nomad(.5f, () =>
        {
            //nsform Target = transform.Find("引导区域-点击猪");
            _CruelWould.DaleIron(TheseLad,1,0);
            _CruelWould.DaleSlum("Click to fire the harpoon", -450);
            _CruelWould.DaleCore(new Vector2[] { TheseLad.position });
        });
    }

    public void Cruel_3()
    {
        CruelSmile = 3;
        _CruelWould.Hone(true);
        DutyAwesome.AgeFletcher().Nomad(1f, () =>
        {
            _CruelWould.DaleIron(TheseLad,1,0);
            _CruelWould.DaleSlum("Hold the button, aim and fire", -450);
            _CruelWould.DaleCore(new Vector2[] { TheseLad.position });
        });
    }
    public void Cruel_4()
    {
        CruelSmile = 4;
        if(TedSlumElk.instance.ClanGush.guide_click_auto>0)
        {
            _CruelWould.Hone(true);
            DutyAwesome.AgeFletcher().Nomad(.5f, () =>
            {
                _CruelWould.DaleIron(PikeLad);
                _CruelWould.DaleSlum("Click auto fire", 0);
                _CruelWould.DaleCore(new Vector2[] { PikeLad.position });
            });
        }
        else
        {
            QuitCacheCandle.AgeFletcher().HornCache("1001","2");
            Cruel_Blood();
        }
    }
    //boss鱼  引导
    public void Cruel_KierOnGovern(Transform mainBossTf, Transform miaoBossTf)
    {
        if (mainBossTf == null && miaoBossTf == null)
        {
            return;
        }
        _CruelWould = MarkUIJazz(nameof(CruelWould)).GetComponent<CruelWould>();
        ClanAwesome.Instance?.BladeSparsely();
        CruelSmile = 6;
        _CruelWould.Hone(true);
        DutyAwesome.AgeFletcher().Nomad(.3f, () =>
        {
            _CruelWould.DaleIron(mainBossTf, 1.2f);
            _CruelWould.DaleSlum("Defeat it for a huge reward.", +550);
            _CruelWould.DaleWaleLad(() =>
            {
                if (_CruelWould == null) return;
                _CruelWould.DaleIron(miaoBossTf, 1.2f);
                _CruelWould.DaleSlum("Only hits here can defeat the boss.", +550);
                _CruelWould.DaleWaleLad(() =>
                    {
                        Cruel_Blood();
                        QuitCacheCandle.AgeFletcher().HornCache("1001", "3");
                        ClanAwesome.Instance?.SecureSparsely();
                    });
            });
        });
    }

    private void OnFerverTimeAnimFinish()
    {
        if (SpotGushAwesome.GetBool(CMillet.If_Hover_Decade_Ridge_Mesh))
        {
            return;
        }
        SpotGushAwesome.SetBool(CMillet.If_Hover_Decade_Ridge_Mesh, true);
        Cruel_EntireFlood();
    }

    private void Cruel_EntireFlood()
    {
        _CruelWould = MarkUIJazz(nameof(CruelWould)).GetComponent<CruelWould>();
         ClanAwesome.Instance?.BladeSparsely();
        _CruelWould.Hone(true);
        DutyAwesome.AgeFletcher().Nomad(.5f, () =>
        {
            CruelSmile = 8;
            _CruelWould.DaleIron(TheseLad);
            _CruelWould.DaleSlum("Rewards drop on every click", -450);
            _CruelWould.DaleCore(new Vector2[] { TheseLad.position });
        });
    }

   public void Cruel_BloodRebel()
    {
        CruelSmile = 0;
        _CruelWould.Hone(false);
        DutyAwesome.AgeFletcher().Nomad(.5f, () =>
        {
            QuitCacheCandle.AgeFletcher().HornCache("1001", "4");
            ClanAwesome.Instance?.SecureSparsely();
            UIAwesome.AgeFletcher().BloodSoSolelyUIHobby(nameof(CruelWould));   
        });
    }




    public void Cruel_Blood()
    {
        CruelSmile = 0;
        _CruelWould.Hone(false);
        DutyAwesome.AgeFletcher().Nomad(.5f, () =>
        {
            UIAwesome.AgeFletcher().BloodSoSolelyUIHobby(nameof(CruelWould));
        });
    }




    //public void Guide_3()
    //{
    //    GuideIndex = 0;
    //    _GuidePanel.Hide(false);
    //    DutyAwesome.GetInstance().Delay(.5f, () =>
    //    {
    //        UIAwesome.GetInstance().CloseOrReturnUIForms(nameof(CruelWould));
    //    });
    //}





    public override void Display(object uiFormParams)
    {
        base.Display(uiFormParams);
        ReclaimTalkWelt();
        DOVirtual.DelayedCall(2f, () =>
{
    BloodUIJazz(nameof(ConcertBowl));
});

        if (m_HueEarDownBureau && m_UIToughCrashEar != null)
        {
            if (m_BoyCrashWest != null) m_BoyCrashWest.SetActive(false);
            if (m_EarCrashWest != null) m_EarCrashWest.SetActive(true);
            m_UIToughCrashEar.Cape();
        }
        else
        {
            if (m_EarCrashWest != null) m_EarCrashWest.SetActive(false);
            if (m_BoyCrashWest != null) m_BoyCrashWest.SetActive(true);
            if (m_UIImageCrash != null) m_UIImageCrash.Cape();
        }

        if (ClanAwesome.Instance != null)
        {
            ClanAwesome.Instance.PromoteSchoolUIReclaim();
            ClanAwesome.Instance.PromotePermUIReclaim();
        }

        if (MoltSexStump != null)
        {
            if (m_ControlSexStumpReclaimEdifice != null)
            {
                StopCoroutine(m_ControlSexStumpReclaimEdifice);
                m_ControlSexStumpReclaimEdifice = null;
            }
            m_ControlSexStumpReclaimEdifice = StartCoroutine(ByControlSexStumpReclaim(5));
        }

        SortGripeHurt?.ReclaimWay();
        DecadeDutyHurt?.ReclaimWay();

        if (ClanAwesome.Instance != null)
        {
            ClanAwesome.Instance.WhyClanSick(GameType.Normal);
        }

        if (m_EaseBergBureau == null)
        {
            m_EaseBergBureau = FindFirstObjectByType<UIEaseBergBureau>();
        }
        if (m_EaseBergBureau != null)
        {
            m_EaseBergBureau.WaistAlikeBasaltic(m_ChartEaseToMoteAcreage);
            if (m_SewageClanSituation == null)
            {
                m_SewageClanSituation = GetComponent<MoteWouldSewageClanSituation>();
                if (m_SewageClanSituation == null)
                {
                    m_SewageClanSituation = FindFirstObjectByType<MoteWouldSewageClanSituation>();
                }
            }
            m_SewageClanSituation?.OnHomePanelDisplay(m_ChartEaseToMoteAcreage);
        }
    }

    public void ReclaimTalkWelt()
    {
        if (m_TalkWelt == null || ClanGushAwesome.AgeFletcher() == null)
        {
            return;
        }

        m_TalkWelt.text = ClauseRide.PhraseIDPer(ClanGushAwesome.AgeFletcher().RunSeed());
    }

    /// <summary>
    /// 非鱼来源（任务/奖励等）：Instantiate 飞金币图标 +，不使用 cash 对象池。
    /// </summary>
    private void WifeHeGratifySeedLetBustPartWelt(Transform startTransform, int cashAmount, double balanceBefore, double balanceAfter)
    {
        GameObject iconObj = m_TalkTough != null ? m_TalkTough.gameObject : null;
        WifeSeedLetBustPartWeltHall(startTransform, iconObj, m_TalkTough != null ? m_TalkTough.transform : null, m_TalkWelt, cashAmount, balanceBefore, balanceAfter, useFishKillCashPool: false);
    }

    /// <summary>
    /// 击杀鱼掉金币：<see cref="ClanAwesome.CashPoolPrefab"/> + <see cref="TraditionDemobilize.FishGoldMove"/>。
    /// </summary>
    private void WifeEaseMistSeedLetBustPartWelt(Transform startTransform, int cashAmount, double balanceBefore, double balanceAfter)
    {
        GameObject iconObj = (m_EaseTalkTough != null) ? m_EaseTalkTough.gameObject : (m_TalkTough != null ? m_TalkTough.gameObject : null);
        WifeSeedLetBustPartWeltHall(startTransform, iconObj, m_TalkTough != null ? m_TalkTough.transform : null, m_TalkWelt, cashAmount, balanceBefore, balanceAfter, useFishKillCashPool: true);
    }

    /// <summary>金币飞入结束后滚余额；balanceBefore/After 为入账前/后展示值（与存档一致）。</summary>
    private void WifeSeedLetBustPartWeltHall(Transform startTransform, GameObject flyIconTemplate, Transform balanceIconTransform, TextMeshProUGUI balanceText, int flyParticleCount, double balanceBefore, double balanceAfter, bool useFishKillCashPool)
    {
        if (balanceText == null || ClanGushAwesome.AgeFletcher() == null)
        {
            ReclaimTalkWelt();
            return;
        }

        Action onFlyComplete = () =>
        {
            balanceText.text = ClauseRide.PhraseIDPer(balanceBefore);
            TraditionDemobilize.BorderClause(balanceBefore, balanceAfter, 0.1f, balanceText,
                () => { balanceText.text = ClauseRide.PhraseIDPer(balanceAfter); });
        };

        if (startTransform == null || flyIconTemplate == null || balanceIconTransform == null)
        {
            onFlyComplete();
            return;
        }

        int n = Mathf.Max(flyParticleCount, 1);
        if (useFishKillCashPool)
        {
            TraditionDemobilize.EaseTalkLone(flyIconTemplate, n, startTransform, balanceIconTransform, onFlyComplete);
        }
        else
        {
            //TraditionDemobilize.GoldMoveBest(flyIconTemplate, n, startTransform, balanceIconTransform, onFlyComplete);
            TraditionDemobilize.UISeedLoneBest(flyIconTemplate, n, startTransform, balanceIconTransform, onFlyComplete);
        }
    }

    /// <summary>
    /// 击杀钻石鱼：<see cref="ClanAwesome.DiamondPoolPrefab"/> + <see cref="TraditionDemobilize.FishDiamondMove"/>，到账与 <see cref="AddDiamond"/> 一致（ZJT）。
    /// </summary>
    private void WifeEaseMistLinkageLetBustSuite(Transform startTransform, int diamondAmount)
    {
        GameObject iconObj = m_EaseLinkageTough != null ? m_EaseLinkageTough.gameObject : (m_LinkageTough != null ? m_LinkageTough.gameObject : null);
        Transform endTf = m_LinkageTough != null ? m_LinkageTough.transform : null;
        if (startTransform == null || iconObj == null || endTf == null)
        {
            ZJT_Manager.AgeFletcher().AddMoney((float)diamondAmount);
            return;
        }

        int n = Mathf.Max(diamondAmount, 1);
        TraditionDemobilize.EaseLinkageLone(iconObj, n, startTransform, endTf, () =>
        {
            ZJT_Manager.AgeFletcher().AddMoney((float)diamondAmount);
        });
    }

    /// <summary>
    /// 收到体力事件后，更新当前场景的UI
    /// </summary>
    private void OnEnergyUpdate(int currentEnergy, float remainingTime, int maxEnergy)
    {
        string energyStr = currentEnergy.ToString();
        if (IndoorWeltTMP != null) IndoorWeltTMP.text = energyStr;
        int threshold = (ClanAwesome.Instance != null) ? ClanAwesome.Instance.HectareHomemaker : 50;
        bool needShowCountdown = currentEnergy <= threshold && remainingTime > 0f;
        WhyRecognizeFreeze(needShowCountdown);
        if (needShowCountdown)
        {
            // 格式化倒计时（显示整数秒）
            int showSeconds = Mathf.CeilToInt(remainingTime);
            string countdownStr = showSeconds.ToString() + "s";
            if (ZoologistWeltTMP != null) ZoologistWeltTMP.text = countdownStr;
        }
    }

    /// <summary>
    /// 显示/隐藏倒计时UI
    /// </summary>
    private void WhyRecognizeFreeze(bool active)
    {
        if (ZoologistWeltTMP != null) ZoologistWeltTMP.gameObject.SetActive(active);
    }

    private void MarkCentralWould()
    {
        MarkUIJazz(nameof(CentralWould));
    }

    /// <summary>金币（船经验）增加后首次达到可升级时，自动打开船坞升级面板。</summary>
    private void OnShipUpgradePopupRequest()
    {
        if (ClanAwesome.Instance == null || !ClanAwesome.Instance.HimPermErectusWay())
        {
            return;
        }
        bool isGamePanelTop = UIAwesome.AgeFletcher().AxMoteWouldFew();
        if (isGamePanelTop)
        {
            QuitCacheCandle.AgeFletcher().HornCache("1010");
            MarkUIJazz(nameof(PermGripeAtWould));
        }
        else
        {
            DOVirtual.DelayedCall(5f, () =>
            {
                OnShipUpgradePopupRequest();
            });
        }
    }

    private void OnDestroy()
    {
        if (Instance == this) Instance = null;
        if (m_ControlSexStumpReclaimEdifice != null)
        {
            StopCoroutine(m_ControlSexStumpReclaimEdifice);
            m_ControlSexStumpReclaimEdifice = null;
        }
        if (onHeGratifyDewJuicyDeviate != null)
        {
            BarelyIon.ToDewJuicy -= onHeGratifyDewJuicyDeviate;
            onHeGratifyDewJuicyDeviate = null;
        }
        if (NoEaseMistSeedLessonDeviate != null)
        {
            BarelyIon.ToEaseDewJuicy -= NoEaseMistSeedLessonDeviate;
            NoEaseMistSeedLessonDeviate = null;
        }
        if (NoEaseMistLinkageLessonDeviate != null)
        {
            BarelyIon.ToEaseDewLinkage -= NoEaseMistLinkageLessonDeviate;
            NoEaseMistLinkageLessonDeviate = null;
        }
        BarelyIon.ToSchoolPursuit -= OnEnergyUpdate;
        BarelyIon.ToClanSickPursuit -= OnGameTypeChanged;
        if (NoEntireDutyDiscWalkerDeviate != null)
        {
            BarelyIon.ToEntireDutyDiscWalker -= NoEntireDutyDiscWalkerDeviate;
            NoEntireDutyDiscWalkerDeviate = null;
        }
        if (NoPermErectusSlimePromoteDeviate != null)
        {
            BarelyIon.ToPermErectusSlimePromote -= NoPermErectusSlimePromoteDeviate;
            NoPermErectusSlimePromoteDeviate = null;
        }
        if (NoKierEaseWorthGovernCruelDeviate != null)
        {
            BarelyIon.ToKierEaseWorthGovernCruelPromote -= NoKierEaseWorthGovernCruelDeviate;
            NoKierEaseWorthGovernCruelDeviate = null;
        }
        SortGripeHurt?.Inconvenient();
        DecadeDutyHurt?.Inconvenient();
        VaseAloftJuicyVideoOnHurt?.Inconvenient();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            UIEaseDeluge.PromoteAlikeKierEase();
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            BarelyIon.ToEaseDewJuicy?.Invoke(transform, 20);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            BarelyIon.ToMoteSexHeadMeDistinctionPromote?.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            MoltSexStump.GetComponent<MoteSexStump>().WifeHoneDisc();
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            UIAwesome.AgeFletcher().DaleUIHobby(nameof(EmitWould));
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            UIAwesome.AgeFletcher().DaleUIHobby(nameof(ScrubSlumWould));
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            UIAwesome.AgeFletcher().DaleUIHobby(nameof(BudJayWould), 5000);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            RewardData Rewards = new RewardData();
            Rewards.rewardNum = 11;
            Rewards.type = RewardType.Diamond;
            UIAwesome.AgeFletcher().DaleUIHobby(nameof(LessonWould)).GetComponent<LessonWould>().Cape(null, Rewards,
           () =>
           {
               BarelyIon.ToSewageClanDormancy?.Invoke();
           }, "1011");
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            UIAwesome.AgeFletcher().DaleUIHobby(nameof(PermGripeAtWould));
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            DewLinkage(1000);
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            ClanAwesome.Instance?.BladeEntireRecognize();
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            ClanAwesome.Instance?.SecureEntireRecognize();
        }
    }

    /// <summary>任务/奖励等界面加金币（非击杀鱼）。疯狂模式仍记入待结算金币。</summary>
    private void SeduceHeGratifyDewJuicyLesson(Transform startTransform, int cashAmount)
    {
        if (cashAmount <= 0)
        {
            return;
        }

        ClanGushAwesome gdm = ClanGushAwesome.AgeFletcher();
        double balanceBefore = ClauseRide.Dimly(gdm.RunSeed());
        gdm.WaxSeed(cashAmount, ThaiAwesome.instance.transform, false);
        double balanceAfter = ClauseRide.Dimly(gdm.RunSeed());
        WifeHeGratifySeedLetBustPartWelt(startTransform, cashAmount, balanceBefore, balanceAfter);
    }

    /// <summary>击杀普通鱼掉金币：爆钱粒子池 + cash 对象池飞币 + 入账。</summary>
    private void SeduceEaseMistSeedLesson(Transform startTransform, int cashAmount)
    {
        if (cashAmount <= 0)
        {
            return;
        }

        if (AxItEntireDuty())
        {
            m_EntireBicycleJuicy += cashAmount;
            return;
        }

        FamousEaseAloftOnSupposition();
        m_UIPluto.WaistPluto();
        ClanGushAwesome gdm = ClanGushAwesome.AgeFletcher();
        double balanceBefore = ClauseRide.Dimly(gdm.RunSeed());
        gdm.WaxSeed(cashAmount, ThaiAwesome.instance.transform, false);
        double balanceAfter = ClauseRide.Dimly(gdm.RunSeed());
        WifeEaseMistSeedLetBustPartWelt(startTransform, cashAmount, balanceBefore, balanceAfter);
    }

    /// <summary>击杀钻石鱼：与金币鱼分流；疯狂模式下也即时发钻（不计入 m_FerverPendingMoney）。</summary>
    private void SeduceEaseMistLinkageLesson(Transform startTransform, int diamondAmount)
    {
        if (diamondAmount <= 0)
        {
            return;
        }

        FamousEaseAloftOnSupposition();
        m_UIPluto.WaistPluto();
        WifeEaseMistLinkageLetBustSuite(startTransform, diamondAmount);
    }

    private void FamousEaseAloftOnSupposition()
    {
        if (m_EaseAloftOnAstute)
        {
            return;
        }

        if (VaseAloftJuicyVideoOnHurt == null)
        {
            VaseAloftJuicyVideoOnHurt = FindFirstObjectByType<EaseAloftJuicyVideoOnMold>();
        }
        if (VaseAloftJuicyVideoOnHurt == null)
        {
            return;
        }

        VaseAloftJuicyVideoOnHurt.Glassmaker();
        m_EaseAloftOnAstute = true;
    }

    private IEnumerator ByControlSexStumpReclaim(int frameCount)
    {
        int safeFrameCount = Mathf.Max(0, frameCount);
        while (safeFrameCount-- > 0)
        {
            yield return null;
        }

        if (MoltSexStump != null && MoltSexStump.gameObject.activeInHierarchy)
        {
            MoltSexStump.LibertyOffNicheAcreageTexts();
        }
        m_ControlSexStumpReclaimEdifice = null;
    }

    private bool AxItEntireDuty()
    {
        return ClanAwesome.Instance != null && ClanAwesome.Instance.ClanSick == GameType.FerverTime;
    }

    private void OnGameTypeChanged(GameType gameType)
    {
        bool isNowFerver = gameType == GameType.FerverTime;

        if (!m_AxEntireBarb && isNowFerver)
        {
            // 首次直接进入 Ferver 时也要初始化爆钱粒子池，避免未订阅击杀粒子事件。
            FamousEaseAloftOnSupposition();
            DaleEntireSludgeJuicy();
        }
        else if (m_AxEntireBarb && !isNowFerver)
        {
            SociallyEntireAbbreviate();
            HoneEntireSludgeJuicy();
        }

        m_AxEntireBarb = isNowFerver;
    }

    private void OnBossFishCrossCenterGuideRequest(Transform mainBossTf, Transform miaoBossTf)
    {
        Cruel_KierOnGovern(mainBossTf, miaoBossTf);
    }



    private void GlassmakerEntireLessonUI()
    {
        m_AxEntireBarb = AxItEntireDuty();

        if (m_AxEntireBarb)
        {
            DaleEntireSludgeJuicy();
        }
        else
        {
            HoneEntireSludgeJuicy();
        }
    }

    private void SociallyEntireAbbreviate()
    {
        int settleMoney = Mathf.Max(0, m_EntireBicycleJuicy);
        if (settleMoney <= 0)
        {
            ChartEntireBicycleUI();
            return;
        }

        double oldGold = ClanGushAwesome.AgeFletcher().RunSeed();
        double targetGold = oldGold + settleMoney;
        ClanGushAwesome.AgeFletcher().WaxSeed(settleMoney);
        m_TalkWelt.text = ClauseRide.PhraseIDPer(targetGold);

        UIAwesome.AgeFletcher().DaleUIHobby(nameof(BudJayWould), new BudJayWould.OpenArgs
        {
            Aloof = settleMoney,
            CentKierEaseAloft = false,
            OrCacheWe = "1009"
        });
        ChartEntireBicycleUI();
    }

    private void ChartEntireBicycleUI()
    {
        m_EntireBicycleJuicy = 0;
    }

    private void GlassmakerEntireSludgeJuicy()
    {
        RebelBarb_SludgeJuicyPloy.Clear();
        if (RebelBarb_SludgeJuicy == null)
        {
            return;
        }

        for (int i = 0; i < RebelBarb_SludgeJuicy.childCount; i++)
        {
            Transform child = RebelBarb_SludgeJuicy.GetChild(i);
            RebelBarb_SludgeJuicyPloy.Add(child);
        }

        Vector3 pivotPos = transform.position;
        if (m_UIToughCrashEar != null)
        {
            pivotPos = m_UIToughCrashEar.transform.position;
        }
        else if (m_UIImageCrash != null)
        {
            pivotPos = m_UIImageCrash.transform.position;
        }

        RebelBarb_SludgeJuicyPloy.Sort((a, b) =>
        {
            float da = Vector2.Distance(a.position, pivotPos);
            float db = Vector2.Distance(b.position, pivotPos);
            return db.CompareTo(da);
        });

        FerverTimeConfig cfg = ClanGushAwesome.AgeFletcher() != null ? ClanGushAwesome.AgeFletcher().m_EntireDutyMillet : null;
        m_EntireSludgeJuicyDiscCollapse = cfg == null ? 4f : Mathf.Max(1f, cfg.FerverCountDownTime - 1f);

        HoneEntireSludgeJuicy();
    }

    public void DewLinkage(double num, Transform flyTransform = null)
    {
        TraditionDemobilize.UILinkageLoneCent(m_LinkageTough.gameObject, Mathf.Min((int)num, 10), Vector2.zero, m_LinkageTough.transform.position, () =>
        {
            ZJT_Manager.AgeFletcher().AddMoney((float)num);
        });
    }

    private void DaleEntireSludgeJuicy()
    {
        if (RebelBarb_SludgeJuicy == null)
        {
            return;
        }

        RebelBarb_SludgeJuicy.gameObject.SetActive(true);
        if (RebelBarb_SludgeJuicyPloy.Count <= 0)
        {
            return;
        }

        float delayTime = m_EntireSludgeJuicyDiscCollapse / RebelBarb_SludgeJuicyPloy.Count;
        for (int i = 0; i < RebelBarb_SludgeJuicyPloy.Count; i++)
        {
            Transform item = RebelBarb_SludgeJuicyPloy[i];
            if (item == null)
            {
                continue;
            }

            item.DOKill();
            item.localScale = Vector3.zero;
            item.DOScale(new Vector3(0.6f, 0.6f, 0.6f), 0.3f).SetEase(Ease.OutBack).SetDelay(delayTime * i);
        }
    }

    private void HoneEntireSludgeJuicy()
    {
        if (RebelBarb_SludgeJuicy == null)
        {
            return;
        }

        RebelBarb_SludgeJuicy.gameObject.SetActive(false);
    }
}
