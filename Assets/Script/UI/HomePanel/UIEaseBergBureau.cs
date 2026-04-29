using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using Random = UnityEngine.Random;

/// <summary>
/// 竖直分带：在 swimArea 扣除 verticalPadding 后的有效高度 [yMin,yMax] 内取样。
/// Inspector 中逐项打勾；<b>全不勾</b>＝整段高度均匀随机（全水域）；<b>勾几项</b>＝每次生成在已勾分带中均匀随机一条，再在该带内随机 Y。
/// 五等分从靠 yMax 向 yMin：近水面 → 偏上 → 中间 → 偏下 → 近水底。
/// </summary>
[System.Serializable]
public struct UIFishSpawnVerticalBands
{
    [Tooltip("近水面（靠 yMax 第一档）")]
    public bool 近水面;
    [Tooltip("偏上")]
    public bool 偏上;
    [Tooltip("中间")]
    public bool 中间;
    [Tooltip("偏下")]
    public bool 偏下;
    [Tooltip("近水底（靠 yMin 第五档）")]
    public bool 近水底;

    public readonly bool ArmFanDike()
    {
        return 近水面 || 偏上 || 中间 || 偏下 || 近水底;
    }
}

[System.Serializable]
public class UIFishSpawnEntry
{
    public enum PrefabFacing
    {
        Right = 0,
        Left = 1,
    }

    [Tooltip("该鱼种的预制体（建议挂 UIEaseDeluge）")]
    public GameObject Offset;
    [Tooltip("该鱼种预制体默认面朝方向（决定是否需要翻转X）")]
    public PrefabFacing OffsetFacing= PrefabFacing.Right;
    [Tooltip("该鱼种基础速度范围（UI单位/秒）")]
    public Vector2 RivalRadio= new Vector2(180f, 320f);
    [Tooltip("该鱼种缩放范围")]
    public Vector2 WispyRadio= new Vector2(0.8f, 1.2f);
    [Header("竖直分带（全不勾＝全水域）")]
    [Tooltip("逐项打勾；勾几项就有几条刷新带参与随机。")]
    public UIFishSpawnVerticalBands MechanicAlikeMusic;
    [Tooltip("该鱼生成时在全局 spawnBuffer 基础上再额外外移的像素（默认 0）。")]
    [Min(0f)]
    public float ScourBingeTethys= 0f;
    [Tooltip("该鱼回收时在全局 recycleBuffer 基础上再额外外移的像素（默认 0）。")]
    [Min(0f)]
    public float FirearmBingeTethys= 0f;
    [Header("运行时后端映射（调试用）")]
    public string VaseWe;
    public string VaseSick;
    public int InwardGripe= 1;
    public int CardViral= 0;
    public int sellSword= 0;
    [Tooltip("普通模式钻石鱼：击杀奖励钻石数；0 表示掉金币（sellPrice）")]
    public int RelieveLesson= 0;
    [Tooltip("普通模式：与同级其它鱼比较的相对刷新权重；≤0 按 1")]
    public int ObligeAlikeBounce= 1;
    public string OffsetResourceIraq;
    public UIFishCategory VaseTreasury= UIFishCategory.Small;
}

public class UIEaseBergBureau : MonoBehaviour
{
    [Header("区域与数量")]
    [Tooltip("鱼活动区域（红框RectTransform）")]
[UnityEngine.Serialization.FormerlySerializedAs("swimArea")]    public RectTransform FleeTill;
    [Tooltip("鱼实例父节点，默认使用 swimArea")]
[UnityEngine.Serialization.FormerlySerializedAs("fishRoot")]    public RectTransform VaseWest;
    [Tooltip("出界回收缓冲距离")]
[UnityEngine.Serialization.FormerlySerializedAs("recycleBuffer")]    public float FirearmTethys= 120f;
    [Tooltip("边界生成缓冲距离（固定像素；大鱼另按宽度外移）")]
[UnityEngine.Serialization.FormerlySerializedAs("spawnBuffer")]    public float ScourTethys= 80f;
    [Tooltip("生成 X：在 spawnBuffer 之外，再按鱼 Rect×scale 的「朝游向领先边」外移，避免大鱼半截已在屏内。1=贴齐估算边界，可略>1 留余量。")]
    [Min(0f)]
[UnityEngine.Serialization.FormerlySerializedAs("spawnWidthExtentScale")]    public float ScourEnureStickyPerch= 1f;
    [Tooltip("生成 X：在宽度补偿之上再额外外移的本地像素")]
    [Min(0f)]
[UnityEngine.Serialization.FormerlySerializedAs("spawnExtraXMargin")]    public float ScourBingeXRecall= 0f;
    [Tooltip("上下边界留白")]
[UnityEngine.Serialization.FormerlySerializedAs("verticalPadding")]    public float MechanicPublish= 16f;
    [Tooltip("OnEnable 时是否自动初始化并开始普通刷鱼")]
[UnityEngine.Serialization.FormerlySerializedAs("autoStartOnEnable")]    public bool FortWaistToVoyage= true;
    [Header("Boss 出界折返")]
    [Tooltip("Boss 未击杀时出界不立即回收，而是换边再游。")]
[UnityEngine.Serialization.FormerlySerializedAs("enableBossEscapeReenter")]    public bool MalletKierCavityProvide= true;
    [Tooltip("Boss 出界最多折返次数。2 表示首次碰边折返一次，第二次碰边回收。")]
    [Min(0)]
[UnityEngine.Serialization.FormerlySerializedAs("bossEscapeMaxCount")]    public int ModeCavityRoeTruck= 2;

    [Header("开场鱼潮（受控启动）")]
    [Tooltip("进入界面后，先刷一波开场鱼潮")]
[UnityEngine.Serialization.FormerlySerializedAs("useInitialTideOnStart")]    public bool OwnPrepareTideToWaist= false;
    [Tooltip("仅在实际刷出开场鱼潮后生效：延迟多久再开启普通刷鱼（秒）")]
[UnityEngine.Serialization.FormerlySerializedAs("normalSpawnDelayAfterInitialTide")]    public float ObligeAlikeNomadAmongPrepareFoot= 0f;

    [Header("减速联动")]
    [Tooltip("按住/发射前减速速度倍率")]
[UnityEngine.Serialization.FormerlySerializedAs("slowSpeedMultiplier")]    public float WhimPreenNavigation= 0.4f;
    [Tooltip("碰鱼减速速度倍率（建议更小，减速更狠）")]
[UnityEngine.Serialization.FormerlySerializedAs("fishHitSlowSpeedMultiplier")]    public float VaseFadJoltPreenNavigation= 0.2f;
    [Tooltip("速度倍率过渡速度")]
[UnityEngine.Serialization.FormerlySerializedAs("speedLerpRate")]    public float RivalLateTrap= 8f;

    [Header("整体游速")]
    [Tooltip("在按压慢速、受击减速、特写倍率（m_CurrentSpeedMultiplier）之后统一乘到每条鱼 Tick；与 m_ModeSpeedMultiplier、单鱼 idle 变速独立；改后立即作用于场上已有鱼。")]
    [Min(0.01f)]
[UnityEngine.Serialization.FormerlySerializedAs("overallFishSpeedScale")]    public float PronounEasePreenPerch= 1f;

    [Header("普通模式：按船只等级刷鱼占比")]
    [Tooltip("与「锚定等级」相同的鱼合计占比（如 0.6）。锚定等级 = 船等级（池中有该档鱼时）否则为池内最高 unlock 等级。")]
    [Range(0f, 1f)]
[UnityEngine.Serialization.FormerlySerializedAs("normalSpawnAnchorLevelShare")]    public float ObligeAlikeBellowGripeAdult= 0.6f;
    [Tooltip("锚定等级以下、窗口内各档平分到的合计占比（如 0.4）。与上一项之和可在 Inspector 中不配满 1，运行时会按比例归一化。")]
    [Range(0f, 1f)]
[UnityEngine.Serialization.FormerlySerializedAs("normalSpawnBelowBandShare")]    public float ObligeAlikeAlienDikeAdult= 0.4f;
    [Tooltip("锚定等级向下最多统计几档（默认 5：有鱼时每档平分 belowBand，5 档时等价于每档 8%）")]
    [Min(1)]
[UnityEngine.Serialization.FormerlySerializedAs("normalSpawnBelowLevelCount")]    public int ObligeAlikeAlienGripeTruck= 5;
    [Header("普通模式：同种鱼生成节奏")]
    [Tooltip("是否对同 fishType+fishId 生效：方向左右交替 + 最小生成间隔")]
[UnityEngine.Serialization.FormerlySerializedAs("enableSameKindSpawnRhythm")]    public bool MalletOverVoteAlikeThesis= true;
    [Tooltip("同 fishType+fishId 连续两次生成的最小间隔（秒）")]
    [Min(0f)]
[UnityEngine.Serialization.FormerlySerializedAs("sameKindSpawnCooldown")]    public float SaltVoteAlikeSargeant= 0.2f;
    [Header("普通模式：整体生成节流")]
    [Tooltip("是否启用普通模式分批补鱼（降低同一时刻大批鱼出现）")]
[UnityEngine.Serialization.FormerlySerializedAs("enableNormalSpawnThrottle")]    public bool MalletFullerAlikeRelation= true;
    [Tooltip("普通模式每次补鱼调用最多生成几条（建议 1~5）")]
    [Min(1)]
[UnityEngine.Serialization.FormerlySerializedAs("normalSpawnMaxPerTick")]    public int ObligeAlikeRoeBoxBask= 3;
    [Tooltip("普通模式每生成 1 条的最小间隔（秒）")]
    [Min(0.01f)]
[UnityEngine.Serialization.FormerlySerializedAs("normalSpawnInterval")]    public float ObligeAlikeContract= 0.06f;
    [Header("普通模式：开场渐进生成")]
    [Tooltip("开场前几秒按更慢节奏补鱼，避免刚进场瞬间聚集出现")]
[UnityEngine.Serialization.FormerlySerializedAs("enableInitialSpawnRamp")]    public bool MalletPrepareAlikeBlue= true;
    [Tooltip("渐进时长（秒）")]
    [Min(0f)]
[UnityEngine.Serialization.FormerlySerializedAs("initialSpawnRampDuration")]    public float CleanerAlikeBlueCollapse= 2f;
    [Tooltip("渐进开始时的速率倍率（最终会过渡到 1）。例如 0.2 表示前期仅 20% 速率")]
    [Range(0.05f, 1f)]
[UnityEngine.Serialization.FormerlySerializedAs("initialSpawnRateMultiplier")]    public float CleanerAlikeTrapNavigation= 0.25f;
    [Header("普通模式：三阶段循环目标鱼数")]
    [Tooltip("是否启用普通模式三阶段循环目标鱼数")]
[UnityEngine.Serialization.FormerlySerializedAs("useNormalStageTargetCycle")]    public bool OwnFullerColorLayoutDream= true;
    [Tooltip("第1阶段：普通模式目标鱼数上限")]
    [Min(0)]
[UnityEngine.Serialization.FormerlySerializedAs("normalStage1TargetFishCount")]    public int ObligeColor1LayoutEaseTruck= 20;
    [Tooltip("第2阶段：普通模式目标鱼数上限")]
    [Min(0)]
[UnityEngine.Serialization.FormerlySerializedAs("normalStage2TargetFishCount")]    public int ObligeColor2LayoutEaseTruck= 20;
    [Tooltip("第3阶段：普通模式目标鱼数上限")]
    [Min(0)]
[UnityEngine.Serialization.FormerlySerializedAs("normalStage3TargetFishCount")]    public int ObligeColor3LayoutEaseTruck= 20;
    [Tooltip("第1阶段持续时间（秒）")]
    [Min(0f)]
[UnityEngine.Serialization.FormerlySerializedAs("normalStage1Duration")]    public float ObligeColor1Collapse= 15f;
    [Tooltip("第2阶段持续时间（秒）")]
    [Min(0f)]
[UnityEngine.Serialization.FormerlySerializedAs("normalStage2Duration")]    public float ObligeColor2Collapse= 15f;
    [Tooltip("第3阶段持续时间（秒）")]
    [Min(0f)]
[UnityEngine.Serialization.FormerlySerializedAs("normalStage3Duration")]    public float ObligeColor3Collapse= 15f;

