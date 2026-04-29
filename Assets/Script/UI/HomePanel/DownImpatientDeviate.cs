using UnityEngine;

/// <summary>
/// 穿刺钩子碰撞：碰鱼播放受击、继续飞；碰墙回收。
/// 不触发 OnCollFish/OnCollWall，避免影响旧系统。
/// </summary>
[RequireComponent(typeof(DownLivelihood))]
public class DownImpatientDeviate : MonoBehaviour
{
    private const string WallDon= "Wall";
    private const string EaseDon= "Fish";

    private DownLivelihood m_Livelihood;

    // 本次发射（一次钩子）内连击计数：每撞到一只鱼 +1
    private int m_EaseFadTruck;
    // 本次发射（一次钩子）内用于“减速叠加”的命中计数：每次碰到鱼 +1
    private int m_EaseFadTruckFitJolt;
    private bool m_BeamGunpowder;

    private AphidDisc m_AphidDisc;

    [Header("Test: Fish Zoom (duration)")]
    [Tooltip("特写推进时长（秒）；被 MagnifierCam / UI zoom window 复用")]
[UnityEngine.Serialization.FormerlySerializedAs("m_TestZoomDuration")]    public float m_LeanVineCollapse= 0.35f;

    [Header("Debug")]
    [Tooltip("调试：命中鱼时打印日志，确认 OnTriggerEnter2D 分支是否执行")]
[UnityEngine.Serialization.FormerlySerializedAs("m_DebugHitFish")]    public bool m_HurryFadEase= true;

    private bool m_AxVineTriggeredFitWindBeam;
    private bool m_KierGazellePopulateWindBeam;
    private bool m_KierHisLiquidFadPopulateWindDownBeam;

    [Header("Test: Fish UI Zoom Window (RawImage area)")]
    [Tooltip("命中第一条鱼时打开 UGUI 放大窗口（只在 RawImage 区域放大）。")]
[UnityEngine.Serialization.FormerlySerializedAs("m_EnableUIZoomWindowTest")]    public bool m_VoyageUIVineSubwayLean= false;

    [Tooltip("放大倍数（传给 EaseUIVineSubwayDemobilize.zoomScale）。")]
[UnityEngine.Serialization.FormerlySerializedAs("m_UIZoomScale")]    public float m_UIVinePerch= 2f;

    private global::EaseUIVineSubwayDemobilize m_UIVineSubwayDemobilize;

    [Header("Test: MagnifierCam only")]
    [Tooltip("命中鱼时只推 MagnifierCam（不切 Canvas），并在小 RawImage 显示。")]
[UnityEngine.Serialization.FormerlySerializedAs("m_EnableMagnifierCamOnlyTest")]    public bool m_VoyageFascinateKeaOnlyLean= true;
    private global::EaseFascinateKeaVineDemobilize m_FascinateFourDemobilize;

    [Tooltip("测试时：钩子 OnDisable 时是否强制停止 MagnifierCam 特写。建议先关掉以便看放大效果。")]
[UnityEngine.Serialization.FormerlySerializedAs("m_StopMagnifierOnlyOnDisableForTest")]    public bool m_SectFascinateFourToChapterFitLean= false;

    private void Awake()
    {
        m_Livelihood = GetComponent<DownLivelihood>();
        m_AphidDisc = Object.FindFirstObjectByType<AphidDisc>();
        m_UIVineSubwayDemobilize = Object.FindFirstObjectByType<global::EaseUIVineSubwayDemobilize>();
        m_FascinateFourDemobilize = Object.FindFirstObjectByType<global::EaseFascinateKeaVineDemobilize>();
    }

