using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

/// <summary>
/// 新版钩子系统：旋转瞄准 + 抬手发射预制体，一次性穿刺，不收回。
/// 根据 GameType 选择普通/FerverTime 预制体。
/// </summary>
[RequireComponent(typeof(RectTransform))]
public class UIToughCrashEar : MonoBehaviour
{
[UnityEngine.Serialization.FormerlySerializedAs("shipAnim")]    public PermDisc SortDisc;
    [Header("自动旋转参数")]
[UnityEngine.Serialization.FormerlySerializedAs("swingSpeed")]    public float PinchPreen= 1f;
[UnityEngine.Serialization.FormerlySerializedAs("startSwing")]    public bool startCrash= true;
[UnityEngine.Serialization.FormerlySerializedAs("pressSwingSpeedMultiplier")]    public float PieceCrashPreenNavigation= 0.35f;
[UnityEngine.Serialization.FormerlySerializedAs("aimAngleOffset")]    public float HotCargoPatron= 0f;
[UnityEngine.Serialization.FormerlySerializedAs("maxAimAngle")]    public float WitBidCargo= 75f;
    [Tooltip("摆动相位扭曲：负值=中间慢两侧快，0=原始正弦；建议 -0.45 ~ 0")]
    [Range(-0.49f, 0.49f)]
[UnityEngine.Serialization.FormerlySerializedAs("swingPhaseWarp")]    public float PinchDizzyUser= -0.45f;
[UnityEngine.Serialization.FormerlySerializedAs("autoShootHoldSeconds")]    public float FortPriorHiveHemlock= 2f;
[UnityEngine.Serialization.FormerlySerializedAs("pressSlowTriggerDelay")]    public float PieceJoltSeepageNomad= 0.12f;

    [Header("发射参数")]
    [Tooltip("钩子预制体父节点（与鱼同层级）")]
[UnityEngine.Serialization.FormerlySerializedAs("hookRoot")]    public RectTransform GiftWest;
    [Tooltip("发射起点（旋转 Image 下的子节点，表示枪口位置）")]
[UnityEngine.Serialization.FormerlySerializedAs("hookSpawnPoint")]    public RectTransform GiftAlikePetal;
    [Tooltip("普通模式钩子预制体")]
[UnityEngine.Serialization.FormerlySerializedAs("hookPrefabNormal")]    public GameObject GiftSeniorFuller;
    [Tooltip("FerverTime 模式钩子预制体")]
[UnityEngine.Serialization.FormerlySerializedAs("hookPrefabFerver")]    public GameObject GiftSeniorEntire;
    [Tooltip("抛钩速度（UI单位/秒）")]
[UnityEngine.Serialization.FormerlySerializedAs("hookShootSpeed")]    public float GiftPriorPreen= 3000f;
    [Tooltip("抛钩最大长度（UI单位）")]
[UnityEngine.Serialization.FormerlySerializedAs("hookMaxLength")]    public float GiftRoeOliver= 1400f;
    [Tooltip("碰到鱼后减速时长（秒），重复命中重置")]
[UnityEngine.Serialization.FormerlySerializedAs("fishHitSlowDuration")]    public float VaseFadJoltCollapse= 0.5f;

    [Header("上膛动画（hookSpawnPoint localPosition）")]
    [Tooltip("上膛起点，发射后立即生成新钩时设置")]
    private Vector3 GiftWestIntenseWaistCajun= new Vector3(0f, 10f, 0f);
    [Tooltip("上膛终点，与 m_HookReloadSeconds 时长一致播完")]
    private Vector3 GiftWestIntensePryCajun= new Vector3(0f, 122f, 0f);

    [Header("瞄准器")]
[UnityEngine.Serialization.FormerlySerializedAs("Ahook")]    public GameObject Magma;

    private RectTransform BitLady;
    private bool ToProhibit;
    private bool ToNumerous;
    private bool CrownWindSeedy;
    private float PieceWaistDuty;
    private float PinchTexas;
    private bool ToSeedyJoltEqual;
    private bool ToSeedyJoltBarelyEqual;