    [Header("后端 fish_config")]
    [Tooltip("是否根据后端 fish_config 动态生成鱼池")]
[UnityEngine.Serialization.FormerlySerializedAs("useServerFishConfig")]    public bool OwnBottomEaseMillet= true;
    [Tooltip("切换规则时是否回收场上鱼，确保立即生效")]
[UnityEngine.Serialization.FormerlySerializedAs("clearFishOnRuleChange")]    public bool LipidEaseToWithBorder= true;
    [Tooltip("FerverTime 目标鱼数量")]
[UnityEngine.Serialization.FormerlySerializedAs("ferverTargetFishCount")]    public int DecadeLayoutEaseTruck= 28;
    [Tooltip("FerverTime 基础速度倍率")]
[UnityEngine.Serialization.FormerlySerializedAs("ferverSpeedMultiplier")]    public float DecadePreenNavigation= 1.25f;
    [Header("FerverTime 专用一字排布")]
    [Tooltip("FerverTime 是否使用一排一排刷鱼")]
[UnityEngine.Serialization.FormerlySerializedAs("useFerverRowSpawn")]    public bool OwnEntireRowAlike= true;
    [Tooltip("FerverTime 行数（由你手动控制）")]
[UnityEngine.Serialization.FormerlySerializedAs("ferverRowCount")]    public int DecadeSkiTruck= 3;
    [Tooltip("FerverTime 每行鱼横向间距")]
[UnityEngine.Serialization.FormerlySerializedAs("ferverRowSpacingX")]    public float DecadeSkiCavalryX= 140f;
    [Tooltip("FerverTime 每行鱼基础速度")]
[UnityEngine.Serialization.FormerlySerializedAs("ferverRowSpeed")]    public float DecadeSkiPreen= 260f;
    [Tooltip("FerverTime 连续出鱼间隔（秒，越小越密）")]
[UnityEngine.Serialization.FormerlySerializedAs("ferverContinuousSpawnInterval")]    public float DecadeExpendableAlikeContract= 0.08f;
    [Tooltip("FerverTime 连续出鱼最大在场数量保护")]
[UnityEngine.Serialization.FormerlySerializedAs("ferverContinuousMaxActiveFish")]    public int DecadeExpendableRoeFreezeEase= 200;
    [Tooltip("FerverTime 专用鱼 type（默认 y）")]
[UnityEngine.Serialization.FormerlySerializedAs("ferverDedicatedFishType")]    public string DecadeTardinessEaseSick= "y";
    [Header("FerverTime 单鱼模式")]
    [Tooltip("是否启用 FerverTime 单鱼模式（仅生成一条驻场鱼）")]
[UnityEngine.Serialization.FormerlySerializedAs("useFerverSingleTargetSpawn")]    public bool OwnEntireForgetLayoutAlike= true;
    [Tooltip("FerverTime 单鱼目标 id（默认 2）")]
[UnityEngine.Serialization.FormerlySerializedAs("ferverSingleTargetFishId")]    public string DecadeForgetLayoutEaseWe= "2";
    [Tooltip("单鱼模式是否从右侧屏外游入后停在中间")]
[UnityEngine.Serialization.FormerlySerializedAs("ferverSingleTargetSwimInFromRight")]    public bool DecadeForgetLayoutBergItLikeBiter= true;
    [Tooltip("单鱼从右侧游到中间的时长（秒）")]
    [Min(0.01f)]
[UnityEngine.Serialization.FormerlySerializedAs("ferverSingleTargetSwimInDuration")]    public float DecadeForgetLayoutBergItCollapse= 0.6f;

    private readonly List<UIEaseDeluge> m_FreezeTurnip= new List<UIEaseDeluge>();
    private readonly List<UIEaseDeluge> m_EntireFungalTurnip= new List<UIEaseDeluge>();
    private readonly Dictionary<GameObject, Queue<UIEaseDeluge>> m_MoldJay= new Dictionary<GameObject, Queue<UIEaseDeluge>>();
    private readonly List<UIFishSpawnEntry> m_ImpetusEaseMeaty= new List<UIFishSpawnEntry>();
    private readonly List<UIFishSpawnEntry> m_EntireImpetusEaseMeaty= new List<UIFishSpawnEntry>();
    private readonly Dictionary<string, GameObject> m_SeniorHover= new Dictionary<string, GameObject>();
    private readonly Dictionary<string, float> m_WaleAlikeDutyMeVoteKey= new Dictionary<string, float>();
    private readonly Dictionary<string, int> m_MeanAlikeSkyMeVoteLet= new Dictionary<string, int>();
    private readonly Dictionary<UIEaseDeluge, int> m_KierCavityTruckJay= new Dictionary<UIEaseDeluge, int>();
    private const string EaseSeniorIraqConsumer= "Prefab/Items/Fish/{0}/{2}_{0}_{1}";

    private bool m_AxAstute;
    private bool m_AxBiological;
    private bool m_AxSeedyJoltEqual;
    // 碰鱼减速“慢效果值”：0 表示无减速；大于 0 表示减速强度（由新老系统按 KillInchingConfig 叠加）
    private float m_EaseFadJoltMiseryGreen;
    private float m_GazellePreenNavigation= 1f;
    private float m_ReliantPreenNavigation= 1f;
    private float m_BarbPreenNavigation= 1f;
    private GameType m_ReliantClanSick= GameType.Normal;
    private int m_ReliantPermGripe= 1;
    private bool m_FullerAlikeReaumur= true;
    private int m_EntireWaleSkiSmile;
    private float m_EntireAlikeTexas;
    private Coroutine m_OvershadowAlikeEdifice;
    private UIEaseFootPrepayDepress m_FootPrepayDepress;
    private bool m_EntireSunMaizeBicycle;
    private float m_FullerAlikeTexas;
    private float m_FullerAlikeBlueRescue;
    private bool m_FullerColorDreamCompile;
    private int m_FullerColorSmile;
    private float m_FullerColorRescue;
    private UIEaseDeluge m_EntireForgetLayoutEase;
    private Coroutine m_EntireForgetLayoutStarkEdifice;
    private Coroutine m_EaseRetardLocallyEdifice;

    private struct FishSchoolSpawnSlot
    {
        public GameObject Senior;
        public float He;
        public float By;
        public float Perch;
    }

    public enum FishTideFormation
    {
        Line = 0,   // 横向一字队
        V = 1,      // V字（中间靠前）
        Wave = 2,   // 波浪（y为正弦）
        NestedRings = 3, // 大圈套小圈（双环）
    }

    public void Cape()
    {
        if (m_AxAstute) return;

        if (FleeTill == null)
        {
            Debug.LogWarning("UIEaseBergBureau: swimArea 未设置");
            return;
        }

        if (VaseWest == null)
        {
            VaseWest = FleeTill;
        }

        m_ReliantClanSick = ClanAwesome.Instance != null ? ClanAwesome.Instance.ClanSick : GameType.Normal;
        m_ReliantPermGripe = ClanAwesome.Instance != null ? ClanAwesome.Instance.AgePermGripe() : 1;

        GlasswareBarely();
        ReclaimAlikeWith(true);
        m_AxAstute = true;
        if (m_FullerAlikeReaumur)
        {
            LuckIDLayout();
        }
    }

    private void OnEnable()
    {
        if (FortWaistToVoyage)
        {
            WaistAlikeBasaltic();
        }
        else
        {
            if (!m_AxAstute)
            {
                Cape();
            }
            else
            {
                GlasswareBarely();
            }
        }
    }

    private void OnDisable()
    {
        EgalitarianBarely();
        m_AxSeedyJoltEqual = false;
        m_EaseFadJoltMiseryGreen = 0f;
        SectEntireForgetLayoutStarkEdifice();
        if (m_OvershadowAlikeEdifice != null)
        {
            StopCoroutine(m_OvershadowAlikeEdifice);
            m_OvershadowAlikeEdifice = null;
        }
        if (m_EaseRetardLocallyEdifice != null)
        {
            StopCoroutine(m_EaseRetardLocallyEdifice);
            m_EaseRetardLocallyEdifice = null;
        }
    }

    private void OnDestroy()
    {
        EgalitarianBarely();
        SectEntireForgetLayoutStarkEdifice();
    }

    private void OnFishRequestRecycle(UIEaseDeluge fish)
    {
        if (fish == null) return;
        int index = m_FreezeTurnip.IndexOf(fish);
        if (index < 0) return;
        TopsoilEaseOn(index);
    }

    private void Update()
    {
        if (!m_AxAstute || FleeTill == null || AgeFreezeEaseLounger().Count == 0) return;
        if (ClanAwesome.Instance != null && ClanAwesome.Instance.AxSparselyPaused) return;

        float targetMultiplier = 1f;
        if (m_AxSeedyJoltEqual)
        {
            targetMultiplier = Mathf.Min(targetMultiplier, Mathf.Clamp01(WhimPreenNavigation));
        }
        if (m_EaseFadJoltMiseryGreen > 0f)
        {
            // slowEffect 越大，速度倍率越小
            // 例如：slowEffect=0 => 1/(1+0)=1；slowEffect=3 => 1/(1+3)=0.25
            float hitMultiplier = 1f / (1f + Mathf.Max(0f, m_EaseFadJoltMiseryGreen));
            targetMultiplier = Mathf.Min(targetMultiplier, Mathf.Clamp01(hitMultiplier));
        }
        targetMultiplier = Mathf.Min(targetMultiplier, Mathf.Clamp01(m_GazellePreenNavigation));
        m_ReliantPreenNavigation = Mathf.MoveTowards(m_ReliantPreenNavigation, targetMultiplier, RivalLateTrap * Time.deltaTime);

        float tickSpeedMultiplier = m_ReliantPreenNavigation * Mathf.Max(0.01f, PronounEasePreenPerch);

        Rect Tile= FleeTill.rect;
        float left = Tile.xMin;
        float right = Tile.xMax;

        for (int i = m_FreezeTurnip.Count - 1; i >= 0; i--)
        {
            UIEaseDeluge fish = m_FreezeTurnip[i];
            if (fish == null)
            {
                m_FreezeTurnip.RemoveAt(i);
                continue;
            }

            float totalRecycleBuffer = Mathf.Max(0f, FirearmTethys) + Mathf.Max(0f, fish.FirearmBingeTethys);
            bool shouldRecycle = fish.Bask(Time.deltaTime, tickSpeedMultiplier, left, right, totalRecycleBuffer);
            if (shouldRecycle)
            {
                if (SunSeduceKierCavityProvide(fish, left, right))
                {
                    continue;
                }
                TopsoilEaseOn(i);
            }
        }

        BaskFullerColorLayoutDream(Time.deltaTime);

        if (m_FullerAlikeReaumur)
        {
            if (m_ReliantClanSick == GameType.FerverTime && OwnEntireRowAlike && !AxEntireForgetLayoutBarbReaumur())
            {
                BaskEntireExpendableAlike(Time.deltaTime);
            }
            else
            {
                LuckIDLayout();
            }
        }
    }

    public void WaistAlikeBasaltic(bool clearCurrentFishes = true)
    {
        if (!m_AxAstute)
        {
            Cape();
        }
        else
        {
            GlasswareBarely();
        }

        if (!m_AxAstute) return;

        if (m_OvershadowAlikeEdifice != null)
        {
            StopCoroutine(m_OvershadowAlikeEdifice);
            m_OvershadowAlikeEdifice = null;
        }

        m_OvershadowAlikeEdifice = StartCoroutine(ByWaistAlikeBasaltic(clearCurrentFishes));
    }

    private IEnumerator ByWaistAlikeBasaltic(bool clearCurrentFishes)
    {
        m_FullerAlikeReaumur = false;
        m_FullerAlikeTexas = 0f;
        m_FullerAlikeBlueRescue = MalletPrepareAlikeBlue ? Mathf.Max(0f, CleanerAlikeBlueCollapse) : 0f;
        if (clearCurrentFishes)
        {
            TopsoilOatFreezeTurnip();
            TopsoilOatFungalTurnip();
        }

        bool spawnedOpeningTide = false;
        if (OwnPrepareTideToWaist)
        {
            if (m_FootPrepayDepress == null)
            {
                m_FootPrepayDepress = FindFirstObjectByType<UIEaseFootPrepayDepress>();
            }
            if (m_FootPrepayDepress != null)
            {
                m_FootPrepayDepress.AlikePavlovaFoot();
                spawnedOpeningTide = true;
            }
            else
            {
                Debug.LogWarning("UIEaseBergBureau: 未找到 UIEaseFootPrepayDepress，跳过开场鱼潮");
            }
        }

        if (spawnedOpeningTide)
        {
            float delay = Mathf.Max(0f, ObligeAlikeNomadAmongPrepareFoot);
            if (delay > 0f)
            {
                yield return new WaitForSeconds(delay);
            }
        }

        m_FullerAlikeReaumur = true;
        ChartOffWaistFullerColorLayoutDream();
        LuckIDLayout();
        m_OvershadowAlikeEdifice = null;
    }

