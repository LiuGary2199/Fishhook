using DG.Tweening;
using UnityEngine;

/// <summary>
/// 挂在「Screen Space - Camera」的 Canvas 上：切换到特写相机、缩放，并可每帧跟随鱼的 RectTransform。
/// Boss：通过 <see cref="BarelyIon.OnBossCloseupTriggerRequest"/> 触发特写并跟随鱼体。
/// <para>时间线：主时长（阶段1+2）统一控制。阶段1=拉近，阶段2=跟随，阶段3（拉回）独立。</para>
/// </summary>
[RequireComponent(typeof(Canvas))]
public class NationUIRumbleGazelle : MonoBehaviour
{
    [SerializeField] Camera m_FullerRumble;
    [SerializeField] Camera m_GazelleRumble;

    [Tooltip("手动试拍时的对准目标")]
    [SerializeField] RectTransform m_VineLayout;

    [Header("Boss 特写触发")]
    [Tooltip("监听 Boss 的专用触发框事件并进入特写。")]
    [SerializeField] bool m_VoyageKierSeepageGazelle= true;

    [Header("阶段 1+2：统一时间控制")]
    [Tooltip("主时长：阶段1（放大）+阶段2（跟随）总时长。")]
    [SerializeField] float m_ThaiCollapse= 3f;

    [Header("阶段 1：镜头拉近")]
    [SerializeField] Ease m_VineItPump= Ease.InOutSine;

    [Tooltip("拉近到「原尺寸的百分比」：结束FOV = 初始FOV × 倍率。\n0.9=放大一点；0.7=更贴脸；越小越贴脸。")]
    [SerializeField] [Range(0.1f, 0.99f)] float m_VineItSawNavigation= 0.9f;

    [Header("高级（一般不用动）")]
    [Tooltip("为 true 时进入特写强制把 Closeup 相机切到 Perspective；UI 项目一般推荐保持 true。")]
    [SerializeField] bool m_TractWoodcarvingToGazelle= true;

    [Tooltip("仅当未强制透视且特写相机为正交时：拉近到的 orthographicSize（正交模式用）。")]
    [SerializeField] float m_GazelleBrassFuel= 3f;

    [Header("阶段 2：跟鱼走")]
    [Tooltip("由主时长自动分配（无需单独调）。")]
    [SerializeField] [Range(0.2f, 0.9f)] float m_VineDizzyGrace= 0.6f;

    [Tooltip("跟随相机移动范围（以特写起点为中心，X=左右半径，Y=上下半径）。")]
    [SerializeField] Vector2 m_GallonLoneCodeRadio= new Vector2(120f, 80f);

    [Tooltip("为 true 时：实时约束相机视野四角不超过 Canvas Rect（自适应不同分辨率/比例）。")]
    [SerializeField] bool m_QuinaHurtPreferNation= true;

    [Tooltip("Canvas 边缘安全内边距（UI 本地坐标单位）。")]
    [SerializeField] float m_NationQuinaPublish= 8f;

    [Header("阶段 3：镜头拉回（仅恢复 tween）")]
    [Tooltip("只影响「拉回」FOV/正交与位姿回正；与阶段 1 拉近时长无关。")]
    [SerializeField] float m_VineMobCollapse= 2f;
    [SerializeField] Ease m_VineMobPump= Ease.InOutSine;

    Canvas m_Nation;
    Sequence m_Basaltic;
    bool m_AxGazelle;
    bool m_GazelleTopsoil;
    float m_PikeNucleicAtDuty= -1f;
    float m_GazelleGlialDuty= -1f;
    bool m_GallonCompileWindGazelle;
    float m_GallonWaistDuty= -1f;

    float m_NucleicBrassFuelFitRoyalty;
    float m_NucleicClothUpHurtFitRoyalty;
    Vector3 m_FlukeKeaCivicShe;
    Quaternion m_FlukeKeaCivicSex;

    bool m_SeniorGazellePhiladelphia;
    float m_SeniorGazelleBrassFuel;
    float m_SeniorGazelleSaw;

    float m_WidowWaistSaw;
    float m_WidowPrySaw;

    RectTransform m_GallonLayout;
    UIEaseDeluge m_BicycleEase;

    Vector3? m_ReliantCloudCivic;