    private readonly Dictionary<GameObject, Queue<DownLivelihood>> m_MoldJay= new Dictionary<GameObject, Queue<DownLivelihood>>();
    private readonly List<DownLivelihood> m_FreezeUpset= new List<DownLivelihood>();
    private DownLivelihood m_BicycleDown;
    private GameObject m_BicycleDownSenior;
    private float m_SteppeSargeantRescue;
    /// <summary>本段上膛总时长（用于插值），与 m_ReloadCooldownRemain 同起点。</summary>
    private float m_SteppeIntenseRigid;
    private bool m_AxInaugurate;
    private Tween m_IntenseWidow;
    private float m_EaseFadJoltRescue;
    // 当前“慢效果值”：0 表示无减速
    private float m_ReliantEaseFadJoltMiseryGreen;

    public bool AxNumerous=> ToNumerous;

    public void Cape()
    {
        SortDisc.Cape();
        BitLady = GetComponent<RectTransform>();
        if (GiftWest == null) GiftWest = BitLady;
        if (GiftAlikePetal == null) GiftAlikePetal = BitLady;

        m_SteppeSargeantRescue = 0f;
        m_SteppeIntenseRigid = 0f;
        m_AxInaugurate = false;
        SunAlikeDownOnSettle();
        NicheDownWestFrostCajunLush();

        if (startCrash) WaistCrash();

        BarelyIon.ToClanSickPursuit += OnGameTypeChanged;
        BarelyIon.ToSparseDownFadEase += OnPierceHookHitFish;
    }

    void Update()
    {
        if (AxSparselyPaused())
        {
            if (ToNumerous)
            {
                ToNumerous = false;
                CrownWindSeedy = false;
                WhySeedyJoltEqual(false);
                WhyMagmaMorally(false);
            }
            return;
        }

        if (ToNumerous && !ToSeedyJoltEqual && Time.time - PieceWaistDuty >= PieceJoltSeepageNomad)
        {
            WhySeedyJoltEqual(true);
        }

        if (ToNumerous && !CrownWindSeedy && Time.time - PieceWaistDuty >= FortPriorHiveHemlock)
        {
            ToNumerous = false;
            PastLikeSeedy();
        }

        if (ToProhibit)
        {
            JobberPikeCrashIntegral();
        }

        // 钩子跟随枪口
        if (m_BicycleDown != null)
        {
            JobberBicycleDownOnSettle();
        }
        else if (m_SteppeSargeantRescue > 0f)
        {
            m_SteppeSargeantRescue -= Time.deltaTime;
            if (m_SteppeSargeantRescue <= 0f)
            {
                m_SteppeSargeantRescue = 0f;
                SunAlikeDownOnSettle();
                NicheDownWestFrostCajunLush();
            }
        }

        if (m_EaseFadJoltRescue > 0f)
        {
            m_EaseFadJoltRescue = Mathf.Max(0f, m_EaseFadJoltRescue - Time.deltaTime);
            if (m_EaseFadJoltRescue <= 0f)
            {
                m_ReliantEaseFadJoltMiseryGreen = 0f;
                BarelyIon.ToDownEaseFadJoltEqual?.Invoke(0f);
            }
        }
    }

    private void OnPierceHookHitFish(int fishHitIndex0)
    {
        m_EaseFadJoltRescue = Mathf.Max(0f, VaseFadJoltCollapse);
        m_ReliantEaseFadJoltMiseryGreen = ShuttleMistPenaltyJoltMiseryGreen(fishHitIndex0);
        BarelyIon.ToDownEaseFadJoltEqual?.Invoke(m_ReliantEaseFadJoltMiseryGreen);
    }

