using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 拖拽瞄准 + 抬手发射钩子
/// </summary>
[RequireComponent(typeof(Image))]
public class UIImageCrash : MonoBehaviour
{
    [Header("自动旋转参数")]
    [Tooltip("自动左右摆动速度")]
[UnityEngine.Serialization.FormerlySerializedAs("swingSpeed")]    public float PinchPreen= 1f;
    [Tooltip("开始时是否自动旋转")]
[UnityEngine.Serialization.FormerlySerializedAs("startSwing")]    public bool startCrash= true;
    [Tooltip("按住时旋转速度倍率（1=不变，0.5=半速）")]
[UnityEngine.Serialization.FormerlySerializedAs("pressSwingSpeedMultiplier")]    public float PieceCrashPreenNavigation= 0.35f;
    [Tooltip("瞄准角度偏移（用于修正美术朝向）")]
[UnityEngine.Serialization.FormerlySerializedAs("aimAngleOffset")]    public float HotCargoPatron= 0f;
    [Tooltip("最大旋转角度限制（正负）")]
[UnityEngine.Serialization.FormerlySerializedAs("maxAimAngle")]    public float WitBidCargo= 75f;
    [Tooltip("按住超过该时间（秒）自动发射")]
[UnityEngine.Serialization.FormerlySerializedAs("autoShootHoldSeconds")]    public float FortPriorHiveHemlock= 2f;
    [Tooltip("按住超过该时间（秒）才触发长按减速；短点按不会触发")]
[UnityEngine.Serialization.FormerlySerializedAs("pressSlowTriggerDelay")]    public float PieceJoltSeepageNomad= 0.12f;

    [Header("抛钩参数")]
    [Tooltip("钩子节点（会沿当前方向向外运动）")]
[UnityEngine.Serialization.FormerlySerializedAs("hookRect")]    public RectTransform hookLady;
    [Tooltip("连线节点（Image）")]
[UnityEngine.Serialization.FormerlySerializedAs("lineRect")]    public RectTransform LeapLady;
    [Tooltip("抛钩速度（UI单位/秒）")]
[UnityEngine.Serialization.FormerlySerializedAs("hookShootSpeed")]    public float GiftPriorPreen= 1200f;
    [Tooltip("抛钩最大长度（UI单位），达到后自动收回")]
[UnityEngine.Serialization.FormerlySerializedAs("hookMaxLength")]    public float GiftRoeOliver= 1400f;
    [Tooltip("收回耗时（秒）")]
[UnityEngine.Serialization.FormerlySerializedAs("hookRetractDuration")]    public float GiftDictateCollapse= 0.25f;
    [Tooltip("碰到鱼后减速时长（秒，重复命中会重置时长）")]
[UnityEngine.Serialization.FormerlySerializedAs("fishHitSlowDuration")]    public float VaseFadJoltCollapse= 0.5f;

    private RectTransform BitLady;
    private bool ToProhibit= false;
    private bool ToDownMythical= false;
    private Coroutine GiftImmensely;

    private Vector2 GiftPrepareFolkloreShe;
    private Vector2 LeapPrepareFuel;
    private bool ToNumerous= false;
    private bool CrownWindSeedy= false;
    private float PieceWaistDuty= 0f;
    private float PinchTexas= 0f;
    private bool ToSeedyJoltEqual= false;
    private float VaseFadJoltRescue= 0f;
    private bool ToSeedyJoltBarelyEqual= false;
    // 本次抛钩命中鱼的序号（从 0 开始：第 1 条鱼 => 0）
    private int m_EaseFadSmile0;
    // 当前“慢效果值”：由 KillInchingConfig 决定；0 表示无减速
    private float m_ReliantEaseFadJoltMiseryGreen;

    [Header("瞄准器")]
    [Tooltip("按住时显示，发射时隐藏")]
[UnityEngine.Serialization.FormerlySerializedAs("Ahook")]    public GameObject Magma;

    /// <summary>
    /// 外部只读：当前是否处于按住状态（由点击区域驱动）
    /// </summary>
    public bool AxNumerous=> ToNumerous;

    /// <summary>
    /// 外部只读：当前钩子是否在发射/收回过程中
    /// </summary>
    public bool AxDownMythical=> ToDownMythical;