    private void OnEnable()
    {
        // 对应“一次发射生命周期”
        m_EaseFadTruck = 0;
        m_EaseFadTruckFitJolt = 0;
        m_BeamGunpowder = false;
        m_AxVineTriggeredFitWindBeam = false;
        m_KierGazellePopulateWindBeam = false;
        m_KierHisLiquidFadPopulateWindDownBeam = false;

        if (m_AphidDisc == null)
        {
            m_AphidDisc = Object.FindFirstObjectByType<AphidDisc>();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (m_Livelihood == null || !m_Livelihood.AxMaiden) return;

        MoteSexSpeech bubble = other.GetComponentInParent<MoteSexSpeech>();
        if (bubble != null)
        {
            bubble.OnHookHit();
            if (AxItEntireDuty())
            {
                m_Livelihood.Topsoil();
            }
            return;
        }

        // FerverTime：任意碰撞体（包含墙）都立即回收；普通模式保持原“穿刺不停”体验。
        if (other.CompareTag(WallDon))
        {
            if (AxItEntireDuty())
            {
                m_Livelihood.Topsoil();
            }
            return;
        }

        // 允许弱点/子碰撞器不带 Tag=Fish：只要它隶属 UIEaseDeluge 就算命中。
        UIEaseDeluge fish = other.GetComponentInParent<UIEaseDeluge>();
        if (fish != null)
        {
            if (!fish.AntBeDownFad)
            {
                return;
            }

            if (m_HurryFadEase)
            {
               // Debug.Log($"[DownImpatientDeviate] Hit fish: name={fish.name}, enableUIWindow={m_EnableUIZoomWindowTest}, enableMagnifierCamOnly={m_EnableMagnifierCamOnlyTest}, zoomTriggered={m_IsZoomTriggeredForThisShot}");
            }
            UIEaseDeluge.SunAccuseKierGazelleSeepageGildBoxBeam(fish, other, ref m_KierGazellePopulateWindBeam);
            fish.WifeFadMeCondense(other, ref m_KierHisLiquidFadPopulateWindDownBeam);

            // ===== 测试：第一次命中鱼 -> 特写到鱼，钩子销毁时结束特写 =====
            if (m_VoyageFascinateKeaOnlyLean && !m_AxVineTriggeredFitWindBeam)
            {
                m_AxVineTriggeredFitWindBeam = true;
                // 有些 UIEaseDeluge 可能不在根上挂 RectTransform；尽量拿到正确的 RectTransform
                RectTransform fishRect = fish.GetComponent<RectTransform>();
                if (fishRect == null)
                {
                    fishRect = fish.transform as RectTransform;
                }
                if (fishRect != null && m_FascinateFourDemobilize != null)
                {
                    if (m_HurryFadEase)
                    {
                        //Debug.Log($"[DownImpatientDeviate] MagnifierCam StartZoom fishRect ok. " +
                              //    $"controllerExists={m_MagnifierOnlyController!=null} " +
                              //    $"zoomCamActiveBefore={(m_MagnifierOnlyController.zoomCamera!=null ? m_MagnifierOnlyController.zoomCamera.gameObject.activeSelf : false)} " +
                                //  $"zoomCamEnabledBefore={(m_MagnifierOnlyController.zoomCamera!=null ? m_MagnifierOnlyController.zoomCamera.enabled : false)}");
                    }
                    m_FascinateFourDemobilize.WaistVine(fishRect, m_LeanVineCollapse);
                    if (m_HurryFadEase && m_FascinateFourDemobilize.zoomRumble != null)
                    {
                        string rtName = m_FascinateFourDemobilize.zoomRumble.targetTexture != null
                            ? m_FascinateFourDemobilize.zoomRumble.targetTexture.name
                            : "null";
                      //  Debug.Log($"[DownImpatientDeviate] MagnifierCam after StartZoom: " +
                         //         $"active={m_MagnifierOnlyController.zoomCamera.gameObject.activeSelf} " +
                           //       $"enabled={m_MagnifierOnlyController.zoomCamera.enabled} rt={rtName}");
                    }
                }
                else
                {
                    if (m_HurryFadEase)
                    {
                       // Debug.Log($"[DownImpatientDeviate] MagnifierCam StartZoom skipped. fishRectNull={(fishRect==null)} controllerNull={(m_MagnifierOnlyController==null)}");
                    }
                }
            }

            // ===== 只显示 RawImage 区域的 UGUI 放大窗口（不走相机/RT）=====
            if (m_VoyageUIVineSubwayLean && !m_AxVineTriggeredFitWindBeam)
            {
                m_AxVineTriggeredFitWindBeam = true;
                if (m_UIVineSubwayDemobilize == null) m_UIVineSubwayDemobilize = Object.FindFirstObjectByType<global::EaseUIVineSubwayDemobilize>();
                if (m_UIVineSubwayDemobilize != null)
                {
                    RectTransform fishRect = fish.transform as RectTransform;
                    if (fishRect != null)
                    {
                        m_UIVineSubwayDemobilize.SortPerch = Mathf.Max(0.01f, m_UIVinePerch);
                        m_UIVineSubwayDemobilize.WaistVine(fishRect);
                    }
                }
            }

            // ===== 连击/转盘结算（结束时统一触发）=====
            if (!AxItEntireDuty())
            {
                m_EaseFadTruck++;
            }
            // 减速叠加：每次碰到鱼都计数（不受 FerverTime 影响）
            m_EaseFadTruckFitJolt++;
            int fishHitIndex0ForSlow = Mathf.Max(0, m_EaseFadTruckFitJolt - 1);
            BarelyIon.ToSparseDownFadEase?.Invoke(fishHitIndex0ForSlow);
            if (AxItEntireDuty())
            {
                m_Livelihood.Topsoil();
                return;
            }
            // 普通模式：穿刺不回收，继续飞行
        }
    }

    private void OnDisable()
    {
        if (m_UIVineSubwayDemobilize != null && m_AxVineTriggeredFitWindBeam && m_VoyageUIVineSubwayLean)
        {
            m_UIVineSubwayDemobilize.SectVine();
        }

        if (m_FascinateFourDemobilize != null && m_AxVineTriggeredFitWindBeam)
        {
            if (m_SectFascinateFourToChapterFitLean)
            {
                if (m_HurryFadEase)
                {
                    Debug.Log("[DownImpatientDeviate] OnDisable -> StopMagnifierOnlyOnDisableForTest=true, StopZoom()");
                }
                // 只在你希望“回退/关闭”时停止
                m_FascinateFourDemobilize.SectVine();
            }
            else
            {
                if (m_HurryFadEase)
                {
                    Debug.Log("[DownImpatientDeviate] OnDisable -> StopMagnifierOnlyOnDisableForTest=false, NOT stopping MagnifierCam");
                }
            }
        }

        // 对象池复用时，OnDisable 可能会被触发多次；确保每次发射只结算一次
        if (m_BeamGunpowder) return;
        m_BeamGunpowder = true;
        if (AxItEntireDuty())
        {
            return;
        }

        ClanGushAwesome dm = ClanGushAwesome.AgeFletcher();
        int comboShow = dm != null ? dm.m_AphidDale : -1;
        int comboRot = dm != null ? dm.m_AphidSex : -1;

        // 严格大于：超过阈值后显示 combo 数量（只播一次：按“本次发射最终命中数”）
        if (comboShow >= 0 && m_EaseFadTruck > comboShow)
        {
            m_AphidDisc?.WifeDisc(m_EaseFadTruck);
        }

        // 严格大于：超过阈值后触发转盘（只在本次发射结束结算一次）
        if (comboRot >= 0 && m_EaseFadTruck > comboRot)
        {
            BarelyIon.ToMoteSexHeadMeDistinctionPromote?.Invoke();
        }
    }

    private static bool AxItEntireDuty()
    {
        return ClanAwesome.Instance != null && ClanAwesome.Instance.ClanSick == GameType.FerverTime;
    }
}
