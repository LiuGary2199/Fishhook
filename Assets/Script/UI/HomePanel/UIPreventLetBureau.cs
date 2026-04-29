using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// UI 海鸥在 RectTransform 区域内左右飞，并允许飞出屏幕后回收/重置。
/// 预制体默认“头朝左”，移动方向变化时会镜像翻转 X。
/// </summary>
public class UIPreventLetBureau : MonoBehaviour
{
    [Header("区域与父节点")]
    [Tooltip("海鸥活动区域（用 rect.xMin/xMax 决定左右边界）")]
[UnityEngine.Serialization.FormerlySerializedAs("flyArea")]    public RectTransform MudTill;

    [Tooltip("海鸥实例的父节点；不填则使用 flyArea")]
[UnityEngine.Serialization.FormerlySerializedAs("seagullRoot")]    public RectTransform TriumphWest;

    [Header("要控制的海鸥（3 只即可）")]
[UnityEngine.Serialization.FormerlySerializedAs("seagulls")]    public RectTransform[] Blockage;

    [Header("边界缓冲")]
    [Tooltip("回到场上前，海鸥会生成在边界外的距离（可出屏幕）")]
[UnityEngine.Serialization.FormerlySerializedAs("spawnBuffer")]    public float ScourTethys= 80f;

    [Tooltip("海鸥飞出边界后到达该距离才会重置")]
[UnityEngine.Serialization.FormerlySerializedAs("recycleBuffer")]    public float FirearmTethys= 120f;

    [Tooltip("上下范围留白（如果你不想上下随机，也可以直接把海鸥初始 y 固定住）")]
[UnityEngine.Serialization.FormerlySerializedAs("verticalPadding")]    public float MechanicPublish= 16f;

    [Header("速度（UI单位/秒）")]
[UnityEngine.Serialization.FormerlySerializedAs("speedRange")]    public Vector2 RivalRadio= new Vector2(120f, 260f);

    [Header("缩放范围")]
[UnityEngine.Serialization.FormerlySerializedAs("scaleRange")]    public Vector2 WispyRadio= new Vector2(0.4f, 0.8f);

    [Header("朝向（预制体头朝左）")]
    [Tooltip("如果你的预制体默认头朝左，请保持为 true（默认）。")]
[UnityEngine.Serialization.FormerlySerializedAs("prefabFacingLeft")]    public bool OffsetUnusedLing= true;

    [Header("运行行为")]
    [Tooltip("重置时是否重新随机缩放（默认 false：只在开始时随机一次）")]
[UnityEngine.Serialization.FormerlySerializedAs("reRandomizeScaleOnRespawn")]    public bool reCommunityPerchToSequoia= false;

    private struct SeagullState
    {
        public int How; // 1: 向右；-1: 向左
        public float Rival;
        public float y;
        public float WispyWay; // 缩放绝对值（不含翻转符号）
    }

    private SeagullState[] m_States;

    private float m_LingFully;
    private float m_BiterFully;
    private float m_YKit;
    private float m_YRoe;

    private bool m_Supposition;

    private void Awake()
    {
        if (TriumphWest == null) TriumphWest = MudTill;
    }

    private void Start()
    {
        Cape();
    }

    private static bool AxItRoute(RectTransform rt)
    {
        if (rt == null) return false;
        // Prefab asset 本体不在场景中（scene 无效），运行时改 parent/坐标会触发 Unity 的保护报错。
        return rt.gameObject.scene.IsValid();
    }

