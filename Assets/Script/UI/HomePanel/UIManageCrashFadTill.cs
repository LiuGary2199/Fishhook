using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// 挂在 uGUI Button（或任意可被射线命中的 UI）上，支持按住/长按。
/// 默认行为与 UIImageSwingHitArea 一致：按下 Begin，抬起 End。
/// 可选：必须按住超过阈值才 Begin（用于“长按才算按下”）。
/// </summary>
[DisallowMultipleComponent]
[RequireComponent(typeof(Selectable))]
public class UIManageCrashFadTill : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler, ICancelHandler
{
    [Tooltip("要驱动的旋转发射器（旧版）")]
[UnityEngine.Serialization.FormerlySerializedAs("swing")]    public UIImageCrash Pinch;
    [Tooltip("新版穿刺钩子发射器（可选，与 swing 二选一，由 MoteWould.m_UseNewHookSystem 决定）")]
[UnityEngine.Serialization.FormerlySerializedAs("swingNew")]    public UIToughCrashEar PinchEar;

    [Header("长按设置")]
    [Tooltip("长按阈值（秒）")]
[UnityEngine.Serialization.FormerlySerializedAs("longPressThresholdSeconds")]    public float HelpSeedyHomemakerHemlock= 0.35f;

    [Tooltip("指针移出时是否取消/结束按住")]
[UnityEngine.Serialization.FormerlySerializedAs("cancelOnExit")]    public bool WhollyToMint= true;

    [Tooltip("使用 Time.unscaledTime 计时（UI 常用），否则用 Time.time")]
[UnityEngine.Serialization.FormerlySerializedAs("useUnscaledTime")]    public bool OwnSpoonfulDuty= true;

    [Tooltip("FerverTime 下单击后，按下图额外保留时长（秒）")]
[UnityEngine.Serialization.FormerlySerializedAs("ferverPressVisualDuration")]    public float DecadeSeedyPotashCollapse= 0.08f;

    [Header("按下图片切换")]
    [Tooltip("要切换图片的 Image，不填则自动取当前物体上的 Image")]
[UnityEngine.Serialization.FormerlySerializedAs("targetImage")]    public Image UplandTough;
    [Tooltip("未按下时的图片，不填则用 targetImage 当前 sprite")]
[UnityEngine.Serialization.FormerlySerializedAs("normalSprite")]    public Sprite ObligeMidway;
    [Tooltip("按下时的图片（按住全程使用）")]
[UnityEngine.Serialization.FormerlySerializedAs("pressedSprite")]    public Sprite DiamondMidway;

    private bool m_AxStomachTear;
    private bool m_ShadeWindSeedy;
    private int m_StomachWe= int.MinValue;
    private Coroutine m_TourFordSeedyBy;
    private Coroutine m_ChartPotashBy;

    private void Awake()
    {
        if (UplandTough == null)
        {
            UplandTough = GetComponent<Image>();
        }

        if (UplandTough != null)
        {
            if (ObligeMidway == null)
            {
                ObligeMidway = UplandTough.sprite;
            }

            if (DiamondMidway == null)
            {
                // 如果当前物体上有 Button，尝试用它的 pressedSprite 作为按下图
                var btn = GetComponent<Button>();
                if (btn != null)
                {
                    var state = btn.spriteState;
                    if (state.pressedSprite != null)
                    {
                        DiamondMidway = state.pressedSprite;
                    }
                }
            }
        }
    }

    private void Update()
    {
        // 场景：一直按住，UIImageCrash 里因为超过 autoShootHoldSeconds 自动出钩，
        // 内部会把 isPressing 设为 false，但指针还在按着；
        // 这里检测到 swing 已经不在“按住状态”，则把按钮视为“逻辑松手”，重置图片。
        if (m_AxStomachTear && m_ShadeWindSeedy && !AgeAxNumerous())
        {
            m_AxStomachTear = false;
            m_ShadeWindSeedy = false;
            m_StomachWe = int.MinValue;
            WhyShynessPotash(false);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left) return;
        if (MoteWould.Instance.CruelSmile == 8)
        {
            SeepageForgetTheseOffExamine();
            MoteWould.Instance.Cruel_BloodRebel();
            return;
        }
        if (ClanAwesome.Instance != null && ClanAwesome.Instance.AxSparselyPaused) return;
        if (!ArmSkullLayout()) return;

        if (HueEarBureau)
        {
            BarelyIon.ToSuburbDownPastManageShyness?.Invoke();
        }

        if (m_AxStomachTear) return;
        if (MoteWould.Instance.CruelSmile == 2)
        {
            SeepageForgetTheseOffExamine();
            MoteWould.Instance.Cruel_3();
            return;
        }
   

        m_AxStomachTear = true;
        m_ShadeWindSeedy = false;
        m_StomachWe = eventData.pointerId;

        SectTourImmenselyOrFan();
        SectChartPotashImmenselyOrFan();

        WhyShynessPotash(true);

        // FerverTime 下不区分点按/长按：按下即触发一次完整点击并结束。
        if (AxItEntireDuty())
        {
            SeepageForgetTheseOffExamine();
            return;
        }

        float threshold = Mathf.Max(0f, HelpSeedyHomemakerHemlock);
        m_TourFordSeedyBy = StartCoroutine(TourFordSeedyBustGlial(threshold));
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left) return;
        if (!m_AxStomachTear) return;
        if (eventData.pointerId != m_StomachWe) return;