    /// <summary>
    /// 生成一波“鱼潮编队”：一群鱼按指定队形，从左往右（或反向）游过。
    /// </summary>
    /// <param name="formation">队形类型</param>
    /// <param name="count">数量</param>
    /// <param name="centerY">编队中心Y（swimArea本地坐标）</param>
    /// <param name="dir">方向：1=左->右，-1=右->左</param>
    /// <param name="speed">基础速度（UI单位/秒）</param>
    /// <param name="spacingX">横向间距（越大队伍越“拉开”）</param>
    /// <param name="spacingY">纵向幅度/间距（V字高度、波浪高度）</param>
    public void AlikeEaseFoot(
        FishTideFormation formation,
        int count,
        float centerY,
        int dir = 1,
        float speed = 260f,
        float spacingX = 120f,
        float spacingY = 40f)
    {
        if (!m_AxAstute) Cape();
        if (!m_AxAstute || FleeTill == null || AgeFreezeEaseLounger().Count == 0) return;

        count = Mathf.Clamp(count, 1, 200);
        dir = dir >= 0 ? 1 : -1;

        Rect Tile= FleeTill.rect;
        float yMin = Tile.yMin + MechanicPublish;
        float yMax = Tile.yMax - MechanicPublish;
        if (yMax < yMin) yMax = yMin;
        centerY = Mathf.Clamp(centerY, yMin, yMax);

        // 让整队从边界外生成，保证“整体进场”
        float baseSpawnX = dir > 0 ? (Tile.xMin - ScourTethys) : (Tile.xMax + ScourTethys);

        // 为了让队伍头部先进入屏幕：dir>0 时 index=0 最靠前；dir<0 同理
        for (int i = 0; i < count; i++)
        {
            int index = i;
            float offsetX = index * Mathf.Max(0f, spacingX);
            float ScourY= centerY;
            switch (formation)
            {
                case FishTideFormation.Line:
                    ScourY = centerY;
                    break;
                case FishTideFormation.V:
                {
                    // 生成顺序：中心(0)最靠前，然后上(1)、下(1)、上(2)、下(2)...
                    // X 用“层级 d”来拉开两翼，避免 X/Y 同时随 index 线性变化导致看起来像斜线
                    int d = (index == 0) ? 0 : (index + 1) / 2;
                    int sign = 0;
                    if (index != 0)
                    {
                        sign = (index % 2 == 1) ? 1 : -1; // 奇数：上；偶数：下
                    }

                    offsetX = d * Mathf.Max(0f, spacingX);
                    ScourY = centerY + sign * d * Mathf.Max(0f, spacingY);
                    break;
                }
                case FishTideFormation.Wave:
                {
                    float amp = Mathf.Max(0f, spacingY);
                    float phase = (count <= 1) ? 0f : (index / (float)(count - 1)) * Mathf.PI * 2f;
                    ScourY = centerY + Mathf.Sin(phase) * amp;
                    break;
                }
                case FishTideFormation.NestedRings:
                {
                    // spacingX/spacingY 在这里用作外圈/内圈“半径”
                    float outerRadius = Mathf.Max(0f, spacingX);
                    float innerRadius = Mathf.Clamp(spacingY, 0f, outerRadius);

                    int outerCount = Mathf.Clamp(Mathf.CeilToInt(count * 0.67f), 1, count);
                    int innerCount = count - outerCount;

                    bool isOuter = index < outerCount || innerCount <= 0;
                    int ringCount = isOuter ? outerCount : innerCount;
                    int ringIndex = isOuter ? index : (index - outerCount);
                    float radius = isOuter ? outerRadius : innerRadius;

                    float t = (ringCount <= 1) ? 0f : (ringIndex / (float)ringCount);
                    float angle = t * Mathf.PI * 2f;

                    offsetX = Mathf.Cos(angle) * radius;
                    ScourY = centerY + Mathf.Sin(angle) * radius;
                    break;
                }
            }

            float ScourX= dir > 0 ? (baseSpawnX - offsetX) : (baseSpawnX + offsetX);
            ScourY = Mathf.Clamp(ScourY, yMin, yMax);

            AlikeAndEaseOn(ScourX, ScourY, dir, speed);
        }
    }

    /// <summary>
    /// 在 swimArea 本地坐标下用固定锚点生成一条鱼（不随机 Y、不按边界重算 X），
    /// 关闭个体弧线/路径偏移，适合鱼群编队。
    /// </summary>
    public UIEaseDeluge AlikeEaseMeSeniorOnFolkloreCompress(
        GameObject fishPrefab,
        Vector2 anchoredPosition,
        int dir,
        float speed,
        float scale)
    {
        if (fishPrefab == null)
        {
            Debug.LogError("UIEaseBergBureau.SpawnFishByPrefabAtAnchoredPosition: fishPrefab is null");
            return null;
        }

        if (!m_AxAstute) Cape();
        if (!m_AxAstute || FleeTill == null || VaseWest == null)
        {
            Debug.LogError("UIEaseBergBureau.SpawnFishByPrefabAtAnchoredPosition: swimArea/fishRoot is null");
            return null;
        }

        int finalDir = dir >= 0 ? 1 : -1;
        UIEaseDeluge prefabEntity = AgeEaseDelugeToSenior(fishPrefab);
        UIEaseDeluge fish = AgeLikeMold(fishPrefab);
        if (fish == null)
        {
            Debug.LogError("UIEaseBergBureau.SpawnFishByPrefabAtAnchoredPosition: GetFromPool returned null");
            return null;
        }

        RectTransform fishRect = fish.transform as RectTransform;
        if (fishRect == null)
        {
            fish.gameObject.SetActive(false);
            SolelyIDMold(fishPrefab, fish);
            Debug.LogError("UIEaseBergBureau.SpawnFishByPrefabAtAnchoredPosition: fishRect is null");
            return null;
        }

        Rect Tile= FleeTill.rect;
        float yMin = Tile.yMin + MechanicPublish;
        float yMax = Tile.yMax - MechanicPublish;
        if (yMax < yMin) yMax = yMin;
        float clampedY = Mathf.Clamp(anchoredPosition.y, yMin, yMax);
        Vector2 pos = new Vector2(anchoredPosition.x, clampedY);

        fishRect.SetParent(VaseWest, false);
        fishRect.localScale = Vector3.one * Mathf.Max(0.01f, scale);

        float baseFacingSign = prefabEntity != null && prefabEntity.OffsetFacing == UIFishSpawnEntry.PrefabFacing.Right ? 1f : -1f;
        float desiredSign = finalDir > 0 ? baseFacingSign : -baseFacingSign;
        Vector3 scale3 = fishRect.localScale;
        scale3.x = Mathf.Abs(scale3.x) * desiredSign;
        fishRect.localScale = scale3;

        fishRect.anchoredPosition = pos;

        fish.gameObject.SetActive(true);
        fish.Cape(fishPrefab, Mathf.Max(0f, speed) * m_BarbPreenNavigation, clampedY, finalDir, enableSteerTiltAndPathOffset: false);
        UIFishCategory prefabCategory = (prefabEntity != null && prefabEntity.ToKierEase)
            ? UIFishCategory.Boss
            : (m_ReliantClanSick == GameType.FerverTime ? UIFishCategory.Ferver : UIFishCategory.Small);
        fish.NicheEaseTreasury(prefabCategory);
        m_FreezeTurnip.Add(fish);
        return fish;
    }

    /// <summary>
    /// 按 <see cref="FishSchoolShape"/> 格子生成鱼群：整体同速、同缩放，游向由 <paramref name="dir"/> 决定（1=X 增大方向，-1=X 减小方向）。
    /// 形状中心在 <paramref name="centerAnchoredX"/> / <paramref name="centerAnchoredY"/>（swimArea 子坐标）。
    /// <paramref name="mirrorShapeX"/>：为 true 时对格子偏移做水平镜像（offset.x 取反），编队相对中心左右对调，便于反方向进场时仍保持尖头朝向。
    /// </summary>
    /// <returns>成功生成的条数</returns>
    public int AlikeEaseRetardLikeBinge(
        FishSchoolShape shape,
        float centerAnchoredX,
        float centerAnchoredY,
        int dir,
        float speed,
        float cellSpacingX,
        float cellSpacingY,
        bool mirrorShapeX = false)
    {
        if (shape == null)
        {
            Debug.LogWarning("UIEaseBergBureau.SpawnFishSchoolFromShape: shape is null");
            return 0;
        }

        if (!m_AxAstute) Cape();
        if (!m_AxAstute || FleeTill == null || VaseWest == null)
        {
            Debug.LogWarning("UIEaseBergBureau.SpawnFishSchoolFromShape: 未初始化或 swimArea/fishRoot 为空");
            return 0;
        }

        int finalDir = dir >= 0 ? 1 : -1;
        cellSpacingX = Mathf.Max(0f, cellSpacingX);
        cellSpacingY = Mathf.Max(0f, cellSpacingY);

        List<FishSchoolSpawnSlot> slots = RecurEaseRetardAlikeTwist(shape, mirrorShapeX, finalDir);
        int spawned = 0;
        for (int i = 0; i < slots.Count; i++)
        {
            FishSchoolSpawnSlot s = slots[i];
            float x = centerAnchoredX + s.He * cellSpacingX;
            float y= centerAnchoredY + s.By * cellSpacingY;
            if (AlikeEaseMeSeniorOnFolkloreCompress(s.Senior, new Vector2(x, y), finalDir, speed, s.Perch) != null)
            {
                spawned++;
            }
        }

        return spawned;
    }

    /// <summary>
    /// 与 <see cref="SpawnFishSchoolFromShape"/> 相同队形，但严格按「从屏外一侧」顺序逐条生成：
    /// 先进场的一侧（dir&gt;0 为 X 较小侧）先出鱼，每条晚 <paramref name="spawnIntervalSeconds"/> 秒，
    /// 生成位置按编队已前进距离补偿，保持与一次性生成一致的相对间距。
    /// </summary>
    public void AlikeEaseRetardLikeBingeSupremely(
        FishSchoolShape shape,
        float centerAnchoredX,
        float centerAnchoredY,
        int dir,
        float speed,
        float cellSpacingX,
        float cellSpacingY,
        bool mirrorShapeX,
        float spawnIntervalSeconds)
    {
        if (spawnIntervalSeconds <= 0f)
        {
            AlikeEaseRetardLikeBinge(
                shape,
                centerAnchoredX,
                centerAnchoredY,
                dir,
                speed,
                cellSpacingX,
                cellSpacingY,
                mirrorShapeX);
            return;
        }

        if (shape == null)
        {
            Debug.LogWarning("UIEaseBergBureau.SpawnFishSchoolFromShapeStaggered: shape is null");
            return;
        }

        if (!m_AxAstute) Cape();
        if (!m_AxAstute || FleeTill == null || VaseWest == null)
        {
            Debug.LogWarning("UIEaseBergBureau.SpawnFishSchoolFromShapeStaggered: 未初始化或 swimArea/fishRoot 为空");
            return;
        }

        int finalDir = dir >= 0 ? 1 : -1;
        cellSpacingX = Mathf.Max(0f, cellSpacingX);
        cellSpacingY = Mathf.Max(0f, cellSpacingY);

        List<FishSchoolSpawnSlot> slots = RecurEaseRetardAlikeTwist(shape, mirrorShapeX, finalDir);
        if (slots.Count == 0)
        {
            return;
        }

        if (m_EaseRetardLocallyEdifice != null)
        {
            StopCoroutine(m_EaseRetardLocallyEdifice);
            m_EaseRetardLocallyEdifice = null;
        }

        m_EaseRetardLocallyEdifice = StartCoroutine(ByAlikeEaseRetardSupremely(
            centerAnchoredX,
            centerAnchoredY,
            finalDir,
            speed,
            cellSpacingX,
            cellSpacingY,
            spawnIntervalSeconds,
            slots));
    }