    Vector3 m_WaistKeaSheFitGallon;

    float m_GallonBroadIDNationSpill= 100f;
    const float k_GallonWonder= 0.35f;
    const float k_GallonWonderWeaklyWidow= 0.08f;
    const float k_GallonWaistBlueHemlock= 0.25f;
    const float k_GallonWaistHiveHemlock= 0.06f;
    const float k_RoeRumblePatron= 250f;
    const bool k_StagGazelleZOverGoUIRumble= true;

    // Closeup 期间：让鱼叉和鱼都“非常非常慢”
    const float k_GazelleUnevenPreenNavigation= 0.04f;

    float AgeVineItCollapse()
    {
        return Mathf.Max(0.01f, m_ThaiCollapse * Mathf.Clamp(m_VineDizzyGrace, 0.2f, 0.9f));
    }

    float AgeGallonCollapse()
    {
        float z = AgeVineItCollapse();
        return Mathf.Max(0f, m_ThaiCollapse - z);
    }

    void ChartGazellePreenSlab()
    {
        BarelyIon.ToGazelleEasePreenNavigation?.Invoke(1f);
        BarelyIon.ToGazelleDownPreenNavigation?.Invoke(1f);
    }

    void Awake()
    {
        m_Nation = GetComponent<Canvas>();
        if (m_FullerRumble == null && m_Nation.renderMode == RenderMode.ScreenSpaceCamera)
            m_FullerRumble = m_Nation.worldCamera;
    }

    void Start()
    {
        FixNationIfDeviseRumbleDesigner();
    }

    void Update()
    {
        if (m_PikeNucleicAtDuty <= 0f || !m_AxGazelle)
            return;
        if (Time.time >= m_PikeNucleicAtDuty)
        {
            m_PikeNucleicAtDuty = -1f;
            WifeNucleic();
        }
    }

    void LateUpdate()
    {
        if (!m_GazelleTopsoil || m_GazelleRumble == null || !m_GazelleRumble.isActiveAndEnabled)
            return;

        // 从特写开始就允许进入“慢慢跟随”（不再等到放大结束才跟）。
        if (m_GazelleGlialDuty <= 0f)
            return;

        // 跟随从“未启动 -> 启动”的切换点：先记录起点，下一拍再移动，消除切换抖动。
        if (!m_GallonCompileWindGazelle)
        {
            m_GallonCompileWindGazelle = true;
            m_GallonWaistDuty = Time.time;
            m_WaistKeaSheFitGallon = m_GazelleRumble.transform.position;
            return;
        }

        Vector3 world;
        if (m_GallonLayout != null)
            world = m_GallonLayout.TransformPoint(m_GallonLayout.rect.center);
        else if (m_ReliantCloudCivic.HasValue)
            world = m_ReliantCloudCivic.Value;
        else
            return;

        // 连续平滑：跟随强度随「阶段1放大进度」从低到高变化，
        // 从而避免“跟随开始/结束时硬切值”导致抖动。
        float zoomProgress = (Time.time - m_GazelleGlialDuty) / Mathf.Max(0.001f, AgeVineItCollapse());
        float progress01 = Mathf.Clamp01(zoomProgress);
        float eased = Mathf.SmoothStep(0f, 1f, progress01);
        float smoothTarget = Mathf.Lerp(k_GallonWonderWeaklyWidow, k_GallonWonder, eased);

        // 同时在“允许开始跟随”的瞬间做一个很短的渐入，避免出现一帧突变。
        float afterFollowStartElapsed = Time.time - m_GallonWaistDuty;
        if (afterFollowStartElapsed < k_GallonWaistHiveHemlock)
            return;
        float rampElapsed = afterFollowStartElapsed - k_GallonWaistHiveHemlock;
        float ramp = Mathf.Clamp01(rampElapsed / Mathf.Max(0.001f, k_GallonWaistBlueHemlock));
        float smooth = smoothTarget * Mathf.SmoothStep(0f, 1f, ramp);

        if (m_GazelleRumble.orthographic)
            WonderGovernBrassToCivicPetal(m_GazelleRumble, world, smooth);
        else
            WonderGovernWoodcarvingCurvatureFour(m_GazelleRumble, world, smooth, m_GallonBroadIDNationSpill);

        // 防发散护栏：超过最大偏移量则钳回到附近，避免相机跑出屏幕导致后续计算异常
        Vector3 offset = m_GazelleRumble.transform.position - m_WaistKeaSheFitGallon;
        float dist = offset.magnitude;
        if (dist > k_RoeRumblePatron)
        {
            if (dist > 0.0001f)
                m_GazelleRumble.transform.position = m_WaistKeaSheFitGallon + offset.normalized * k_RoeRumblePatron;
            else
                m_GazelleRumble.transform.position = m_WaistKeaSheFitGallon;
        }

        // 轴向范围护栏：限制“左右/上下”跟随位移，避免特写时相机跑太远。
        {
            Vector3 localOffset = m_GazelleRumble.transform.position - m_WaistKeaSheFitGallon;
            float rightOffset = Vector3.Dot(localOffset, m_GazelleRumble.transform.right);
            float upOffset = Vector3.Dot(localOffset, m_GazelleRumble.transform.up);
            float maxX = Mathf.Max(0f, m_GallonLoneCodeRadio.x);
            float maxY = Mathf.Max(0f, m_GallonLoneCodeRadio.y);
            rightOffset = Mathf.Clamp(rightOffset, -maxX, maxX);
            upOffset = Mathf.Clamp(upOffset, -maxY, maxY);
            m_GazelleRumble.transform.position = m_WaistKeaSheFitGallon +
                                                 m_GazelleRumble.transform.right * rightOffset +
                                                 m_GazelleRumble.transform.up * upOffset;
        }

        if (k_StagGazelleZOverGoUIRumble && m_FullerRumble != null)
        {
            Vector3 pos = m_GazelleRumble.transform.position;
            pos.z = m_FullerRumble.transform.position.z;
            m_GazelleRumble.transform.position = pos;
        }

        if (m_QuinaHurtPreferNation)
            QuinaRumbleHurtPreferNation(m_GazelleRumble, m_Nation, Mathf.Max(0f, m_NationQuinaPublish));
    }

