using System;
using System.Collections.Generic;
using DG.Tweening;
using Spine;
using Spine.Unity;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;
public class UIEaseDeluge : MonoBehaviour
{
    [Header("Spine 动画")]
    [Tooltip("游动时循环动画名")]
[UnityEngine.Serialization.FormerlySerializedAs("idleAnimName")]    public string TeleDiscLust= "idle";
    [Tooltip("受击动画名（播放一次后回到 idle）")]
[UnityEngine.Serialization.FormerlySerializedAs("hitAnimName")]    public string EyeDiscLust= "hit";
    [Tooltip("Ferver 单鱼受击动画A（留空则回退到 hitAnimName）")]
[UnityEngine.Serialization.FormerlySerializedAs("ferverSingleHitAnimNameA")]    public string DecadeForgetFadDiscLustA= "beat_1";
    [Tooltip("Ferver 单鱼受击动画B（留空则回退到 hitAnimName）")]
[UnityEngine.Serialization.FormerlySerializedAs("ferverSingleHitAnimNameB")]    public string DecadeForgetFadDiscLustB= "beat_2";
    [Tooltip("idle 基础播放速度（TrackEntry.TimeScale 的一部分）")]
[UnityEngine.Serialization.FormerlySerializedAs("idleAnimBaseTimeScale")]    public float TeleDiscShedDutyPerch= 1f;
    [Tooltip("idle 运行时变速乘子（有效速度 = 基础 × 本值；对象 Init 时重置为 1）。改值后可调用 RefreshIdleAnimTimeScale 或 SetIdleAnimSpeedMultiplier。")]
[UnityEngine.Serialization.FormerlySerializedAs("idleAnimSpeedMultiplier")]    public float TeleDiscPreenNavigation= 1f;
    [Tooltip("非致命受击（Boss 未中弱点等）动画播放速度")]
[UnityEngine.Serialization.FormerlySerializedAs("hitAnimTimeScale")]    public float EyeDiscDutyPerch= 1f;
    [Tooltip("致命/死亡向动画播放速度（普通鱼受击即死、Boss 弱点命中等）")]
[UnityEngine.Serialization.FormerlySerializedAs("deathAnimTimeScale")]    public float WidenDiscDutyPerch= 1f;
    


    [Header("碰撞/攻击")]
    [Tooltip("触发受击的对象 Tag（比如 Hook）")]
[UnityEngine.Serialization.FormerlySerializedAs("attackerTag")]    public string ObserverDon= "Hook";
    [Tooltip("是否允许被场景鱼钩命中。预览/展示鱼可关闭以避免进入击杀逻辑。")]
[UnityEngine.Serialization.FormerlySerializedAs("canBeHookHit")]    public bool AntBeDownFad= true;
    [Tooltip("受击后禁用自身碰撞，避免重复触发")]
[UnityEngine.Serialization.FormerlySerializedAs("disableColliderOnHit")]    public bool LeaningCondenseToFad= true;

    [Header("Boss 属性")]
    [Tooltip("是否为 Boss 鱼（Boss 鱼只有命中特定脆弱部位才会死亡）")]
[UnityEngine.Serialization.FormerlySerializedAs("isBossFish")]    public bool ToKierEase= false;

    [Tooltip("Boss 的脆弱部位碰撞器：仅当 Hook 命中该碰撞器时才会死亡；未配置则视为所有命中都可死亡。")]
[UnityEngine.Serialization.FormerlySerializedAs("bossWeakCollider")]    public Collider2D ModePoreCondense;

    [Tooltip("Boss 特写触发碰撞器：单次发射内首次命中时由钩子碰撞逻辑发特写请求（每发最多一次）。")]
[UnityEngine.Serialization.FormerlySerializedAs("bossCloseupTriggerCollider")]    public Collider2D ModeGazelleSeepageCondense;

    [Tooltip("Boss 非致命受击专用 Spine 动画名（未命中弱点时）。留空则仍用 hitAnimName。")]
[UnityEngine.Serialization.FormerlySerializedAs("bossNonLethalHitAnimName")]    public string ModeHisLiquidFadDiscLust= "";

    /// <summary>
    /// Boss 特写：同一发钩子内只请求一次（避免特写结束后钩子仍碰触发框再次进特写）。
    /// 调用方在每发开始时将 <paramref name="closeupConsumedThisShot"/> 复位为 false（如钩子 OnEnable）。
    /// </summary>
    public static void SunAccuseKierGazelleSeepageGildBoxBeam(UIEaseDeluge fish, Collider2D hitCollider, ref bool closeupConsumedThisShot)
    {
        if (closeupConsumedThisShot || fish == null || hitCollider == null)
            return;
        if (!fish.ToKierEase || fish.ModeGazelleSeepageCondense == null || hitCollider != fish.ModeGazelleSeepageCondense)
            return;
        closeupConsumedThisShot = true;
        BarelyIon.ToKierGazelleSeepagePromote?.Invoke(fish);
    }

    [Header("生成配置")]
    [Tooltip("预制体默认面朝：游动方向改变时会按此决定是否水平翻转。Left=美术默认朝左，Right=朝右。")]
[UnityEngine.Serialization.FormerlySerializedAs("prefabFacing")]    public UIFishSpawnEntry.PrefabFacing OffsetFacing= UIFishSpawnEntry.PrefabFacing.Left;
    [Tooltip("游动速度随机范围（UI 单位/秒）。X = 最慢，Y = 最快；生成时在区间内随机取一条速度。")]
[UnityEngine.Serialization.FormerlySerializedAs("speedRange")]    public Vector2 RivalRadio= new Vector2(180f, 320f);
    [Tooltip("生成时的整体缩放随机范围。X = 最小缩放，Y = 最大缩放（相对 localScale）。")]
[UnityEngine.Serialization.FormerlySerializedAs("scaleRange")]    public Vector2 WispyRadio= new Vector2(0.8f, 1.2f);
    [Header("竖直分带（全不勾＝全水域）")]
    [Tooltip("逐项打勾；勾几项就有几条刷新带参与随机。鱼潮/编队仍用固定 Y。")]
[UnityEngine.Serialization.FormerlySerializedAs("verticalSpawnBands")]    public UIFishSpawnVerticalBands MechanicAlikeMusic;
    [Tooltip("该鱼生成时在全局 spawnBuffer 基础上再额外外移的像素（默认 0，仅个别大鱼可调大）。")]
    [Min(0f)]
[UnityEngine.Serialization.FormerlySerializedAs("spawnExtraBuffer")]    public float ScourBingeTethys= 0f;
    [Tooltip("该鱼回收时在全局 recycleBuffer 基础上再额外外移的像素（默认 0，仅个别大鱼可调大）。")]
    [Min(0f)]
[UnityEngine.Serialization.FormerlySerializedAs("recycleExtraBuffer")]    public float FirearmBingeTethys= 0f;