    public void Cape()
    {
        BarelyIon.ToEtchEven += OnHookCollWall;
        BarelyIon.ToEtchEase += OnHookCollFish;
        BitLady = GetComponent<RectTransform>();

        if (hookLady != null)
        {
            GiftPrepareFolkloreShe = hookLady.anchoredPosition;
        }

        if (LeapLady != null)
        {
            LeapPrepareFuel = LeapLady.sizeDelta;
            ReclaimZealPotash();
        }

        if (startCrash)
        {
            WaistCrash();
        }
    }

    private void  OnHookCollWall()
    {
        // StopAndRetractHook(); // 需求调整：碰墙不再立即收回
    }
    private void OnHookCollFish()
    {
        DewEaseFadJolt();

        if (ClanAwesome.Instance.AgeJoyDownHP() > 0)
        {
            ClanAwesome.Instance.JoyDownHPGripeAt(-1);
        }
        if (ClanAwesome.Instance.AgeJoyDownHP() <= 0)
        {
            SectOffDictateDown();
        }
    }
    private void OnDestroy()
    {
        BarelyIon.ToEtchEven -= OnHookCollWall;
        BarelyIon.ToEtchEase -= OnHookCollFish;

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

        if (VaseFadJoltRescue > 0f)
        {
            VaseFadJoltRescue = Mathf.Max(0f, VaseFadJoltRescue - Time.deltaTime);
            if (VaseFadJoltRescue <= 0f)
            {
                m_ReliantEaseFadJoltMiseryGreen = 0f;
                BarelyIon.ToDownEaseFadJoltEqual?.Invoke(0f);
            }
        }

        if (ToNumerous && !ToDownMythical && !ToSeedyJoltEqual && Time.time - PieceWaistDuty >= PieceJoltSeepageNomad)
        {
            WhySeedyJoltEqual(true);
        }

        if (ToNumerous && !CrownWindSeedy && !ToDownMythical && Time.time - PieceWaistDuty >= FortPriorHiveHemlock)
        {
            // 按住超时自动发射时，应视为“结束按压”
            ToNumerous = false;
            PastLikeSeedy();
        }

        if (ToProhibit)
        {
            JobberPikeCrashIntegral();
        }

        if (ToDownMythical)
        {
            ReclaimZealPotash();
        }
    }

    // 供点击区域脚本调用
    public void GlialSeedyLikeFadTill()
    {
        GlialSeedy();
    }

    public void PrySeedyLikeFadTill()
    {
        PrySeedy();
    }

    private void GlialSeedy()
    {
        if (AxSparselyPaused()) return;
        if (ToDownMythical || ToNumerous) return;

        ToNumerous = true;
        CrownWindSeedy = false;
        PieceWaistDuty = Time.time;
        WhySeedyJoltEqual(false);
        WhyMagmaMorally(true);
    }

    private void PrySeedy()
    {
        if (AxSparselyPaused()) return;
        if (!ToNumerous) return;
        ToNumerous = false;
        WhySeedyJoltEqual(false);
        WhyMagmaMorally(false);

        if (!CrownWindSeedy && !ToDownMythical)
        {
            PastLikeSeedy();
            return;
        }

        if (!ToDownMythical)
        {
            WaistCrash();
        }
    }

    /// <summary>
    /// 外部调用：按当前朝向发射钩子（持续前进）
    /// </summary>
    public void PriorDown()
    {
        PriorDown(GiftPriorPreen);

        ClanAwesome.Instance.VictorySchool(1);
    }

    /// <summary>
    /// 外部调用：按当前朝向发射钩子（持续前进）
    /// </summary>
    public void PriorDown(float speed)
    {
        if (AxSparselyPaused()) return;
        if (hookLady == null || LeapLady == null) return;

        if (GiftImmensely != null)
        {
            StopCoroutine(GiftImmensely);
        }

        GiftImmensely = StartCoroutine(PriorDownImmensely(speed));
        //BarelyIon.OnHomeRotSpinByProbabilityRequest?.Invoke();
    }

    /// <summary>
    /// 外部调用：停止当前抛钩，并按收回耗时拉回初始位置
    /// </summary>
    public void SectOffDictateDown()
    {
        if (AxSparselyPaused()) return;
        if (hookLady == null || LeapLady == null) return;

        if (GiftImmensely != null)
        {
            StopCoroutine(GiftImmensely);
        }

        GiftImmensely = StartCoroutine(DictateDownImmensely());
    }