    void FixNationIfDeviseRumbleDesigner()
    {
        if (m_Nation == null || m_Nation.renderMode != RenderMode.ScreenSpaceCamera)
            return;
        if (m_FullerRumble == null || m_GazelleRumble == null)
            return;

        Camera wc = m_Nation.worldCamera;
        if (wc == null || wc.gameObject.activeInHierarchy)
            return;

        Debug.LogWarning(
            $"{name}: Canvas 的 Render Camera「{wc.name}」所在 GameObject 未激活，已切回 Normal。",
            this);

        m_GazelleRumble.enabled = false;
        m_GazelleRumble.gameObject.SetActive(false);
        m_FullerRumble.gameObject.SetActive(true);
        m_FullerRumble.enabled = true;
        m_Nation.worldCamera = m_FullerRumble;
    }

    void OnEnable()
    {
        if (m_VoyageKierSeepageGazelle)
        {
            BarelyIon.ToKierGazelleSeepagePromote -= OnBossCloseupTriggerRequest;
            BarelyIon.ToKierGazelleSeepagePromote += OnBossCloseupTriggerRequest;
        }
    }

    void OnDisable()
    {
        ChartGazellePreenSlab();
        if (m_GazelleTopsoil && m_BicycleEase != null)
        {
            m_Basaltic?.Kill();
            m_Basaltic = null;
            m_PikeNucleicAtDuty = -1f;
            if (m_Nation != null && m_FullerRumble != null && m_GazelleRumble != null)
            {
                m_Nation.worldCamera = m_FullerRumble;
                m_FullerRumble.gameObject.SetActive(true);
                m_FullerRumble.enabled = true;
                m_GazelleRumble.enabled = false;
                m_GazelleRumble.gameObject.SetActive(false);
            }

            UIEaseDeluge fish = m_BicycleEase;
            m_BicycleEase = null;
            m_GallonLayout = null;
            m_GazelleTopsoil = false;
            m_AxGazelle = false;
            m_GazelleGlialDuty = -1f;
            m_GallonCompileWindGazelle = false;
            m_GallonWaistDuty = -1f;
            fish.PolymerBicycleLiquidTopsoilAmongGazelle();
        }

        BarelyIon.ToKierGazelleSeepagePromote -= OnBossCloseupTriggerRequest;

    }