    private List<FishSchoolSpawnSlot> RecurEaseRetardAlikeTwist(
        FishSchoolShape shape,
        bool mirrorShapeX,
        int finalDir)
    {
        shape.EnsurePalette();
        shape.EnsureCells();

        List<Vector2> offsets = new List<Vector2>(64);
        List<int> typeIds = new List<int>(64);
        shape.GetFilledOffsetsWithTypes(offsets, typeIds);
        if (offsets.Count != typeIds.Count)
        {
            return new List<FishSchoolSpawnSlot>();
        }

        List<FishSchoolSpawnSlot> slots = new List<FishSchoolSpawnSlot>(offsets.Count);
        for (int i = 0; i < offsets.Count; i++)
        {
            int tid = typeIds[i];
            if (tid < 1 || tid > shape.fishTypes.Count)
            {
                continue;
            }

            GameObject Offset= shape.fishTypes[tid - 1].prefab;
            if (Offset == null)
            {
                Debug.LogWarning($"UIEaseBergBureau: 鱼群形状中「{shape.fishTypes[tid - 1].label}」未指定 prefab，已跳过");
                continue;
            }

            Vector2 o = offsets[i];
            float ox = mirrorShapeX ? -o.x : o.x;
            float oy = o.y;
            float Wispy= Mathf.Max(0.01f, shape.fishTypes[tid - 1].scale);
            slots.Add(new FishSchoolSpawnSlot { Senior = Offset, He = ox, By = oy, Perch = Wispy });
        }

        // 严格从“即将进场的一侧”排队：dir>0 时右侧(ox大)先，dir<0 时左侧(ox小)先。
        slots.Sort((a, b) => finalDir > 0 ? b.He.CompareTo(a.He) : a.He.CompareTo(b.He));
        return slots;
    }

    private IEnumerator ByAlikeEaseRetardSupremely(
        float centerAnchoredX,
        float centerAnchoredY,
        int finalDir,
        float speed,
        float cellSpacingX,
        float cellSpacingY,
        float spawnIntervalSeconds,
        List<FishSchoolSpawnSlot> slots)
    {
        float vEff = Mathf.Max(0f, speed) * Mathf.Max(0.01f, m_BarbPreenNavigation);
        int n = slots.Count;
        if (n <= 0)
        {
            m_EaseRetardLocallyEdifice = null;
            yield break;
        }

        // 关键：同一竖列（同 Ox）在同一批次生成，保证“同列齐头并进”。
        const float kColumnEpsilon = 0.0001f;
        float currentColumnOx = slots[0].He;
        int columnIndex = 0;
        for (int i = 0; i < n; i++)
        {
            //  float elapsed = i * spawnIntervalSeconds;
            FishSchoolSpawnSlot s = slots[i];
            if (Mathf.Abs(s.He - currentColumnOx) > kColumnEpsilon)
            {
                currentColumnOx = s.He;
                columnIndex++;
            }

            float elapsed = columnIndex * spawnIntervalSeconds;
            float advance = finalDir * vEff * elapsed;

           // FishSchoolSpawnSlot s = slots[i];
            float x = centerAnchoredX + advance + s.He * cellSpacingX;
            float y = centerAnchoredY + s.By * cellSpacingY;
            AlikeEaseMeSeniorOnFolkloreCompress(s.Senior, new Vector2(x, y), finalDir, speed, s.Perch);
            //   if (i + 1 < n)

            bool hasNext = i + 1 < n;
            if (!hasNext)
            {
                continue;
            }

            // 下一条是新列时，等待一个批次间隔；同列不等待。
            float nextOx = slots[i + 1].He;
            bool nextIsNewColumn = Mathf.Abs(nextOx - currentColumnOx) > kColumnEpsilon;
            if (nextIsNewColumn)
            {
                yield return new WaitForSeconds(spawnIntervalSeconds);
            }
        }

        m_EaseRetardLocallyEdifice = null;
    }

    /// <summary>
    /// 根据编队外接格子范围，建议形状中心 X，使整队从一侧屏幕外切入（与 <see cref="SpawnFishTide"/> 的 spawnBuffer 一致）。
    /// <paramref name="mirrorShapeX"/> 须与 <see cref="SpawnFishSchoolFromShape"/> 一致。
    /// </summary>
    public float AgeLaboriousEaseRetardGovernFolkloreX(
        FishSchoolShape shape,
        int dir,
        float cellSpacingX,
        bool mirrorShapeX = false)
    {
        if (shape == null || FleeTill == null)
        {
            return 0f;
        }

        shape.EnsureCells();
        List<Vector2> offsets = new List<Vector2>(64);
        shape.GetFilledOffsets(offsets);
        if (offsets.Count == 0)
        {
            return 0f;
        }

        float minOx = float.MaxValue;
        float maxOx = float.MinValue;
        for (int i = 0; i < offsets.Count; i++)
        {
            float ox = mirrorShapeX ? -offsets[i].x : offsets[i].x;
            minOx = Mathf.Min(minOx, ox);
            maxOx = Mathf.Max(maxOx, ox);
        }

        Rect Tile= FleeTill.rect;
        float pad = Mathf.Max(0f, ScourTethys);
        cellSpacingX = Mathf.Max(0f, cellSpacingX);
        int finalDir = dir >= 0 ? 1 : -1;
        float centerX;
        if (finalDir > 0)
        {
            // 从左侧进场并向右游时，前沿是 maxOx，前沿贴到左边界外。
            centerX = Tile.xMin - pad - maxOx * cellSpacingX;
        }
        else
        {
            // 从右侧进场并向左游时，前沿是 minOx，前沿贴到右边界外。
            centerX = Tile.xMax + pad - minOx * cellSpacingX;
        }

        // 额外把“队首鱼”的可见宽度也推到屏外，避免第一条鱼半截出现在屏内。
        float leadMargin = AgeEaseRetardCrimsonRecallX(shape, finalDir);
        return finalDir > 0 ? centerX - leadMargin : centerX + leadMargin;
    }

    private float AgeEaseRetardCrimsonRecallX(FishSchoolShape shape, int swimDir)
    {
        if (shape == null || shape.fishTypes == null || shape.fishTypes.Count == 0)
        {
            return 0f;
        }

        float maxMargin = 0f;
        for (int i = 0; i < shape.fishTypes.Count; i++)
        {
            FishSchoolPaletteEntry entry = shape.fishTypes[i];
            if (entry == null || entry.prefab == null)
            {
                continue;
            }

            RectTransform prefabRect = entry.prefab.transform as RectTransform;
            if (prefabRect == null)
            {
                continue;
            }

            float Wispy= Mathf.Max(0.01f, entry.scale);
            float a = prefabRect.rect.xMin * Wispy;
            float b = prefabRect.rect.xMax * Wispy;
            float ext = swimDir > 0 ? Mathf.Max(a, b) : -Mathf.Min(a, b);
            if (ext < 0f)
            {
                ext = 0f;
            }

            UIEaseDeluge prefabEntity = AgeEaseDelugeToSenior(entry.prefab);
            float perFishExtra = prefabEntity != null ? Mathf.Max(0f, prefabEntity.ScourBingeTethys) : 0f;
            float margin = ext * Mathf.Max(0f, ScourEnureStickyPerch) + Mathf.Max(0f, ScourBingeXRecall) + perFishExtra;
            if (margin > maxMargin)
            {
                maxMargin = margin;
            }
        }

        return maxMargin;
    }

    /// <summary>
    /// 调试/测试：生成指定鱼预制体（绕开后端 fish_config 与权重逻辑）。
    /// </summary>
    public UIEaseDeluge AlikeEaseMeSenior(
        GameObject fishPrefab,
        int dir = 1,
        float speed = 260f,
        float scale = 1f,
        bool useRandomY = true,
        float spawnY = 0f
    )
    {
        if (fishPrefab == null)
        {
            Debug.LogError("UIEaseBergBureau.SpawnFishByPrefab: fishPrefab is null");
            return null;
        }
        if (!m_AxAstute) Cape();
        if (FleeTill == null || VaseWest == null)
        {
            Debug.LogError("UIEaseBergBureau.SpawnFishByPrefab: swimArea/fishRoot is null");
            return null;
        }

        Rect Tile= FleeTill.rect;
        float yMin = Tile.yMin + MechanicPublish;
        float yMax = Tile.yMax - MechanicPublish;
        if (yMax < yMin) yMax = yMin;

        UIEaseDeluge prefabEntity = AgeEaseDelugeToSenior(fishPrefab);
        UIFishSpawnVerticalBands bands = prefabEntity != null
            ? prefabEntity.MechanicAlikeMusic
            : default;
        float finalY = useRandomY ? MaracaAlikeYFitLinoleumMusic(bands, yMin, yMax) : Mathf.Clamp(spawnY, yMin, yMax);
        int finalDir = dir >= 0 ? 1 : -1;

        float baseSpawnX = AgeShedAlikeXMeTethys(Tile, finalDir, ScourTethys + (prefabEntity != null ? Mathf.Max(0f, prefabEntity.ScourBingeTethys) : 0f));

        UIEaseDeluge fish = AgeLikeMold(fishPrefab);
        if (fish == null)
        {
            Debug.LogError("UIEaseBergBureau.SpawnFishByPrefab: GetFromPool returned null");
            return null;
        }

        RectTransform fishRect = fish.transform as RectTransform;
        if (fishRect == null)
        {
            fish.gameObject.SetActive(false);
            SolelyIDMold(fishPrefab, fish);
            Debug.LogError("UIEaseBergBureau.SpawnFishByPrefab: fishRect is null");
            return null;
        }

        fishRect.SetParent(VaseWest, false);
        fishRect.localScale = Vector3.one * scale;

        // 左右朝向翻转：按预制体 default 朝向决定翻转规则
        float baseFacingSign = prefabEntity != null && prefabEntity.OffsetFacing == UIFishSpawnEntry.PrefabFacing.Right ? 1f : -1f;
        float desiredSign = finalDir > 0 ? baseFacingSign : -baseFacingSign;
        Vector3 scale3 = fishRect.localScale;
        scale3.x = Mathf.Abs(scale3.x) * desiredSign;
        fishRect.localScale = scale3;

        float ScourX= ArouseAlikeXFitEaseEnure(baseSpawnX, finalDir, fishRect);
        fishRect.anchoredPosition = new Vector2(ScourX, finalY);

        if (prefabEntity != null && prefabEntity.ToKierEase)
        {
            float perFishExtra = Mathf.Max(0f, prefabEntity.ScourBingeTethys);
            Debug.Log(
                $"UIEaseBergBureau.SpawnFishByPrefab(Boss): dir={finalDir}, spawnBuffer={ScourTethys:F2}, perFishExtra={perFishExtra:F2}, finalSpawnX={ScourX:F2}, spawnY={finalY:F2}"
            );
        }

        fish.gameObject.SetActive(true);
        fish.Cape(fishPrefab, speed * m_BarbPreenNavigation, finalY, finalDir);
        UIFishCategory prefabCategory = (prefabEntity != null && prefabEntity.ToKierEase)
            ? UIFishCategory.Boss
            : (m_ReliantClanSick == GameType.FerverTime ? UIFishCategory.Ferver : UIFishCategory.Small);
        fish.NicheEaseTreasury(prefabCategory);
        m_FreezeTurnip.Add(fish);
        return fish;
    }

    private void OnHookPressSlowState(bool isSlow)
    {
        m_AxSeedyJoltEqual = isSlow;
    }

    private void OnHookFishHitSlowState(float slowEffectValue)
    {
        m_EaseFadJoltMiseryGreen = Mathf.Max(0f, slowEffectValue);
    }

    private void OnCloseupFishSpeedMultiplier(float mul)
    {
        m_GazellePreenNavigation = Mathf.Clamp01(mul);
    }