    [Header("游动变速")]
    [Tooltip("是否启用变速效果。仅对 enableSteerTiltAndPathOffset=true 的鱼生效（鱼潮/排队鱼保持不受影响）。")]
[UnityEngine.Serialization.FormerlySerializedAs("enableIdleSpeedVariant")]    public bool MalletRomePreenBravely= true;
    [Tooltip("触发间隔（秒）。每隔该时间进行一次触发判定。")]
    [Min(0f)]
[UnityEngine.Serialization.FormerlySerializedAs("idleSpeedVariantTriggerIntervalSec")]    public float TelePreenBravelySeepageContractDog= 3f;
    [Tooltip("触发几率。0 永不触发；1 每次到点必触发。")]
    [Range(0f, 1f)]
[UnityEngine.Serialization.FormerlySerializedAs("idleSpeedVariantTriggerProbability")]    public float TelePreenBravelySeepageDistinction= 0f;
    [Tooltip("变速事件总时长（秒）：过渡向目标 + 保持目标倍率 + 过渡回 1。保持时长 = 本值 − 2×过渡时间（不小于 0）；若总时长不足 2×过渡，则前后两半各用于升/降，无保持段。期间不触发间隔判定。")]
    [Min(0f)]
[UnityEngine.Serialization.FormerlySerializedAs("idleSpeedVariantDurationSec")]    public float TelePreenBravelyCollapseDog= 1f;
    [Tooltip("进入变速后的加速概率（0~1）。p=0 永不加速（必减速）；p=1 必定加速。")]
    [Range(0f, 1f)]
[UnityEngine.Serialization.FormerlySerializedAs("idleSpeedVariantAccelProbability")]    public float TelePreenBravelyAdoptDistinction= 0.5f;
    [Tooltip("加速目标倍率：目标倍率 = 1 + 加速百分比（例如 0.2 => 1.2）。")]
    [Min(0f)]
[UnityEngine.Serialization.FormerlySerializedAs("idleSpeedVariantAccelPercent")]    public float TelePreenBravelyAdoptConsist= 0.2f;
    [Tooltip("减速目标倍率：目标倍率 = 1 - 减速度百分比（例如 0.2 => 0.8）。")]
    [Min(0f)]
[UnityEngine.Serialization.FormerlySerializedAs("idleSpeedVariantDecelPercent")]    public float TelePreenBravelyDecelConsist= 0.2f;
    [Tooltip("单次过渡时长（秒）：升到目标倍率与降回 1 各用本时长。对称使用；总时长不足 2×本值时两段时间均分总时长。")]
    [Min(0f)]
[UnityEngine.Serialization.FormerlySerializedAs("idleSpeedVariantRampTimeSec")]    public float TelePreenBravelyBlueDutyDog= 0.5f;
    //[Tooltip("命中后是否自动回到 idle")]
    //public bool returnToIdleAfterHit = true;

    [Header("转弧线")]
    [Tooltip("转向触发间隔（秒）。每隔该时间尝试一次：以概率进入一次转向弧线。")]
[UnityEngine.Serialization.FormerlySerializedAs("steerTurnTriggerIntervalSec")]    public float LarvaGiftSeepageContractDog= 3f;
    [Tooltip("转向触发概率（0~1）。0 永不转；1 每次到点必转。")]
    [Range(0f, 1f)]
[UnityEngine.Serialization.FormerlySerializedAs("steerTurnTriggerProbability")]    public float LarvaGiftSeepageDistinction= 0f;
    [Tooltip("转向事件总时长（秒）：过渡到最大仰角 + 保持该角游动 + 对称回正。保持时长 = 本值 − 2×过渡时间（不小于 0）；若总时长不足 2×过渡，则前后两半各用于升/降角，无保持段。")]
    [Min(0f)]
[UnityEngine.Serialization.FormerlySerializedAs("steerTurnTotalDurationSec")]    public float LarvaGiftRigidCollapseDog= 1f;
    [Tooltip("转正负极角概率（0~1）。0 必定选择负仰角，1 必定选择正仰角。")]
    [Range(0f, 1f)]
[UnityEngine.Serialization.FormerlySerializedAs("steerPositivePitchProbability")]    public float LarvaBluebirdCrushDistinction= 0.5f;
    [Tooltip("正仰角（度），0=水平，90=垂直。作为“抬头”的正方向角度幅值。")]
    [Range(0f, 90f)]
[UnityEngine.Serialization.FormerlySerializedAs("steerPositivePitchDeg")]    public float LarvaBluebirdCrushDeg= 30f;
    [Tooltip("负仰角（度），0=水平，90=垂直。作为“低头”的负方向角度幅值。")]
    [Range(0f, 90f)]
[UnityEngine.Serialization.FormerlySerializedAs("steerNegativePitchDeg")]    public float LarvaIdeologyCrushRod= 30f;
    [Tooltip("单次过渡时长（秒）：从水平转到最大仰角与从最大仰角回水平各用本时长。总时长不足 2×本值时两段时间均分总时长。")]
    [Min(0f)]
[UnityEngine.Serialization.FormerlySerializedAs("steerTurnToMaxTimeSec")]    public float LarvaGiftIDRoeDutyDog= 0.3f;

    [Header("调试显示")]
    [Tooltip("可选：用于显示 lv+type+id 的 TMP 文本")]
[UnityEngine.Serialization.FormerlySerializedAs("debugInfoText")]    public TMP_Text NovelSlumWelt;

    private RectTransform m_Lady;
    private float m_ShedPreen;
    private float m_ShedY;
    private int m_LoneDir;
    private float m_DelftIraqYPatron;
    private bool m_Astute;
    private bool m_SectLoneAmongLiquidFad;
    private bool m_LiquidJuicyOffOnConfront;
    private GameObject m_InventSenior;
    private bool m_AxFadPrinter;
    private bool m_DebateDNAToFadSocially;
    private bool m_VoyageRomePreenBravelyRuntime;
    private bool m_VoyageDelftGiftImpetus;
    private bool m_KierWorthGovernCruelLean;
    private bool m_DebateGateKierPorePetalMeCruel;

    private enum SteerTurnState
    {
        Waiting = 0,
        Active = 1,
    }

    private SteerTurnState m_DelftGiftEqual;
    private float m_DelftGiftContractTexas;
    private float m_DelftGiftHarmony;
    private float m_DelftGiftRigidCollapse;
    private float m_DelftGiftIDRoeCollapse;
    private float m_DelftGiftCrushRoeRod; // 有符号：正仰角/负仰角
    
    private enum IdleSpeedVariantState
    {
        WaitingForInterval = 0,
        Active = 1,
    }
    
    private IdleSpeedVariantState m_RomePreenBravelyEqual;
    private float m_RomePreenBravelyContractTexas;
    private float m_RomePreenBravelyCacheHarmony;
    private float m_RomePreenBravelyWaistNavigation;
    private float m_RomePreenBravelyLayoutNavigation;
[UnityEngine.Serialization.FormerlySerializedAs("m_SkeletonGraphic")]
    public SkeletonGraphic m_AllusionPaucity;
[UnityEngine.Serialization.FormerlySerializedAs("m_MiaoSkeletonGraphic")]    public SkeletonGraphic m_MiaoAllusionPaucity;
[UnityEngine.Serialization.FormerlySerializedAs("m_Collider2D")]

    public Collider2D m_Condense2D;
    private Collider2D[] m_OatMemorable2D;
    private int m_Gripe= 1;
    private int m_HP= 1;
    private int m_Lesson= 10;
    /// <summary>钻石鱼击杀奖励（&gt;0 时走 OnFishAddDiamond，不再发本鱼金币）。</summary>
    private int m_LinkageLesson= 0;
    private string m_EaseWe= string.Empty;
    private string m_EaseSick= string.Empty;
[UnityEngine.Serialization.FormerlySerializedAs("m_FishCategory")]    public UIFishCategory m_EaseTreasury= UIFishCategory.Small;

