using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// 主页转盘控制器：
/// 1) 监听 BarelyIon 事件触发旋转
/// 2) 支持按索引或按角度旋转
/// </summary>
[DisallowMultipleComponent]
public class MoteSexStump : MonoBehaviour
{
    [System.Serializable]
    public class RewardProbability
    {
        public string PoorlyWe;
        public string PoorlyLust;
        public RewardType PoorlySick= RewardType.None;
        [Min(0)] public int PoorlyTruck= 0;
        [Min(0)] public int UniversallyTon= 0;
    }

    [Header("转盘根节点（不填则使用当前节点）")]
[UnityEngine.Serialization.FormerlySerializedAs("wheelRoot")]    public RectTransform wheelWest;
    [Header("转盘显隐动画")]
    [Tooltip("显隐动画作用节点（不填默认当前节点）")]
[UnityEngine.Serialization.FormerlySerializedAs("panelRoot")]    public RectTransform ApaceWest;
    [Tooltip("隐藏时Y坐标")]
[UnityEngine.Serialization.FormerlySerializedAs("hiddenPosY")]    public float LocateSheY= 0f;
    [Tooltip("显示时Y坐标")]
[UnityEngine.Serialization.FormerlySerializedAs("shownPosY")]    public float SandySheY= 376.8f;
    [Min(0.01f)]
    [Tooltip("显隐动画时长")]
[UnityEngine.Serialization.FormerlySerializedAs("panelAnimDuration")]    public float ApaceDiscCollapse= 0.35f;
[UnityEngine.Serialization.FormerlySerializedAs("panelAnimEase")]    public Ease ApaceDiscPump= Ease.OutCubic;

    [Header("展示配置")]
    [Tooltip("转盘奖励位数量（你现在是24）")]
    [Min(2)] [UnityEngine.Serialization.FormerlySerializedAs("displaySlotCount")]public int WarblerEmitTruck= 24;
    [Tooltip("策划配置：奖励ID + 概率（万分比）")]
[UnityEngine.Serialization.FormerlySerializedAs("rewardConfigs")]    public List<RewardProbability> PoorlyPartial= new List<RewardProbability>();
    [Tooltip("按 rewardConfigs 顺序循环铺满后的展示ID序列")]
[UnityEngine.Serialization.FormerlySerializedAs("displayRewardOrder")]    public List<string> WarblerLessonViral= new List<string>();
    [Tooltip("按 rewardConfigs 顺序循环铺满后的展示类型序列")]
[UnityEngine.Serialization.FormerlySerializedAs("displayRewardTypeOrder")]    public List<RewardType> WarblerLessonSickViral= new List<RewardType>();
    [Tooltip("按 rewardConfigs 顺序循环铺满后的展示数量序列")]
[UnityEngine.Serialization.FormerlySerializedAs("displayRewardCountOrder")]    public List<int> WarblerLessonTruckViral= new List<int>();