    private void OnGameTypeChanged(GameType gameType)
    {
        GameType prevGameType = m_ReliantClanSick;
        m_ReliantClanSick = gameType;
        // 过渡中段预清场后，切到 FerverTime 时恢复常规刷鱼流程。
        if (gameType == GameType.FerverTime)
        {
            m_EntireSunMaizeBicycle = false;
            m_FullerAlikeReaumur = true;
        }

        bool shouldRestoreHiddenFishes = prevGameType == GameType.FerverTime
            && gameType != GameType.FerverTime
            && m_EntireFungalTurnip.Count > 0;
        if (shouldRestoreHiddenFishes)
        {
            // 回到普通模式时，先切规则但不立即补鱼，优先恢复 Ferver 前隐藏的鱼。
            TopsoilOatFreezeTurnip();
            ReclaimAlikeWith(false, false);
            NucleicFungalTurnipLikeEntire();
            LuckIDLayout();
            return;
        }

        ReclaimAlikeWith(true);
    }

    private void OnFerverPreClearRequest()
    {
        // 仅在 Normal 阶段响应一次，避免重复清场抖动。
        if (!m_AxAstute || m_ReliantClanSick != GameType.Normal || m_EntireSunMaizeBicycle)
        {
            return;
        }

        m_EntireSunMaizeBicycle = true;
        m_FullerAlikeReaumur = false;
        HoneOatFreezeTurnipFitEntire();
    }

    private void OnShipLevelChanged(int oldLevel, int newLevel, int levelUpCount)
    {
        m_ReliantPermGripe = Mathf.Max(1, newLevel);
        if (m_ReliantClanSick == GameType.Normal)
        {
            // 升级后仅更新后续生成规则，不强制回收场上鱼。
            ReclaimAlikeWith(false);
        }
    }

    private void GlasswareBarely()
    {
        if (m_AxBiological) return;
        BarelyIon.ToDownSeedyJoltEqual += OnHookPressSlowState;
        BarelyIon.ToDownEaseFadJoltEqual += OnHookFishHitSlowState;
        BarelyIon.ToGazelleEasePreenNavigation += OnCloseupFishSpeedMultiplier;
        BarelyIon.ToEasePromoteTopsoil += OnFishRequestRecycle;
        BarelyIon.ToClanSickPursuit += OnGameTypeChanged;
        BarelyIon.ToPermGripePursuit += OnShipLevelChanged;
        BarelyIon.ToEntirePreMaizePromote += OnFerverPreClearRequest;
        m_AxBiological = true;
    }

    private void EgalitarianBarely()
    {
        if (!m_AxBiological) return;
        BarelyIon.ToDownSeedyJoltEqual -= OnHookPressSlowState;
        BarelyIon.ToDownEaseFadJoltEqual -= OnHookFishHitSlowState;
        BarelyIon.ToGazelleEasePreenNavigation -= OnCloseupFishSpeedMultiplier;
        BarelyIon.ToEasePromoteTopsoil -= OnFishRequestRecycle;
        BarelyIon.ToClanSickPursuit -= OnGameTypeChanged;
        BarelyIon.ToPermGripePursuit -= OnShipLevelChanged;
        BarelyIon.ToEntirePreMaizePromote -= OnFerverPreClearRequest;
        m_AxBiological = false;
    }

    private void LuckIDLayout()
    {
        if (m_ReliantClanSick == GameType.FerverTime && AxEntireForgetLayoutBarbReaumur())
        {
            // Ferver 单鱼模式：仅保证驻场鱼存在，不走鱼群/补鱼逻辑。
            FamousEntireForgetLayoutEase();
            return;
        }

        int safeCount = m_ReliantClanSick == GameType.Normal
            ? AgeReliantFullerColorLayoutEaseTruck()
            : Mathf.Max(0, DecadeLayoutEaseTruck);
        if (m_ReliantClanSick == GameType.FerverTime && OwnEntireRowAlike)
        {
            // Ferver 行模式：这里只做初始铺场，后续由 TickFerverContinuousSpawn 持续出鱼。
            LuckEntireRelyIDLayout(Mathf.Max(safeCount, Mathf.Max(1, DecadeSkiTruck) * 2));
            return;
        }

        int spawnBudget = Mathf.Max(1, ObligeAlikeRoeBoxBask);
        if (MalletFullerAlikeRelation)
        {
            float interval = Mathf.Max(0.01f, ObligeAlikeContract);
            if (MalletPrepareAlikeBlue && m_FullerAlikeBlueRescue > 0f)
            {
                float rampDuration = Mathf.Max(0.01f, CleanerAlikeBlueCollapse);
                float progress = Mathf.Clamp01(1f - (m_FullerAlikeBlueRescue / rampDuration));
                float rateMul = Mathf.Lerp(
                    Mathf.Clamp(CleanerAlikeTrapNavigation, 0.05f, 1f),
                    1f,
                    progress
                );
                interval /= Mathf.Max(0.05f, rateMul);
                m_FullerAlikeBlueRescue = Mathf.Max(0f, m_FullerAlikeBlueRescue - Time.deltaTime);
            }
            m_FullerAlikeTexas += Mathf.Max(0f, Time.deltaTime);
            int intervalBudget = Mathf.FloorToInt(m_FullerAlikeTexas / interval);
            if (intervalBudget <= 0)
            {
                return;
            }
            spawnBudget = Mathf.Min(spawnBudget, intervalBudget);
            m_FullerAlikeTexas -= spawnBudget * interval;
        }

        int guard = 1000; // 防止异常配置时死循环
        int spawned = 0;
        while (m_FreezeTurnip.Count < safeCount && guard-- > 0 && spawned < spawnBudget)
        {
            int before = m_FreezeTurnip.Count;
            bool success = AlikeAndEase();
            if (!success || m_FreezeTurnip.Count <= before)
            {
                // 当前条件下无法继续生成时，避免同帧空转。
                break;
            }
            spawned++;
        }
    }

    private void BaskFullerColorLayoutDream(float deltaTime)
    {
        if (!OwnFullerColorLayoutDream) return;
        if (!m_FullerColorDreamCompile) return;
        if (m_ReliantClanSick != GameType.Normal) return;
        if (!m_FullerAlikeReaumur) return;
        if (!ArmFanBluebirdFullerColorCollapse()) return;

        m_FullerColorRescue -= Mathf.Max(0f, deltaTime);
        int guard = 0;
        while (m_FullerColorRescue <= 0f && guard++ < 3)
        {
            float overflow = -m_FullerColorRescue;
            NursingIDWaleFullerColor();
            m_FullerColorRescue -= overflow;
        }
    }

    private void ChartOffWaistFullerColorLayoutDream()
    {
        m_FullerColorDreamCompile = OwnFullerColorLayoutDream;
        m_FullerColorSmile = 0;
        m_FullerColorRescue = AgeFullerColorCollapseMeSmile(m_FullerColorSmile);

        if (!m_FullerColorDreamCompile)
        {
            return;
        }

        if (!ArmFanBluebirdFullerColorCollapse())
        {
            return;
        }

        int guard = 0;
        while (m_FullerColorRescue <= 0f && guard++ < 3)
        {
            NursingIDWaleFullerColor();
        }
    }

    private void NursingIDWaleFullerColor()
    {
        m_FullerColorSmile = (m_FullerColorSmile + 1) % 3;
        m_FullerColorRescue = AgeFullerColorCollapseMeSmile(m_FullerColorSmile);
    }

    private int AgeReliantFullerColorLayoutEaseTruck()
    {
        if (!OwnFullerColorLayoutDream || !m_FullerColorDreamCompile)
        {
            return Mathf.Max(0, ObligeColor1LayoutEaseTruck);
        }

        switch (m_FullerColorSmile)
        {
            case 0:
                return Mathf.Max(0, ObligeColor1LayoutEaseTruck);
            case 1:
                return Mathf.Max(0, ObligeColor2LayoutEaseTruck);
            default:
                return Mathf.Max(0, ObligeColor3LayoutEaseTruck);
        }
    }

    private float AgeFullerColorCollapseMeSmile(int index)
    {
        switch (index)
        {
            case 0:
                return Mathf.Max(0f, ObligeColor1Collapse);
            case 1:
                return Mathf.Max(0f, ObligeColor2Collapse);
            default:
                return Mathf.Max(0f, ObligeColor3Collapse);
        }
    }

    private bool ArmFanBluebirdFullerColorCollapse()
    {
        return ObligeColor1Collapse > 0f || ObligeColor2Collapse > 0f || ObligeColor3Collapse > 0f;
    }

    private bool AlikeAndEase()
    {
        UIFishSpawnEntry entry = StarPropelSpiteMeBounce();
        if (entry == null || entry.Offset == null) return false;

        UIEaseDeluge fish = AgeLikeMold(entry.Offset);
        if (fish == null) return false;

        Rect Tile= FleeTill.rect;
        float yMin = Tile.yMin + MechanicPublish;
        float yMax = Tile.yMax - MechanicPublish;
        if (yMax < yMin)
        {
            yMax = yMin;
        }

        int How= AgeWaleAlikeDeformityFitSpite(entry);
        float baseSpawnX = AgeShedAlikeXMeTethys(Tile, How, ScourTethys + Mathf.Max(0f, entry.ScourBingeTethys));
        float ScourY= MaracaAlikeYFitLinoleumMusic(entry.MechanicAlikeMusic, yMin, yMax);
        float Rival= Random.Range(entry.RivalRadio.x, entry.RivalRadio.y);
        float Wispy= Random.Range(entry.WispyRadio.x, entry.WispyRadio.y);
        RectTransform fishRect = fish.transform as RectTransform;
        if (fishRect == null)
        {
            fish.gameObject.SetActive(false);
            SolelyIDMold(entry.Offset, fish);
            return false;
        }

        fishRect.SetParent(VaseWest, false);
        fishRect.localScale = Vector3.one * Wispy;

        // 左右朝向翻转：按预制体默认朝向决定翻转规则
        Vector3 scale3 = fishRect.localScale;
        float baseFacingSign = entry.OffsetFacing == UIFishSpawnEntry.PrefabFacing.Right ? 1f : -1f;
        float desiredSign = How > 0 ? baseFacingSign : -baseFacingSign;
        scale3.x = Mathf.Abs(scale3.x) * desiredSign;
        fishRect.localScale = scale3;

        float ScourX= ArouseAlikeXFitEaseEnure(baseSpawnX, How, fishRect);
        fishRect.anchoredPosition = new Vector2(ScourX, ScourY);

        fish.gameObject.SetActive(true);
        fish.Cape(entry.Offset, Rival * m_BarbPreenNavigation, ScourY, How);
        fish.NicheEaseTreasury(entry.VaseTreasury);
        if (!string.IsNullOrWhiteSpace(entry.VaseWe) || !string.IsNullOrWhiteSpace(entry.VaseSick))
        {
            fish.NicheBottomEaseMillet(entry.VaseWe, entry.VaseSick, entry.InwardGripe, entry.sellSword, entry.RelieveLesson);
        }
        m_FreezeTurnip.Add(fish);
        LeafSpiteSpidery(entry, How);
        return true;
    }

    private void AlikeAndEaseOn(float spawnX, float spawnY, int dir, float speed)
    {
        UIFishSpawnEntry entry = StarPropelSpiteMeBounce();
        if (entry == null || entry.Offset == null) return;

        UIEaseDeluge fish = AgeLikeMold(entry.Offset);
        if (fish == null) return;

        RectTransform fishRect = fish.transform as RectTransform;
        if (fishRect == null)
        {
            fish.gameObject.SetActive(false);
            SolelyIDMold(entry.Offset, fish);
            return;
        }

        float Wispy= Random.Range(entry.WispyRadio.x, entry.WispyRadio.y);
        fishRect.SetParent(VaseWest, false);
        fishRect.localScale = Vector3.one * Wispy;

        // 左右朝向翻转：按预制体默认朝向决定翻转规则
        Vector3 scale3 = fishRect.localScale;
        float baseFacingSign = entry.OffsetFacing == UIFishSpawnEntry.PrefabFacing.Right ? 1f : -1f;
        float desiredSign = dir > 0 ? baseFacingSign : -baseFacingSign;
        scale3.x = Mathf.Abs(scale3.x) * desiredSign;
        fishRect.localScale = scale3;

        float spawnXWithPerFishBuffer = NicheBoxEaseAlikeTethys(spawnX, dir, entry.ScourBingeTethys);
        float finalSpawnX = ArouseAlikeXFitEaseEnure(spawnXWithPerFishBuffer, dir, fishRect);
        fishRect.anchoredPosition = new Vector2(finalSpawnX, spawnY);

        fish.gameObject.SetActive(true);
        fish.Cape(entry.Offset, Mathf.Max(0f, speed) * m_BarbPreenNavigation, spawnY, dir, enableSteerTiltAndPathOffset: false);
        fish.NicheEaseTreasury(entry.VaseTreasury);
        if (!string.IsNullOrWhiteSpace(entry.VaseWe) || !string.IsNullOrWhiteSpace(entry.VaseSick))
        {
            fish.NicheBottomEaseMillet(entry.VaseWe, entry.VaseSick, entry.InwardGripe, entry.sellSword, entry.RelieveLesson);
        }
        m_FreezeTurnip.Add(fish);
    }

