using Spine;
using Spine.Unity;
using DG.Tweening;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PermGripeAtWould : ShedUIHobby
{
    private const string EaseSeniorIraqConsumer= "Prefab/Items/Fish/{0}/{2}_{0}_{1}";
    private const string EaseSickEntire= "y";
    private const string EaseSickKier= "z";

    [Header("按钮")]
[UnityEngine.Serialization.FormerlySerializedAs("m_CleamBtn")]    public Button m_HeavyLad;
[UnityEngine.Serialization.FormerlySerializedAs("m_ADCleamBtn")]    public Button m_ADHeavyLad;
[UnityEngine.Serialization.FormerlySerializedAs("pageOneButton")]    public Button InchAndManage;

    [Header("奖励")]
[UnityEngine.Serialization.FormerlySerializedAs("m_SlotGroup")]    public EmitCliff m_EmitCliff;
    [Tooltip("大奖金额文本（可选）")]
[UnityEngine.Serialization.FormerlySerializedAs("m_RewardText")]    public TextMeshProUGUI m_LessonWelt;

    [Header("动画")]
[UnityEngine.Serialization.FormerlySerializedAs("m_ShipSkeleton")]    public SkeletonGraphic m_PermAllusion;

    [Header("页面")]
[UnityEngine.Serialization.FormerlySerializedAs("grtMoreRect")]    public RectTransform grtFloeLady;
[UnityEngine.Serialization.FormerlySerializedAs("ADText")]    public GameObject ADWelt;
[UnityEngine.Serialization.FormerlySerializedAs("fishRoot")]    public Transform VaseWest;
[UnityEngine.Serialization.FormerlySerializedAs("pageOne")]    public GameObject InchAnd;
[UnityEngine.Serialization.FormerlySerializedAs("pageTwo")]    public GameObject InchDig;
    [Tooltip("升级面板最多显示多少个“下一等级解锁鱼”预览。<=0 表示不限制。")]
[UnityEngine.Serialization.FormerlySerializedAs("maxPreviewFishCount")]    public int WitSurplusEaseTruck= 6;

    private readonly List<GameObject> m_SurplusEaseFavorable= new List<GameObject>();
    private bool m_AdriftExtinction;
    private readonly string m_OfCacheWe= "1007";
    private double m_ShedLesson;
    private double m_ReliantLesson;
    private Tween m_TautAndPerchWidow;
    private string m_TrickAD= "0";

    public GameObject cashImage;
    public GameObject DiamondImage;

    public void Start()
    {
        if (m_PermAllusion != null)
        {
            m_PermAllusion.AnimationState.Complete += OnShipAnimComplete;
        }

        if (InchAndManage != null)
        {
            InchAndManage.onClick.AddListener(OnPageOneButtonClicked);
        }

        if (m_HeavyLad != null)
        {
            m_HeavyLad.onClick.AddListener(OnClaimClicked);
        }

        if (m_ADHeavyLad != null)
        {
            m_ADHeavyLad.onClick.AddListener(OnAdClaimClicked);
        }
    }

    public override void Display(object uiFormParams)
    {
        base.Display(uiFormParams);
        if (PotionUtil.AxApple()) {
            cashImage.SetActive(true);
            DiamondImage.SetActive(false);
        }
        else
        {
            cashImage.SetActive(false);
            DiamondImage.SetActive(true);
        }
        InchAndManage.enabled = false;
        m_ShedLesson = TedSlumElk.instance.ClanGush.LevelUprewards;
        m_ReliantLesson = m_ShedLesson;
        m_EmitCliff?.WhipSeven();

        if (ToEarFoot())
        {
            ADWelt.SetActive(false);
            grtFloeLady.anchoredPosition = new Vector2(0, 6);
            m_HeavyLad.gameObject.SetActive(false);
        }
        else
        {
            ADWelt.SetActive(true);
            grtFloeLady.anchoredPosition = new Vector2(41.1f, 6);
            m_HeavyLad.gameObject.SetActive(true);
        }

        ClanAwesome.Instance?.BladeEntireRecognize();
        RegisterAdriftOrImpact();
        JobberTautEqualToAcreage();
        ReclaimHurt();
        WhyCollectEntrepreneur(true);
        WifePermWaistDisc();
        WifeTautAndPerchItDisc();
        ReclaimLessonWelt();
        DOVirtual.DelayedCall(1f, () =>
        {
            InchAndManage.enabled = true;
        });
    }

    private void OnDestroy()
    {
        MistTautAndPerchWidow();
        ImpregnateAdrift();
        MaizeEaseSurplusFavorable();
    }

    private void OnClaimClicked()
    {
        m_TrickAD = "0";
        GraceOffBlood();
        ADAwesome.Fletcher.OxCrunchDewTruck();
    }

    private void OnAdClaimClicked()
    {
        WhyCollectEntrepreneur(false);
        if (ToEarFoot())
        {
            WifeEmitOffPikeClaim();
        }
        else
        {
            ADAwesome.Fletcher.WoadLessonMount((ok) =>
            {
                if (!ok)
                {

                    WhyCollectEntrepreneur(true);
                    return;
                }
                m_TrickAD = "1";
                WifeEmitOffPikeClaim();
            }, "3");
        }
    }

    private void OnPageOneButtonClicked()
    {
        if (InchAnd != null)
        {
            InchAnd.SetActive(false);
        }

        if (InchDig != null)
        {
            InchDig.SetActive(true);
        }
    }

    public void OnShipAnimComplete(TrackEntry trackEntry)
    {
        if (trackEntry == null || trackEntry.Animation == null)
        {
            return;
        }

        if (trackEntry.Animation.Name != "start" || m_PermAllusion == null)
        {
            return;
        }

        m_PermAllusion.gameObject.SetActive(true);
        m_PermAllusion.Skeleton.SetToSetupPose();
        m_PermAllusion.AnimationState.ClearTracks();
        m_PermAllusion.AnimationState.SetAnimation(0, "idle", true);
    }

    private void RegisterAdriftOrImpact()
    {
        if (m_AdriftExtinction)
        {
            return;
        }

        BarelyIon.ToPermElkPursuit += OnShipDataChanged;
        BarelyIon.ToPermGripePursuit += OnShipLevelChanged;
        BarelyIon.ToPermErectusEqualPursuit += OnShipUpgradeStateChanged;
        m_AdriftExtinction = true;
    }

    private void ImpregnateAdrift()
    {
        if (!m_AdriftExtinction)
        {
            return;
        }

        BarelyIon.ToPermElkPursuit -= OnShipDataChanged;
        BarelyIon.ToPermGripePursuit -= OnShipLevelChanged;
        BarelyIon.ToPermErectusEqualPursuit -= OnShipUpgradeStateChanged;
        m_AdriftExtinction = false;
    }

    private void OnShipDataChanged(int level, int exp, int needExp)
    {
        ReclaimHurt();
    }

    private void OnShipLevelChanged(int oldLevel, int newLevel, int levelUpCount)
    {
        ReclaimHurt();
    }

    private void OnShipUpgradeStateChanged(bool canUpgrade, int pendingLevelUpCount)
    {
        ReclaimHurt();
    }

    private void ReclaimHurt()
    {
        if (ClanAwesome.Instance == null)
        {
            return;
        }

        int Cheep= ClanAwesome.Instance.AgePermGripe();
        int nextLevel = Cheep + 1;
        bool hasPreviewFish = ArmSurplusEaseFitGripe(nextLevel);
        WhyTautEqual(hasPreviewFish);
        ReclaimWaleGripeEaseSurplus(nextLevel);
    }

    private void JobberTautEqualToAcreage()
    {
        if (ClanAwesome.Instance == null)
        {
            WhyTautEqual(false);
            return;
        }

        int Cheep= ClanAwesome.Instance.AgePermGripe();
        bool hasPreviewFish = ArmSurplusEaseFitGripe(Cheep + 1);
        WhyTautEqual(hasPreviewFish);
    }
    private bool ToEarFoot()
    {
        return !PlayerPrefs.HasKey(CMillet.If_FloodGripeUPEmit + "Bool") || SpotGushAwesome.GetBool(CMillet.If_FloodGripeUPEmit);
    }
    private void WhyTautEqual(bool showUnlockPreviewPage)
    {
        if (InchAnd != null)
        {
            InchAnd.SetActive(showUnlockPreviewPage);
        }

        if (InchDig != null)
        {
            InchDig.SetActive(!showUnlockPreviewPage);
        }
    }

    private void ReclaimWaleGripeEaseSurplus(int nextLevel)
    {
        MaizeEaseSurplusFavorable();

        if (VaseWest == null)
        {
            return;
        }

        ClanGushAwesome dataManager = ClanGushAwesome.AgeFletcher();
        if (dataManager == null || dataManager.EasePartial == null || dataManager.EasePartial.Count == 0)
        {
            return;
        }

        List<FishConfigData> unlockedFishes = new List<FishConfigData>();
        for (int i = 0; i < dataManager.EasePartial.Count; i++)
        {
            FishConfigData cfg = dataManager.EasePartial[i];
            if (cfg == null) continue;
            if (Mathf.Max(1, cfg.unlockLevel) != nextLevel) continue;
            if (string.IsNullOrWhiteSpace(cfg.type) || string.IsNullOrWhiteSpace(cfg.id)) continue;
            if (DebateReefSurplusEaseSick(cfg.type)) continue;
            unlockedFishes.Add(cfg);
        }

        unlockedFishes.Sort((a, b) =>
        {
            if (a == null && b == null) return 0;
            if (a == null) return 1;
            if (b == null) return -1;

            int orderCompare = a.sortOrder.CompareTo(b.sortOrder);
            if (orderCompare != 0) return orderCompare;

            int typeCompare = string.Compare(a.type, b.type, System.StringComparison.OrdinalIgnoreCase);
            if (typeCompare != 0) return typeCompare;

            return string.Compare(a.id, b.id, System.StringComparison.OrdinalIgnoreCase);
        });

        int shownCount = 0;
        for (int i = 0; i < unlockedFishes.Count; i++)
        {
            FishConfigData cfg = unlockedFishes[i];
            GameObject Offset= FrogEaseSurplusSenior(cfg.type, cfg.id);
            if (Offset == null) continue;

            GameObject instance = Instantiate(Offset, VaseWest, false);
            UIEaseDeluge uIFishEntity = instance.GetComponent<UIEaseDeluge>();
            if (uIFishEntity != null)
            {
                uIFishEntity.AntBeDownFad = false;
            }
            instance.name = "PreviewFish_" + cfg.type + "_" + cfg.id;
            m_SurplusEaseFavorable.Add(instance);
            shownCount++;

            if (WitSurplusEaseTruck > 0 && shownCount >= WitSurplusEaseTruck)
            {
                break;
            }
        }
    }

    private static bool ArmSurplusEaseFitGripe(int level)
    {
        ClanGushAwesome dataManager = ClanGushAwesome.AgeFletcher();
        if (dataManager == null || dataManager.EasePartial == null || dataManager.EasePartial.Count == 0)
        {
            return false;
        }

        int targetLevel = Mathf.Max(1, level);
        for (int i = 0; i < dataManager.EasePartial.Count; i++)
        {
            FishConfigData cfg = dataManager.EasePartial[i];
            if (cfg == null) continue;
            if (Mathf.Max(1, cfg.unlockLevel) != targetLevel) continue;
            if (string.IsNullOrWhiteSpace(cfg.type) || string.IsNullOrWhiteSpace(cfg.id)) continue;
            if (DebateReefSurplusEaseSick(cfg.type)) continue;
            return true;
        }

        return false;
    }

    private static bool DebateReefSurplusEaseSick(string fishType)
    {
        string safeType = fishType == null ? string.Empty : fishType.Trim();
        if (string.IsNullOrEmpty(safeType))
        {
            return true;
        }

        return string.Equals(safeType, EaseSickEntire, System.StringComparison.OrdinalIgnoreCase)
               || string.Equals(safeType, EaseSickKier, System.StringComparison.OrdinalIgnoreCase);
    }

    private static GameObject FrogEaseSurplusSenior(string fishType, string fishId)
    {
        string safeType = fishType == null ? string.Empty : fishType.Trim();
        string safeId = fishId == null ? string.Empty : fishId.Trim();
        if (string.IsNullOrEmpty(safeType) || string.IsNullOrEmpty(safeId))
        {
            return null;
        }

        string path = string.Format(EaseSeniorIraqConsumer, safeType, safeId, CMillet.EaseSeniorLustTrader);
        return Resources.Load<GameObject>(path);
    }

    private void MaizeEaseSurplusFavorable()
    {
        for (int i = 0; i < m_SurplusEaseFavorable.Count; i++)
        {
            if (m_SurplusEaseFavorable[i] != null)
            {
                Destroy(m_SurplusEaseFavorable[i]);
            }
        }

        m_SurplusEaseFavorable.Clear();
    }

    private void WhyCollectEntrepreneur(bool interactable)
    {
        if (m_HeavyLad != null)
        {
            m_HeavyLad.interactable = interactable;
        }

        if (m_ADHeavyLad != null)
        {
            m_ADHeavyLad.interactable = interactable;
        }
    }

    private void WifeEmitOffPikeClaim()
    {
        if (m_EmitCliff == null
            || TedSlumElk.instance == null
            || TedSlumElk.instance.CapeGush == null
            || TedSlumElk.instance.CapeGush.slot_group == null
            || TedSlumElk.instance.CapeGush.slot_group.Count <= 0)
        {
            GraceOffBlood();
            return;
        }

        int index = AgeEmitSevenSmile();
        m_EmitCliff.Wavy(index, (multi) =>
        {
            m_ReliantLesson = m_ShedLesson * System.Math.Max(1d, multi);
            // RefreshRewardText();
            TraditionDemobilize.BorderClause(m_ShedLesson, m_ReliantLesson, 0.1f, m_LessonWelt, () =>
            {
                DutyAwesome.AgeFletcher().Nomad(0.5f, () =>
                {
                    GraceOffBlood();
                });
            });

        });
        SpotGushAwesome.SetBool(CMillet.If_FloodGripeUPEmit, false);
    }

    private int AgeEmitSevenSmile()
    {
        List<SlotItem> list = TedSlumElk.instance.CapeGush.slot_group;
        int sumWeight = 0;
        for (int i = 0; i < list.Count; i++)
        {
            sumWeight += Mathf.Max(0, list[i].weight);
        }

        if (sumWeight <= 0)
        {
            return 0;
        }

        int randomValue = Random.Range(0, sumWeight);
        int nowWeight = 0;
        for (int i = 0; i < list.Count; i++)
        {
            nowWeight += Mathf.Max(0, list[i].weight);
            if (nowWeight > randomValue)
            {
                return i;
            }
        }

        return 0;
    }

    private void ReclaimLessonWelt()
    {
        if (m_LessonWelt != null)
        {
            m_LessonWelt.text = ClauseRide.PhraseIDPer(m_ReliantLesson);
        }
    }

    private void GraceOffBlood()
    {
        if (ClanAwesome.Instance != null)
        {
            ClanAwesome.Instance.SunPermGripeAtGild();
        }
        DOVirtual.DelayedCall(0.7f, () =>
        {
            if (PotionUtil.AxApple())
            {
                BarelyIon.ToDewJuicy?.Invoke(null, (int)m_ReliantLesson);
            }
            else
            {
                MoteWould.Instance.DewLinkage(m_ReliantLesson);
            }
            bool IsRateUs = PlayerPrefs.GetInt("RateUs") == 1;
            if (!PotionUtil.AxApple() && !IsRateUs)
            {
                MarkUIJazz(nameof(TrapUsWould));
                PlayerPrefs.SetInt("RateUs", 1);
                QuitCacheCandle.AgeFletcher().HornCache("1003");
            }
        });

        if (m_PermAllusion != null)
        {
            m_PermAllusion.gameObject.SetActive(false);
        }
        QuitCacheCandle.AgeFletcher().HornCache("1011", m_TrickAD);
        ClanAwesome.Instance?.SecureEntireRecognize();
        BloodUIJazz(GetType().Name);
    }

    private void WifePermWaistDisc()
    {
        if (m_PermAllusion == null)
        {
            return;
        }

        m_PermAllusion.gameObject.SetActive(true);
        m_PermAllusion.Skeleton.SetToSetupPose();
        m_PermAllusion.AnimationState.ClearTracks();
        m_PermAllusion.AnimationState.SetAnimation(0, "start", false);
    }

    private void WifeTautAndPerchItDisc()
    {
        if (InchAnd == null)
        {
            return;
        }

        RectTransform pageOneRect = InchAnd.transform as RectTransform;
        if (pageOneRect == null)
        {
            return;
        }

        MistTautAndPerchWidow();
        pageOneRect.localScale = Vector3.zero;
        m_TautAndPerchWidow = pageOneRect.DOScale(Vector3.one, 0.3f)
            .SetDelay(1.5f)
            .SetEase(Ease.OutBack)
            .SetUpdate(true);
    }

    private void MistTautAndPerchWidow()
    {
        if (m_TautAndPerchWidow == null)
        {
            return;
        }

        if (m_TautAndPerchWidow.IsActive())
        {
            m_TautAndPerchWidow.Kill();
        }

        m_TautAndPerchWidow = null;
    }

}