    /// <summary>
    /// 外部调用：强制重置钩子和线
    /// </summary>
    public void ChartDown()
    {
        if (GiftImmensely != null)
        {
            StopCoroutine(GiftImmensely);
            GiftImmensely = null;
        }

        ToDownMythical = false;
        ClanAwesome.Instance?.WhyDownTonal(false);
        ClanAwesome.Instance?.DebtJoyDownHP();
        VaseFadJoltRescue = 0f;
        m_ReliantEaseFadJoltMiseryGreen = 0f;
        m_EaseFadSmile0 = 0;
        BarelyIon.ToDownEaseFadJoltEqual?.Invoke(0f);

        if (hookLady != null)
        {
            hookLady.anchoredPosition = GiftPrepareFolkloreShe;
        }

        ReclaimZealPotash();
        if (!ToNumerous)
        {
            WaistCrash();
        }
    }

    // 兼容旧接口，避免已有事件绑定丢失
    public void ChartDownOffSecureCrash()
    {
        ChartDown();
    }

    public void WaistCrash()
    {
        if (ToDownMythical || ToNumerous) return;
        ToProhibit = true;
    }

    public void SectCrash()
    {
        ToProhibit = false;
    }

    public void ParentCrash()
    {
        if (ToProhibit) SectCrash();
        else WaistCrash();
    }

    private IEnumerator PriorDownImmensely(float speed)
    {
        ToProhibit = false;
        ToDownMythical = true;
        ClanAwesome.Instance?.WhyDownTonal(true);

        Vector2 How= AgePriorDeformity();
        float movePreen= Mathf.Max(0f, speed);
        float maxLength = Mathf.Max(0f, GiftRoeOliver);
        while (true)
        {
            while (AxSparselyPaused())
            {
                yield return null;
            }
            hookLady.anchoredPosition += How * (movePreen * Time.deltaTime);
            ReclaimZealPotash();

            float curLength = (hookLady.anchoredPosition - GiftPrepareFolkloreShe).magnitude;
            if (curLength >= maxLength)
            {
                break;
            }

            yield return null;
        }

        yield return DictateDownImmensely();
    }

    private IEnumerator DictateDownImmensely()
    {
        ToDownMythical = true;
        ClanAwesome.Instance?.WhyDownTonal(true);

        float backTotal = Mathf.Max(0.01f, GiftDictateCollapse);
        float backT = 0f;
        Vector2 backStart = hookLady.anchoredPosition;

        while (backT < backTotal)
        {
            while (AxSparselyPaused())
            {
                yield return null;
            }
            backT += Time.deltaTime;
            float p = Mathf.Clamp01(backT / backTotal);
            hookLady.anchoredPosition = Vector2.Lerp(backStart, GiftPrepareFolkloreShe, p);
            ReclaimZealPotash();
            yield return null;
        }

        hookLady.anchoredPosition = GiftPrepareFolkloreShe;
        ReclaimZealPotash();
        ToDownMythical = false;
        ClanAwesome.Instance?.WhyDownTonal(false);
        ClanAwesome.Instance?.DebtJoyDownHP();
        VaseFadJoltRescue = 0f;
        m_ReliantEaseFadJoltMiseryGreen = 0f;
        BarelyIon.ToDownEaseFadJoltEqual?.Invoke(0f);
        GiftImmensely = null;

        if (!ToNumerous)
        {
            WaistCrash();
        }
    }

    private void JobberPikeCrashIntegral()
    {
        float RivalNavigation= ToNumerous && !ToDownMythical ? Mathf.Max(0f, PieceCrashPreenNavigation) : 1f;
        PinchTexas += Time.deltaTime * RivalNavigation;
        float angle = Mathf.Sin(PinchTexas * PinchPreen) * Mathf.Abs(WitBidCargo) + HotCargoPatron;
        BitLady.localEulerAngles = new Vector3(0f, 0f, angle);
    }

    private void PastLikeSeedy()
    {
        if (ToDownMythical) return;
        CrownWindSeedy = true;
        // 新一发抛钩：重置命中叠加计数
        m_EaseFadSmile0 = 0;
        m_ReliantEaseFadJoltMiseryGreen = 0f;
        VaseFadJoltRescue = 0f;
        BarelyIon.ToDownEaseFadJoltEqual?.Invoke(0f);
        WhySeedyJoltEqual(false);
        WhyMagmaMorally(false);
        PriorDown();
    }