    public GameObject InventSenior=> m_InventSenior;
    public int Gripe=> m_Gripe;
    public int HP=> m_HP;
    public int Lesson=> m_Lesson;
    public int LinkageLesson=> m_LinkageLesson;
    public string EaseWe=> m_EaseWe;
    public string EaseSick=> m_EaseSick;
    public UIFishCategory EaseTreasury=> m_EaseTreasury;

    /// <summary>用于相机跟随/UGUI 对焦（根或自身 RectTransform）。</summary>
    public RectTransform EaseLadyReference=> m_Lady != null ? m_Lady : transform as RectTransform;

    bool m_BicycleLiquidTopsoilAmongGazelle;

    private void Awake()
    {
        m_Lady = GetComponent<RectTransform>();
        m_Condense2D = GetComponent<Collider2D>();
        m_OatMemorable2D = GetComponentsInChildren<Collider2D>(true);
        ReclaimHurrySlumWelt();
    }

    private void OnEnable()
    {
        if (m_AllusionPaucity == null) m_AllusionPaucity = GetComponentInChildren<SkeletonGraphic>(true);
        if (m_AllusionPaucity != null && m_AllusionPaucity.AnimationState != null)
        {
            m_AllusionPaucity.AnimationState.Complete -= OnAnimComplete;
            m_AllusionPaucity.AnimationState.Complete += OnAnimComplete;
        }

    }

    private void OnDisable()
    {
        if (m_AllusionPaucity != null && m_AllusionPaucity.AnimationState != null)
        {
            m_AllusionPaucity.AnimationState.Complete -= OnAnimComplete;
        }

    }

    /// <summary>
    /// 对外触发：请求生成一条 Boss 鱼（通过 BarelyIon 广播）。
    /// </summary>
    public static void PromoteAlikeKierEase()
    {
        BarelyIon.ToAlikeKierEasePromote?.Invoke();
    }

    /// <param name="enableSteerTiltAndPathOffset">false：关闭身体 Z 倾角与沿倾角的路径偏移（鱼潮编队等需保持队形）</param>
    public void Cape(GameObject sourcePrefab, float baseSpeed, float baseY, int moveDir, bool enableSteerTiltAndPathOffset = true)
    {
        if (m_Lady == null) m_Lady = GetComponent<RectTransform>();
        if (m_AllusionPaucity == null) m_AllusionPaucity = GetComponentInChildren<SkeletonGraphic>(true);

        m_InventSenior = sourcePrefab;
        m_ShedPreen = Mathf.Max(0f, baseSpeed);
        m_ShedY = baseY;
        m_LoneDir = moveDir >= 0 ? 1 : -1;
        m_VoyageDelftGiftImpetus = enableSteerTiltAndPathOffset;
        ChartDelftGiftEqual();

        m_DelftIraqYPatron = 0f;
        m_Astute = true;
        m_SectLoneAmongLiquidFad = false;
        m_LiquidJuicyOffOnConfront = false;
        m_AxFadPrinter = false;
        m_DebateDNAToFadSocially = false;
        TeleDiscPreenNavigation = 1f;
        bool bossGuideDone = SpotGushAwesome.GetBool(CMillet.If_guide_Mode_Juggle_Mesh);
        // 只在“未触发过引导”时做：出生先关弱点/提示动画，到中心触发引导时再打开。
        m_DebateGateKierPorePetalMeCruel = ToKierEase && !bossGuideDone;
        // 事件是否已发：引导做过则视为已发，后续不再触发中心点引导。
        m_KierWorthGovernCruelLean = bossGuideDone;
        
        // enableSteerTiltAndPathOffset=false 的鱼（鱼潮/排队/编队/Ferver 行）不跑变速逻辑，避免破坏队形与节奏。
        m_VoyageRomePreenBravelyRuntime = enableSteerTiltAndPathOffset;
        m_RomePreenBravelyEqual = IdleSpeedVariantState.WaitingForInterval;
        m_RomePreenBravelyContractTexas = 0f;
        m_RomePreenBravelyCacheHarmony = 0f;
        m_RomePreenBravelyWaistNavigation = 1f;
        m_RomePreenBravelyLayoutNavigation = 1f;

        if (m_OatMemorable2D != null && m_OatMemorable2D.Length > 0)
        {
            for (int i = 0; i < m_OatMemorable2D.Length; i++)
            {
                if (m_OatMemorable2D[i] != null) m_OatMemorable2D[i].enabled = true;
            }
        }
        else if (m_Condense2D != null)
        {
            m_Condense2D.enabled = true;
        }

        // Boss 初始化：仅在“未触发过引导”时，先隐藏弱点表现。
        if (m_MiaoAllusionPaucity != null)
        {
            bool shouldShowMiao = ToKierEase && !m_DebateGateKierPorePetalMeCruel;
            m_MiaoAllusionPaucity.gameObject.SetActive(shouldShowMiao);
        }
        if (ToKierEase && ModePoreCondense != null)
        {
            ModePoreCondense.enabled = !m_DebateGateKierPorePetalMeCruel;
        }

        // 保证主骨骼可见并重置到游动动画，避免对象池复用后出现空白鱼。
        if (m_AllusionPaucity != null)
        {
            m_AllusionPaucity.gameObject.SetActive(true);
        }

        NicheDelftCityIntegral(0f);
        WifeRome();
    }

    /// <summary>
    /// 切换主鱼 Spine 动画前的“重置”步骤。
    /// 注意：该操作开销较高，只应在必要时调用。
    /// </summary>
    void SoldierThaiAllusionFitDiscOrange()
    {
        if (m_AllusionPaucity == null) return;
        if (m_AllusionPaucity.Skeleton != null)
        {
            m_AllusionPaucity.Skeleton.SetToSetupPose();
        }
        if (m_AllusionPaucity.AnimationState != null)
        {
            m_AllusionPaucity.AnimationState.ClearTracks();
        }
    }

    /// <summary>
    /// 智能切换动画：同动画不重复切；仅在必要时执行 SetupPose/ClearTracks。
    /// </summary>
    /// <param name="animName">目标动画名</param>
    /// <param name="loop">是否循环</param>
    /// <param name="timeScale">轨道播放速度</param>
    /// <param name="forceResetBeforeSet">是否强制在切换前执行重置</param>
    /// <returns>返回当前有效 TrackEntry；失败返回 null</returns>
    TrackEntry SunNobleDiscIfNeed(string animName, bool loop, float timeScale, bool forceResetBeforeSet = false)
    {
        if (string.IsNullOrEmpty(animName)) return null;
        if (m_AllusionPaucity == null || m_AllusionPaucity.AnimationState == null) return null;

        TrackEntry current = m_AllusionPaucity.AnimationState.GetCurrent(0);
        if (current != null && current.Animation != null &&
            string.Equals(current.Animation.Name, animName, StringComparison.Ordinal) &&
            current.Loop == loop)
        {
            // 同动画同 loop：不重建轨道，只更新速度。
            current.TimeScale = Mathf.Max(0f, timeScale);
            return current;
        }

        // 仅在跨动画切换（或调用方强制）时，执行较重的重置流程。
        if (forceResetBeforeSet || current == null || current.Animation == null ||
            !string.Equals(current.Animation.Name, animName, StringComparison.Ordinal))
        {
            SoldierThaiAllusionFitDiscOrange();
        }

        TrackEntry entry = m_AllusionPaucity.AnimationState.SetAnimation(0, animName, loop);
        if (entry != null)
        {
            entry.TimeScale = Mathf.Max(0f, timeScale);
        }
        return entry;
    }