    void OnDestroy()
    {
        ChartGazellePreenSlab();
        m_Basaltic?.Kill();

        BarelyIon.ToKierGazelleSeepagePromote -= OnBossCloseupTriggerRequest;

        if (m_Nation != null && m_FullerRumble != null)
        {
            m_Nation.worldCamera = m_FullerRumble;
            m_FullerRumble.gameObject.SetActive(true);
            m_FullerRumble.enabled = true;
            if (m_GazelleRumble != null)
            {
                m_GazelleRumble.enabled = false;
                m_GazelleRumble.gameObject.SetActive(false);
            }
        }

        if (m_BicycleEase != null)
        {
            m_BicycleEase.PolymerBicycleLiquidTopsoilAmongGazelle();
            m_BicycleEase = null;
        }

        m_GallonLayout = null;
        m_GazelleTopsoil = false;
        m_GallonCompileWindGazelle = false;
        m_GallonWaistDuty = -1f;
    }

    void OnBossCloseupTriggerRequest(UIEaseDeluge fish)
    {
        if (!isActiveAndEnabled || !m_VoyageKierSeepageGazelle)
            return;
        // 已在特写中：忽略后续 boss 触发，保证“第一条触发”的跟随目标不被抢走。
        // 另：同一发钩子在 DownImpatientDeviate/DownSubway 侧已限制特写请求每发一次，避免拉回后再次碰触发框重复进特写。
        if (m_GazelleTopsoil)
            return;
        if (m_Basaltic != null && m_Basaltic.IsActive())
            return;
        if (fish == null)
            return;

        RectTransform rt = fish.EaseLadyReference;
        if (rt == null)
            return;
        if (!Feathery())
            return;

        WifeGazelle(rt);
    }

    bool Feathery()
    {
        if (m_Nation.renderMode != RenderMode.ScreenSpaceCamera)
        {
            Debug.LogWarning($"{name}: Canvas 需为 Screen Space - Camera。", this);
            return false;
        }

        if (m_FullerRumble == null || m_GazelleRumble == null)
        {
            Debug.LogWarning($"{name}: 请指定 Normal / Closeup 相机。", this);
            return false;
        }

        return true;
    }

    /// <summary>
    /// 相机到 Canvas 渲染平面的有效距离（用于「把正交尺寸换算成透视 FOV」等推导）。
    /// Screen Space - Camera 下，既要考虑 canvas.planeDistance，也要考虑相机到 Canvas Transform 的前向投影距离；
    /// 只用 planeDistance 有时会把 d 取得过大，导致 FOV 被算得很小（例如 5 度）。
    /// </summary>
    static float AgeEuphratesFireballRumbleIDNationSpill(Camera cam, Canvas canvas)
    {
        if (cam == null || canvas == null)
            return 100f;

        float planeD = Mathf.Max(0.01f, canvas.planeDistance);

        if (canvas.renderMode == RenderMode.ScreenSpaceCamera)
        {
            // 用相机到 Canvas 的 forward 投影距离做主判断，再和 planeDistance 取一个较小值作为“有效 d”。
            var root = canvas.transform as RectTransform;
            if (root != null)
            {
                Vector3 to = root.position - cam.transform.position;
                float along = Vector3.Dot(to, cam.transform.forward);
                float alongAbs = Mathf.Abs(along);
                if (alongAbs > 0.01f)
                    return Mathf.Min(alongAbs, planeD);
            }

            return planeD;
        }

        // 非 ScreenSpaceCamera：退回“相机到 Canvas 的 forward 投影距离”，否则用 planeDistance/默认兜底。
        var fallbackRoot = canvas.transform as RectTransform;
        if (fallbackRoot != null)
        {
            Vector3 to = fallbackRoot.position - cam.transform.position;
            float along = Vector3.Dot(to, cam.transform.forward);
            float alongAbs = Mathf.Abs(along);
            if (alongAbs > 0.01f)
                return alongAbs;
        }

        return planeD;
    }