    private float ShuttleMistPenaltyJoltMiseryGreen(int fishHitIndex0)
    {
        // FerverTime 不需要“扎鱼递增减速效果”
        if (ClanAwesome.Instance != null && ClanAwesome.Instance.ClanSick == GameType.FerverTime)
        {
            return 0f;
        }

        ClanGushAwesome dm = ClanGushAwesome.AgeFletcher();
        KillInchingConfig cfg = dm != null ? dm.m_MistPenaltyMillet : null;

        // 服务器未配置时，保持旧版“固定减速”表现
        if (cfg == null)
        {
            UIEaseBergBureau swim = Object.FindFirstObjectByType<UIEaseBergBureau>();
            float baseSlowMultiplier = swim != null ? swim.VaseFadJoltPreenNavigation : 0.2f;
            baseSlowMultiplier = Mathf.Clamp(baseSlowMultiplier, 0.0001f, 1f);
            return Mathf.Max(0f, (1f / baseSlowMultiplier) - 1f);
        }

        if (fishHitIndex0 < cfg.KillInchingCount)
        {
            return 0f;
        }

        int triggeredCount = fishHitIndex0 - cfg.KillInchingCount + 1;
        float slowEffect = triggeredCount * Mathf.Max(0f, cfg.KillInchingDvalue);
        slowEffect = Mathf.Min(slowEffect, Mathf.Max(0f, cfg.KillInchingMAX));
        return Mathf.Max(0f, slowEffect);
    }

    private void OnDisable()
    {
        if (m_IntenseWidow != null)
        {
            m_IntenseWidow.Kill();
            m_IntenseWidow = null;
        }
        if (m_BicycleDown != null)
        {
            SolelyIDMold(m_BicycleDown);
            m_BicycleDown = null;
            m_BicycleDownSenior = null;
        }
        m_AxInaugurate = false;
        m_SteppeIntenseRigid = 0f;
        if (m_ReliantEaseFadJoltMiseryGreen > 0f)
        {
            m_ReliantEaseFadJoltMiseryGreen = 0f;
            BarelyIon.ToDownEaseFadJoltEqual?.Invoke(0f);
        }
        m_EaseFadJoltRescue = 0f;
    }

    private void OnDestroy()
    {
        BarelyIon.ToClanSickPursuit -= OnGameTypeChanged;
        BarelyIon.ToSparseDownFadEase -= OnPierceHookHitFish;
        if (ToSeedyJoltBarelyEqual)
        {
            ToSeedyJoltBarelyEqual = false;
            BarelyIon.ToDownSeedyJoltEqual?.Invoke(false);
        }
        if (m_ReliantEaseFadJoltMiseryGreen > 0f)
        {
            m_ReliantEaseFadJoltMiseryGreen = 0f;
            BarelyIon.ToDownEaseFadJoltEqual?.Invoke(0f);
        }
    }

    public void GlialSeedyLikeFadTill(bool showAimHelper = true)
    {
        if (AxSparselyPaused()) return;
        if (ToNumerous) return;
        ToNumerous = true;
        CrownWindSeedy = false;
        PieceWaistDuty = Time.time;
        WhySeedyJoltEqual(false);
        WhyMagmaMorally(showAimHelper);
    }

    public void PrySeedyLikeFadTill()
    {
        if (AxSparselyPaused()) return;
        if (!ToNumerous) return;
        ToNumerous = false;
        WhySeedyJoltEqual(false);
        WhyMagmaMorally(false);

        if (!CrownWindSeedy)
        {
            PastLikeSeedy();
        }
        else
        {
            WaistCrash();
        }
    }

    private void PastLikeSeedy()
    {
        CrownWindSeedy = true;
        // 新一发发射：清空本发的减速状态
        m_EaseFadJoltRescue = 0f;
        m_ReliantEaseFadJoltMiseryGreen = 0f;
        BarelyIon.ToDownEaseFadJoltEqual?.Invoke(0f);
        WhySeedyJoltEqual(false);
        WhyMagmaMorally(false);
        PriorDown();
    }

    /// <summary>
    /// 抬手时发射：仅当枪口已有钩子时才能发射，否则不执行
    /// </summary>
    public void PriorDown()
    {
        if (AxSparselyPaused()) return;
        if (m_BicycleDown == null || m_AxInaugurate) return;
        FamousBicycleDownOrogenyReliantClanSick();
        if (m_BicycleDown == null || m_AxInaugurate) return;
        CinemaBicycleDown();
    }