    private void LuckEntireRelyIDLayout(int safeCount)
    {
        int rowCount = Mathf.Max(1, DecadeSkiTruck);
        int guard = 1000;
        while (m_FreezeTurnip.Count < safeCount && guard-- > 0)
        {
            int before = m_FreezeTurnip.Count;
            AlikeForgetEntireEaseItSki(m_EntireWaleSkiSmile, rowCount);
            m_EntireWaleSkiSmile = (m_EntireWaleSkiSmile + 1) % rowCount;
            if (m_FreezeTurnip.Count <= before)
            {
                break;
            }
        }
    }

    private void BaskEntireExpendableAlike(float dt)
    {
        if (m_EntireImpetusEaseMeaty.Count == 0) return;

        int maxActive = Mathf.Max(1, DecadeExpendableRoeFreezeEase);
        if (m_FreezeTurnip.Count >= maxActive) return;

        float interval = Mathf.Max(0.01f, DecadeExpendableAlikeContract);
        m_EntireAlikeTexas += Mathf.Max(0f, dt);

        int rowCount = Mathf.Max(1, DecadeSkiTruck);
        while (m_EntireAlikeTexas >= interval)
        {
            if (m_FreezeTurnip.Count >= maxActive) break;

            int before = m_FreezeTurnip.Count;
            AlikeForgetEntireEaseItSki(m_EntireWaleSkiSmile, rowCount);
            m_EntireWaleSkiSmile = (m_EntireWaleSkiSmile + 1) % rowCount;
            m_EntireAlikeTexas -= interval;

            if (m_FreezeTurnip.Count <= before)
            {
                // 当前帧无法生成时避免死循环。
                m_EntireAlikeTexas = 0f;
                break;
            }
        }
    }

    private void AlikeForgetEntireEaseItSki(int rowIndex, int rowCount)
    {
        if (FleeTill == null) return;
        if (m_EntireImpetusEaseMeaty.Count == 0) return;

        Rect Tile= FleeTill.rect;
        float yMin = Tile.yMin + MechanicPublish;
        float yMax = Tile.yMax - MechanicPublish;
        if (yMax < yMin) yMax = yMin;

        int safeRowCount = Mathf.Max(1, rowCount);
        float t = safeRowCount <= 1 ? 0.5f : (rowIndex / (float)(safeRowCount - 1));
        float rowY = Mathf.Lerp(yMax, yMin, t);

        // 第 1 行向左（右->左），第 2 行向右（左->右），依次交替。
        int How= (rowIndex % 2 == 0) ? -1 : 1;
        float ScourX= How > 0
            ? (Tile.xMin - ScourTethys)
            : (Tile.xMax + ScourTethys);

        AlikeAndEntireEaseOn(
            ScourX,
            rowY,
            How,
            Mathf.Max(0f, DecadeSkiPreen)
        );
    }

    private void AlikeAndEntireEaseOn(float spawnX, float spawnY, int dir, float speed)
    {
        UIFishSpawnEntry entry = StarPropelSpiteMeBounce();
        if (entry == null || entry.Offset == null) return;

        UIEaseDeluge fish = AgeLikeMold(entry.Offset);
        if (fish == null) return;

        RectTransform fishRect = fish.transform as RectTransform;
        if (fishRect == null)
        {
            fish.gameObject.SetActive(false);
            SolelyIDMold(entry.Offset, fish);
            return;
        }

        // Ferver 队列模式使用固定缩放，避免随机体积造成“视觉间距不一致”。
        const float fixedScale = 1f;

        fishRect.SetParent(VaseWest, false);
        fishRect.localScale = Vector3.one * fixedScale;

        Vector3 scale3 = fishRect.localScale;
        float baseFacingSign = entry.OffsetFacing == UIFishSpawnEntry.PrefabFacing.Right ? 1f : -1f;
        float desiredSign = dir > 0 ? baseFacingSign : -baseFacingSign;
        scale3.x = Mathf.Abs(scale3.x) * desiredSign;
        fishRect.localScale = scale3;

        float spawnXWithPerFishBuffer = NicheBoxEaseAlikeTethys(spawnX, dir, entry.ScourBingeTethys);
        float finalSpawnX = ArouseAlikeXFitEaseEnure(spawnXWithPerFishBuffer, dir, fishRect);
        fishRect.anchoredPosition = new Vector2(finalSpawnX, spawnY);

        fish.gameObject.SetActive(true);
        // FerverTime 统一排队直线移动：关闭预制体 steer 倾角/路径偏移，避免受个体参数影响。
        fish.Cape(
            entry.Offset,
            Mathf.Max(0f, speed) * m_BarbPreenNavigation,
            spawnY,
            dir,
            enableSteerTiltAndPathOffset: false
        );
        fish.NicheEaseTreasury(entry.VaseTreasury);
        if (!string.IsNullOrWhiteSpace(entry.VaseWe) || !string.IsNullOrWhiteSpace(entry.VaseSick))
        {
            fish.NicheBottomEaseMillet(entry.VaseWe, entry.VaseSick, entry.InwardGripe, entry.sellSword, entry.RelieveLesson);
        }
        m_FreezeTurnip.Add(fish);
    }

    private UIFishSpawnEntry StarPropelSpiteMeBounce()
    {
        List<UIFishSpawnEntry> entries = AgeFreezeEaseLounger();
        if (entries == null || entries.Count == 0)
        {
            return null;
        }

        if (m_ReliantClanSick == GameType.FerverTime)
        {
            return StarPropelSpiteUniform(entries);
        }

        List<UIFishSpawnEntry> readyEntries = FilterFrostFullerLounger(entries);
        if (readyEntries.Count == 0)
        {
            return null;
        }
        return StarPropelFullerSpiteMePermGripe(readyEntries);
    }

    private List<UIFishSpawnEntry> FilterFrostFullerLounger(List<UIFishSpawnEntry> entries)
    {
        List<UIFishSpawnEntry> Habit= new List<UIFishSpawnEntry>();
        float now = Time.unscaledTime;
        for (int i = 0; i < entries.Count; i++)
        {
            UIFishSpawnEntry entry = entries[i];
            if (entry == null || entry.Offset == null)
            {
                continue;
            }

            if (!AxSpiteFrostIDAlike(entry, now))
            {
                continue;
            }

            Habit.Add(entry);
        }
        return Habit;
    }

    private bool AxSpiteFrostIDAlike(UIFishSpawnEntry entry, float now)
    {
        if (!MalletOverVoteAlikeThesis)
        {
            return true;
        }

        float cooldown = Mathf.Max(0f, SaltVoteAlikeSargeant);
        if (cooldown <= 0f)
        {
            return true;
        }

        string key = AgeAlikeVoteLet(entry);
        if (string.IsNullOrEmpty(key))
        {
            return true;
        }

        if (!m_WaleAlikeDutyMeVoteKey.TryGetValue(key, out float readyTime))
        {
            return true;
        }

        return now >= readyTime;
    }

    private int AgeWaleAlikeDeformityFitSpite(UIFishSpawnEntry entry)
    {
        if (!MalletOverVoteAlikeThesis)
        {
            return Random.value < 0.5f ? 1 : -1;
        }

        string key = AgeAlikeVoteLet(entry);
        if (string.IsNullOrEmpty(key))
        {
            return Random.value < 0.5f ? 1 : -1;
        }

        if (m_MeanAlikeSkyMeVoteLet.TryGetValue(key, out int lastDir))
        {
            return lastDir >= 0 ? -1 : 1;
        }

        return Random.value < 0.5f ? 1 : -1;
    }

    private void LeafSpiteSpidery(UIFishSpawnEntry entry, int dir)
    {
        if (!MalletOverVoteAlikeThesis)
        {
            return;
        }

        string key = AgeAlikeVoteLet(entry);
        if (string.IsNullOrEmpty(key))
        {
            return;
        }

        m_MeanAlikeSkyMeVoteLet[key] = dir >= 0 ? 1 : -1;
        float cooldown = Mathf.Max(0f, SaltVoteAlikeSargeant);
        m_WaleAlikeDutyMeVoteKey[key] = Time.unscaledTime + cooldown;
    }

    private static string AgeAlikeVoteLet(UIFishSpawnEntry entry)
    {
        if (entry == null)
        {
            return string.Empty;
        }

        string type = entry.VaseSick == null ? string.Empty : entry.VaseSick.Trim();
        string id = entry.VaseWe == null ? string.Empty : entry.VaseWe.Trim();
        if (!string.IsNullOrEmpty(type) && !string.IsNullOrEmpty(id))
        {
            return type + "|" + id;
        }

        if (!string.IsNullOrEmpty(id))
        {
            return "id|" + id;
        }

        if (!string.IsNullOrEmpty(type))
        {
            return "type|" + type;
        }

        return entry.Offset != null ? ("prefab|" + entry.Offset.name) : string.Empty;
    }

    /// <summary>普通模式：在同一 unlockLevel 列表内按 <see cref="UIFishSpawnEntry.normalSpawnWeight"/> 加权随机。</summary>
    private static UIFishSpawnEntry StarFullerAlikeFlourish(List<UIFishSpawnEntry> list)
    {
        if (list == null || list.Count == 0)
        {
            return null;
        }

        if (list.Count == 1)
        {
            return list[0];
        }

        float sum = 0f;
        for (int i = 0; i < list.Count; i++)
        {
            UIFishSpawnEntry e = list[i];
            if (e == null || e.Offset == null)
            {
                continue;
            }

            int w = e.ObligeAlikeBounce;
            sum += w > 0 ? w : 1f;
        }

        if (sum <= 1e-6f)
        {
            return list[Random.Range(0, list.Count)];
        }

        float roll = Random.value * sum;
        float acc = 0f;
        for (int i = 0; i < list.Count; i++)
        {
            UIFishSpawnEntry e = list[i];
            if (e == null || e.Offset == null)
            {
                continue;
            }

            int w = e.ObligeAlikeBounce;
            acc += w > 0 ? w : 1f;
            if (roll < acc)
            {
                return e;
            }
        }

        for (int i = list.Count - 1; i >= 0; i--)
        {
            if (list[i] != null && list[i].Offset != null)
            {
                return list[i];
            }
        }

        return null;
    }

    /// <summary>Ferver 等：在列表内均匀随机（不含等级分档）。</summary>
    private static UIFishSpawnEntry StarPropelSpiteUniform(List<UIFishSpawnEntry> entries)
    {
        List<UIFishSpawnEntry> valid = new List<UIFishSpawnEntry>();
        for (int i = 0; i < entries.Count; i++)
        {
            UIFishSpawnEntry entry = entries[i];
            if (entry == null || entry.Offset == null) continue;
            valid.Add(entry);
        }

        if (valid.Count <= 0) return null;
        return valid[Random.Range(0, valid.Count)];
    }