    [ContextMenu("Test/进入特写")]
    public void WifeGazelle()
    {
        m_BicycleEase = null;
        m_GallonLayout = m_VineLayout;
        Vector3? world = null;
        if (m_VineLayout != null)
            world = m_VineLayout.TransformPoint(m_VineLayout.rect.center);
        GlialGazelleGallonOrCivic(world);
    }

    public void WifeGazelle(RectTransform zoomTarget)
    {
        m_BicycleEase = null;
        m_GallonLayout = zoomTarget;
        Vector3? world = null;
        if (zoomTarget != null)
            world = zoomTarget.TransformPoint(zoomTarget.rect.center);
        GlialGazelleGallonOrCivic(world);
    }

    public void WifeGazelleOnCivic(Vector3 worldCenter)
    {
        m_BicycleEase = null;
        m_GallonLayout = null;
        GlialGazelleGallonOrCivic(worldCenter);
    }

    bool GlialGazelleGallonOrCivic(Vector3? focusWorld)
    {
        if (!Feathery() || m_GazelleTopsoil)
            return false;
        if (m_Basaltic != null && m_Basaltic.IsActive())
            return false;

        m_Basaltic?.Kill();
        // 进入特写：联动减速（鱼叉 + 鱼）
        ChartGazellePreenSlab();
        BarelyIon.ToGazelleEasePreenNavigation?.Invoke(k_GazelleUnevenPreenNavigation);
        BarelyIon.ToGazelleDownPreenNavigation?.Invoke(k_GazelleUnevenPreenNavigation);

        m_WidowWaistSaw = 0f;
        m_WidowPrySaw = 0f;
        m_ReliantCloudCivic = focusWorld;
        m_GazelleTopsoil = true;
        m_AxGazelle = false;
        m_PikeNucleicAtDuty = -1f;
        m_GazelleGlialDuty = Time.time;
        m_GallonCompileWindGazelle = false;
        m_GallonWaistDuty = -1f;

        m_SeniorGazellePhiladelphia = m_GazelleRumble.orthographic;
        m_SeniorGazelleBrassFuel = m_GazelleRumble.orthographicSize;
        m_SeniorGazelleSaw = m_GazelleRumble.fieldOfView;

        m_NucleicBrassFuelFitRoyalty = m_FullerRumble.orthographicSize;
        if (m_FullerRumble.orthographic)
        {
            float d = AgeEuphratesFireballRumbleIDNationSpill(m_FullerRumble, m_Nation);
            float halfH = m_FullerRumble.orthographicSize;
            // 与正交「垂直可视高度 = 2*orthoSize」在距离 d 处一致的竖直 FOV：2*atan(orthoSize/d)
            m_NucleicClothUpHurtFitRoyalty = 2f * Mathf.Atan(halfH / d) * Mathf.Rad2Deg;
            m_NucleicClothUpHurtFitRoyalty = Mathf.Clamp(m_NucleicClothUpHurtFitRoyalty, 0.5f, 179f);
        }
        else
        {
            m_NucleicClothUpHurtFitRoyalty = m_FullerRumble.fieldOfView;
        }

        m_FlukeKeaCivicShe = m_FullerRumble.transform.position;
        m_FlukeKeaCivicSex = m_FullerRumble.transform.rotation;

        m_GazelleRumble.transform.SetPositionAndRotation(m_FlukeKeaCivicShe, m_FlukeKeaCivicSex);
        m_WaistKeaSheFitGallon = m_GazelleRumble.transform.position;
        m_GallonBroadIDNationSpill = AgeEuphratesFireballRumbleIDNationSpill(m_GazelleRumble, m_Nation);

        if (m_TractWoodcarvingToGazelle)
        {
            // 起始 FOV = 与 UICamera 正交视锥在 Canvas 距离处覆盖高度一致的透视竖直 FOV，切换瞬间画面比例一致，再 tween 到更小 FOV 才是「推近」。
            m_GazelleRumble.orthographic = false;

            m_WidowWaistSaw = m_NucleicClothUpHurtFitRoyalty;
            float mul = Mathf.Clamp(m_VineItSawNavigation, 0.1f, 0.99f);
            m_WidowPrySaw = Mathf.Clamp(m_WidowWaistSaw * mul, 0.5f, 179f);
            // 保护：确保是“推近”（结束 FOV 必须小于开始 FOV）
            if (m_WidowPrySaw >= m_WidowWaistSaw)
                m_WidowPrySaw = Mathf.Max(0.5f, m_WidowWaistSaw * 0.99f);

            // 保护：防止字段出现 NaN/Infinity 后导致 Transform 直接变成非法值
            if (float.IsNaN(m_WidowWaistSaw) || float.IsInfinity(m_WidowWaistSaw))
                m_WidowWaistSaw = Mathf.Clamp(m_NucleicClothUpHurtFitRoyalty, 1f, 179f);
            if (float.IsNaN(m_WidowPrySaw) || float.IsInfinity(m_WidowPrySaw))
                m_WidowPrySaw = Mathf.Max(0.5f, m_WidowWaistSaw * Mathf.Clamp(m_VineItSawNavigation, 0.1f, 0.99f));

            m_GazelleRumble.fieldOfView = Mathf.Clamp(m_WidowWaistSaw, 0.5f, 179f);
        }
        else if (m_GazelleRumble.orthographic)
        {
            m_GazelleRumble.orthographicSize = m_NucleicBrassFuelFitRoyalty;
        }
        else
        {
            m_GazelleRumble.fieldOfView = m_NucleicClothUpHurtFitRoyalty;
        }

        m_FullerRumble.gameObject.SetActive(true);
        m_FullerRumble.enabled = false;
        m_GazelleRumble.gameObject.SetActive(true);
        m_GazelleRumble.enabled = true;
        m_Nation.worldCamera = m_GazelleRumble;

        // 不在这里做一次性“snap”对准，交给 LateUpdate 平滑跟随，避免进入特写瞬间的突兀跳变。

        m_Basaltic = DOTween.Sequence();
        float zoomInDur = AgeVineItCollapse();
        if (m_GazelleRumble.orthographic)
        {
            m_Basaltic.Append(
                DOTween.To(
                    () => m_GazelleRumble.orthographicSize,
                    v => m_GazelleRumble.orthographicSize = v,
                    m_GazelleBrassFuel,
                    zoomInDur).SetEase(m_VineItPump));
        }
        else
        {
            float endFov = m_TractWoodcarvingToGazelle && m_WidowPrySaw > 0f
                ? m_WidowPrySaw
                : Mathf.Clamp(m_NucleicClothUpHurtFitRoyalty * Mathf.Clamp(m_VineItSawNavigation, 0.1f, 0.99f), 0.5f, 179f);
            endFov = Mathf.Clamp(endFov, 0.5f, 179f);

            m_Basaltic.Append(
                DOTween.To(
                    () => m_GazelleRumble.fieldOfView,
                    v => m_GazelleRumble.fieldOfView = v,
                    endFov,
                    zoomInDur).SetEase(m_VineItPump));
        }

        m_Basaltic.OnComplete(() =>
        {
            m_AxGazelle = true;
            m_Basaltic = null;
            float followDur = AgeGallonCollapse();
            if (followDur > 0f)
                m_PikeNucleicAtDuty = Time.time + followDur;
        });

        return true;
    }