    /// <summary>运行时修改 idle 变速乘子并立即作用到当前 idle 轨道（受击中不修改）。</summary>
    public void WhyRomeDiscPreenNavigation(float multiplier)
    {
        TeleDiscPreenNavigation = Mathf.Max(0f, multiplier);
        ReclaimRomeDiscDutyPerch();
    }

    /// <summary>按当前「基础 × 变速」刷新轨道 0 上 idle 的 TimeScale（正在播受击时不处理）。</summary>
    public void ReclaimRomeDiscDutyPerch()
    {
        if (m_AllusionPaucity == null || m_AllusionPaucity.AnimationState == null) return;
        if (m_AxFadPrinter) return;
        if (string.IsNullOrEmpty(TeleDiscLust)) return;
        TrackEntry te = m_AllusionPaucity.AnimationState.GetCurrent(0);
        if (te == null || te.Animation == null) return;
        if (!string.Equals(te.Animation.Name, TeleDiscLust, StringComparison.Ordinal))
        {
            return;
        }

        te.TimeScale = Mathf.Max(0f, TeleDiscShedDutyPerch * TeleDiscPreenNavigation);
    }

    void JobberRomePreenBravely(float dt)
    {
        if (!MalletRomePreenBravely || !m_VoyageRomePreenBravelyRuntime)
        {
            if (TeleDiscPreenNavigation != 1f)
            {
                WhyRomeDiscPreenNavigation(1f);
            }
            m_RomePreenBravelyEqual = IdleSpeedVariantState.WaitingForInterval;
            m_RomePreenBravelyContractTexas = 0f;
            m_RomePreenBravelyCacheHarmony = 0f;
            return;
        }

        float intervalSec = Mathf.Max(0f, TelePreenBravelySeepageContractDog);
        float durationSec = Mathf.Max(0f, TelePreenBravelyCollapseDog);

        if (m_RomePreenBravelyEqual == IdleSpeedVariantState.WaitingForInterval)
        {
            if (intervalSec <= 0f)
            {
                return;
            }

            m_RomePreenBravelyContractTexas += dt;
            float p = Mathf.Clamp01(TelePreenBravelySeepageDistinction);
            if (m_RomePreenBravelyContractTexas < intervalSec) return;

            // 可能 dt 跨过多个间隔：循环处理，概率上保持正确。
            while (m_RomePreenBravelyContractTexas >= intervalSec)
            {
                m_RomePreenBravelyContractTexas -= intervalSec;
                if (Random.value < p)
                {
                    WaistRomePreenBravely();
                    break;
                }
            }
            return;
        }

        if (m_RomePreenBravelyEqual == IdleSpeedVariantState.Active)
        {
            m_RomePreenBravelyCacheHarmony += dt;
            float curMul = UncookedRomePreenBravelyNavigation(
                m_RomePreenBravelyCacheHarmony,
                durationSec,
                Mathf.Max(0f, TelePreenBravelyBlueDutyDog),
                m_RomePreenBravelyWaistNavigation,
                m_RomePreenBravelyLayoutNavigation);
            curMul = Mathf.Max(0f, curMul);
            WhyRomeDiscPreenNavigation(curMul);

            if (m_RomePreenBravelyCacheHarmony >= durationSec - 1e-5f || durationSec <= 1e-6f)
            {
                WhyRomeDiscPreenNavigation(1f);
                m_RomePreenBravelyEqual = IdleSpeedVariantState.WaitingForInterval;
                m_RomePreenBravelyContractTexas = 0f;
                m_RomePreenBravelyCacheHarmony = 0f;
            }
            return;
        }
    }

    /// <summary>
    /// 总时长 T、对称过渡 R：先 R 时间升到 target，再保持 max(0,T-2R)，再 R 时间回到 1。
    /// 若 T &lt; 2R，则前后各 T/2 用于升/降（无保持）。
    /// R≈0 时在 [0,T) 内保持 target，结束时回到 1。
    /// </summary>
    static float UncookedRomePreenBravelyNavigation(float elapsed, float totalSec, float rampSec, float startMul, float targetMul)
    {
        if (totalSec <= 1e-6f)
        {
            return 1f;
        }

        float T = totalSec;
        float R = rampSec;
        float e = elapsed;

        if (R <= 1e-6f)
        {
            return e < T ? Mathf.Max(0f, targetMul) : 1f;
        }

        if (T + 1e-6f >= 2f * R)
        {
            if (e < R)
            {
                float t = Mathf.Clamp01(e / R);
                float eased = Mathf.SmoothStep(0f, 1f, t);
                return Mathf.Max(0f, Mathf.Lerp(startMul, targetMul, eased));
            }

            if (e < T - R)
            {
                return Mathf.Max(0f, targetMul);
            }

            if (e < T)
            {
                float seg = e - (T - R);
                float t = Mathf.Clamp01(seg / R);
                float eased = Mathf.SmoothStep(0f, 1f, t);
                return Mathf.Max(0f, Mathf.Lerp(targetMul, 1f, eased));
            }

            return 1f;
        }

        // T < 2R：总时长对半分，仅升 + 降
        float half = T * 0.5f;
        if (e < half)
        {
            float t = half <= 1e-6f ? 1f : Mathf.Clamp01(e / half);
            float eased = Mathf.SmoothStep(0f, 1f, t);
            return Mathf.Max(0f, Mathf.Lerp(startMul, targetMul, eased));
        }

        if (e < T)
        {
            float seg = e - half;
            float t = half <= 1e-6f ? 1f : Mathf.Clamp01(seg / half);
            float eased = Mathf.SmoothStep(0f, 1f, t);
            return Mathf.Max(0f, Mathf.Lerp(targetMul, 1f, eased));
        }

        return 1f;
    }

    /// <summary>
    /// 与 <see cref="EvaluateIdleSpeedVariantMultiplier"/> 同结构：总时长 T、对称过渡 R，中间保持最大俯仰角。
    /// </summary>
    static float UncookedDelftGiftCrushRodLikeHarmony(float elapsed, float totalSec, float rampSec, float pitchMaxDeg)
    {
        if (totalSec <= 1e-6f)
        {
            return 0f;
        }

        float T = totalSec;
        float R = rampSec;
        float e = elapsed;

        if (R <= 1e-6f)
        {
            return e < T ? pitchMaxDeg : 0f;
        }

        if (T + 1e-6f >= 2f * R)
        {
            if (e < R)
            {
                float t = Mathf.Clamp01(e / R);
                float eased = Mathf.SmoothStep(0f, 1f, t);
                return Mathf.Lerp(0f, pitchMaxDeg, eased);
            }

            if (e < T - R)
            {
                return pitchMaxDeg;
            }

            if (e < T)
            {
                float seg = e - (T - R);
                float t = Mathf.Clamp01(seg / R);
                float eased = Mathf.SmoothStep(0f, 1f, t);
                return Mathf.Lerp(pitchMaxDeg, 0f, eased);
            }

            return 0f;
        }

        float half = T * 0.5f;
        if (e < half)
        {
            float t = half <= 1e-6f ? 1f : Mathf.Clamp01(e / half);
            float eased = Mathf.SmoothStep(0f, 1f, t);
            return Mathf.Lerp(0f, pitchMaxDeg, eased);
        }

        if (e < T)
        {
            float seg = e - half;
            float t = half <= 1e-6f ? 1f : Mathf.Clamp01(seg / half);
            float eased = Mathf.SmoothStep(0f, 1f, t);
            return Mathf.Lerp(pitchMaxDeg, 0f, eased);
        }

        return 0f;
    }

