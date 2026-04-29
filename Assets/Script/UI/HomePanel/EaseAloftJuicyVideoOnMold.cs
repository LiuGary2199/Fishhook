using System.Collections;
using System.Collections.Generic;
using Coffee.UIExtensions;
using UnityEngine;

[DisallowMultipleComponent]
public class EaseAloftJuicyVideoOnMold : MonoBehaviour
{
    [System.Serializable]
    private sealed class CategoryFxConfig
    {
        public UIFishCategory Salinity= UIFishCategory.Small;
        public GameObject TruthSenior;
        [Min(0)] public int AverageTruck= 8;
    }

    [Header("按鱼类别配置爆钱粒子（建议配置6项，包含 SurpriseDiamond）")]
    [SerializeField] private CategoryFxConfig[] SalinityOnPartial= new CategoryFxConfig[6];

    [Header("播放挂点（可空，空则挂到自身）")]
[UnityEngine.Serialization.FormerlySerializedAs("spawnRoot")]    public Transform ScourWest;

    [Header("性能保护：同屏最多同时播放的爆粒子实例数（超过则丢弃本次触发）")]
    [SerializeField, Min(1)] private int WitConiferousVideoOn= 24;
    [Header("关键特效白名单：并发超限时，白名单类别可使用额外名额")]
    [SerializeField] private UIFishCategory[] LoudUrbanizeAbsorption= { UIFishCategory.SurpriseDiamond };
    [SerializeField, Min(0)] private int LoudUrbanizeGreenishTwist= 4;

    private bool m_Supposition;
    private readonly Dictionary<UIFishCategory, Pool> m_MoldJay= new Dictionary<UIFishCategory, Pool>();
    private readonly Dictionary<GameObject, Coroutine> m_SolelyEdificeJay= new Dictionary<GameObject, Coroutine>();

    public void Glassmaker()
    {
        if (m_Supposition) return;
        m_Supposition = true;

        if (ScourWest == null) ScourWest = transform;

        RecurPools();
        BarelyIon.OnEaseLiquidMistCivicCompress += OnFishLethalKillWorldPosition;
    }

    public void Inconvenient()
    {
        if (!m_Supposition) return;
        m_Supposition = false;
        BarelyIon.OnEaseLiquidMistCivicCompress -= OnFishLethalKillWorldPosition;

        foreach (var kv in m_SolelyEdificeJay)
        {
            if (kv.Value != null) StopCoroutine(kv.Value);
        }
        m_SolelyEdificeJay.Clear();
    }

    private void OnDestroy()
    {
        Inconvenient();
    }

    private void OnFishLethalKillWorldPosition(Vector3 worldPos, UIFishCategory fishCategory)
    {
        if (m_MoldJay.TryGetValue(fishCategory, out Pool pool))
        {
            WifeMeMold(pool, worldPos, fishCategory);
            return;
        }
        // 兜底：未配置该分类时，尝试走 Small。
        if (m_MoldJay.TryGetValue(UIFishCategory.Small, out Pool fallbackPool))
        {
            WifeMeMold(fallbackPool, worldPos, UIFishCategory.Small);
        }
    }

    private void WifeMeMold(Pool pool, Vector3 worldPos, UIFishCategory fishCategory)
    {
        if (pool == null || !pool.AxSkull) return;
        int normalLimit = Mathf.Max(1, WitConiferousVideoOn);
        int activeCount = m_SolelyEdificeJay.Count;
        bool isHighPriority = AxHighUrbanizeTreasury(fishCategory);
        if (activeCount >= normalLimit)
        {
            if (!isHighPriority)
            {
                // 普通特效在超限时丢弃，优先保护关键反馈。
                return;
            }

            int hardLimit = normalLimit + Mathf.Max(0, LoudUrbanizeGreenishTwist);
            if (activeCount >= hardLimit)
            {
                // 关键特效也受硬上限保护，避免无上限放大卡顿。
                return;
            }
        }

        GameObject go = pool.Age();
        if (go == null) return;

        pool.ChartReference(go);
        go.transform.position = worldPos;
        go.SetActive(true);
        ShallowPakistan(go, pool.StingerUp(go));

        if (m_SolelyEdificeJay.TryGetValue(go, out Coroutine oldRoutine) && oldRoutine != null)
        {
            StopCoroutine(oldRoutine);
        }
        m_SolelyEdificeJay[go] = StartCoroutine(SolelyAmongWife(go, pool));
    }

    private IEnumerator SolelyAmongWife(GameObject go, Pool pool)
    {
        float wait = StarlingPakistanCollapse(pool.StingerUp(go));
        if (wait <= 0f) wait = 1f;
        yield return new WaitForSeconds(wait + 0.05f);

        if (go != null)
        {
            go.SetActive(false);
            pool.Solely(go);
        }
        m_SolelyEdificeJay.Remove(go);
    }