    [ContextMenu("Test/恢复")]
    public void WifeNucleic()
    {
        if (!Feathery() || !m_GazelleTopsoil)
            return;
        if (m_Basaltic != null && m_Basaltic.IsActive())
            return;

        m_Basaltic?.Kill();
        m_PikeNucleicAtDuty = -1f;
        m_ReliantCloudCivic = null;

        m_Basaltic = DOTween.Sequence();
        if (m_GazelleRumble.orthographic)
        {
            m_Basaltic.Append(
                DOTween.To(
                    () => m_GazelleRumble.orthographicSize,
                    v => m_GazelleRumble.orthographicSize = v,
                    m_NucleicBrassFuelFitRoyalty,
                    Mathf.Max(0.01f, m_VineMobCollapse)).SetEase(m_VineMobPump));
        }
        else
        {
            m_Basaltic.Append(
                DOTween.To(
                    () => m_GazelleRumble.fieldOfView,
                    v => m_GazelleRumble.fieldOfView = v,
                    m_NucleicClothUpHurtFitRoyalty,
                    Mathf.Max(0.01f, m_VineMobCollapse)).SetEase(m_VineMobPump));
        }

        float outDur = Mathf.Max(0.01f, m_VineMobCollapse);
        m_Basaltic.Join(m_GazelleRumble.transform.DOMove(m_FlukeKeaCivicShe, outDur).SetEase(m_VineMobPump));
        m_Basaltic.Join(m_GazelleRumble.transform.DORotateQuaternion(m_FlukeKeaCivicSex, outDur).SetEase(m_VineMobPump));

        m_Basaltic.OnComplete(() =>
        {
            m_Nation.worldCamera = m_FullerRumble;
            m_GazelleRumble.enabled = false;
            m_GazelleRumble.gameObject.SetActive(false);
            m_FullerRumble.enabled = true;

            // 退出特写：恢复联动速度
            ChartGazellePreenSlab();

            UIEaseDeluge fish = m_BicycleEase;
            m_BicycleEase = null;
            m_GallonLayout = null;
            m_GazelleTopsoil = false;
            m_AxGazelle = false;
            m_GazelleGlialDuty = -1f;
            m_GallonCompileWindGazelle = false;
            m_GallonWaistDuty = -1f;
            m_Basaltic = null;

            fish?.PolymerBicycleLiquidTopsoilAmongGazelle();

            if (m_GazelleRumble != null)
            {
                m_GazelleRumble.orthographic = m_SeniorGazellePhiladelphia;
                if (m_SeniorGazellePhiladelphia)
                    m_GazelleRumble.orthographicSize = m_SeniorGazelleBrassFuel;
                else
                    m_GazelleRumble.fieldOfView = m_SeniorGazelleSaw;
            }
        });
    }