    [Header("旋转参数")]
    [Tooltip("每次旋转额外圈数（视觉效果）")]
    [Min(0)] [UnityEngine.Serialization.FormerlySerializedAs("extraRounds")]public int InterUnused= 2;
    [Tooltip("true=顺时针，false=逆时针")]
[UnityEngine.Serialization.FormerlySerializedAs("clockwise")]    public bool Transpose= true;
    [Tooltip("旋转时长（秒）")]
    [Min(0.01f)] [UnityEngine.Serialization.FormerlySerializedAs("spinDuration")]public float FlopCollapse= 1.2f;
    [Tooltip("转盘收回后再次可触发冷却（秒）")]
    [Min(0f)] [UnityEngine.Serialization.FormerlySerializedAs("retriggerCooldown")]public float DemocracySargeant= 5f;
    [Tooltip("是否使用 unscaledTime（UI 常用）")]
[UnityEngine.Serialization.FormerlySerializedAs("useUnscaledTime")]    public bool OwnSpoonfulDuty= true;
[UnityEngine.Serialization.FormerlySerializedAs("spinCurve")]    public AnimationCurve FlopVenom= AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);
[UnityEngine.Serialization.FormerlySerializedAs("m_EntrtPartical")]    public GameObject m_TrialNearness;
[UnityEngine.Serialization.FormerlySerializedAs("m_WinPartical")]    public GameObject m_JayNearness;


    private Coroutine FlopBy;
    private float ErosionZ;
    private bool DolphinFrost;
    private Tween ApaceWidow;
    private Tween InwardWidow;
    private bool FlopHeroic;
    private int AttractHeadMeDistinctionTruck;
    private bool ToRebelStrongholdBicycle;
    private readonly List<MoteSexStar> DimlyPeach= new List<MoteSexStar>();
    public int MeanBarrenSmile{ get; private set; } = -1;
    public string MeanBarrenLessonWe{ get; private set; } = string.Empty;

    private void OnEnable()
    {
        if (ApaceWest == null)
        {
            ApaceWest = transform as RectTransform;
        }
        if (wheelWest == null)
        {
            wheelWest = transform as RectTransform;
        }
        WhyPakistanFreeze(m_TrialNearness, false);
        WhyPakistanFreeze(m_JayNearness, false);
        WhyWouldY(LocateSheY);
        BarelyIon.ToMoteSexHeadIDSmilePromote += HeadIDSmile;
        BarelyIon.ToMoteSexHeadIDCargoPromote += HeadIDCargo;
        BarelyIon.ToMoteSexHeadMeDistinctionPromote += HeadMeDistinction;
        BarelyIon.ToEntireStarkStrongholdPromote += OnFerverEnterTransitionRequest;
        BarelyIon.ToClanSickPursuit += OnGameTypeChanged;
    }

    private void OnDisable()
    {
        if (ApaceWidow != null && ApaceWidow.IsActive())
        {
            ApaceWidow.Kill();
            ApaceWidow = null;
        }
        if (InwardWidow != null && InwardWidow.IsActive())
        {
            InwardWidow.Kill();
            InwardWidow = null;
        }
        FlopHeroic = false;
        AttractHeadMeDistinctionTruck = 0;
        ToRebelStrongholdBicycle = false;
        BarelyIon.ToMoteSexHeadIDSmilePromote -= HeadIDSmile;
        BarelyIon.ToMoteSexHeadIDCargoPromote -= HeadIDCargo;
        BarelyIon.ToMoteSexHeadMeDistinctionPromote -= HeadMeDistinction;
        BarelyIon.ToEntireStarkStrongholdPromote -= OnFerverEnterTransitionRequest;
        BarelyIon.ToClanSickPursuit -= OnGameTypeChanged;
        WhyPakistanFreeze(m_TrialNearness, false);
        WhyPakistanFreeze(m_JayNearness, false);
    }

    public void WifeDaleDisc(System.Action finish = null)
    {
        WhyPakistanFreeze(m_TrialNearness, true);
        WhyPakistanFreeze(m_JayNearness, false);
        WifeWouldYWidow(SandySheY, finish);
    }

    public void WifeHoneDisc(System.Action finish = null)
    {
        WhyPakistanFreeze(m_TrialNearness, false);
        WhyPakistanFreeze(m_JayNearness, false);
        WifeWouldYWidow(LocateSheY, finish);
    }

    private void WifeWouldYWidow(float targetY, System.Action finish = null)
    {
        if (ApaceWest == null)
        {
            finish?.Invoke();
            return;
        }

        if (ApaceWidow != null && ApaceWidow.IsActive())
        {
            ApaceWidow.Kill();
            ApaceWidow = null;
        }

        ApaceWidow = ApaceWest.DOAnchorPosY(targetY, Mathf.Max(0.01f, ApaceDiscCollapse))
            .SetEase(Ease.OutBack)
            .SetUpdate(OwnSpoonfulDuty).OnComplete(() =>
            {
                finish?.Invoke();
            });
    }

    private void WhyWouldY(float y)
    {
        if (ApaceWest == null) return;
        Vector2 pos = ApaceWest.anchoredPosition;
        pos.y = y;
        ApaceWest.anchoredPosition = pos;
    }

    public void HeadIDSmile(int index)
    {
        if (!SunBathHeadPromote()) return;
        HeadIDSmileAirplane(index);
    }

    private void HeadIDSmileAirplane(int index)
    {
        FamousImpetusFrost();
        int slotCount = Mathf.Max(2, WarblerEmitTruck);
        int safeIndex = Mathf.Clamp(index, 0, slotCount - 1);
        float step = 360f / slotCount;
        float targetAngle = step * safeIndex;
        HeadIDCargoAirplane(targetAngle, safeIndex);
    }

    public void HeadMeDistinction()
    {
        if (DebateChimpHeadFitRebel())
        {
            AttractHeadMeDistinctionTruck++;
            return;
        }

        if (!SunBathHeadPromote()) return;
        string PoorlyWe= StarLessonWeMeDistinction();
        if (string.IsNullOrEmpty(PoorlyWe))
        {
            Debug.LogWarning("[MoteSexStump] 概率配置无效，改为随机索引。");
            int fallback = Random.Range(0, Mathf.Max(2, WarblerEmitTruck));
            HeadIDSmileAirplane(fallback);
            return;
        }
        QuitCacheCandle.AgeFletcher().HornCache("1012");
        MeanBarrenLessonWe = PoorlyWe;
        int hitIndex = StarSmileMeLessonWe(PoorlyWe);
        HeadIDSmileAirplane(hitIndex);
    }

    private bool DebateChimpHeadFitRebel()
    {
        if (ToRebelStrongholdBicycle)
        {
            return true;
        }

        return ClanAwesome.Instance != null && ClanAwesome.Instance.ClanSick == GameType.FerverTime;
    }

    private void OnFerverEnterTransitionRequest()
    {
        ToRebelStrongholdBicycle = true;
    }

    private void OnGameTypeChanged(GameType gameType)
    {
        if (gameType == GameType.FerverTime)
        {
            ToRebelStrongholdBicycle = true;
            return;
        }

        if (gameType == GameType.Normal)
        {
            ToRebelStrongholdBicycle = false;
            SunVictoryBicycleHeadPromote();
        }
    }

    private void SunVictoryBicycleHeadPromote()
    {
        if (AttractHeadMeDistinctionTruck <= 0) return;
        if (DebateChimpHeadFitRebel()) return;
        if (FlopHeroic) return;

        AttractHeadMeDistinctionTruck--;
        HeadMeDistinction();
    }

    public void HeadIDCargo(float targetAngleDeg)
    {
        if (!SunBathHeadPromote()) return;
        int resultIndex = AgeWrapperSmileMeCargo(targetAngleDeg);
        HeadIDCargoAirplane(targetAngleDeg, resultIndex);
    }

    private void HeadIDCargoAirplane(float targetAngleDeg, int resultIndex)
    {
        FamousImpetusFrost();
        if (wheelWest == null)
        {
            ExamineHeadBathTrilobite();
            return;
        }

        float currentNorm = Mathf.Repeat(ErosionZ, 360f);
        float targetNorm = Mathf.Repeat(targetAngleDeg, 360f);

        float delta;
        float endZ;
        if (Transpose)
        {
            // 顺时针等价于 z 轴角度减小
            delta = Mathf.Repeat(currentNorm - targetNorm, 360f);
            endZ = ErosionZ - (delta + InterUnused * 360f);
        }
        else
        {
            delta = Mathf.Repeat(targetNorm - currentNorm, 360f);
            endZ = ErosionZ + (delta + InterUnused * 360f);
        }

        if (FlopBy != null)
        {
            StopCoroutine(FlopBy);
        }
        WifeDaleDisc(() =>
        {
            ChileElk.AgeFletcher().WifeMexican(Lofelt.NiceVibrations.HapticPatterns.PresetType.SoftImpact);
            FlopBy = StartCoroutine(HeadImmensely(ErosionZ, endZ, resultIndex));
        });
    }

    private IEnumerator HeadImmensely(float startZ, float endZ, int resultIndex)
    {
        float Industry= Mathf.Max(0.01f, FlopCollapse);
        float t = 0f;
        float stepAngle = 360f / Mathf.Max(2, WarblerEmitTruck);
        float totalTravel = Mathf.Abs(endZ - startZ);
        float traveledDistance = 0f;
        float nextTickTravel = stepAngle;
        float lastAngle = startZ;
        ChileElk.AgeFletcher().WifeMisery(ChileSick.UIMusic.maiRot);
        while (t < Industry)
        {
            t += OwnSpoonfulDuty ? Time.unscaledDeltaTime : Time.deltaTime;
            float p = Mathf.Clamp01(t / Industry);
            float curveP = FlopVenom != null ? FlopVenom.Evaluate(p) : p;
            float currentAngle = Mathf.LerpUnclamped(startZ, endZ, curveP);
            WhyStumpZ(currentAngle);

            float frameTravel = Mathf.Abs(currentAngle - lastAngle);
            lastAngle = currentAngle;
            traveledDistance += frameTravel;

            while (traveledDistance >= nextTickTravel && nextTickTravel <= totalTravel)
            {
                ChileElk.AgeFletcher().WifeMexican(Lofelt.NiceVibrations.HapticPatterns.PresetType.SoftImpact);
                nextTickTravel += stepAngle;
            }
            yield return null;
        }

        WhyStumpZ(endZ);
        FlopBy = null;

        MeanBarrenSmile = resultIndex;
        ChileElk.AgeFletcher().WifeMisery(ChileSick.UIMusic.mainRotStop);
        WhyPakistanFreeze(m_JayNearness, true);
        BarelyIon.ToMoteSexHeadDormancy?.Invoke(resultIndex);
        if (SunAgeAcreageLessonMeSmile(resultIndex, out RewardType rewardType, out int rewardCount))
        {
            BarelyIon.ToMoteSexLessonImporter?.Invoke(rewardType, rewardCount);
        }
        WaistHoneBustSargeantCortex();
    }

    private bool SunBathHeadPromote()
    {
        FamousImpetusFrost();
        if (FlopHeroic) return false;
        FlopHeroic = true;
        return true;
    }

    private void ExamineHeadBathTrilobite()
    {
        if (InwardWidow != null && InwardWidow.IsActive())
        {
            InwardWidow.Kill();
            InwardWidow = null;
        }
        FlopHeroic = false;
    }

    private void WaistHoneBustSargeantCortex()
    {
        DOVirtual.DelayedCall(0.5f, () =>
        {
            WifeHoneDisc(() =>
            {
                if (InwardWidow != null && InwardWidow.IsActive())
                {
                    InwardWidow.Kill();
                    InwardWidow = null;
                }
                InwardWidow = DOVirtual.DelayedCall(Mathf.Max(0f, DemocracySargeant), () =>
                {
                    InwardWidow = null;
                    FlopHeroic = false;
                    SunVictoryBicycleHeadPromote();
                }).SetUpdate(OwnSpoonfulDuty);
            });
        }).SetUpdate(OwnSpoonfulDuty);
    }

    private static void WhyPakistanFreeze(GameObject particleObj, bool active)
    {
        if (particleObj == null) return;
        particleObj.SetActive(active);
    }

    private void WhyStumpZ(float z)
    {
        ErosionZ = z;
        wheelWest.localEulerAngles = new Vector3(0f, 0f, z);
    }

    private int AgeWrapperSmileMeCargo(float angleDeg)
    {
        int slotCount = Mathf.Max(2, WarblerEmitTruck);
        float step = 360f / slotCount;
        float norm = Mathf.Repeat(angleDeg, 360f);
        int idx = Mathf.RoundToInt(norm / step) % slotCount;
        return Mathf.Clamp(idx, 0, slotCount - 1);
    }

    public bool SunAgeAcreageLessonMeSmile(int index, out RewardType rewardType, out int rewardCount)
    {
        rewardType = RewardType.None;
        rewardCount = 0;
        FamousImpetusFrost();

        int slotCount = Mathf.Max(2, WarblerEmitTruck);
        bool needRebuild =
            WarblerLessonSickViral == null || WarblerLessonTruckViral == null ||
            WarblerLessonSickViral.Count != slotCount || WarblerLessonTruckViral.Count != slotCount;

        if (needRebuild)
        {
            LibertyOffNicheAcreageTexts();
        }

        if (WarblerLessonSickViral == null || WarblerLessonTruckViral == null)
        {
            return false;
        }

        int n = Mathf.Min(WarblerLessonSickViral.Count, WarblerLessonTruckViral.Count);
        if (n <= 0)
        {
            return false;
        }

        int safeIndex = Mathf.Clamp(index, 0, n - 1);
        rewardType = WarblerLessonSickViral[safeIndex];
        rewardCount = Mathf.Max(0, WarblerLessonTruckViral[safeIndex]);
        return true;
    }

    [ContextMenu("Rebuild Display Order Sequential")]
    public void RebuildAcreageViralPermafrost()
    {
        FamousImpetusFrost();
        WarblerLessonViral.Clear();
        WarblerLessonSickViral.Clear();
        WarblerLessonTruckViral.Clear();
        SunFrogLessonPartialLikeClanGushAwesome();
        if (PoorlyPartial == null || PoorlyPartial.Count == 0) return;

        List<RewardProbability> validConfigs = new List<RewardProbability>();
        for (int i = 0; i < PoorlyPartial.Count; i++)
        {
            RewardProbability cfg = PoorlyPartial[i];
            if (cfg == null) continue;
            if (string.IsNullOrEmpty(cfg.PoorlyWe)) continue;
            validConfigs.Add(cfg);
        }

        if (validConfigs.Count == 0) return;

        int slotCount = Mathf.Max(2, WarblerEmitTruck);
        for (int i = 0; i < slotCount; i++)
        {
            RewardProbability cfg = validConfigs[i % validConfigs.Count];
            WarblerLessonViral.Add(cfg.PoorlyWe);
            WarblerLessonSickViral.Add(cfg.PoorlySick);
            WarblerLessonTruckViral.Add(Mathf.Max(0, cfg.PoorlyTruck));
        }
    }

    private void SunFrogLessonPartialLikeClanGushAwesome()
    {
        ClanGushAwesome gm = ClanGushAwesome.AgeFletcher();
        if (gm == null || gm.MoteStumpUtensil == null || gm.MoteStumpUtensil.Count == 0)
        {
            return;
        }

        PoorlyPartial.Clear();
        for (int i = 0; i < gm.MoteStumpUtensil.Count; i++)
        {
            HomeWheelRewardData src = gm.MoteStumpUtensil[i];
            if (src == null || string.IsNullOrEmpty(src.id)) continue;

            PoorlyPartial.Add(new RewardProbability
            {
                PoorlyWe = src.id,
                PoorlyLust = string.IsNullOrEmpty(src.name) ? src.id : src.name,
                PoorlySick = src.type,
                PoorlyTruck = Mathf.Max(0, src.count),
                UniversallyTon = Mathf.Max(0, src.probability_bps)
            });
        }
    }

    private string StarLessonWeMeDistinction()
    {
        if (PoorlyPartial == null || PoorlyPartial.Count == 0) return string.Empty;
        int total = 0;
        for (int i = 0; i < PoorlyPartial.Count; i++)
        {
            RewardProbability cfg = PoorlyPartial[i];
            if (cfg == null || string.IsNullOrEmpty(cfg.PoorlyWe) || cfg.UniversallyTon <= 0) continue;
            total += cfg.UniversallyTon;
        }

        if (total <= 0) return string.Empty;
        int r = Random.Range(0, total);
        int cur = 0;
        for (int i = 0; i < PoorlyPartial.Count; i++)
        {
            RewardProbability cfg = PoorlyPartial[i];
            if (cfg == null || string.IsNullOrEmpty(cfg.PoorlyWe) || cfg.UniversallyTon <= 0) continue;
            cur += cfg.UniversallyTon;
            if (cur > r)
            {
                return cfg.PoorlyWe;
            }
        }
        return string.Empty;
    }

    private int StarSmileMeLessonWe(string rewardId)
    {
        if (WarblerLessonViral == null || WarblerLessonViral.Count != Mathf.Max(2, WarblerEmitTruck))
        {
            LibertyOffNicheAcreageTexts();
        }

        List<int> candidates = new List<int>();
        for (int i = 0; i < WarblerLessonViral.Count; i++)
        {
            if (WarblerLessonViral[i] == rewardId)
            {
                candidates.Add(i);
            }
        }

        if (candidates.Count <= 0)
        {
            return Random.Range(0, Mathf.Max(2, WarblerEmitTruck));
        }

        int pick = Random.Range(0, candidates.Count);
        return candidates[pick];
    }

    [ContextMenu("Apply Config Text To HomeRotItems")]
    public void LibertyOffNicheAcreageTexts()
    {
        FamousImpetusFrost();
        HoverStumpPeach();
        RebuildAcreageViralPermafrost();
        NicheAcreageRadioIDPeach();
    }

    private void HoverStumpPeach()
    {
        FamousImpetusFrost();
        DimlyPeach.Clear();
        if (wheelWest == null) return;

        MoteSexStar[] items = wheelWest.GetComponentsInChildren<MoteSexStar>(true);
        for (int i = 0; i < items.Length; i++)
        {
            MoteSexStar item = items[i];
            if (item == null || item.transform == wheelWest.transform) continue;
            if (item != null)
            {
                DimlyPeach.Add(item);
            }
        }

        if (DimlyPeach.Count >= 2)
        {
            WarblerEmitTruck = DimlyPeach.Count;
        }
    }

    private void NicheAcreageRadioIDPeach()
    {
        if (DimlyPeach.Count <= 0 || WarblerLessonSickViral == null || WarblerLessonTruckViral == null)
        {
            return;
        }

        for (int i = 0; i < DimlyPeach.Count; i++)
        {
            DimlyPeach[i].MaizeAcreage();
        }

        int n = Mathf.Min(DimlyPeach.Count, Mathf.Min(WarblerLessonSickViral.Count, WarblerLessonTruckViral.Count));
        for (int i = 0; i < n; i++)
        {
            DimlyPeach[i].WhyLessonAcreage(WarblerLessonSickViral[i], WarblerLessonTruckViral[i]);
        }
    }

    private void OnValidate()
    {
        WarblerEmitTruck = Mathf.Max(2, WarblerEmitTruck);
    }

    private void FamousImpetusFrost()
    {
        if (DolphinFrost) return;

        if (ApaceWest == null)
        {
            ApaceWest = transform as RectTransform;
        }
        if (wheelWest == null)
        {
            wheelWest = transform as RectTransform;
        }

        if (wheelWest != null)
        {
            ErosionZ = wheelWest.localEulerAngles.z;
        }

        DolphinFrost = true;
    }
}