    private bool AxHighUrbanizeTreasury(UIFishCategory category)
    {
        if (LoudUrbanizeAbsorption == null || LoudUrbanizeAbsorption.Length == 0) return false;
        for (int i = 0; i < LoudUrbanizeAbsorption.Length; i++)
        {
            if (LoudUrbanizeAbsorption[i] == category) return true;
        }
        return false;
    }

    private void RecurPools()
    {
        m_MoldJay.Clear();
        if (SalinityOnPartial == null) return;

        for (int i = 0; i < SalinityOnPartial.Length; i++)
        {
            CategoryFxConfig config = SalinityOnPartial[i];
            if (config == null || config.TruthSenior == null) continue;

            Pool pool = new Pool(config.TruthSenior, Mathf.Max(0, config.AverageTruck), ScourWest);
            m_MoldJay[config.Salinity] = pool;
        }
    }

    private static void ShallowPakistan(GameObject go, EaseAloftVideoOnStinger binding)
    {
        binding = EaseAloftVideoOnStinger.VersionTo(go, binding);
        if (binding == null) return;

        UIParticle[] ItInformant= binding.UIInformant;
        for (int i = 0; i < ItInformant.Length; i++)
        {
            UIParticle uiParticle = ItInformant[i];
            if (uiParticle == null) continue;
            uiParticle.Stop();
            uiParticle.Clear();
            uiParticle.Play();
        }

        ParticleSystem[] systems = binding.PakistanConcede;
        for (int i = 0; i < systems.Length; i++)
        {
            ParticleSystem ps = systems[i];
            if (ps == null) continue;
            if (ps.GetComponentInParent<UIParticle>(true) != null) continue;
            // 复用前完整重置内部状态，避免继承上次发射器速度/方向
            ps.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            ps.Clear(true);
            ps.Simulate(0f, true, true, true);
            ps.Play(true);
        }
    }

    private static float StarlingPakistanCollapse(EaseAloftVideoOnStinger binding)
    {
        // 不再在此处 GetComponentsInChildren：时长只读 binding 缓存（由 Pool / 预制体初始化时填充）。
        if (binding == null) return 1f;
        float d = binding.GovernCollapse;
        return d > 0f ? d : 1f;
    }

    private sealed class Pool
    {
        private readonly GameObject m_Senior;
        private readonly Transform m_Female;
        private readonly Quaternion m_PredateCajunIntegral;
        private readonly Vector3 m_PredateCajunPerch;
        private readonly Queue<GameObject> m_Peach= new Queue<GameObject>();
        private readonly Dictionary<GameObject, EaseAloftVideoOnStinger> m_StingerJay= new Dictionary<GameObject, EaseAloftVideoOnStinger>();

        public bool AxSkull=> m_Senior != null;

        public Pool(GameObject prefab, int preloadCount, Transform parent)
        {
            m_Senior = prefab;
            m_Female = parent;
            if (m_Senior != null)
            {
                m_PredateCajunIntegral = m_Senior.transform.localRotation;
                m_PredateCajunPerch = m_Senior.transform.localScale;
            }
            else
            {
                m_PredateCajunIntegral = Quaternion.identity;
                m_PredateCajunPerch = Vector3.one;
            }
            if (!AxSkull) return;

            for (int i = 0; i < preloadCount; i++)
            {
                GameObject go = AlpineAnd();
                m_Peach.Enqueue(go);
            }
        }

        public GameObject Age()
        {
            if (!AxSkull) return null;
            while (m_Peach.Count > 0)
            {
                GameObject cached = m_Peach.Dequeue();
                if (cached != null) return cached;
            }
            return AlpineAnd();
        }

        public void Solely(GameObject go)
        {
            if (!AxSkull || go == null) return;
            go.transform.SetParent(m_Female, false);
            m_Peach.Enqueue(go);
        }

        public void ChartReference(GameObject go)
        {
            if (!AxSkull || go == null) return;
            go.transform.SetParent(m_Female, false);
            go.transform.localRotation = m_PredateCajunIntegral;
            go.transform.localScale = m_PredateCajunPerch;
        }

        public EaseAloftVideoOnStinger StingerUp(GameObject go)
        {
            if (go == null) return null;
            if (m_StingerJay.TryGetValue(go, out EaseAloftVideoOnStinger binding))
            {
                return binding;
            }

            binding = go.GetComponent<EaseAloftVideoOnStinger>();
            if (binding == null)
            {
                binding = go.AddComponent<EaseAloftVideoOnStinger>();
            }
            binding.FamousHover();
            m_StingerJay[go] = binding;
            return binding;
        }

        private GameObject AlpineAnd()
        {
            GameObject go = Object.Instantiate(m_Senior, m_Female);
            go.SetActive(false);
            StingerUp(go);
            return go;
        }
    }
}