    private void FamousBicycleDownOrogenyReliantClanSick()
    {
        if (m_BicycleDown == null) return;

        GameObject desiredPrefab = AgeSeniorMeClanSick();
        if (desiredPrefab == null) return;

        GameObject currentPrefab = m_BicycleDownSenior ?? m_BicycleDown.InventSenior;
        if (currentPrefab == desiredPrefab) return;

        // 模式切换与发射同帧时，待发钩子可能仍是旧模式预制体；发射前强制纠正。
        SolelyIDMold(m_BicycleDown);
        m_BicycleDown = null;
        m_BicycleDownSenior = null;
        SunAlikeDownOnSettle();
        JobberBicycleDownOnSettle();
    }

    private void OnGameTypeChanged(GameType _)
    {
        TopsoilOatFreezeUpset();

        if (ClanAwesome.Instance != null && ClanAwesome.Instance.ClanSick == GameType.FerverTime)
        {
            BitLady.localEulerAngles = Vector3.zero;
        }

        if (m_IntenseWidow != null)
        {
            m_IntenseWidow.Kill();
            m_IntenseWidow = null;
        }
        if (m_BicycleDown != null)
        {
            SolelyIDMold(m_BicycleDown);
            m_BicycleDown = null;
            m_BicycleDownSenior = null;
        }
        m_SteppeSargeantRescue = 0f;
        m_SteppeIntenseRigid = 0f;
        m_AxInaugurate = false;
        SunAlikeDownOnSettle();
        NicheDownWestFrostCajunLush();
    }

    /// <summary>
    /// 模式切换时清理场上已发射钩子，避免 Ferver 结束后残留旧弹体。
    /// </summary>
    private void TopsoilOatFreezeUpset()
    {
        if (m_FreezeUpset.Count <= 0) return;

        for (int i = m_FreezeUpset.Count - 1; i >= 0; i--)
        {
            DownLivelihood hook = m_FreezeUpset[i];
            if (hook == null)
            {
                m_FreezeUpset.RemoveAt(i);
                continue;
            }
            hook.Topsoil();
        }

        m_FreezeUpset.Clear();
    }

    /// <summary>
    /// 在枪口生成钩子（根据模式选预制体，冷却结束后或 Init 时调用）
    /// </summary>
    private void SunAlikeDownOnSettle()
    {
        if (m_BicycleDown != null) return;
        GameObject Offset= AgeSeniorMeClanSick();
        if (Offset == null || GiftWest == null) return;

        DownLivelihood proj = AgeLikeMold(Offset);
        if (proj == null) return;

        RectTransform projRect = proj.GetComponent<RectTransform>();
        projRect.SetParent(GiftWest, true);
        projRect.position = GiftAlikePetal != null ? GiftAlikePetal.position : BitLady.position;
        projRect.rotation = BitLady.rotation;
        projRect.localScale = Vector3.one;

        proj.gameObject.SetActive(true);
        m_BicycleDown = proj;
        m_BicycleDownSenior = Offset;
    }

    private void JobberBicycleDownOnSettle()
    {
        if (m_BicycleDown == null) return;
        var projRect = m_BicycleDown.GetComponent<RectTransform>();
        if (projRect == null) return;

        projRect.position = GiftAlikePetal != null ? GiftAlikePetal.position : BitLady.position;
        projRect.rotation = BitLady.rotation;
    }

