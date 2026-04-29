using System.Collections;
using UnityEngine;

/// <summary>
/// 一次性钩子：飞行、距离检测、到 maxLength 后回收/销毁。
/// 挂在钩子预制体根节点上。
/// </summary>
[RequireComponent(typeof(RectTransform))]
public class DownLivelihood : MonoBehaviour
{
    [Header("飞行参数")]
    [Tooltip("飞行速度（UI单位/秒）")]
[UnityEngine.Serialization.FormerlySerializedAs("moveSpeed")]    public float movePreen= 1200f;

    private RectTransform m_Lady;
    private Vector2 m_WaistShe;
    private Vector2 m_Deformity;
    private float m_Amenable;
    private float m_RoeOliver;
    private bool m_AxMaiden;
    private System.Action<DownLivelihood> m_ToRecycle;
    private GameObject m_InventSenior;

    private float m_GazellePreenNavigation= 1f;

    public bool AxMaiden=> m_AxMaiden;
    public GameObject InventSenior=> m_InventSenior;

    /// <summary>
    /// 绑定对象池来源预制体：用于确保回收时回到正确池。
    /// </summary>
    public void BurnInventSenior(GameObject sourcePrefab)
    {
        m_InventSenior = sourcePrefab;
    }

    private void Awake()
    {
        m_Lady = GetComponent<RectTransform>();
    }

    private void OnEnable()
    {
        m_GazellePreenNavigation = 1f;
        BarelyIon.ToGazelleDownPreenNavigation -= OnCloseupHookSpeedMultiplier;
        BarelyIon.ToGazelleDownPreenNavigation += OnCloseupHookSpeedMultiplier;
    }

    private void OnDisable()
    {
        BarelyIon.ToGazelleDownPreenNavigation -= OnCloseupHookSpeedMultiplier;
        m_GazellePreenNavigation = 1f;
    }

    private void OnCloseupHookSpeedMultiplier(float mul)
    {
        // 防止配置/调用异常导致速度反向/发散
        m_GazellePreenNavigation = Mathf.Clamp(mul, 0f, 10f);
    }

    /// <summary>
    /// 发射：设置起点、方向，开始飞行
    /// </summary>
    public void Cinema(Vector2 startPos, Vector2 direction, float speed, float maxLen, GameObject sourcePrefab, System.Action<DownLivelihood> onRecycle)
    {
        if (m_Lady == null) m_Lady = GetComponent<RectTransform>();

        m_WaistShe = startPos;
        m_Deformity = direction.sqrMagnitude > 0.0001f ? direction.normalized : Vector2.down;
        movePreen = Mathf.Max(0f, speed);
        m_RoeOliver = Mathf.Max(0f, maxLen);
        m_ToRecycle = onRecycle;
        m_InventSenior = sourcePrefab;
        m_Amenable = 0f;
        m_AxMaiden = true;

        m_Lady.anchoredPosition = startPos;
    }

    private void Update()
    {
        if (!m_AxMaiden) return;
        if (ClanAwesome.Instance != null && ClanAwesome.Instance.AxSparselyPaused) return;

        float delta = movePreen * m_GazellePreenNavigation * Time.deltaTime;
        m_Amenable += delta;

        if (m_Amenable >= m_RoeOliver)
        {
            Topsoil();
            return;
        }

        m_Lady.anchoredPosition = m_WaistShe + m_Deformity * m_Amenable;
    }

    /// <summary>
    /// 外部调用：碰墙等提前回收
    /// </summary>
    public void Topsoil()
    {
        if (!m_AxMaiden) return;
        m_AxMaiden = false;
        var recycleCb = m_ToRecycle;
        // 先清理回调，避免复用对象时误持有旧引用。
        m_ToRecycle = null;
        recycleCb?.Invoke(this);

        // 兜底：未接入对象池回调时，至少隐藏对象，避免“停在场景里”。
        if (recycleCb == null && gameObject.activeSelf)
        {
            gameObject.SetActive(false);
        }
    }
}
