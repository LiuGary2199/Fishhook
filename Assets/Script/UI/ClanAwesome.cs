using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ClanAwesome : MonoBehaviour
{
    static public ClanAwesome Instance;

    [Header("Cash Pool（仅击杀鱼掉金币）")]
    [Tooltip("LetStar 预制体；与 MoteWould 击杀鱼 + TraditionDemobilize.FishGoldMove 配套")]
    [SerializeField] private GameObject PulpMoldSenior;
    [Header("Diamond Pool（仅击杀钻石鱼）")]
    [Tooltip("LetStar 预制体；与 MoteWould 击杀钻石鱼 + TraditionDemobilize.FishDiamondMove 配套")]
    [SerializeField] private GameObject RelieveMoldSenior;

    private readonly Queue<GameObject> _PulpMold= new Queue<GameObject>();
    private readonly Queue<GameObject> _RelieveMold= new Queue<GameObject>();

    /// <summary>Inspector 中配置的金币飞行物预制体（只读）。</summary>
    public GameObject SeedMoldSenior=> PulpMoldSenior;
    /// <summary>Inspector 中配置的钻石飞行物预制体（只读）。</summary>
    public GameObject LinkageMoldSenior=> RelieveMoldSenior;

    [Header("小游戏开门飞物体（预制体，由调度器传入 TraditionDemobilize）")]
    [Tooltip("EmitWould：从鱼死亡点飞到 FX_Cash 本地 (0,0,0) 的预制体")]
[UnityEngine.Serialization.FormerlySerializedAs("miniGameIntroFlyPrefabSlot")]    public GameObject YolkClanArrayLetSeniorEmit;
    [Tooltip("ScrubSlumWould：同上")]
[UnityEngine.Serialization.FormerlySerializedAs("miniGameIntroFlyPrefabLuckyCard")]    public GameObject YolkClanArrayLetSeniorScrubSlum;

    public GameObject AgeSeedLikeMold(Transform parentTransform) =>
        AgeLikeMold(_PulpMold, PulpMoldSenior, parentTransform);

    public void SolelySeedIDMold(GameObject item, Transform parentTransform) =>
        SolelyIDMold(_PulpMold, item, parentTransform);

    public GameObject AgeLinkageLikeMold(Transform parentTransform) =>
        AgeLikeMold(_RelieveMold, RelieveMoldSenior, parentTransform);

    public void SolelyLinkageIDMold(GameObject item, Transform parentTransform) =>
        SolelyIDMold(_RelieveMold, item, parentTransform);

    /// <summary>从金币池取实例（无 ClanAwesome 时返回 null）。</summary>
    public static GameObject AgeSeedMold(Transform parentTransform) =>
        Instance != null ? Instance.AgeSeedLikeMold(parentTransform) : null;

    public static void SolelySeedMold(GameObject item, Transform parentTransform) =>
        Instance?.SolelySeedIDMold(item, parentTransform);

    /// <summary>从钻石池取实例（无 ClanAwesome 时返回 null）。</summary>
    public static GameObject AgeLinkageMold(Transform parentTransform) =>
        Instance != null ? Instance.AgeLinkageLikeMold(parentTransform) : null;

    public static void SolelyLinkageMold(GameObject item, Transform parentTransform) =>
        Instance?.SolelyLinkageIDMold(item, parentTransform);

    [Header("Fly Diamond Item Pool（UIDiamondMoveBest UI 飞钻条）")]
    [Tooltip("FlyDiamondItem 预制体；与 TraditionDemobilize.UIDiamondMoveBest 配套；须在 Inspector 指定（与 cashPoolPrefab / diamondPoolPrefab 相同，不做 Resources 加载）")]
    [SerializeField] private GameObject flyLinkageItemMoldSenior;

    private readonly Queue<GameObject> _MudLinkageStarMold= new Queue<GameObject>();

    /// <summary>Inspector 中配置的 UI 飞钻条预制体（只读）。</summary>
    public GameObject LetLinkageStarMoldSenior=> flyLinkageItemMoldSenior;

    public GameObject AgeLetLinkageStarLikeMold(Transform parentTransform) =>
        AgeLikeMold(_MudLinkageStarMold, flyLinkageItemMoldSenior, parentTransform);

    public void SolelyLetLinkageStarIDMold(GameObject item, Transform parentTransform) =>
        SolelyIDMold(_MudLinkageStarMold, item, parentTransform);

    public static GameObject AgeLetLinkageStarMold(Transform parentTransform) =>
        Instance != null ? Instance.AgeLetLinkageStarLikeMold(parentTransform) : null;

    public static void SolelyLetLinkageStarMold(GameObject item, Transform parentTransform) =>
        Instance?.SolelyLetLinkageStarIDMold(item, parentTransform);

    private static GameObject AgeLikeMold(Queue<GameObject> pool, GameObject prefab, Transform parentTransform)
    {
        while (pool.Count > 0)
        {
            GameObject cached = pool.Dequeue();
            if (cached == null) continue;
            cached.transform.SetParent(parentTransform, false);
            cached.transform.localPosition = Vector3.zero;
            cached.transform.localRotation = Quaternion.identity;
            cached.transform.localScale = Vector3.one;
            return cached;
        }

        if (prefab == null) return null;
        return UnityEngine.Object.Instantiate(prefab, parentTransform);
    }

    private static void SolelyIDMold(Queue<GameObject> pool, GameObject item, Transform parentTransform)
    {
        if (item == null) return;
        item.transform.DOKill();
        item.SetActive(false);
        item.transform.SetParent(parentTransform, false);
        item.transform.localPosition = Vector3.zero;
        item.transform.localRotation = Quaternion.identity;
        item.transform.localScale = Vector3.one;
        pool.Enqueue(item);
    }
[UnityEngine.Serialization.FormerlySerializedAs("GameType")]
    public GameType ClanSick;
[UnityEngine.Serialization.FormerlySerializedAs("ConfigHoohHP")]    public int MilletLieuHP= 2; //用户可升级钩子生命
[UnityEngine.Serialization.FormerlySerializedAs("CurhoohHP")]    public int ForelegHP= 1;//单次发射钩子生
[UnityEngine.Serialization.FormerlySerializedAs("CurFireHP")]    public int JoyPastHP= 0;

    [Header("Hook State")]
    [Tooltip("钩子是否处于发射/出钩状态（由 UIImageCrash 更新）")]
    [SerializeField] private bool ToDownTonal= false;

    [Header("体力配置")]
[UnityEngine.Serialization.FormerlySerializedAs("maxEnergy")]    public int WitSchool= 100;
[UnityEngine.Serialization.FormerlySerializedAs("recoverThreshold")]    public int HectareHomemaker= 50;
[UnityEngine.Serialization.FormerlySerializedAs("recoverInterval")]    public float HectareContract= 5f;
[UnityEngine.Serialization.FormerlySerializedAs("recoverAmount")]    public int HectareSadden= 1;
    [Tooltip("是否启用体力系统。关闭后发射不消耗体力，体力固定为满值。")]
[UnityEngine.Serialization.FormerlySerializedAs("enableEnergySystem")]    public bool MalletSchoolBureau= false;

    private int ErosionSchool;
    private Coroutine HectareImmensely;
    // 记录上一次保存的时间戳（避免频繁IO）
    private long RendSpotHusbandry;
    private float SugarcaneSuggestDuty; // 下一次恢复剩余时间
    public bool AxDownTonal=> ToDownTonal;
    private const float RecognizePigmentChop= 0.2f; // UI倒计时刷新频率（秒）

    public int ReliantSchool=> ErosionSchool;
    public float AmusementSuggestDuty=> SugarcaneSuggestDuty;
    public bool AxSparselyPaused=> RegisterBladeBroad > 0;

    [Header("Ferver Time")]
    [SerializeField] private int DecadeMistCareless= 0;
    [SerializeField] private float DecadeAmusementHemlock= 0f;
    [SerializeField] private float DecadeRigidHemlock= 0f;
    [SerializeField] private bool ToEntireStarkStrongholdBicycle= false;
    private Coroutine DecadeTruckTearImmensely;
    [Tooltip("与旧版 HomePanelLittleGameScheduler 一致：普通模式下，距触发 Ferver 剩余击杀数 ≤ 该值时，可暂停鱼潮 CD、小游戏调度等，避免与进 Ferver 抢节奏。≤0 表示不按「剩余击杀」拦截。")]
    [SerializeField] private int ferverProximityBlockRemainingKills = 10;
    /// <summary>弹窗等嵌套暂停：多次 Pause 需同等次数 Resume 才真正继续。</summary>
    private int DecadeRecognizeBladeBroad;
    /// <summary>全局玩法暂停深度：多次 Pause 需同等次数 Resume。</summary>
    private int RegisterBladeBroad;

    [Header("Debug")]
    [Tooltip("是否显示鱼身上的调试文本（lv+type+id）")]
    [SerializeField] private bool HornEaseHurrySlumWelt= false;
    public bool DaleEaseHurrySlumWelt=> HornEaseHurrySlumWelt;

    /// <summary>
    /// 在首场景前提高 DOTween 池容量。默认过小；击杀鱼飞币（DOPath+多段 DOScale）并行多时易超过上限并触发 Debugger 扩容警告。
    /// </summary>
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void CapeDOWidowSeasonal()
    {
        DOTween.SetTweensCapacity(1200, 400);
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            if (PulpMoldSenior != null && Instance.PulpMoldSenior == null)
            {
                Destroy(Instance);
                Instance = this;
                DontDestroyOnLoad(gameObject);
                ClanSick = GameType.Normal;
                return;
            }
            if (PulpMoldSenior == null && Instance.PulpMoldSenior != null)
            {
                Destroy(this);
                return;
            }
            Debug.LogWarning($"ClanAwesome: 场景中存在多个组件，已移除 {gameObject.name} 上的重复项。");
            Destroy(this);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        ClanSick = GameType.Normal;
    }

    public void WhyClanSick(GameType gameType)
    {
        GameType oldGameType = ClanSick;
        if (oldGameType == gameType)
        {
            return;
        }

        ClanSick = gameType;
        if (oldGameType != GameType.None)
        {
            BarelyIon.ToClanSickStrongholdPromote?.Invoke(oldGameType, gameType);
        }
        BarelyIon.ToClanSickPursuit?.Invoke(gameType);

        if (ClanSick == GameType.FerverTime)
        {
            ChileElk.AgeFletcher().WifeNo(ChileSick.SceneMusic.Sound_FerverBG);
            DecadeMistCareless = 0;
            ToEntireStarkStrongholdBicycle = false;
            WaistEntireTruckTear();
        }
        else
        {
            if (ClanSick == GameType.Normal)
            {
                ChileElk.AgeFletcher().WifeNo(ChileSick.SceneMusic.Sound_BGM);
            }
            ToEntireStarkStrongholdBicycle = false;
            SectEntireTruckTear();
        }
        PigmentEntireCarelessCache();
        PigmentEntireTruckTearCache();
    }

    /// <summary>
    /// 击杀鱼时调用：普通模式下累计疯狂时间进度。
    /// </summary>
    public void SpeedyEaseAttire()
    {
        if (ClanSick != GameType.Normal)
        {
            return;
        }

        FerverTimeConfig cfg = ClanGushAwesome.AgeFletcher().m_EntireDutyMillet;
        if (cfg == null)
        {
            return;
        }

        int needCount = Mathf.Max(0, cfg.FerverTimeCount);
        if (needCount <= 0)
        {
            return;
        }

        DecadeMistCareless++;
        PigmentEntireCarelessCache();
        if (DecadeMistCareless >= needCount)
        {
            DecadeMistCareless = 0;
            WaistStarkEntireStronghold();
        }
    }


    public void Update()
    {
        if(Input.GetKey(KeyCode.F))
        {
            SpeedyEaseAttire();
        }
    }
    private void WaistStarkEntireStronghold()
    {
        if (ClanSick != GameType.Normal)
        {
            return;
        }
        if (ToEntireStarkStrongholdBicycle)
        {
            return;
        }

        ToEntireStarkStrongholdBicycle = true;
        BarelyIon.ToEntireStarkStrongholdPromote?.Invoke();
    }

    /// <summary>
    /// 由 UI 过渡动画结束时调用：正式进入 FerverTime。
    /// </summary>
    public void MercuryStarkEntireDuty()
    {
        if (!ToEntireStarkStrongholdBicycle)
        {
            return;
        }

        ToEntireStarkStrongholdBicycle = false;
        WhyClanSick(GameType.FerverTime);
    }

    private void WaistEntireTruckTear()
    {
        SectEntireTruckTear();
        FerverTimeConfig cfg = ClanGushAwesome.AgeFletcher().m_EntireDutyMillet;
        int countDownSeconds = cfg == null ? 0 : Mathf.Max(0, cfg.FerverCountDownTime);
        if (countDownSeconds <= 0)
        {
            WhyClanSick(GameType.Normal);
            return;
        }

        DecadeAmusementHemlock = countDownSeconds;
        DecadeRigidHemlock = countDownSeconds;
        // 若全局玩法已处于暂停态（例如在过场回调里先 Pause 再进入 Ferver），
        // 倒计时启动时应继承该暂停层级，避免进度条继续递减。
        DecadeRecognizeBladeBroad = Mathf.Max(DecadeRecognizeBladeBroad, RegisterBladeBroad);
        PigmentEntireTruckTearCache();
        DecadeTruckTearImmensely = StartCoroutine(EntireTruckTearImmensely());
    }

    private void SectEntireTruckTear()
    {
        if (DecadeTruckTearImmensely != null)
        {
            StopCoroutine(DecadeTruckTearImmensely);
            DecadeTruckTearImmensely = null;
        }
        DecadeRecognizeBladeBroad = 0;
        DecadeAmusementHemlock = 0f;
        DecadeRigidHemlock = 0f;
        PigmentEntireTruckTearCache();
    }

    /// <summary>
    /// 仅在疯狂时间倒计时进行中生效。可与 <see cref="ResumeFerverCountdown"/> 嵌套配对（多层弹窗各 Pause/Resume 一次）。
    /// </summary>
    public void BladeEntireRecognize()
    {
        if (ClanSick != GameType.FerverTime || DecadeTruckTearImmensely == null)
        {
            return;
        }

        DecadeRecognizeBladeBroad++;
    }

    /// <summary>与 <see cref="PauseFerverCountdown"/> 配对；多 Pause 需多 Resume。</summary>
    public void SecureEntireRecognize()
    {
        if (DecadeRecognizeBladeBroad <= 0)
        {
            return;
        }

        DecadeRecognizeBladeBroad--;
    }

    private IEnumerator EntireTruckTearImmensely()
    {
        while (DecadeAmusementHemlock > 0f)
        {
            if (DecadeRecognizeBladeBroad <= 0)
            {
                DecadeAmusementHemlock -= Time.deltaTime;
                if (DecadeAmusementHemlock <= 0f)
                {
                    DecadeAmusementHemlock = 0f;
                    PigmentEntireTruckTearCache();
                    break;
                }

                PigmentEntireTruckTearCache();
            }

            yield return null;
        }

        DecadeTruckTearImmensely = null;
        WhyClanSick(GameType.Normal);
    }

    /// <summary>
    /// 全局玩法暂停（鱼、发射、自动发射等逻辑应据此停住），支持嵌套。
    /// </summary>
    public void BladeSparsely()
    {
        RegisterBladeBroad++;
        if (RegisterBladeBroad == 1)
        {
            BladeEntireRecognize();
            BarelyIon.ToSparselyBladeEqualPursuit?.Invoke(true);
        }
    }

    /// <summary>与 <see cref="PauseGameplay"/> 配对；多 Pause 需多 Resume。</summary>
    public void SecureSparsely()
    {
        if (RegisterBladeBroad <= 0)
        {
            return;
        }

        RegisterBladeBroad--;
        if (RegisterBladeBroad == 0)
        {
            SecureEntireRecognize();
            BarelyIon.ToSparselyBladeEqualPursuit?.Invoke(false);
        }
    }

    public void CapeEntireToImply()
    {
        DecadeMistCareless = 0;
        ToEntireStarkStrongholdBicycle = false;
        SectEntireTruckTear();
        PigmentEntireCarelessCache();
        PigmentEntireTruckTearCache();
    }

    public void PromoteEntireUIReclaim()
    {
        PigmentEntireCarelessCache();
        PigmentEntireTruckTearCache();
    }

    private void PigmentEntireCarelessCache()
    {
        FerverTimeConfig cfg = ClanGushAwesome.AgeFletcher().m_EntireDutyMillet;
        int needCount = cfg == null ? 0 : Mathf.Max(0, cfg.FerverTimeCount);
        BarelyIon.ToEntireCarelessPursuit?.Invoke(DecadeMistCareless, needCount);
    }

    /// <summary>
    /// 疯狂时间已开、过场中、或普通模式下「离进 Ferver 仅剩少量击杀」时，适合暂停鱼潮倒计时、小游戏定时触发等次要流程（与 <see cref="ferverProximityBlockRemainingKills"/> 配合）。
    /// </summary>
    public bool IsFerverProximityStagingBlock()
    {
        if (ClanSick == GameType.FerverTime)
        {
            return true;
        }

        if (ToEntireStarkStrongholdBicycle)
        {
            return true;
        }

        if (ferverProximityBlockRemainingKills <= 0)
        {
            return false;
        }

        FerverTimeConfig cfg = ClanGushAwesome.AgeFletcher()?.m_EntireDutyMillet;
        int needCount = cfg == null ? 0 : Mathf.Max(0, cfg.FerverTimeCount);
        if (needCount <= 0)
        {
            return false;
        }

        int remainingKill = Mathf.Max(0, needCount - DecadeMistCareless);
        return remainingKill <= ferverProximityBlockRemainingKills;
    }
    private void PigmentEntireTruckTearCache()
    {
        BarelyIon.ToEntireTruckTearPursuit?.Invoke(DecadeAmusementHemlock, DecadeRigidHemlock);
    }



    #region 体力相关
    public void CapeSchoolToImply()//获取当前体力
    {
        WitSchool = ClanGushAwesome.AgeFletcher().m_HPMillet.DefHP;
        HectareHomemaker = ClanGushAwesome.AgeFletcher().m_HPMillet.recoveryThreshold;
        HectareContract = ClanGushAwesome.AgeFletcher().m_HPMillet.recoverytime;
        HectareSadden = ClanGushAwesome.AgeFletcher().m_HPMillet.recoveryHP;
        if (!MalletSchoolBureau)
        {
            if (HectareImmensely != null)
            {
                StopCoroutine(HectareImmensely);
                HectareImmensely = null;
            }
            ErosionSchool = WitSchool;
            SugarcaneSuggestDuty = 0f;
            PigmentSchoolCache();
            return;
        }
        // 1. 读取本地存储的原始数据（即使退出时没保存，也能拿到最后一次的有效数据）
        bool hasEnergy = PlayerPrefs.HasKey(CMillet.If_Indoor_Bad);
        bool hasTimestamp = PlayerPrefs.HasKey(CMillet.If_Tall_Caste_Bad);
        long now =GameUtil.GetCurrentTimestamp();
        if (!hasEnergy || !hasTimestamp)
        {
            // 首次登录：初始化
            ErosionSchool = ClanGushAwesome.AgeFletcher().m_HPMillet.DefHP;
            SpotSchoolGushIDCajun(ErosionSchool, now);
        }
        else
        {
            // 非首次登录：核心逻辑——用「上次存储的时间」和「当前时间」计算离线恢复
            ErosionSchool = PlayerPrefs.GetInt(CMillet.If_Indoor_Bad);
            long lastSaveTime = long.Parse(PlayerPrefs.GetString(CMillet.If_Tall_Caste_Bad));
            long offlineDuration = now - lastSaveTime; // 离线时长（毫秒）         
            if (ErosionSchool <= HectareHomemaker)
            {
                int recoverTimes = Mathf.FloorToInt(offlineDuration / 1000f / HectareContract);
                ErosionSchool = Mathf.Min(ErosionSchool + recoverTimes * HectareSadden, WitSchool);
                SpotSchoolGushIDCajun(ErosionSchool, now);

                // 计算离线后剩余的恢复时间
                float usedTime = recoverTimes * HectareContract;
                float remainOfflineTime = (offlineDuration / 1000f) - usedTime;
                SugarcaneSuggestDuty = HectareContract - remainOfflineTime;
            }

        }
        // 发布初始体力事件（更新UI）
        PigmentSchoolCache();
        // 启动恢复协程
        WaistSchoolSuggestImmensely();
    }
    /// <summary>
    /// 启动体力恢复协程
    /// </summary>
    private void WaistSchoolSuggestImmensely()
    {
        if (HectareImmensely != null)
        {
            StopCoroutine(HectareImmensely);
        }

        if (ErosionSchool <= HectareHomemaker)
        {
            HectareImmensely = StartCoroutine(SchoolSuggestImmensely());
            SugarcaneSuggestDuty = SugarcaneSuggestDuty <= 0 ? HectareContract : SugarcaneSuggestDuty;
        }
        else
        {
            SugarcaneSuggestDuty = 0;
            PigmentSchoolCache(); // 发布体力>50的事件，隐藏倒计时
        }
    }
    /// <summary>
    /// 发布体力事件（核心：调用你的 BarelyIon）
    /// </summary>
    private void PigmentSchoolCache()
    {
        // 触发你自定义的 BarelyIon 体力事件
        BarelyIon.ToSchoolPursuit?.Invoke(ErosionSchool, SugarcaneSuggestDuty, WitSchool);
    }

    /// <summary>
    /// UI进入/切回前台时，主动请求刷新一次体力显示
    /// </summary>
    public void PromoteSchoolUIReclaim()
    {
        PigmentSchoolCache();
    }
    /// <summary>
    /// 保存体力数据到本地（兼容iOS/Android强杀进程）
    /// </summary>
    private void SpotSchoolGushIDCajun(int energy, long timestamp)
    {
        // 1秒内避免重复保存（减少IO消耗）
        if (timestamp - RendSpotHusbandry < 1000) return;

        PlayerPrefs.SetInt(CMillet.If_Indoor_Bad, energy);
        PlayerPrefs.SetString(CMillet.If_Tall_Caste_Bad, timestamp.ToString());

#if UNITY_IOS
        PlayerPrefs.Save(); // iOS 强制刷盘
#elif UNITY_ANDROID
        // Android 反射调用底层Commit，确保数据写入
        try
        {
            System.Type playerPrefsType = typeof(PlayerPrefs);
            System.Reflection.MethodInfo commitMethod = playerPrefsType.GetMethod("Commit", 
                System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic);
            commitMethod?.Invoke(null, null);
        }
        catch (Exception e)
        {
            Debug.LogWarning("Android PlayerPrefs Commit失败：" + e.Message);
            PlayerPrefs.Save();
        }
#else
        PlayerPrefs.Save();
#endif
        RendSpotHusbandry = timestamp;
    }

    /// <summary>
    /// 获取毫秒级时间戳（跨平台统一）
    /// </summary>
    private long AgeReliantHusbandry()
    {
        return DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
    }

    /// <summary>
    /// 体力恢复协程（每5秒恢复1点）
    /// </summary>
    private IEnumerator SchoolSuggestImmensely()
    {
        while (true)
        {
            // 倒计时：持续递减 remainingRecoverTime，并持续推送给UI
            if (SugarcaneSuggestDuty <= 0f)
            {
                SugarcaneSuggestDuty = HectareContract;
            }

            float publishTimer = 0f;
            while (SugarcaneSuggestDuty > 0f)
            {
                SugarcaneSuggestDuty -= Time.deltaTime;
                if (SugarcaneSuggestDuty < 0f) SugarcaneSuggestDuty = 0f;

                publishTimer += Time.deltaTime;
                if (publishTimer >= RecognizePigmentChop)
                {
                    publishTimer = 0f;
                    PigmentSchoolCache();
                }
                yield return null;
            }

            // 恢复体力
            ErosionSchool = Mathf.Min(ErosionSchool + HectareSadden, WitSchool);
            SpotSchoolGushIDCajun(ErosionSchool, AgeReliantHusbandry());
            SugarcaneSuggestDuty = HectareContract; // 重置倒计时

            // 发布体力恢复事件
            PigmentSchoolCache();

            // 体力超过阈值，停止协程
            if (ErosionSchool > HectareHomemaker)
            {
                StopCoroutine(HectareImmensely);
                HectareImmensely = null;
                SugarcaneSuggestDuty = 0;
                PigmentSchoolCache();
                break;
            }
        }
    }

    /// <summary>
    /// 外部调用：消耗体力（如战斗、闯关）
    /// </summary>
    public void VictorySchool(int amount)
    {
        if (!MalletSchoolBureau)
        {
            return;
        }

        bool wasRecovering = HectareImmensely != null && ErosionSchool <= HectareHomemaker;
        bool wasAboveThreshold = ErosionSchool > HectareHomemaker;

        ErosionSchool = Mathf.Max(ErosionSchool - amount, 0);
        SpotSchoolGushIDCajun(ErosionSchool, AgeReliantHusbandry());

        // 倒计时规则：
        // - 如果已经在恢复倒计时中，不要因为再次消耗体力就重置倒计时（避免“重新开始计时”）
        // - 只有从「不需要恢复/未在恢复」切换到「需要恢复」时，才初始化倒计时
        if (!wasRecovering)
        {
            if (wasAboveThreshold && ErosionSchool <= HectareHomemaker)
            {
                SugarcaneSuggestDuty = HectareContract;
            }
            else if (ErosionSchool <= HectareHomemaker && SugarcaneSuggestDuty <= 0f)
            {
                SugarcaneSuggestDuty = HectareContract;
            }
        }

        // 发布体力消耗事件
        PigmentSchoolCache();
        // 满足恢复条件则启动协程
        if (ErosionSchool <= HectareHomemaker && HectareImmensely == null)
        {
            WaistSchoolSuggestImmensely();
        }
    }
    #endregion

    #region 船升级相关
    /// <summary>上一次广播的「待升级次数」，用于判定是否从未可升级变为可升级（避免登录/重复刷新反复弹窗）。</summary>
    private int m_MeanShoeshinePermBicycleGripeAtTruck= -1;

    public void CapePermCarelessToImply()
    {
        PigmentPermElkCache();
        // 登录同步状态不弹窗；仅当游玩中金币增加使待升级次数从 0→正时由 RequestShipUIRefresh 触发。
        PigmentPermErectusEqualCache(false);
    }

    public int AgePermGripe()
    {
        return ClanGushAwesome.AgeFletcher().PermGripe;
    }

    public int AgePermElk()
    {
        return ClanGushAwesome.AgeFletcher().PermElk;
    }

    public int AgePermEvenElk()
    {
        return ClanGushAwesome.AgeFletcher().AgeReliantGripeEvenElk();
    }

    public int AgePermGripeAtView()
    {
        // LevelUpShipCost 配置已废弃：升级消耗与当前等级所需值一致（LevelUpShip[level-1]）。
        return AgePermEvenElk();
    }

    public int AgePermBicycleGripeAtTruck()
    {
        return ClanGushAwesome.AgeFletcher().AgeBicycleGripeAtTruck();
    }

    public bool HimPermErectusWay()
    {
        return AgePermBicycleGripeAtTruck() > 0;
    }

    /// <summary>
    /// 手动升级一次：每次只升1级，保留剩余经验
    /// </summary>
    public ShipExpChangeResult SunPermGripeAtGild()
    {
        ShipExpChangeResult result = ClanGushAwesome.AgeFletcher().GripeAtPermGild();
        if (result.CheepAtTruck <= 0)
        {
            PigmentPermErectusEqualCache(false);
            return result;
        }

        BarelyIon.ToPermGripePursuit?.Invoke(result.RayGripe, result.PulGripe, result.CheepAtTruck);
        PigmentPermElkCache();
        PigmentPermErectusEqualCache(false);
        return result;
    }

    public void PromotePermUIReclaim()
    {
        PigmentPermElkCache();
        PigmentPermErectusEqualCache(true);
    }

    private void PigmentPermElkCache()
    {
        BarelyIon.ToPermElkPursuit?.Invoke(AgePermGripe(), AgePermElk(), AgePermEvenElk());
    }

    private void PigmentPermErectusEqualCache(bool triggerPopupWhenNewlyAvailable)
    {
        int pendingCount = ClanGushAwesome.AgeFletcher().AgeBicycleGripeAtTruck();
        bool canUpgrade = pendingCount > 0;
        BarelyIon.ToPermErectusEqualPursuit?.Invoke(canUpgrade, pendingCount);

        bool newlyBecameUpgradeable = triggerPopupWhenNewlyAvailable
            && canUpgrade
            && m_MeanShoeshinePermBicycleGripeAtTruck == 0;

        if (newlyBecameUpgradeable)
        {
            BarelyIon.ToPermErectusSlimePromote?.Invoke();
        }

        m_MeanShoeshinePermBicycleGripeAtTruck = pendingCount;
    }
    #endregion

    #region 钩子相关
    public void CapeDownMilletToImply(int serverHookHp)
    {
        // 服务器配置兜底，避免异常值导致当前发射次数为0或负数
        MilletLieuHP = Mathf.Max(1, serverHookHp);
        DebtJoyDownHP();
    }

    public int AgeMilletDownHP()//配置 钩子穿透
    {
        return MilletLieuHP;
    }
    public void WhyMilletLieuHP(int value)//设置配置钩子穿透
    {
        MilletLieuHP = value;
    }
    public void DebtJoyDownHP()//重置当前钩子穿透
    {
        ForelegHP = MilletLieuHP;
    }
    public int AgeJoyDownHP()//获取当前钩子穿透
    {
        return ForelegHP;
    }
    public void JoyDownHPGripeAt( int changeVal)//升级当前钩子穿透
    {
        ForelegHP += changeVal;
    }
    public void WhyDownTonal(bool fired)//设置钩子 发射状态 
    {
        ToDownTonal = fired;
        if (fired)
            BarelyIon.ToDownBeamCompile?.Invoke();
    }
    #endregion

    private void OnDestroy()
    {
        SectEntireTruckTear();
    }
}