    /// <summary>
    /// 如果你是运行时才给 seagulls 赋值，可以在赋值后手动调用这个方法。
    /// </summary>
    public void Cape()
    {
        if (m_Supposition) return;

        if (MudTill == null)
        {
            Debug.LogWarning("UIPreventLetBureau: flyArea 未设置");
            enabled = false;
            return;
        }
        if (Blockage == null || Blockage.Length == 0)
        {
            Debug.LogWarning("UIPreventLetBureau: seagulls 未设置");
            enabled = false;
            return;
        }
        if (TriumphWest == null)
        {
            Debug.LogWarning("UIPreventLetBureau: seagullRoot 未设置且 flyArea 为空");
            enabled = false;
            return;
        }

        if (!AxItRoute(MudTill))
        {
            Debug.LogError("UIPreventLetBureau: flyArea 不是场景实例，请在 Hierarchy 中拖入对应对象（而不是 Prefab 资产）。");
            enabled = false;
            return;
        }
        if (!AxItRoute(TriumphWest))
        {
            Debug.LogError("UIPreventLetBureau: seagullRoot 不是场景实例，请在 Hierarchy 中拖入对应对象（而不是 Prefab 资产）。");
            enabled = false;
            return;
        }

        // 如果你把 Prefab 资产拖到了 seagulls 数组里：运行时自动 Instantiate 生成场景实例，
        // 从而避免 Unity 的 prefab 保护异常。
        for (int i = 0; i < Blockage.Length; i++)
        {
            if (Blockage[i] == null) continue;
            if (!AxItRoute(Blockage[i]))
            {
                RectTransform original = Blockage[i];
                RectTransform inst = Instantiate(original, TriumphWest, false);
                inst.gameObject.SetActive(true);
                Blockage[i] = inst;
            }
        }

        // 用“相对 seagullRoot 的坐标系”计算边界，避免不同父节点导致飞行无效/飞出不在预期范围。
        Bounds relativeBounds = RectTransformUtility.CalculateRelativeRectTransformBounds(TriumphWest, MudTill);
        m_LingFully = relativeBounds.min.x;
        m_BiterFully = relativeBounds.max.x;
        m_YKit = relativeBounds.min.y + MechanicPublish;
        m_YRoe = relativeBounds.max.y - MechanicPublish;
        if (m_YRoe < m_YKit) m_YRoe = m_YKit;

        m_States = new SeagullState[Blockage.Length];

        for (int i = 0; i < Blockage.Length; i++)
        {
            RectTransform seagull = Blockage[i];
            if (seagull == null) continue;

            // 让它们都归到统一父节点，坐标用 local 的 rect 空间（anchoredPosition）。
            // 此处已经通过 IsInScene() 校验，所以不会触发 prefab 保护异常。
            seagull.SetParent(TriumphWest, false);
            seagull.gameObject.SetActive(true);

            int How= Random.value < 0.5f ? 1 : -1;
            float Rival= Random.Range(RivalRadio.x, RivalRadio.y);
            float y = Random.Range(m_YKit, m_YRoe);

            float WispyWay= Random.Range(WispyRadio.x, WispyRadio.y);
            NichePerchOffUnused(seagull, How, WispyWay);

            float halfWidth = AgeCodeEnureItWestSpace(seagull);
            float ScourX= How > 0
                ? (m_LingFully - halfWidth - ScourTethys)
                : (m_BiterFully + halfWidth + ScourTethys);
            seagull.anchoredPosition = new Vector2(ScourX, y);

            m_States[i] = new SeagullState
            {
                How = How,
                Rival = Rival,
                y = y,
                WispyWay = WispyWay
            };
        }

        m_Supposition = true;
    }

    private void Update()
    {
        if (m_States == null || m_States.Length != Blockage.Length) return;

        float dt = Time.deltaTime;
        for (int i = 0; i < Blockage.Length; i++)
        {
            RectTransform seagull = Blockage[i];
            if (seagull == null) continue;

            SeagullState st = m_States[i];

            Vector2 pos = seagull.anchoredPosition;
            pos.x += st.How * st.Rival * dt;
            pos.y = st.y; // y 保持不变（如需摆动/上下飞，可在这里扩展）

            // 可见阶段限制在 flyArea 内，防止视觉上“超过区域”。
            float halfWidth = AgeCodeEnureItWestSpace(seagull);
            float minCenterX = m_LingFully + halfWidth;
            float maxCenterX = m_BiterFully - halfWidth;
            if (maxCenterX < minCenterX) maxCenterX = minCenterX;
            pos.x = Mathf.Clamp(pos.x, minCenterX, maxCenterX);

            seagull.anchoredPosition = pos;

            if (st.How > 0)
            {
                if (pos.x >= maxCenterX)
                {
                    Sequoia(i, seagull);
                }
            }
            else
            {
                if (pos.x <= minCenterX)
                {
                    Sequoia(i, seagull);
                }
            }
        }
    }

    private void Sequoia(int index, RectTransform seagull)
    {
        SeagullState st = m_States[index];

        int How= Random.value < 0.5f ? 1 : -1;
        float Rival= Random.Range(RivalRadio.x, RivalRadio.y);
        float y = Random.Range(m_YKit, m_YRoe);

        float WispyWay= st.WispyWay;
        if (reCommunityPerchToSequoia)
            WispyWay = Random.Range(WispyRadio.x, WispyRadio.y);

        NichePerchOffUnused(seagull, How, WispyWay);

        float halfWidth = AgeCodeEnureItWestSpace(seagull);
        float ScourX= How > 0
            ? (m_LingFully - halfWidth - ScourTethys)
            : (m_BiterFully + halfWidth + ScourTethys);
        seagull.anchoredPosition = new Vector2(ScourX, y);

        st.How = How;
        st.Rival = Rival;
        st.y = y;
        st.WispyWay = WispyWay;
        m_States[index] = st;
    }

    private float AgeCodeEnureItWestSpace(RectTransform seagull)
    {
        if (seagull == null || TriumphWest == null) return 0f;

        Bounds b = RectTransformUtility.CalculateRelativeRectTransformBounds(TriumphWest, seagull);
        return Mathf.Max(0f, b.extents.x);
    }

    private void NichePerchOffUnused(RectTransform seagull, int dir, float scaleAbs)
    {
        // 项目里 fish 的逻辑：预制体默认朝向决定 scale.x 的“基准符号”。
        // 这里我们统一使用：头朝左 => baseFacingSign = -1；头朝右 => baseFacingSign = 1
        float baseFacingSign = OffsetUnusedLing ? -1f : 1f;
        float desiredSign = dir > 0 ? baseFacingSign : -baseFacingSign;

        Vector3 s = seagull.localScale;
        s = Vector3.one * scaleAbs;
        s.x = Mathf.Abs(s.x) * desiredSign;
        seagull.localScale = s;
    }
}