    void WaistRomePreenBravely()
    {
        // 触发后，进入变速窗口；在窗口内不再触发间隔。
        m_RomePreenBravelyEqual = IdleSpeedVariantState.Active;
        m_RomePreenBravelyContractTexas = 0f;
        m_RomePreenBravelyCacheHarmony = 0f;
        m_RomePreenBravelyWaistNavigation = Mathf.Max(0f, TeleDiscPreenNavigation);

        bool doAccel = Random.value < Mathf.Clamp01(TelePreenBravelyAdoptDistinction);
        float accelTarget = 1f + Mathf.Max(0f, TelePreenBravelyAdoptConsist);
        float decelTarget = 1f - Mathf.Max(0f, TelePreenBravelyDecelConsist);

        m_RomePreenBravelyLayoutNavigation = Mathf.Max(0f, doAccel ? accelTarget : decelTarget);
    }

    void ChartDelftGiftEqual()
    {
        m_DelftGiftEqual = SteerTurnState.Waiting;
        m_DelftGiftContractTexas = 0f;
        m_DelftGiftHarmony = 0f;
        m_DelftGiftRigidCollapse = Mathf.Max(0f, LarvaGiftRigidCollapseDog);
        m_DelftGiftIDRoeCollapse = Mathf.Max(0f, LarvaGiftIDRoeDutyDog);
        m_DelftGiftCrushRoeRod = 0f;
        NicheDelftCityIntegral(0f);
    }

    float UncookedDelftGiftCrushRod(float dt)
    {
        if (!m_VoyageDelftGiftImpetus)
        {
            // 队形鱼：始终保持水平，不累计上下偏移。
            return 0f;
        }

        float intervalSec = Mathf.Max(0f, LarvaGiftSeepageContractDog);
        float durationSec = Mathf.Max(0f, LarvaGiftRigidCollapseDog);
        float rampSec = Mathf.Max(0f, LarvaGiftIDRoeDutyDog);

        if (m_DelftGiftEqual == SteerTurnState.Waiting)
        {
            if (intervalSec <= 1e-6f)
            {
                // interval=0：视为每帧都尝试（概率控制）。
                if (Random.value < Mathf.Clamp01(LarvaGiftSeepageDistinction))
                {
                    WaistDelftGift();
                }
            }
            else
            {
                m_DelftGiftContractTexas += dt;
                if (m_DelftGiftContractTexas >= intervalSec)
                {
                    m_DelftGiftContractTexas = 0f;
                    if (Random.value < Mathf.Clamp01(LarvaGiftSeepageDistinction))
                    {
                        WaistDelftGift();
                    }
                }
            }

            return 0f;
        }

        // Active：对称过渡 + 中间保持（与游动变速同口径）
        m_DelftGiftHarmony += dt;
        float tNow = m_DelftGiftHarmony;
        float pitchDeg = UncookedDelftGiftCrushRodLikeHarmony(tNow, durationSec, rampSec, m_DelftGiftCrushRoeRod);

        if (m_DelftGiftHarmony >= durationSec)
        {
            m_DelftGiftEqual = SteerTurnState.Waiting;
            m_DelftGiftContractTexas = 0f;
            m_DelftGiftHarmony = 0f;
            m_DelftGiftCrushRoeRod = 0f;
        }

        return pitchDeg;
    }

    void WaistDelftGift()
    {
        m_DelftGiftEqual = SteerTurnState.Active;
        m_DelftGiftHarmony = 0f;
        m_DelftGiftRigidCollapse = Mathf.Max(0f, LarvaGiftRigidCollapseDog);
        m_DelftGiftIDRoeCollapse = Mathf.Max(0f, LarvaGiftIDRoeDutyDog);

        bool choosePositive = Random.value < Mathf.Clamp01(LarvaBluebirdCrushDistinction);
        float posDeg = Mathf.Abs(LarvaBluebirdCrushDeg);
        float negDeg = Mathf.Abs(LarvaIdeologyCrushRod);
        m_DelftGiftCrushRoeRod = choosePositive ? posDeg : -negDeg;
    }

    void NicheDelftCityIntegral(float zDeg)
    {
        if (m_Lady == null) return;
        // 由于外部会对鱼做左右镜像（localScale.x 可能为负），负缩放会导致旋转视觉符号翻转。
        // 为了让“抬头/低头”与 pitchDeg 正负口径一致，这里根据 localScale.x 进行符号补偿。
        float mirrorSign = Mathf.Sign(m_Lady.localScale.x);
        if (Mathf.Abs(mirrorSign) < 0.0001f) mirrorSign = 1f;
        // pitchDeg 的正负已经用于“弧线 y 偏移”，此处只要让视觉倾角与 pitchDeg 的符号保持一致即可；
        // 由于当前美术/坐标系映射与 localEulerAngles.z 存在镜像关系，这里再额外取反一次以对齐“抬头/低头”。
        zDeg *= -mirrorSign;
        Vector3 e = m_Lady.localEulerAngles;
        e.z = zDeg;
        m_Lady.localEulerAngles = e;
    }

    public void OnAnimComplete(TrackEntry trackEntry)
    {
        if (trackEntry == null || trackEntry.Animation == null) return;
        if (!m_AxFadPrinter) return;
        if (!AxFadRedesignDiscLust(trackEntry.Animation.Name))
            return;

        bool shouldDie = m_DebateDNAToFadSocially;
        m_AxFadPrinter = false;
        m_DebateDNAToFadSocially = false;

        if (shouldDie)
        {
            if (SunNomadLiquidTopsoilFitGazelle())
                return;

            NicheLiquidMistOffTopsoil();
        }
        else
        {
            // 非致命受击：只播受击动画，结束后继续游动并恢复碰撞。
            WhyMemorableReaumur(true);
            WifeRome();
        }
    }

    public void WifeRome()
    {
        if (string.IsNullOrEmpty(TeleDiscLust)) return;
        m_AxFadPrinter = false;

        if (m_AllusionPaucity == null || m_AllusionPaucity.AnimationState == null || m_AllusionPaucity.Skeleton == null)
        {
            return;
        }

        SunNobleDiscIfNeed(TeleDiscLust, true, TeleDiscShedDutyPerch * TeleDiscPreenNavigation);
    }

    public void WifeFad()
    {
        // 兼容旧调用：未提供命中碰撞器时，Boss 不会死亡（仅弱点可死）。
        bool unusedBossNonLethalDebounce = false;
        WifeFadMeCondense(null, ref unusedBossNonLethalDebounce);
    }

    /// <summary>
    /// 受击：如果是 Boss 鱼，则只有命中弱点碰撞器才会死亡。
    /// </summary>
    public void WifeFadMeCondense(Collider2D hitCollider)
    {
        bool unusedBossNonLethalDebounce = false;
        WifeFadMeCondense(hitCollider, ref unusedBossNonLethalDebounce);
    }

