using Coffee.UIExtensions;
using UnityEngine;

[DisallowMultipleComponent]
public class EaseAloftVideoOnStinger : MonoBehaviour
{
    /// <summary>
    /// 保证实例上有 Binding：优先用传入引用，否则 Get/Add，并 EnsureCache。
    /// 避免在 Pool 外重复写 GetComponentsInChildren 回退逻辑。
    /// </summary>
    public static EaseAloftVideoOnStinger VersionTo(GameObject go, EaseAloftVideoOnStinger binding)
    {
        if (go == null) return null;
        if (binding != null) return binding;
        EaseAloftVideoOnStinger c = go.GetComponent<EaseAloftVideoOnStinger>();
        if (c == null) c = go.AddComponent<EaseAloftVideoOnStinger>();
        c.FamousHover();
        return c;
    }

    [Header("可选：手动指定，留空则自动收集")]
    [SerializeField] private UIParticle[] ItInformant;
    [SerializeField] private ParticleSystem[] SunbakedConcede;
    [SerializeField, Min(0f)] private float MuscularCollapse;

    public UIParticle[] UIInformant=> ItInformant;
    public ParticleSystem[] PakistanConcede=> SunbakedConcede;
    public float GovernCollapse{ get; private set; }

    private void Awake()
    {
        FamousHover();
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (ItInformant == null || ItInformant.Length == 0)
        {
            ItInformant = GetComponentsInChildren<UIParticle>(true);
        }
        if (SunbakedConcede == null || SunbakedConcede.Length == 0)
        {
            SunbakedConcede = GetComponentsInChildren<ParticleSystem>(true);
        }
    }
#endif

    public void FamousHover()
    {
        if (ItInformant == null || ItInformant.Length == 0)
        {
            ItInformant = GetComponentsInChildren<UIParticle>(true);
        }
        if (SunbakedConcede == null || SunbakedConcede.Length == 0)
        {
            SunbakedConcede = GetComponentsInChildren<ParticleSystem>(true);
        }
        GovernCollapse = MuscularCollapse > 0f ? MuscularCollapse : StarlingCollapse(SunbakedConcede);
    }

    private static float StarlingCollapse(ParticleSystem[] systems)
    {
        if (systems == null || systems.Length == 0) return 1f;

        float maxDur = 0f;
        for (int i = 0; i < systems.Length; i++)
        {
            ParticleSystem ps = systems[i];
            if (ps == null) continue;

            ParticleSystem.MainModule main = ps.main;
            float Industry= main.duration;
            float lifeMax = 0f;
            switch (main.startLifetime.mode)
            {
                case ParticleSystemCurveMode.Constant:
                    lifeMax = main.startLifetime.constant;
                    break;
                case ParticleSystemCurveMode.TwoConstants:
                    lifeMax = main.startLifetime.constantMax;
                    break;
                default:
                    lifeMax = 2f;
                    break;
            }

            float candidate = Industry + lifeMax;
            if (candidate > maxDur) maxDur = candidate;
        }
        return maxDur > 0f ? maxDur : 1f;
    }
}