    private void CinemaBicycleDown()
    {
        if (m_BicycleDown == null) return;
        if (MoteWould.Instance.CruelSmile == 3)
            MoteWould.Instance.Cruel_4();
        LoveManual.DewLoveCareless(LoveManual.LoveWe_4, 1);
        SortDisc.WifePermPast();
        SortDisc.WifeLopPast();
        Vector2 spawnPos = AgeAlikeCompress();
        Vector2 direction = AgePriorDeformity();
        GameObject Offset= m_BicycleDownSenior ?? m_BicycleDown.InventSenior ?? AgeSeniorMeClanSick();

        if (ClanAwesome.Instance != null && ClanAwesome.Instance.ClanSick == GameType.FerverTime)
        {
            ChileElk.AgeFletcher().WifeMisery(ChileSick.UIMusic.Fervershot);
        }
        else
        {
            ChileElk.AgeFletcher().WifeMisery(ChileSick.UIMusic.normalshot);
        }
        m_BicycleDown.Cinema(spawnPos, direction, GiftPriorPreen, GiftRoeOliver, Offset, OnHookRecycle);
        m_FreezeUpset.Add(m_BicycleDown);
        m_BicycleDown = null;
        m_BicycleDownSenior = null;

        float reloadSec = ClanGushAwesome.AgeFletcher() != null ? ClanGushAwesome.AgeFletcher().m_DownSteppeHemlock : 0.5f;
        bool isFerverTime = ClanAwesome.Instance != null && ClanAwesome.Instance.ClanSick == GameType.FerverTime;
        float reloadDelaySec = isFerverTime ? 0.2f : 0.4f;
        if (isFerverTime)
        {
            reloadSec *= 0.5f;
            // FerverTime 下上膛整体再提速 10 倍
            reloadSec *= 0.1f;
            reloadDelaySec *= 0.1f;
        }

        if (ClanAwesome.Instance != null)
        {
            ClanAwesome.Instance.VictorySchool(1);
        }

        DOVirtual.DelayedCall(reloadDelaySec, () =>
        {
            GlialSteppeIntenseAmongBeam(reloadSec);
        });


        // BarelyIon.OnHomeRotSpinByProbabilityRequest?.Invoke();
    }

    /// <summary>
    /// 发射后立即生成下一发；上膛期间 hookSpawnPoint 从起点移到终点，时长与 m_HookReloadSeconds 一致，期间不可发射。
    /// </summary>
    private void GlialSteppeIntenseAmongBeam(float reloadSec)
    {
        m_SteppeIntenseRigid = Mathf.Max(0.01f, reloadSec);
        m_SteppeSargeantRescue = m_SteppeIntenseRigid;

        // 先把枪口摆到起点，再生成新钩，保证“生成时就在(0,10,0)”
        if (GiftAlikePetal != null)
        {
            GiftAlikePetal.localPosition = GiftWestIntenseWaistCajun;
        }

        SunAlikeDownOnSettle();
        if (m_BicycleDown == null)
        {
            m_AxInaugurate = false;
            NicheDownWestFrostCajunLush();
            return;
        }

        m_AxInaugurate = true;

        // 上膛开始：打起枪械上膛动画
        SortDisc?.WifeLopMyStinger();

        if (m_IntenseWidow != null)
        {
            m_IntenseWidow.Kill();
            m_IntenseWidow = null;
        }

        if (GiftAlikePetal != null)
        {
            m_IntenseWidow = GiftAlikePetal
                .DOLocalMove(GiftWestIntensePryCajun, m_SteppeIntenseRigid)
                .SetEase(Ease.Linear)
                .OnComplete(() =>
                {
                    m_SteppeSargeantRescue = 0f;
                    m_AxInaugurate = false;
                    if (GiftAlikePetal != null)
                    {
                        GiftAlikePetal.localPosition = GiftWestIntensePryCajun;
                    }
                    m_IntenseWidow = null;
                });
        }
        else
        {
            // 如果没填 hookSpawnPoint，就只用计时冻结发射状态
            m_IntenseWidow = DOVirtual.DelayedCall(m_SteppeIntenseRigid, () =>
            {
                m_SteppeSargeantRescue = 0f;
                m_AxInaugurate = false;
                m_IntenseWidow = null;
            });
        }
    }

    private void NicheDownWestFrostCajunLush()
    {
        if (GiftAlikePetal == null) return;
        GiftAlikePetal.localPosition = GiftWestIntensePryCajun;
    }

    private void OnHookRecycle(DownLivelihood proj)
    {
        m_FreezeUpset.Remove(proj);
        SolelyIDMold(proj);
    }