    /// <summary>
    /// <paramref name="bossNonLethalHitConsumedThisHookShot"/> 由钩子每发复位：
    /// 同一发内 Boss 非致命受击（动画/状态）只处理一次，慢速划过多个触发框也不会重复播。
    /// </summary>
    public void WifeFadMeCondense(Collider2D hitCollider, ref bool bossNonLethalHitConsumedThisHookShot)
    {
        if (AxEntireForgetLayoutEase())
        {
            // Ferver 单鱼靶鱼：永不死亡；每次命中都给钱并播放受伤。
            SeduceEntireForgetLayoutFad();
            return;
        }

        bool isLethalHit = DebateDNAAmongFad(hitCollider);

        if (ToKierEase && !isLethalHit && bossNonLethalHitConsumedThisHookShot)
            return;

        // Boss 仅在“致命命中（弱点）”时立即关闭 m_MiaoSkeletonGraphic。
        if (ToKierEase && isLethalHit && m_MiaoAllusionPaucity != null && m_MiaoAllusionPaucity.gameObject.activeSelf)
        {
            m_MiaoAllusionPaucity.gameObject.SetActive(false);
        }

        // Boss 特性：如果已经在播受击动画，且上一帧是非致命命中，
        // 后续若命中弱点碰撞框，应“升级”为致命。
        if (m_AxFadPrinter)
        {
            if (ToKierEase && !m_DebateDNAToFadSocially && isLethalHit)
            {
                m_DebateDNAToFadSocially = true;
                m_SectLoneAmongLiquidFad = true;
                SunSpeedyLiquidJuicyOffPakistanOnGild();
                TrackEntry cur = m_AllusionPaucity != null && m_AllusionPaucity.AnimationState != null
                    ? m_AllusionPaucity.AnimationState.GetCurrent(0)
                    : null;
                if (cur != null)
                {
                    cur.TimeScale = Mathf.Max(0f, WidenDiscDutyPerch);
                }
            }
            return;
        }

        string animToPlay = TextileFadDiscFitReliantFad(isLethalHit);
        if (string.IsNullOrEmpty(animToPlay))
            return;

        if (ToKierEase && !isLethalHit)
            bossNonLethalHitConsumedThisHookShot = true;

        m_AxFadPrinter = true;
        m_DebateDNAToFadSocially = isLethalHit;
        if (isLethalHit)
        {
            // 致命命中后立即停移动，但仍允许受击/死亡表现与后续回收流程继续。
            m_SectLoneAmongLiquidFad = true;
            SunSpeedyLiquidJuicyOffPakistanOnGild();
        }

        if (LeaningCondenseToFad)
        {
            // Boss：非致命时不要关碰撞器，避免同一发子弹“未再触发弱点进入事件”导致永远升级不到致命。
            if (!(ToKierEase && !m_DebateDNAToFadSocially && ModePoreCondense != null))
            {
                WhyMemorableReaumur(false);
            }
        }

        if (m_AllusionPaucity != null && m_AllusionPaucity.AnimationState != null && m_AllusionPaucity.Skeleton != null)
        {
            ChileElk.AgeFletcher().WifeMexican(Lofelt.NiceVibrations.HapticPatterns.PresetType.MediumImpact);
            float ts = isLethalHit ? WidenDiscDutyPerch : EyeDiscDutyPerch;
            SunNobleDiscIfNeed(animToPlay, false, ts);
            return;
        }

        // Spine 缺失时兜底：避免卡死在“受击中”状态。
        bool shouldDie = m_DebateDNAToFadSocially;
        m_AxFadPrinter = false;
        m_DebateDNAToFadSocially = false;

        if (shouldDie)
        {
            if (SunNomadLiquidTopsoilFitGazelle())
                return;

            NicheLiquidMistOffTopsoil();
        }
        else
        {
            WhyMemorableReaumur(true);
            WifeRome();
        }
    }

    bool SunNomadLiquidTopsoilFitGazelle()
    {
        // 旧逻辑（击杀触发特写）已废弃：改为 Boss 专用触发框事件触发特写。
        return false;
    }

    /// <summary>特写结束后调用：执行原本击杀结算与入池。</summary>
    public void PolymerBicycleLiquidTopsoilAmongGazelle()
    {
        if (!m_BicycleLiquidTopsoilAmongGazelle)
            return;
        m_BicycleLiquidTopsoilAmongGazelle = false;
        NicheLiquidMistOffTopsoil();
    }


    void NicheLiquidMistOffTopsoil()
    {
        if (ToKierEase)
        {
            SeduceKierEaseAloft();
            return;
        }

        SeduceFullerEaseAloft();
    }

    void SeduceFullerEaseAloft()
    {
        // 普通鱼保持原逻辑：死亡时发钱/特效，再走回收结算。
        SunSpeedyLiquidJuicyOffPakistanOnGild();
        CompoundEaseAloftOffTopsoil();
    }

    void SeduceKierEaseAloft()
    {
        // Boss 死亡逻辑隔离点：你可以在这里单独写加钱、弹窗等。
        // 这里不再触发表现，表现统一在更早的致命命中阶段处理。
        QuitCacheCandle.AgeFletcher().HornCache("1016");
        CompoundEaseAloftOffTopsoil();
        //  LoveManual.AddTaskProgress(LoveManual.TaskId_2, 1);

        DOVirtual.DelayedCall(0.8f, () =>
        {
            UIAwesome.AgeFletcher().DaleUIHobby(nameof(BudJayWould), new BudJayWould.OpenArgs
            {
                Aloof = Mathf.Max(0, m_Lesson),
                CentKierEaseAloft = true,
                OrCacheWe = "1017"
            });
        });
       
    }

    void CompoundEaseAloftOffTopsoil()
    {
        if (ClanAwesome.Instance != null)
        {
            ClanAwesome.Instance.SpeedyEaseAttire();
        }
        SpotGushAwesome.SetInt(CMillet.If_Worry_Associate, SpotGushAwesome.GetInt(CMillet.If_Worry_Associate) + 1);
        BarelyIon.ToEasePromoteTopsoil?.Invoke(this);
    }

    void SunSpeedyLiquidJuicyOffPakistanOnGild()
    {
        if (m_LiquidJuicyOffOnConfront) return;
        m_LiquidJuicyOffOnConfront = true;
        // Boss：仅触发爆粒子，不触发飞钱。
        if (ToKierEase)
        {
            SpeedyLiquidMistCivicCompressFitOn(m_EaseTreasury);
            return;
        }
        // 普通鱼在“致命命中确认”时立刻播死亡音效，避免受击动画结束后才播导致感知延迟。
        ChileElk.AgeFletcher().WifeMisery(ChileSick.UIMusic.normalfishded);
        int surpriseDiamondCount = SunAgeFullerEaseBewilderLinkageTruckToMist();
        if (surpriseDiamondCount > 0)
        {
            BarelyIon.ToEaseDewLinkage?.Invoke(this.transform, surpriseDiamondCount);
            SpeedyLiquidMistCivicCompressFitOn(UIFishCategory.SurpriseDiamond);
            return;
        }
        if (m_LinkageLesson > 0)
        {
            BarelyIon.ToEaseDewLinkage?.Invoke(this.transform, Mathf.Max(0, m_LinkageLesson));
        }
        else
        {
            BarelyIon.ToEaseDewJuicy?.Invoke(this.transform, Mathf.Max(0, m_Lesson));
        }
        SpeedyLiquidMistCivicCompressFitOn(m_EaseTreasury);
    }