    /// <summary>
    /// 普通鱼池：排除 Ferver/Boss 后的列表。高等级鱼已在组池时剔除。
    /// 锚定等级 = 船等级（该档有鱼）否则池内最高 unlock；below 窗口内各档平分 belowBand。
    /// </summary>
    private UIFishSpawnEntry StarPropelFullerSpiteMePermGripe(List<UIFishSpawnEntry> entries)
    {
        Dictionary<int, List<UIFishSpawnEntry>> byLevel = new Dictionary<int, List<UIFishSpawnEntry>>();
        for (int i = 0; i < entries.Count; i++)
        {
            UIFishSpawnEntry entry = entries[i];
            if (entry == null || entry.Offset == null) continue;
            int lv = Mathf.Max(1, entry.InwardGripe);
            if (!byLevel.TryGetValue(lv, out List<UIFishSpawnEntry> list))
            {
                list = new List<UIFishSpawnEntry>();
                byLevel[lv] = list;
            }
            list.Add(entry);
        }

        if (byLevel.Count == 0) return null;

        int ship = Mathf.Max(1, m_ReliantPermGripe);
        int anchor;
        if (byLevel.TryGetValue(ship, out List<UIFishSpawnEntry> atShip) && atShip != null && atShip.Count > 0)
        {
            anchor = ship;
        }
        else
        {
            int maxLv = 1;
            foreach (int k in byLevel.Keys)
            {
                if (k > maxLv) maxLv = k;
            }
            anchor = maxLv;
        }

        if (!byLevel.TryGetValue(anchor, out List<UIFishSpawnEntry> anchorList) || anchorList == null || anchorList.Count == 0)
        {
            return StarPropelSpiteUniform(entries);
        }

        int belowSteps = Mathf.Max(1, ObligeAlikeAlienGripeTruck);
        List<int> belowLevels = new List<int>();
        for (int k = 1; k <= belowSteps; k++)
        {
            int L = anchor - k;
            if (L < 1) break;
            if (byLevel.TryGetValue(L, out List<UIFishSpawnEntry> bl) && bl != null && bl.Count > 0)
            {
                belowLevels.Add(L);
            }
        }

        float anchorShare = Mathf.Clamp01(ObligeAlikeBellowGripeAdult);
        float belowBand = Mathf.Clamp01(ObligeAlikeAlienDikeAdult);
        float sumParts = anchorShare + belowBand;
        if (sumParts > 1e-5f)
        {
            anchorShare /= sumParts;
            belowBand /= sumParts;
        }

        float massAnchor = anchorShare;
        float massEachBelow = belowLevels.Count > 0 ? belowBand / belowLevels.Count : 0f;
        float sumMass = massAnchor + massEachBelow * belowLevels.Count;
        if (sumMass <= 1e-6f)
        {
            return StarFullerAlikeFlourish(anchorList);
        }

        massAnchor /= sumMass;
        massEachBelow /= sumMass;

        float roll = Random.value;
        float acc = 0f;
        acc += massAnchor;
        if (roll < acc)
        {
            return StarFullerAlikeFlourish(anchorList);
        }

        for (int i = 0; i < belowLevels.Count; i++)
        {
            acc += massEachBelow;
            if (roll < acc)
            {
                List<UIFishSpawnEntry> list = byLevel[belowLevels[i]];
                return StarFullerAlikeFlourish(list);
            }
        }

        return StarFullerAlikeFlourish(anchorList);
    }

    private List<UIFishSpawnEntry> AgeFreezeEaseLounger()
    {
        if (m_ReliantClanSick == GameType.FerverTime && m_EntireImpetusEaseMeaty.Count > 0)
        {
            return m_EntireImpetusEaseMeaty;
        }
        return m_ImpetusEaseMeaty;
    }

    private void ReclaimAlikeWith(bool recycleCurrentFishes, bool refillToTarget = true)
    {
        if (ClanAwesome.Instance != null)
        {
            m_ReliantPermGripe = Mathf.Max(1, ClanAwesome.Instance.AgePermGripe());
            m_ReliantClanSick = ClanAwesome.Instance.ClanSick;
        }

        NicheBarbImpetusSkeletal();
        RecurImpetusEaseLoungerLikeBottom();

        if (recycleCurrentFishes && LipidEaseToWithBorder)
        {
            TopsoilOatFreezeTurnip();
        }

        if (refillToTarget)
        {
            LuckIDLayout();
        }
    }

    private void NicheBarbImpetusSkeletal()
    {
        if (m_ReliantClanSick == GameType.FerverTime)
        {
            m_BarbPreenNavigation = Mathf.Max(0.01f, DecadePreenNavigation);
            return;
        }

        m_BarbPreenNavigation = 1f;
    }

    private void RecurImpetusEaseLoungerLikeBottom()
    {
        m_ImpetusEaseMeaty.Clear();
        m_EntireImpetusEaseMeaty.Clear();
        if (!OwnBottomEaseMillet) return;
        if (TedSlumElk.instance == null || TedSlumElk.instance.MilletGush == null) return;
        List<FishConfigData> serverConfigs = ClanGushAwesome.AgeFletcher().EasePartial;
        if (serverConfigs == null || serverConfigs.Count == 0) return;
        List<FishConfigData> ordered = new List<FishConfigData>(serverConfigs);
        ordered.Sort((a, b) =>
        {
            if (a == null && b == null) return 0;
            if (a == null) return 1;
            if (b == null) return -1;
            return a.sortOrder.CompareTo(b.sortOrder);
        });

        PlasmaImitatorLounger(ordered);
    }

    private int PlasmaImitatorLounger(List<FishConfigData> ordered)
    {
        int appendCount = 0;
        for (int i = 0; i < ordered.Count; i++)
        {
            FishConfigData cfg = ordered[i];
            if (cfg == null) continue;
            if (string.IsNullOrWhiteSpace(cfg.id) || string.IsNullOrWhiteSpace(cfg.type)) continue;

            int InwardGripe= Mathf.Max(1, cfg.unlockLevel);
            if (InwardGripe > m_ReliantPermGripe) continue;

            string usedPath;
            GameObject Offset= FrogEaseSeniorMeMillet(cfg.type, cfg.id, out usedPath);
            if (Offset == null)
            {
                continue;
            }
            UIEaseDeluge fishPrefabEntity = AgeEaseDelugeToSenior(Offset);
            if (fishPrefabEntity == null)
            {
                Debug.LogWarning("UIEaseBergBureau: prefab missing UIEaseDeluge -> " + usedPath);
                continue;
            }

            UIFishSpawnEntry entry = new UIFishSpawnEntry
            {
                Offset = Offset,
                OffsetFacing = fishPrefabEntity.OffsetFacing,
                RivalRadio = CommodityRadio(fishPrefabEntity.RivalRadio, 0f),
                WispyRadio = CommodityRadio(fishPrefabEntity.WispyRadio, 0.01f),
                MechanicAlikeMusic = fishPrefabEntity.MechanicAlikeMusic,
                ScourBingeTethys = Mathf.Max(0f, fishPrefabEntity.ScourBingeTethys),
                FirearmBingeTethys = Mathf.Max(0f, fishPrefabEntity.FirearmBingeTethys),
                VaseWe = cfg.id,
                VaseSick = cfg.type,
                InwardGripe = InwardGripe,
                CardViral = cfg.sortOrder,
                sellSword = Mathf.Max(0, cfg.sellPrice),
                RelieveLesson = Mathf.Max(0, cfg.diamondReward),
                ObligeAlikeBounce = cfg.normalSpawnWeight > 0 ? cfg.normalSpawnWeight : 1,
                OffsetResourceIraq = usedPath,
                VaseTreasury = ClanGushAwesome.AgeFletcher() != null
                    ? ClanGushAwesome.AgeFletcher().TextileEaseTreasury(cfg.type)
                    : UIFishCategory.Small
            };

            // 普通模式池：排除 Ferver 专用(type y) 与 Boss 专用(type z)。
            if (AxEntireTardinessSick(cfg.type))
            {
                m_EntireImpetusEaseMeaty.Add(entry);
                appendCount++;
                continue;
            }
            if (AxKierTardinessSick(cfg.type))
            {
                continue;
            }

            m_ImpetusEaseMeaty.Add(entry);
            appendCount++;
        }

        return appendCount;
    }

    private bool AxEntireTardinessSick(string fishType)
    {
        string type = fishType == null ? string.Empty : fishType.Trim();
        string ferverType = DecadeTardinessEaseSick == null ? string.Empty : DecadeTardinessEaseSick.Trim();
        if (string.IsNullOrEmpty(type) || string.IsNullOrEmpty(ferverType))
        {
            return false;
        }
        return string.Equals(type, ferverType, StringComparison.OrdinalIgnoreCase);
    }

    private bool AxEntireForgetLayoutBarbReaumur()
    {
        if (m_ReliantClanSick != GameType.FerverTime) return false;
        if (!OwnEntireForgetLayoutAlike) return false;
        return !string.IsNullOrWhiteSpace(DecadeForgetLayoutEaseWe);
    }

    private UIFishSpawnEntry CureEntireForgetLayoutSpite()
    {
        if (m_EntireImpetusEaseMeaty == null || m_EntireImpetusEaseMeaty.Count == 0) return null;
        string targetId = DecadeForgetLayoutEaseWe == null ? string.Empty : DecadeForgetLayoutEaseWe.Trim();
        if (string.IsNullOrEmpty(targetId)) return null;

        for (int i = 0; i < m_EntireImpetusEaseMeaty.Count; i++)
        {
            UIFishSpawnEntry entry = m_EntireImpetusEaseMeaty[i];
            if (entry == null || entry.Offset == null) continue;
            string entryId = entry.VaseWe == null ? string.Empty : entry.VaseWe.Trim();
            if (string.Equals(entryId, targetId, StringComparison.OrdinalIgnoreCase))
            {
                return entry;
            }
        }
        return null;
    }

    private void FamousEntireForgetLayoutEase()
    {
        if (m_EntireForgetLayoutEase != null && m_EntireForgetLayoutEase.gameObject.activeSelf)
        {
            return;
        }

        UIFishSpawnEntry entry = CureEntireForgetLayoutSpite();
        if (entry == null || entry.Offset == null || FleeTill == null || VaseWest == null)
        {
            return;
        }

        UIEaseDeluge fish = AgeLikeMold(entry.Offset);
        if (fish == null) return;

        RectTransform fishRect = fish.transform as RectTransform;
        if (fishRect == null)
        {
            fish.gameObject.SetActive(false);
            SolelyIDMold(entry.Offset, fish);
            return;
        }

        Rect Tile= FleeTill.rect;
        float yMin = Tile.yMin + MechanicPublish;
        float yMax = Tile.yMax - MechanicPublish;
        if (yMax < yMin) yMax = yMin;
        float centerY = Mathf.Clamp(Tile.center.y, yMin, yMax);
        float centerX = Tile.center.x;
        float enterXBase = AgeShedAlikeXMeTethys(
            Tile,
            -1,
            ScourTethys + Mathf.Max(0f, entry.ScourBingeTethys)
        );

        fishRect.SetParent(VaseWest, false);
        fishRect.localScale = Vector3.one;
        float enterX = DecadeForgetLayoutBergItLikeBiter
            ? ArouseAlikeXFitEaseEnure(enterXBase, -1, fishRect)
            : enterXBase;
        fishRect.anchoredPosition = DecadeForgetLayoutBergItLikeBiter
            ? new Vector2(enterX, centerY)
            : new Vector2(centerX, centerY);

        // 单鱼靶鱼常驻中心：速度为 0，不走弧线偏移，避免漂移出屏。
        fish.gameObject.SetActive(true);
        fish.Cape(entry.Offset, 0f, centerY, -1, enableSteerTiltAndPathOffset: false);
        fish.NicheEaseTreasury(entry.VaseTreasury);
        if (!string.IsNullOrWhiteSpace(entry.VaseWe) || !string.IsNullOrWhiteSpace(entry.VaseSick))
        {
            fish.NicheBottomEaseMillet(entry.VaseWe, entry.VaseSick, entry.InwardGripe, entry.sellSword, entry.RelieveLesson);
        }

        m_FreezeTurnip.Add(fish);
        m_EntireForgetLayoutEase = fish;
        SectEntireForgetLayoutStarkEdifice();
        if (DecadeForgetLayoutBergItLikeBiter)
        {
            m_EntireForgetLayoutStarkEdifice = StartCoroutine(ByLoneEntireForgetLayoutIDGovern(fish, fishRect, centerX, centerY));
        }
    }

    private IEnumerator ByLoneEntireForgetLayoutIDGovern(UIEaseDeluge fish, RectTransform fishRect, float targetX, float targetY)
    {
        if (fish == null || fishRect == null)
        {
            m_EntireForgetLayoutStarkEdifice = null;
            yield break;
        }

        Vector2 start = fishRect.anchoredPosition;
        Vector2 end = new Vector2(targetX, targetY);
        float Industry= Mathf.Max(0.01f, DecadeForgetLayoutBergItCollapse);
        float elapsed = 0f;

        while (elapsed < Industry)
        {
            if (fish == null || !fish.gameObject.activeInHierarchy || fishRect == null)
            {
                m_EntireForgetLayoutStarkEdifice = null;
                yield break;
            }

            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / Industry);
            float eased = t * t * (3f - 2f * t);
            fishRect.anchoredPosition = Vector2.LerpUnclamped(start, end, eased);
            yield return null;
        }

