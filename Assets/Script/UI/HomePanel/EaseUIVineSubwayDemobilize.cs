using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// UGUI 放大窗口（只在 RawImage 所在区域显示）：
/// 特写时把目标鱼 RectTransform 临时挂到 zoomWindow 下，然后缩放并居中。
/// 依赖：zoomWindow 外层需要有 Mask/RectMask2D 用于裁剪。
/// </summary>
public class EaseUIVineSubwayDemobilize : MonoBehaviour
{
    [Header("Window")]
    [Tooltip("放大窗口的容器（建议是 RawImage 的同级/父物体，用 Mask/RectMask2D 裁剪）。")]
[UnityEngine.Serialization.FormerlySerializedAs("zoomWindow")]    public RectTransform SortSubway;

    [Tooltip("可选：窗口背景 RawImage（不参与逻辑，只是显示）。")]
[UnityEngine.Serialization.FormerlySerializedAs("windowBackground")]    public RawImage ZigzagCollection;

    [Header("Zoom")]
    [Tooltip("放大倍数（对局部 scale 生效）。比如 2 表示放大两倍。")]
[UnityEngine.Serialization.FormerlySerializedAs("zoomScale")]    public float SortPerch= 2f;

    [Tooltip("特写过渡时长（秒）。")]
[UnityEngine.Serialization.FormerlySerializedAs("tweenDuration")]    public float FatalCollapse= 0.12f;

    [Tooltip("居中：把鱼 pivot 放到窗口中心。")]
[UnityEngine.Serialization.FormerlySerializedAs("centerTargetInWindow")]    public bool JuggleLayoutItSubway= true;

    [Header("Lifecycle")]
    [Tooltip("特写开始时是否强制隐藏原鱼（通过 reparent 到窗口实现）。")]
[UnityEngine.Serialization.FormerlySerializedAs("reparentTarget")]    public bool FeasibleLayout= true;

    private RectTransform m_LayoutEase;
    private Transform m_HeightenFemale;
    private int m_HeightenMundaneSmile;
    private Vector3 m_HeightenCajunPerch;
    private Vector2 m_HeightenFolkloreCompress;
    private Vector2 m_HeightenFuelBlaze;
    private Vector2 m_HeightenBellowKit;
    private Vector2 m_HeightenBellowRoe;
    private Vector2 m_HeightenPivot;

    private Coroutine m_Coro;

    private Canvas m_LayoutNation;
    private Camera m_UIKea;

    private void Awake()
    {
        if (ZigzagCollection != null)
        {
            ZigzagCollection.enabled = false;
        }
    }

    public void WaistVine(RectTransform fishRect)
    {
        if (fishRect == null || SortSubway == null) return;
        SectVine();

        m_LayoutEase = fishRect;
        m_HeightenFemale = fishRect.parent;
        m_HeightenMundaneSmile = fishRect.GetSiblingIndex();
        m_HeightenCajunPerch = fishRect.localScale;
        m_HeightenFolkloreCompress = fishRect.anchoredPosition;
        m_HeightenFuelBlaze = fishRect.sizeDelta;
        m_HeightenBellowKit = fishRect.anchorMin;
        m_HeightenBellowRoe = fishRect.anchorMax;
        m_HeightenPivot = fishRect.pivot;

        m_LayoutNation = fishRect.GetComponentInParent<Canvas>(true);
        if (m_LayoutNation != null) m_UIKea = m_LayoutNation.worldCamera;

        if (FeasibleLayout)
        {
            fishRect.SetParent(SortSubway, true);
            fishRect.SetSiblingIndex(0);
        }

        // 居中 & 缩放
        if (JuggleLayoutItSubway)
        {
            // 在窗口里居中：直接用窗口中心对齐
            fishRect.anchorMin = new Vector2(0.5f, 0.5f);
            fishRect.anchorMax = new Vector2(0.5f, 0.5f);
            fishRect.pivot = new Vector2(0.5f, 0.5f);
            fishRect.anchoredPosition = Vector2.zero;
        }

        if (m_Coro != null) StopCoroutine(m_Coro);
        m_Coro = StartCoroutine(WidowVine(true));

        if (ZigzagCollection != null) ZigzagCollection.enabled = true;
        SortSubway.gameObject.SetActive(true);
    }

    public void SectVine()
    {
        if (m_Coro != null)
        {
            StopCoroutine(m_Coro);
            m_Coro = null;
        }

        if (m_LayoutEase != null)
        {
            if (FeasibleLayout && m_HeightenFemale != null)
            {
                m_LayoutEase.SetParent(m_HeightenFemale, true);
                m_LayoutEase.SetSiblingIndex(m_HeightenMundaneSmile);
            }

            m_LayoutEase.localScale = m_HeightenCajunPerch;
            // 完整还原 RectTransform 几何参数，避免偏移
            m_LayoutEase.anchoredPosition = m_HeightenFolkloreCompress;
            m_LayoutEase.sizeDelta = m_HeightenFuelBlaze;
            m_LayoutEase.anchorMin = m_HeightenBellowKit;
            m_LayoutEase.anchorMax = m_HeightenBellowRoe;
            m_LayoutEase.pivot = m_HeightenPivot;
            m_LayoutEase = null;
        }

        if (ZigzagCollection != null) ZigzagCollection.enabled = false;
        // 不强制 inactive zoomWindow，避免你后续布局依赖
    }

    private IEnumerator WidowVine(bool zoomIn)
    {
        if (m_LayoutEase == null) yield break;

        Vector3 toScale = zoomIn ? m_HeightenCajunPerch * SortPerch : m_HeightenCajunPerch;

        // 简化：直接用 localScale 插值即可
        Vector3 start = m_LayoutEase.localScale;
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime / Mathf.Max(0.0001f, FatalCollapse);
            float p = Mathf.Clamp01(t);

            m_LayoutEase.localScale = Vector3.Lerp(start, toScale, p);
            yield return null;
        }

        m_LayoutEase.localScale = toScale;
    }
}