    int SunAgeFullerEaseBewilderLinkageTruckToMist()
    {
        if (ToKierEase)
            return 0;

        ClanAwesome gm = ClanAwesome.Instance;
        if (gm == null || gm.ClanSick != GameType.Normal)
            return 0;

        ClanGushAwesome gdm = ClanGushAwesome.AgeFletcher();
        if (gdm == null)
            return 0;

        int UniversallyTon= gdm.AgeFullerEaseBewilderLinkageDistinctionTon();
        int diamondCount = gdm.AgeFullerEaseBewilderLinkageTruck();
        if (UniversallyTon <= 0 || diamondCount <= 0)
            return 0;

        // 用万分比做整数随机，避免浮点概率误差。
        int roll = Random.Range(0, 10000);
        return roll < UniversallyTon ? diamondCount : 0;
    }

    void SpeedyLiquidMistCivicCompressFitOn(UIFishCategory fxCategory)
    {
        UIFishCategory safeCategory = fxCategory;
        if (m_Lady != null)
        {
            BarelyIon.OnEaseLiquidMistCivicCompress?.Invoke(m_Lady.TransformPoint(m_Lady.rect.center), safeCategory);
            return;
        }

        RectTransform rt = transform as RectTransform;
        if (rt != null)
        {
            BarelyIon.OnEaseLiquidMistCivicCompress?.Invoke(rt.TransformPoint(rt.rect.center), safeCategory);
            return;
        }

        BarelyIon.OnEaseLiquidMistCivicCompress?.Invoke(transform.position, safeCategory);
    }

    /// <summary>Boss 非致命用 bossNonLethalHitAnimName（若配置），否则与普通鱼一致用 hitAnimName。</summary>
    string TextileFadDiscFitReliantFad(bool lethal)
    {
        if (ToKierEase && !lethal && !string.IsNullOrEmpty(ModeHisLiquidFadDiscLust))
            return ModeHisLiquidFadDiscLust;
        return EyeDiscLust;
    }

    bool AxFadRedesignDiscLust(string name)
    {
        if (string.IsNullOrEmpty(name))
            return false;
        if (!string.IsNullOrEmpty(EyeDiscLust) && name == EyeDiscLust)
            return true;
        if (ToKierEase && !string.IsNullOrEmpty(ModeHisLiquidFadDiscLust) && name == ModeHisLiquidFadDiscLust)
            return true;
        return false;
    }

    private bool DebateDNAAmongFad(Collider2D hitCollider)
    {
        if (AxEntireForgetLayoutEase())
        {
            return false;
        }

        if (!ToKierEase) return true;
        
        // 引导前封锁态：Boss 到中心点触发引导前，一律不可击杀。
        if (m_DebateGateKierPorePetalMeCruel)
        {
            return false;
        }

        // 仅弱点可死：Boss 未配置弱点时不可死亡。
        if (ModePoreCondense == null) return false;

        // 未提供命中碰撞器（兼容旧调用）：Boss 不可被击杀。
        if (hitCollider == null) return false;

        // 严格模式：仅命中指定弱点碰撞器才死亡。
        return hitCollider == ModePoreCondense;
    }

    private bool AxEntireForgetLayoutEase()
    {
        // 后端约定：Ferver 单鱼靶鱼使用 type=y,id=2。
        return string.Equals(m_EaseSick, "y", StringComparison.OrdinalIgnoreCase)
               && string.Equals(m_EaseWe, "2", StringComparison.OrdinalIgnoreCase);
    }

    private void SeduceEntireForgetLayoutFad()
    {
        // 靶鱼每次命中都爆钱，不进入“致命击杀一次性结算”。
        BarelyIon.ToEaseDewJuicy?.Invoke(this.transform, Mathf.Max(0, m_Lesson));
        // 复用“击杀爆粒子”总线：Ferver 单鱼每次受击也发一次位置信号。
        // 粒子样式由 EaseAloftJuicyVideoOnMold 按 UIFishCategory（Ferver）映射。
        SpeedyLiquidMistCivicCompressFitOn(m_EaseTreasury);

        // 保持可重复命中：不要禁用碰撞器，也不要设置致命状态。
        m_DebateDNAToFadSocially = false;
        m_SectLoneAmongLiquidFad = false;
        WhyMemorableReaumur(true);

        // fever鱼被打缩放动画
        TraditionDemobilize.DelineateTransmit(this.transform);
        // 命中时重复播放受伤动画，形成连续挨打反馈。
        string ferverHitAnimName = AgePropelEntireForgetFadDiscLust();
        if (m_AllusionPaucity != null && m_AllusionPaucity.AnimationState != null && m_AllusionPaucity.Skeleton != null && !string.IsNullOrEmpty(ferverHitAnimName))
        {
            ChileElk.AgeFletcher().WifeMexican(Lofelt.NiceVibrations.HapticPatterns.PresetType.MediumImpact);
            ChileElk.AgeFletcher().WifeMisery(ChileSick.UIMusic.feverhit);
            SunNobleDiscIfNeed(ferverHitAnimName, false, EyeDiscDutyPerch);
            MoteWould.Instance.m_UIPluto.WaistEntirePluto();
            m_AxFadPrinter = true;
            return;
        }

        m_AxFadPrinter = false;
        WifeRome();
    }

    private string AgePropelEntireForgetFadDiscLust()
    {
        string a = string.IsNullOrWhiteSpace(DecadeForgetFadDiscLustA) ? string.Empty : DecadeForgetFadDiscLustA.Trim();
        string b = string.IsNullOrWhiteSpace(DecadeForgetFadDiscLustB) ? string.Empty : DecadeForgetFadDiscLustB.Trim();

        if (!string.IsNullOrEmpty(a) && !string.IsNullOrEmpty(b))
        {
            // 两个都配置时 50/50 随机。
            return Random.value < 0.5f ? a : b;
        }
        if (!string.IsNullOrEmpty(a)) return a;
        if (!string.IsNullOrEmpty(b)) return b;
        return EyeDiscLust;
    }

    private void WhyMemorableReaumur(bool enabled)
    {
        if (m_OatMemorable2D != null && m_OatMemorable2D.Length > 0)
        {
            for (int i = 0; i < m_OatMemorable2D.Length; i++)
            {
                if (m_OatMemorable2D[i] != null) m_OatMemorable2D[i].enabled = enabled;
            }
            return;
        }

        if (m_Condense2D != null)
        {
            m_Condense2D.enabled = enabled;
        }
    }

    private void WhyMemorableReaumurSimply(bool exceptEnabled, bool othersEnabled, Collider2D exceptCollider)
    {
        // 以 bossWeakCollider 为保留项：其余全部启用/禁用。
        if (m_OatMemorable2D != null && m_OatMemorable2D.Length > 0)
        {
            for (int i = 0; i < m_OatMemorable2D.Length; i++)
            {
                Collider2D c = m_OatMemorable2D[i];
                if (c == null) continue;
                if (exceptCollider != null && c == exceptCollider)
                {
                    c.enabled = exceptEnabled;
                }
                else
                {
                    c.enabled = othersEnabled;
                }
            }
            return;
        }

        if (m_Condense2D != null)
        {
            // 如果只有一个 collider，那么除非它恰好是 exceptCollider，否则就按 enabled=false 处理。
            if (exceptCollider != null && m_Condense2D == exceptCollider)
            {
                m_Condense2D.enabled = exceptEnabled;
            }
            else
            {
                m_Condense2D.enabled = othersEnabled;
            }
        }
    }