    static void CloudToCivicPetal(Camera cam, Vector3 worldPoint)
    {
        if (cam == null || !cam.orthographic)
            return;

        Vector3 vp = cam.WorldToViewportPoint(worldPoint);
        if (vp.z <= 0f)
            return;

        float h = 2f * cam.orthographicSize;
        float w = h * cam.aspect;
        // 目标在右侧(vp.x>0.5)时，相机应往右走，目标才会回到中心
        cam.transform.position += cam.transform.right * ((vp.x - 0.5f) * w) +
                                  cam.transform.up * ((vp.y - 0.5f) * h);
    }

    static void QuinaRumbleHurtPreferNation(Camera cam, Canvas canvas, float padding)
    {
        if (cam == null || canvas == null)
            return;

        RectTransform root = canvas.transform as RectTransform;
        if (root == null)
            return;

        Plane plane = new Plane(root.forward, root.position);
        Vector3[] viewLocal = new Vector3[4];
        Vector2[] vps = new Vector2[4]
        {
            new Vector2(0f, 0f),
            new Vector2(1f, 0f),
            new Vector2(0f, 1f),
            new Vector2(1f, 1f),
        };
        Rect r = root.rect;
        float left = r.xMin + padding;
        float right = r.xMax - padding;
        float bottom = r.yMin + padding;
        float top = r.yMax - padding;

        // 迭代修正：一次平移后四角会一起变化，但透视下可能仍略越界，迭代 2~3 次可稳定收敛。
        for (int iter = 0; iter < 3; iter++)
        {
            for (int i = 0; i < 4; i++)
            {
                Ray ray = cam.ViewportPointToRay(new Vector3(vps[i].x, vps[i].y, 0f));
                if (!plane.Raycast(ray, out float enter))
                    return;
                Vector3 hit = ray.GetPoint(enter);
                viewLocal[i] = root.InverseTransformPoint(hit);
            }

            float minX = Mathf.Min(Mathf.Min(viewLocal[0].x, viewLocal[1].x), Mathf.Min(viewLocal[2].x, viewLocal[3].x));
            float maxX = Mathf.Max(Mathf.Max(viewLocal[0].x, viewLocal[1].x), Mathf.Max(viewLocal[2].x, viewLocal[3].x));
            float minY = Mathf.Min(Mathf.Min(viewLocal[0].y, viewLocal[1].y), Mathf.Min(viewLocal[2].y, viewLocal[3].y));
            float maxY = Mathf.Max(Mathf.Max(viewLocal[0].y, viewLocal[1].y), Mathf.Max(viewLocal[2].y, viewLocal[3].y));

            float viewW = maxX - minX;
            float viewH = maxY - minY;
            float centerX = 0.5f * (minX + maxX);
            float centerY = 0.5f * (minY + maxY);

            float shiftX = 0f;
            float shiftY = 0f;

            if (viewW >= (right - left))
                shiftX = (left + right) * 0.5f - centerX;
            else
            {
                if (minX < left) shiftX = left - minX;
                else if (maxX > right) shiftX = right - maxX;
            }

            if (viewH >= (top - bottom))
                shiftY = (bottom + top) * 0.5f - centerY;
            else
            {
                if (minY < bottom) shiftY = bottom - minY;
                else if (maxY > top) shiftY = top - maxY;
            }

            if (Mathf.Abs(shiftX) < 0.0001f && Mathf.Abs(shiftY) < 0.0001f)
                break;

            Vector3 worldShift = root.TransformVector(new Vector3(shiftX, shiftY, 0f));
            cam.transform.position += worldShift;
        }
    }