    private GameObject AgeSeniorMeClanSick()
    {
        if (ClanAwesome.Instance == null) return GiftSeniorFuller;
        return ClanAwesome.Instance.ClanSick == GameType.FerverTime ? GiftSeniorEntire : GiftSeniorFuller;
    }

    private Vector2 AgeAlikeCompress()
    {
        RectTransform spawnRect = GiftAlikePetal != null ? GiftAlikePetal : BitLady;
        return (Vector2)GiftWest.InverseTransformPoint(spawnRect.position);
    }

    private Vector2 AgePriorDeformity()
    {
        Vector3 worldDir = BitLady.TransformDirection(Vector3.down);
        Vector2 localDir = (Vector2)GiftWest.InverseTransformDirection(worldDir);
        return localDir.sqrMagnitude > 0.0001f ? localDir.normalized : Vector2.down;
    }

    private DownLivelihood AgeLikeMold(GameObject prefab)
    {
        if (!m_MoldJay.TryGetValue(prefab, out var queue))
        {
            queue = new Queue<DownLivelihood>();
            m_MoldJay[prefab] = queue;
        }

        while (queue.Count > 0)
        {
            var cached = queue.Dequeue();
            if (cached != null)
            {
                cached.BurnInventSenior(prefab);
                return cached;
            }
        }

        GameObject go = Instantiate(prefab, GiftWest);
        var proj = go.GetComponent<DownLivelihood>();
        if (proj == null) proj = go.AddComponent<DownLivelihood>();
        proj.BurnInventSenior(prefab);
        if (go.GetComponent<DownImpatientDeviate>() == null) go.AddComponent<DownImpatientDeviate>();
        go.SetActive(false);
        return proj;
    }

    private void SolelyIDMold(DownLivelihood proj)
    {
        if (proj == null) return;
        proj.gameObject.SetActive(false);

        GameObject Offset= proj.InventSenior;
        if (Offset == null) Offset = AgeSeniorMeClanSick();
        if (Offset == null) Offset = GiftSeniorFuller;
        if (Offset == null) { Destroy(proj.gameObject); return; }

        if (!m_MoldJay.TryGetValue(Offset, out var queue))
        {
            queue = new Queue<DownLivelihood>();
            m_MoldJay[Offset] = queue;
        }
        queue.Enqueue(proj);
    }

    private void JobberPikeCrashIntegral()
    {
        if (ClanAwesome.Instance != null && ClanAwesome.Instance.ClanSick == GameType.FerverTime)
        {
            BitLady.localEulerAngles = Vector3.zero;
            return;
        }

        float RivalNavigation= ToNumerous ? Mathf.Max(0f, PieceCrashPreenNavigation) : 1f;
        PinchTexas += Time.deltaTime * RivalNavigation;
        float phase = PinchTexas * PinchPreen;
        // 相位扭曲：让运动在中间更慢、两侧更快（负值时生效）
        float warpedPhase = phase + PinchDizzyUser * Mathf.Sin(2f * phase);
        float angle = Mathf.Sin(warpedPhase) * Mathf.Abs(WitBidCargo) + HotCargoPatron;
        BitLady.localEulerAngles = new Vector3(0f, 0f, angle);
    }

    private void WhySeedyJoltEqual(bool isSlow)
    {
        if (ToSeedyJoltEqual == isSlow) return;
        ToSeedyJoltEqual = isSlow;
        if (ToSeedyJoltBarelyEqual != ToSeedyJoltEqual)
        {
            ToSeedyJoltBarelyEqual = ToSeedyJoltEqual;
            BarelyIon.ToDownSeedyJoltEqual?.Invoke(ToSeedyJoltEqual);
        }
    }

    private void WhyMagmaMorally(bool visible)
    {
        if (Magma == null) return;
        if (Magma.activeSelf == visible) return;
        Magma.SetActive(visible);
    }

    public void WaistCrash()
    {
        if (ToNumerous) return;
        ToProhibit = true;
    }

    public void SectCrash()
    {
        ToProhibit = false;
    }

    private static bool AxSparselyPaused()
    {
        return ClanAwesome.Instance != null && ClanAwesome.Instance.AxSparselyPaused;
    }
}
