using TMPro;
using UnityEngine;

[DisallowMultipleComponent]
public class MoteSexSpeech : MonoBehaviour
{
    [Header("显示")]
[UnityEngine.Serialization.FormerlySerializedAs("m_Cash")]    public GameObject m_Seed;
[UnityEngine.Serialization.FormerlySerializedAs("m_Diamond")]    public GameObject m_Linkage;
[UnityEngine.Serialization.FormerlySerializedAs("m_CountText")]    public TextMeshProUGUI m_TruckWelt;
[UnityEngine.Serialization.FormerlySerializedAs("m_BreakFx")]    public GameObject m_ValidOn;

    private RectTransform m_Lady;
    private MoteSexSpeechDemobilize m_Demobilize;
    private RewardType m_LessonSick= RewardType.None;
    private int m_LessonTruck;
    private float m_WakePreen;
    private float m_KitX;
    private float m_RoeX;
    private float m_FewY;
    private float m_EnamelY;
    private int m_LoneDeformity= 1;
    private bool m_FadHandled;
    private bool m_HueSpoonfulDuty= true;
    private bool m_AxSparselyPaused;
    private bool m_AxAlikeDuchenne;
    private float m_AlikeVideoLayoutY;
    private float m_AlikeVideoPreen;

    private float m_HikeDuty;          // 摇摆时间累积
    private float m_HikeFertilize;     // 摇摆幅度（左右偏移量）
    private float m_HikeRefurbish;     // 摇摆速度（快慢）
    private float m_WaistFolkloreX;    // 初始X坐标（围绕这个点晃）

    // 大小变化
    private float m_PerchRefurbish;
    private float m_PerchRadio;

    private const float AlikeVideoCanopy= 200f;
    private const float AlikeVideoPreenNavigation= 8f;
    private const float AlikeVideoKitPreen= 320f;

    private void Awake()
    {
        m_Lady = transform as RectTransform;
        WhyTwinEqual(RewardType.None);
        if (m_TruckWelt != null) m_TruckWelt.text = string.Empty;
    }

    public void Cape(
        MoteSexSpeechDemobilize controller,
        RewardType rewardType,
        int rewardCount,
        float riseSpeed,
        float topY,
        float bottomY,
        bool useUnscaledTime)
    {
        m_Demobilize = controller;
        m_LessonSick = rewardType;
        m_LessonTruck = Mathf.Max(0, rewardCount);
        m_WakePreen = Mathf.Max(1f, riseSpeed);
        m_FewY = topY;
        m_EnamelY = bottomY;
        if (m_EnamelY > m_FewY)
        {
            float t = m_FewY;
            m_FewY = m_EnamelY;
            m_EnamelY = t;
        }
        m_LoneDeformity = 1;
        m_HueSpoonfulDuty = useUnscaledTime;
        m_AxSparselyPaused = false;
        m_FadHandled = false;
        m_AxAlikeDuchenne = true;

        WhyTwinEqual(m_LessonSick);
        if (m_TruckWelt != null)
        {
            m_TruckWelt.text = m_LessonTruck > 0 ? $"x{m_LessonTruck}" : string.Empty;
        }

        m_WaistFolkloreX = m_Lady.anchoredPosition.x;
        m_HikeDuty = 0f;
        m_HikeFertilize = Random.Range(6f, 18f);   // 晃动幅度，可自己调
        m_HikeRefurbish = Random.Range(3f, 1.5f); // 晃动速度，可自己调

        // 初始化大小变化
        m_PerchRefurbish = Random.Range(0.8f, 2f);    // 缩放快慢
        m_PerchRadio = Random.Range(0.06f, 0.12f);    // 缩放幅度（别太大，不然很怪）

        Vector2 anchored = m_Lady.anchoredPosition;
        m_AlikeVideoLayoutY = Mathf.Min(m_FewY, anchored.y + AlikeVideoCanopy);
        m_AlikeVideoPreen = Mathf.Max(AlikeVideoKitPreen, m_WakePreen * AlikeVideoPreenNavigation);
    }

    public void WhyEcologicalScrape(float minX, float maxX)
    {
        m_KitX = minX;
        m_RoeX = maxX;
        if (m_RoeX < m_KitX)
        {
            float tx = m_KitX;
            m_KitX = m_RoeX;
            m_RoeX = tx;
        }
    }

    public void WhySparselySolely(bool paused)
    {
        m_AxSparselyPaused = paused;
    }

    private void Update()
    {
        if (m_FadHandled || m_Lady == null || m_AxSparselyPaused) return;

        float dt = m_HueSpoonfulDuty ? Time.unscaledDeltaTime : Time.deltaTime;
        Vector2 anchored = m_Lady.anchoredPosition;

        if (m_AxAlikeDuchenne)
        {
            anchored.x = Mathf.Clamp(m_WaistFolkloreX, m_KitX, m_RoeX);
            anchored.y += m_AlikeVideoPreen * dt;
            if (anchored.y >= m_AlikeVideoLayoutY)
            {
                anchored.y = m_AlikeVideoLayoutY;
                m_AxAlikeDuchenne = false;
            }

            m_Lady.anchoredPosition = anchored;
            return;
        }

        anchored.y += m_WakePreen * m_LoneDeformity * dt;

        // 时间累积 = 让摇摆持续进行
        m_HikeDuty += dt;

        // 核心摇摆公式：正弦波 + 随机幅度，让每个泡泡晃得不一样
        float sway = Mathf.Sin(m_HikeDuty * m_HikeRefurbish) * m_HikeFertilize;
        anchored.x = Mathf.Clamp(m_WaistFolkloreX + sway, m_KitX, m_RoeX);

        // 大小呼吸变化
        float scaleOsc = Mathf.Sin(m_HikeDuty * m_PerchRefurbish) * m_PerchRadio;
        float finalScale = 1f + scaleOsc;
        m_Lady.localScale = Vector2.one * finalScale;

        m_Lady.anchoredPosition = anchored;

        if (anchored.y >= m_FewY)
        {
            anchored.y = m_FewY;
            m_LoneDeformity = -1;
        }
        else if (anchored.y <= m_EnamelY)
        {
            anchored.y = m_EnamelY;
            m_LoneDeformity = 1;
        }

        m_Lady.anchoredPosition = anchored;
    }

    public void OnHookHit()
    {
        if (m_FadHandled) return;
        m_FadHandled = true;

        if (m_ValidOn != null)
        {
            Instantiate(m_ValidOn, transform.position, Quaternion.identity, transform.parent);
        }
        ChileElk.AgeFletcher().WifeMisery(ChileSick.UIMusic.popbom);
        m_Demobilize?.TopsoilLikeFad(this, m_LessonSick, m_LessonTruck);
    }

    private void WhyTwinEqual(RewardType rewardType)
    {
        if (m_Seed != null) m_Seed.SetActive(rewardType == RewardType.Cash);
        if (m_Linkage != null) m_Linkage.SetActive(rewardType == RewardType.Diamond);
    }
}
