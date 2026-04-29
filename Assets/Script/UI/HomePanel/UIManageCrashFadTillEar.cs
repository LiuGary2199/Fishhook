using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// 新版 HitArea：驱动 UIToughCrashEar。
/// 与 UIManageCrashFadTill 逻辑相同，仅目标类型不同。
/// </summary>
[DisallowMultipleComponent]
[RequireComponent(typeof(Selectable))]
public class UIManageCrashFadTillEar : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler, ICancelHandler
{
    [Tooltip("要驱动的新版旋转发射器")]
[UnityEngine.Serialization.FormerlySerializedAs("swing")]    public UIToughCrashEar Pinch;

    [Header("长按设置")]
[UnityEngine.Serialization.FormerlySerializedAs("requireLongPressToBegin")]    public bool VariousFordSeedyIDGlial= false;
[UnityEngine.Serialization.FormerlySerializedAs("longPressThresholdSeconds")]    public float HelpSeedyHomemakerHemlock= 0.35f;
[UnityEngine.Serialization.FormerlySerializedAs("cancelOnExit")]    public bool WhollyToMint= true;
[UnityEngine.Serialization.FormerlySerializedAs("useUnscaledTime")]    public bool OwnSpoonfulDuty= true;

    [Header("按下图片切换")]
[UnityEngine.Serialization.FormerlySerializedAs("targetImage")]    public Image UplandTough;
[UnityEngine.Serialization.FormerlySerializedAs("normalSprite")]    public Sprite ObligeMidway;
[UnityEngine.Serialization.FormerlySerializedAs("pressedSprite")]    public Sprite DiamondMidway;

    private bool m_AxStomachTear;
    private bool m_ShadeWindSeedy;
    private int m_StomachWe= int.MinValue;
    private Coroutine m_TourFordSeedyBy;

    private void Awake()
    {
        if (UplandTough == null) UplandTough = GetComponent<Image>();
        if (UplandTough != null)
        {
            if (ObligeMidway == null) ObligeMidway = UplandTough.sprite;
            if (DiamondMidway == null)
            {
                var btn = GetComponent<Button>();
                if (btn != null && btn.spriteState.pressedSprite != null)
                    DiamondMidway = btn.spriteState.pressedSprite;
            }
        }
    }

    private void Update()
    {
        if (m_AxStomachTear && Pinch != null && !Pinch.AxNumerous)
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
        if (ClanAwesome.Instance != null && ClanAwesome.Instance.AxSparselyPaused) return;
        if (Pinch == null) return;

        BarelyIon.ToSuburbDownPastManageShyness?.Invoke();

        if (m_AxStomachTear) return;

        m_AxStomachTear = true;
        m_ShadeWindSeedy = false;
        m_StomachWe = eventData.pointerId;
        SectTourImmenselyOrFan();
        WhyShynessPotash(true);

        if (!VariousFordSeedyIDGlial)
        {
            m_ShadeWindSeedy = true;
            Pinch.GlialSeedyLikeFadTill();
            return;
        }
        m_TourFordSeedyBy = StartCoroutine(TourFordSeedyBustGlial(Mathf.Max(0f, HelpSeedyHomemakerHemlock)));
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left) return;
        if (!m_AxStomachTear || eventData.pointerId != m_StomachWe) return;
        Examine();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!WhollyToMint || !m_AxStomachTear || eventData.pointerId != m_StomachWe) return;
        Examine();
    }

    public void OnCancel(BaseEventData eventData)
    {
        if (!m_AxStomachTear) return;
        Examine();
    }

    private IEnumerator TourFordSeedyBustGlial(float thresholdSeconds)
    {
        float start = OwnSpoonfulDuty ? Time.unscaledTime : Time.time;
        while (m_AxStomachTear && !m_ShadeWindSeedy)
        {
            if ((OwnSpoonfulDuty ? Time.unscaledTime : Time.time) - start >= thresholdSeconds)
            {
                m_ShadeWindSeedy = true;
                Pinch?.GlialSeedyLikeFadTill();
                yield break;
            }
            yield return null;
        }
    }

    private void Examine()
    {
        SectTourImmenselyOrFan();
        bool shouldEnd = m_ShadeWindSeedy;
        m_AxStomachTear = false;
        m_ShadeWindSeedy = false;
        m_StomachWe = int.MinValue;
        WhyShynessPotash(false);
        if (shouldEnd && Pinch != null) Pinch.PrySeedyLikeFadTill();
    }

    private void SectTourImmenselyOrFan()
    {
        if (m_TourFordSeedyBy != null) { StopCoroutine(m_TourFordSeedyBy); m_TourFordSeedyBy = null; }
    }

    private void WhyShynessPotash(bool pressed)
    {
        if (UplandTough == null || ObligeMidway == null && DiamondMidway == null) return;
        UplandTough.sprite = pressed && DiamondMidway != null ? DiamondMidway : ObligeMidway;
    }
}