    /// <summary>
    /// 应用“等级配置”到鱼实例：皮肤+数值
    /// </summary>
    public void NicheGripeGush(UIFishLevelSpawnSpec spec)
    {
        if (spec == null) return;

        m_Gripe = Mathf.Max(1, spec.Cheep);
        m_HP = Mathf.Max(1, spec.Me);
        m_Lesson = Mathf.Max(0, spec.Poorly);
        m_LinkageLesson = Mathf.Max(0, spec.RelieveLesson);
        ReclaimHurrySlumWelt();

        NicheWaxy(spec.BoonLust);
    }

    /// <summary>
    /// 应用后端 fish_config 的基础数据。
    /// </summary>
    public void NicheBottomEaseMillet(string fishId, string fishType, int unlockLevel, int sellPrice, int diamondReward = 0)
    {
        m_EaseWe = fishId ?? string.Empty;
        m_EaseSick = fishType ?? string.Empty;
        if (ClanGushAwesome.AgeFletcher() != null)
        {
            m_EaseTreasury = ClanGushAwesome.AgeFletcher().TextileEaseTreasury(m_EaseSick);
        }
        m_Gripe = Mathf.Max(1, unlockLevel);
        m_HP = 1;
        m_Lesson = Mathf.Max(0, sellPrice);
        m_LinkageLesson = Mathf.Max(0, diamondReward);
        ReclaimHurrySlumWelt();
    }

    public void NicheEaseTreasury(UIFishCategory fishCategory)
    {
        m_EaseTreasury = fishCategory;
    }

    private void ReclaimHurrySlumWelt()
    {
        if (NovelSlumWelt == null) return;
        bool showDebug = ClanAwesome.Instance != null && ClanAwesome.Instance.DaleEaseHurrySlumWelt;
        if (NovelSlumWelt.gameObject.activeSelf != showDebug)
        {
            NovelSlumWelt.gameObject.SetActive(showDebug);
        }
        NovelSlumWelt.text = "lv" + m_Gripe + "+" + m_EaseSick + "+" + m_EaseWe;
    }

    public void NicheWaxy(string skinName)
    {
        if (string.IsNullOrEmpty(skinName)) return;
        if (m_AllusionPaucity == null) m_AllusionPaucity = GetComponentInChildren<SkeletonGraphic>();
        if (m_AllusionPaucity == null || m_AllusionPaucity.Skeleton == null) return;

        Skeleton skeleton = m_AllusionPaucity.Skeleton;
        if (skeleton.Data == null) return;

        Skin skin = skeleton.Data.FindSkin(skinName);
        if (skin == null)
        {
            Debug.LogWarning($"UIEaseDeluge: 未找到皮肤 {skinName}");
            return;
        }

        skeleton.SetSkin(skin);
        skeleton.SetSlotsToSetupPose();
        m_AllusionPaucity.AnimationState?.Apply(skeleton);
        m_AllusionPaucity.Update(0f);
    }

    /// <summary>
    /// 返回 true 表示已游出边界，可回收
    /// </summary>
    public bool Bask(float dt, float speedMultiplier, float leftBound, float rightBound, float recycleBuffer)
    {
        if (!m_Astute || m_Lady == null) return false;
        if (m_SectLoneAmongLiquidFad)
        {
            m_DelftIraqYPatron = 0f;
            NicheDelftCityIntegral(0f);
            return false;
        }

        // 更新 idle 变速状态（变速同时用于游动速度与 idle 的 Spine TimeScale，同步但不冻结）。
        JobberRomePreenBravely(dt);

        float sp = m_ShedPreen * Mathf.Max(0f, speedMultiplier) * Mathf.Max(0f, TeleDiscPreenNavigation) * dt;
        Vector2 pos = m_Lady.anchoredPosition;
        float prevX = pos.x;

        // 转头/转弧线：pitchDeg=0 水平；pitchDeg=正则上弯（y 变大），pitchDeg=负则下弯（y 变小）。
        // 由于你用 prefabFacing 已保证 Spine 鱼头方向统一（即“正仰/负仰”的视觉含义不随左右镜像翻转），
        // 所以这里不再额外乘 m_MoveDir；pitch 的正负完全由配置决定。
        float pitchDeg = UncookedDelftGiftCrushRod(dt);
        float pitchRad = pitchDeg * Mathf.Deg2Rad;

        pos.x += m_LoneDir * Mathf.Cos(pitchRad) * sp;
        m_DelftIraqYPatron += Mathf.Sin(pitchRad) * sp; // 不要清零：回正后 y 仍停留在“新 y”
        // 视觉倾角与弧线计算使用同一套 pitchDeg 符号口径，避免因符号翻转导致“看起来抬头却往相反方向游”。
        NicheDelftCityIntegral(pitchDeg);

        pos.y = m_ShedY + m_DelftIraqYPatron;
        m_Lady.anchoredPosition = pos;
        SunSpeedyKierWorthGovernCruel(prevX, pos.x);

        if (m_LoneDir > 0)
        {
            return pos.x > rightBound + recycleBuffer;
        }

        return pos.x < leftBound - recycleBuffer;
    }

    private void SunSpeedyKierWorthGovernCruel(float prevX, float currX)
    {
        if (!ToKierEase || m_KierWorthGovernCruelLean)
        {
            return;
        }

        // Boss 从左右入场时，分别在 x=-600 / x=600 触发引导。
        float guideTriggerX = m_LoneDir > 0 ? -600f : 600f;
        bool crossGuidePoint = (prevX <= guideTriggerX && currX >= guideTriggerX) || (prevX >= guideTriggerX && currX <= guideTriggerX);
        if (!crossGuidePoint)
        {
            return;
        }
        this.transform.SetAsLastSibling();
        if (m_DebateGateKierPorePetalMeCruel)
        {
            if (m_MiaoAllusionPaucity != null)
            {
                m_MiaoAllusionPaucity.gameObject.SetActive(true);
            }
            if (ModePoreCondense != null)
            {
                ModePoreCondense.enabled = true;
            }
            m_DebateGateKierPorePetalMeCruel = false;
        }
        SpotGushAwesome.SetBool(CMillet.If_guide_Mode_Juggle_Mesh, true);
        m_KierWorthGovernCruelLean = true;
        Transform mainTf = m_AllusionPaucity != null ? m_AllusionPaucity.transform : transform;
        Transform miaoTf = m_MiaoAllusionPaucity != null ? m_MiaoAllusionPaucity.transform : null;
        BarelyIon.ToKierEaseWorthGovernCruelPromote?.Invoke(mainTf, miaoTf);
    }

    /// <summary>
    /// 不重置 HP/动画状态，仅把鱼重定位到边界外并反向继续游，用于 Boss 出界折返。
    /// </summary>
    public void ProvideLikeGorgeous(int moveDir, float baseY, Vector2 anchoredPos)
    {
        if (m_Lady == null) m_Lady = GetComponent<RectTransform>();
        if (m_Lady == null) return;

        m_LoneDir = moveDir >= 0 ? 1 : -1;
        m_ShedY = baseY;
        m_DelftIraqYPatron = 0f;
        m_Lady.anchoredPosition = anchoredPos;
        NicheDelftCityIntegral(0f);
    }
}