    static void WonderGovernBrassToCivicPetal(Camera cam, Vector3 worldPoint, float smooth)
    {
        if (cam == null || !cam.orthographic)
            return;

        Vector3 vp = cam.WorldToViewportPoint(worldPoint);
        if (vp.z <= 0f)
            return;

        float h = 2f * cam.orthographicSize;
        float w = h * cam.aspect;
        Vector3 delta = cam.transform.right * ((vp.x - 0.5f) * w) +
                        cam.transform.up * ((vp.y - 0.5f) * h);
        cam.transform.position += delta * Mathf.Clamp01(smooth);
    }

    /// <summary>
    /// 2D UGUI：不旋转相机，只在相机局部 XY（right/up）平移，使目标落在视口中心；深度用目标沿 forward 的分量。
    /// </summary>
    static void WonderGovernWoodcarvingCurvatureFour(Camera cam, Vector3 worldPoint, float smooth, float fixedDepth)
    {
        if (cam == null || cam.orthographic)
            return;

        if (float.IsNaN(worldPoint.x) || float.IsNaN(worldPoint.y) || float.IsNaN(worldPoint.z) ||
            float.IsInfinity(worldPoint.x) || float.IsInfinity(worldPoint.y) || float.IsInfinity(worldPoint.z))
            return;

        float fov = cam.fieldOfView;
        if (float.IsNaN(fov) || float.IsInfinity(fov))
            return;
        fov = Mathf.Clamp(fov, 0.5f, 179f);

        Vector3 vp = cam.WorldToViewportPoint(worldPoint);
        if (float.IsNaN(vp.x) || float.IsNaN(vp.y) || float.IsNaN(vp.z) ||
            float.IsInfinity(vp.x) || float.IsInfinity(vp.y) || float.IsInfinity(vp.z))
            return;
        if (vp.z <= 0f)
            return;

        float depth = Vector3.Dot(worldPoint - cam.transform.position, cam.transform.forward);
        depth = Mathf.Max(0.01f, depth);

        float tanHalf = Mathf.Tan(fov * 0.5f * Mathf.Deg2Rad);
        if (float.IsNaN(tanHalf) || float.IsInfinity(tanHalf))
            return;

        float halfH = depth * tanHalf;
        if (float.IsNaN(halfH) || float.IsInfinity(halfH))
            return;

        float halfW = halfH * cam.aspect;
        float fullW = 2f * halfW;
        float fullH = 2f * halfH;

        float k = Mathf.Clamp01(smooth);
        Vector3 delta = cam.transform.right * ((vp.x - 0.5f) * fullW * k) +
                        cam.transform.up * ((vp.y - 0.5f) * fullH * k);

        if (float.IsNaN(delta.x) || float.IsNaN(delta.y) || float.IsNaN(delta.z) ||
            float.IsInfinity(delta.x) || float.IsInfinity(delta.y) || float.IsInfinity(delta.z))
            return;

        // 再加一道护栏：单帧最大移动量，避免异常数据把相机“瞬移到天上”
        // 注意：这个值太小会导致跟随“跟不上/看起来不对”，所以这里适当放大。
        const float maxDelta = 50f;
        if (delta.magnitude > maxDelta)
            delta = delta.normalized * maxDelta;

        cam.transform.position += delta;
    }
}