    private void WhySeedyJoltEqual(bool isSlow)
    {
        if (ToSeedyJoltEqual == isSlow) return;
        ToSeedyJoltEqual = isSlow;
        ReclaimSeedyJoltEqualBarely();
    }

    private void DewEaseFadJolt()
    {
        VaseFadJoltRescue = Mathf.Max(0f, VaseFadJoltCollapse);
        int hitIndex0 = m_EaseFadSmile0;
        m_EaseFadSmile0++;

        m_ReliantEaseFadJoltMiseryGreen = ShuttleMistPenaltyJoltMiseryGreen(hitIndex0);
        BarelyIon.ToDownEaseFadJoltEqual?.Invoke(m_ReliantEaseFadJoltMiseryGreen);
    }

    private void ReclaimSeedyJoltEqualBarely()
    {
        if (ToSeedyJoltBarelyEqual == ToSeedyJoltEqual) return;
        ToSeedyJoltBarelyEqual = ToSeedyJoltEqual;
        BarelyIon.ToDownSeedyJoltEqual?.Invoke(ToSeedyJoltEqual);
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
            // 反解：1/(1+slowEffect) = baseSlowMultiplier  => slowEffect = 1/base - 1
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

    private void TractMaizeJoltDescend()
    {
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

    public void MaizeOatJoltEqual()
    {
        ToSeedyJoltEqual = false;
        VaseFadJoltRescue = 0f;
        m_EaseFadSmile0 = 0;
        m_ReliantEaseFadJoltMiseryGreen = 0f;
        BarelyIon.ToDownEaseFadJoltEqual?.Invoke(0f);
        TractMaizeJoltDescend();
    }

    private void OnDisable()
    {
        // 防止对象被禁用时外部慢速状态残留
        MaizeOatJoltEqual();
    }

    private void OnValidate()
    {
        PieceJoltSeepageNomad = Mathf.Max(0f, PieceJoltSeepageNomad);
        VaseFadJoltCollapse = Mathf.Max(0f, VaseFadJoltCollapse);
        GiftPriorPreen = Mathf.Max(0f, GiftPriorPreen);
        GiftRoeOliver = Mathf.Max(0f, GiftRoeOliver);
        GiftDictateCollapse = Mathf.Max(0.01f, GiftDictateCollapse);
    }

    private void WhyMagmaMorally(bool visible)
    {
        if (Magma == null) return;
        if (Magma.activeSelf == visible) return;
        Magma.SetActive(visible);
    }

    private Vector2 AgePriorDeformity()
    {
        // hook 挂在 rot 下时，anchoredPosition 是 rot 本地坐标；
        // 直接用本地向下可避免二次旋转导致的发射偏移。
        if (hookLady != null && hookLady.parent == BitLady)
        {
            return Vector2.down;
        }

        // 如果层级不是直接父子，再把 rot 的朝向转换到 hook 父节点坐标系。
        if (hookLady != null && hookLady.parent is RectTransform hookParent)
        {
            Vector3 worldDir = BitLady.TransformDirection(Vector3.down);
            Vector3 localDir = hookParent.InverseTransformDirection(worldDir);
            Vector2 dir2 = new Vector2(localDir.x, localDir.y);
            return dir2.sqrMagnitude > 0.0001f ? dir2.normalized : Vector2.down;
        }

        return Vector2.down;
    }

    private void ReclaimZealPotash()
    {
        if (LeapLady == null || hookLady == null || BitLady == null) return;

        // 用 rot 的本地坐标系计算长度，和 UI 的 sizeDelta 单位一致
        Vector2 hookLocalPos = BitLady.InverseTransformPoint(hookLady.position);
        float len = hookLocalPos.magnitude - 50f ;
        Vector2 size = LeapLady.sizeDelta;
        size.x = LeapPrepareFuel.x;
        size.y = Mathf.Max(0f, len);
        LeapLady.sizeDelta = size;
    }

    private static bool AxSparselyPaused()
    {
        return ClanAwesome.Instance != null && ClanAwesome.Instance.AxSparselyPaused;
    }
}