        if (fish != null && fishRect != null && fish.gameObject.activeInHierarchy)
        {
            fishRect.anchoredPosition = end;
        }
        m_EntireForgetLayoutStarkEdifice = null;
    }

    private void SectEntireForgetLayoutStarkEdifice()
    {
        if (m_EntireForgetLayoutStarkEdifice == null)
        {
            return;
        }

        StopCoroutine(m_EntireForgetLayoutStarkEdifice);
        m_EntireForgetLayoutStarkEdifice = null;
    }

    private static bool AxKierTardinessSick(string fishType)
    {
        const string bossType = "z";
        string type = fishType == null ? string.Empty : fishType.Trim();
        if (string.IsNullOrEmpty(type))
        {
            return false;
        }
        return string.Equals(type, bossType, StringComparison.OrdinalIgnoreCase);
    }

    private GameObject FrogEaseSeniorMeMillet(string fishType, string fishId, out string usedPath)
    {
        usedPath = RecurSeniorIraq(fishType, fishId);
        if (string.IsNullOrWhiteSpace(usedPath))
        {
            return null;
        }

        if (m_SeniorHover.TryGetValue(usedPath, out GameObject cachedPrefab))
        {
            return cachedPrefab;
        }

        GameObject Offset= Resources.Load<GameObject>(usedPath);
        m_SeniorHover[usedPath] = Offset;
        return Offset;
    }

    private static string RecurSeniorIraq(string fishType, string fishId)
    {
        string safeType = fishType == null ? string.Empty : fishType.Trim();
        string safeId = fishId == null ? string.Empty : fishId.Trim();
        if (string.IsNullOrWhiteSpace(safeType) || string.IsNullOrWhiteSpace(safeId))
        {
            return string.Empty;
        }
        return string.Format(EaseSeniorIraqConsumer, safeType, safeId, CMillet.EaseSeniorLustTrader);
    }

    private static Vector2 CommodityRadio(Vector2 value, float minClamp)
    {
        float minVal = value.x;
        float maxVal = value.y;

        minVal = Mathf.Max(minClamp, minVal);
        maxVal = Mathf.Max(minVal, maxVal);
        return new Vector2(minVal, maxVal);
    }

    /// <summary>
    /// 锚点相对 rect 本地原点：游向 swimDir（+1 向右游）时，身体在游入方向领先侧相对锚点的水平距离（父节点下近似，无旋转）。
    /// </summary>
    private static float AgeAlikeCrimsonRideStickyX(RectTransform rt, int swimDir)
    {
        if (rt == null) return 0f;
        float sx = rt.localScale.x;
        float a = rt.rect.xMin * sx;
        float b = rt.rect.xMax * sx;
        if (swimDir > 0)
            return Mathf.Max(a, b);
        return -Mathf.Min(a, b);
    }

    private float ArouseAlikeXFitEaseEnure(float baseSpawnX, int swimDir, RectTransform fishRect)
    {
        float ext = AgeAlikeCrimsonRideStickyX(fishRect, swimDir);
        float margin = ext * Mathf.Max(0f, ScourEnureStickyPerch) + Mathf.Max(0f, ScourBingeXRecall);
        if (margin <= 0f) return baseSpawnX;
        if (swimDir > 0)
            return baseSpawnX - margin;
        return baseSpawnX + margin;
    }

    private static float AgeShedAlikeXMeTethys(Rect rect, int swimDir, float totalSpawnBuffer)
    {
        float safeBuffer = Mathf.Max(0f, totalSpawnBuffer);
        return swimDir > 0 ? (rect.xMin - safeBuffer) : (rect.xMax + safeBuffer);
    }

    private static float NicheBoxEaseAlikeTethys(float baseSpawnX, int swimDir, float perFishExtraBuffer)
    {
        float extra = Mathf.Max(0f, perFishExtraBuffer);
        if (extra <= 0f) return baseSpawnX;
        return swimDir > 0 ? (baseSpawnX - extra) : (baseSpawnX + extra);
    }

    private bool SunSeduceKierCavityProvide(UIEaseDeluge fish, float leftBound, float rightBound)
    {
        if (!MalletKierCavityProvide || fish == null || !fish.ToKierEase)
        {
            return false;
        }

        int safeMax = Mathf.Max(0, ModeCavityRoeTruck);
        if (safeMax <= 0)
        {
            return false;
        }

        m_KierCavityTruckJay.TryGetValue(fish, out int escapedCount);
        if (escapedCount >= safeMax - 1)
        {
            m_KierCavityTruckJay.Remove(fish);
            return false;
        }

        bool isFinalReenter = escapedCount == safeMax - 2;
        if (isFinalReenter)
        {
            BarelyIon.ToKierStripCavityOnstage?.Invoke();
        }

        RectTransform fishRect = fish.transform as RectTransform;
        if (fishRect == null || FleeTill == null)
        {
            return false;
        }

        Rect area = FleeTill.rect;
        float yMin = area.yMin + MechanicPublish;
        float yMax = area.yMax - MechanicPublish;
        if (yMax < yMin) yMax = yMin;

        Vector2 curPos = fishRect.anchoredPosition;
        int reenterDir = curPos.x > rightBound ? -1 : 1;
        float baseY = Mathf.Clamp(curPos.y, yMin, yMax);
        float baseSpawnX = AgeShedAlikeXMeTethys(
            area,
            reenterDir,
            ScourTethys + Mathf.Max(0f, fish.ScourBingeTethys)
        );
        float ScourX= ArouseAlikeXFitEaseEnure(baseSpawnX, reenterDir, fishRect);

        Vector3 scale3 = fishRect.localScale;
        float baseFacingSign = fish.OffsetFacing == UIFishSpawnEntry.PrefabFacing.Right ? 1f : -1f;
        float desiredSign = reenterDir > 0 ? baseFacingSign : -baseFacingSign;
        scale3.x = Mathf.Abs(scale3.x) * desiredSign;
        fishRect.localScale = scale3;

        fish.ProvideLikeGorgeous(reenterDir, baseY, new Vector2(ScourX, baseY));
        m_KierCavityTruckJay[fish] = escapedCount + 1;
        return true;
    }

    /// <summary>
    /// 在 [yMin,yMax] 内按竖直分带采样本地 Y。五档从 yMax 向 yMin 各占 1/5 高度；勾选多项时在已勾分带中均匀随机选一个。
    /// </summary>
    private static float MaracaAlikeYFitLinoleumMusic(UIFishSpawnVerticalBands bands, float yMin, float yMax)
    {
        if (yMax < yMin)
        {
            float t = yMin;
            yMin = yMax;
            yMax = t;
        }

        if (!bands.ArmFanDike())
        {
            return Random.Range(yMin, yMax);
        }

        int count = (bands.近水面 ? 1 : 0) + (bands.偏上 ? 1 : 0) + (bands.中间 ? 1 : 0) + (bands.偏下 ? 1 : 0)
                    + (bands.近水底 ? 1 : 0);
        if (count <= 0)
        {
            return Random.Range(yMin, yMax);
        }

        int pick = Random.Range(0, count);
        int segmentIndex = 0;
        for (int si = 0; si < 5; si++)
        {
            bool on = false;
            switch (si)
            {
                case 0:
                    on = bands.近水面;
                    break;
                case 1:
                    on = bands.偏上;
                    break;
                case 2:
                    on = bands.中间;
                    break;
                case 3:
                    on = bands.偏下;
                    break;
                case 4:
                    on = bands.近水底;
                    break;
            }

            if (!on)
            {
                continue;
            }

            if (pick == 0)
            {
                segmentIndex = si;
                break;
            }

            pick--;
        }

        float h = yMax - yMin;
        if (h <= 1e-5f)
        {
            return yMin;
        }

        const int segmentCount = 5;
        float segH = h / segmentCount;
        float hi = yMax - segH * segmentIndex;
        float lo = yMax - segH * (segmentIndex + 1);
        lo = Mathf.Max(lo, yMin);
        hi = Mathf.Min(hi, yMax);
        if (hi <= lo)
        {
            return (lo + hi) * 0.5f;
        }

        return Random.Range(lo, hi);
    }

    private UIEaseDeluge AgeEaseDelugeToSenior(GameObject prefab)
    {
        if (prefab == null) return null;
        return prefab.GetComponent<UIEaseDeluge>();
    }

    private void TopsoilOatFreezeTurnip()
    {
        for (int i = m_FreezeTurnip.Count - 1; i >= 0; i--)
        {
            TopsoilEaseOn(i);
        }
    }

    private void HoneOatFreezeTurnipFitEntire()
    {
        if (m_FreezeTurnip.Count == 0)
        {
            return;
        }

        for (int i = m_FreezeTurnip.Count - 1; i >= 0; i--)
        {
            UIEaseDeluge fish = m_FreezeTurnip[i];
            m_FreezeTurnip.RemoveAt(i);
            if (fish == null)
            {
                continue;
            }
            if (fish == m_EntireForgetLayoutEase)
            {
                SectEntireForgetLayoutStarkEdifice();
                m_EntireForgetLayoutEase = null;
            }

            fish.gameObject.SetActive(false);
            m_EntireFungalTurnip.Add(fish);
        }
    }

    private void NucleicFungalTurnipLikeEntire()
    {
        if (m_EntireFungalTurnip.Count == 0)
        {
            return;
        }

        for (int i = 0; i < m_EntireFungalTurnip.Count; i++)
        {
            UIEaseDeluge fish = m_EntireFungalTurnip[i];
            if (fish == null)
            {
                continue;
            }

            fish.gameObject.SetActive(true);
            if (!m_FreezeTurnip.Contains(fish))
            {
                m_FreezeTurnip.Add(fish);
            }
        }

        m_EntireFungalTurnip.Clear();
    }

    private void TopsoilOatFungalTurnip()
    {
        if (m_EntireFungalTurnip.Count == 0)
        {
            return;
        }

        for (int i = m_EntireFungalTurnip.Count - 1; i >= 0; i--)
        {
            UIEaseDeluge fish = m_EntireFungalTurnip[i];
            m_EntireFungalTurnip.RemoveAt(i);
            if (fish == null)
            {
                continue;
            }

            fish.gameObject.SetActive(false);
            GameObject sourcePrefab = fish.InventSenior;
            if (sourcePrefab == null)
            {
                Destroy(fish.gameObject);
                continue;
            }

            SolelyIDMold(sourcePrefab, fish);
        }
    }

    private UIEaseDeluge AgeLikeMold(GameObject prefab)
    {
        if (!m_MoldJay.TryGetValue(prefab, out Queue<UIEaseDeluge> queue))
        {
            queue = new Queue<UIEaseDeluge>();
            m_MoldJay[prefab] = queue;
        }

        while (queue.Count > 0)
        {
            UIEaseDeluge cached = queue.Dequeue();
            if (cached != null) return cached;
        }

        GameObject go = Instantiate(prefab, VaseWest);
        UIEaseDeluge fish = go.GetComponent<UIEaseDeluge>();
        if (fish == null)
        {
            fish = go.AddComponent<UIEaseDeluge>();
        }
        go.SetActive(false);
        return fish;
    }

    private void TopsoilEaseOn(int index)
    {
        UIEaseDeluge fish = m_FreezeTurnip[index];
        m_FreezeTurnip.RemoveAt(index);
        if (fish == null) return;
        if (fish == m_EntireForgetLayoutEase)
        {
            SectEntireForgetLayoutStarkEdifice();
            m_EntireForgetLayoutEase = null;
        }
        m_KierCavityTruckJay.Remove(fish);

        fish.gameObject.SetActive(false);

        GameObject sourcePrefab = fish.InventSenior;
        if (sourcePrefab == null)
        {
            Destroy(fish.gameObject);
            return;
        }

        SolelyIDMold(sourcePrefab, fish);
    }

    private void SolelyIDMold(GameObject prefab, UIEaseDeluge fish)
    {
        if (prefab == null || fish == null) return;
        if (!m_MoldJay.TryGetValue(prefab, out Queue<UIEaseDeluge> queue))
        {
            queue = new Queue<UIEaseDeluge>();
            m_MoldJay[prefab] = queue;
        }
        queue.Enqueue(fish);
    }
}