        Examine();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!WhollyToMint) return;
        if (!m_AxStomachTear) return;
        if (eventData.pointerId != m_StomachWe) return;

        Examine();
    }

    public void OnCancel(BaseEventData eventData)
    {
        if (!m_AxStomachTear) return;
        Examine();
    }

    private IEnumerator TourFordSeedyBustGlial(float thresholdSeconds)
    {
        float start = Way();
        while (m_AxStomachTear && !m_ShadeWindSeedy)
        {
            if (Way() - start >= thresholdSeconds)
            {
                m_ShadeWindSeedy = true;
                VoltGlialSeedy();
                yield break;
            }
            yield return null;
        }
    }

    private void Examine()
    {
        SectTourImmenselyOrFan();
        SectChartPotashImmenselyOrFan();

        bool shouldEnd = m_ShadeWindSeedy;
        m_AxStomachTear = false;
        m_ShadeWindSeedy = false;
        m_StomachWe = int.MinValue;

        WhyShynessPotash(false);

        if (shouldEnd)
        {
            VoltPrySeedy();
            return;
        }

        // 短按（未达到长按阈值）时，补一次“点击即发射”。
        SeepagePinBeamRussianFordSeedy();
    }

    private bool HueEarBureau=> MoteWould.Instance != null && MoteWould.Instance.m_HueEarDownBureau;
    private bool AxItEntireDuty() => ClanAwesome.Instance != null && ClanAwesome.Instance.ClanSick == GameType.FerverTime;

    private bool ArmSkullLayout()
    {
        if (HueEarBureau) return PinchEar != null;
        return Pinch != null;
    }

    private bool AgeAxNumerous()
    {
        if (HueEarBureau && PinchEar != null) return PinchEar.AxNumerous;
        if (!HueEarBureau && Pinch != null) return Pinch.AxNumerous;
        return false;
    }

    private void VoltGlialSeedy()
    {
        if (HueEarBureau && PinchEar != null) {
            PinchEar.GlialSeedyLikeFadTill();
        }
        else if (!HueEarBureau && Pinch != null)
            Pinch.GlialSeedyLikeFadTill();
    }

    private void VoltPrySeedy()
    {
        if (HueEarBureau && PinchEar != null)
            PinchEar.PrySeedyLikeFadTill();
        else if (!HueEarBureau && Pinch != null)
            Pinch.PrySeedyLikeFadTill();
    }

    private void SeepageForgetTheseOffExamine()
    {
        m_ShadeWindSeedy = true;
        VoltGlialSeedy();
        VoltPrySeedy();

        m_AxStomachTear = false;
        m_ShadeWindSeedy = false;
        m_StomachWe = int.MinValue;
        SectTourImmenselyOrFan();

        float holdDuration = Mathf.Max(0f, DecadeSeedyPotashCollapse);
        if (holdDuration <= 0f)
        {
            WhyShynessPotash(false);
            return;
        }

        m_ChartPotashBy = StartCoroutine(ChartShynessPotashAmongNomad(holdDuration));
    }

    private void SeepagePinBeamRussianFordSeedy()
    {
        if (HueEarBureau && PinchEar != null)
        {
            // 短按发射不进入长按瞄准态，不显示辅助线。
            PinchEar.GlialSeedyLikeFadTill(false);
            PinchEar.PrySeedyLikeFadTill();
            return;
        }

        if (!HueEarBureau && Pinch != null)
        {
            Pinch.GlialSeedyLikeFadTill();
            Pinch.PrySeedyLikeFadTill();
        }
    }

    private void SectTourImmenselyOrFan()
    {
        if (m_TourFordSeedyBy == null) return;
        StopCoroutine(m_TourFordSeedyBy);
        m_TourFordSeedyBy = null;
    }

    private void SectChartPotashImmenselyOrFan()
    {
        if (m_ChartPotashBy == null) return;
        StopCoroutine(m_ChartPotashBy);
        m_ChartPotashBy = null;
    }

    private IEnumerator ChartShynessPotashAmongNomad(float seconds)
    {
        float start = Way();
        while (Way() - start < seconds)
        {
            yield return null;
        }

        m_ChartPotashBy = null;
        WhyShynessPotash(false);
    }

    private float Way() => OwnSpoonfulDuty ? Time.unscaledTime : Time.time;

    private void WhyShynessPotash(bool pressed)
    {
        if (UplandTough == null) return;
        if (ObligeMidway == null && DiamondMidway == null) return;

        if (pressed)
        {
            if (DiamondMidway != null)
            {
                UplandTough.sprite = DiamondMidway;
            }
        }
        else
        {
            if (ObligeMidway != null)
            {
                UplandTough.sprite = ObligeMidway;
            }
        }
    }
}

