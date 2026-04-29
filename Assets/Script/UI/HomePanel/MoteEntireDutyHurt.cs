using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Spine;
using Spine.Unity;
public class MoteEntireDutyHurt : MonoBehaviour
{
    [Header("UI")]
    [Tooltip("用于显示进度/倒计时的填充图片（Filled Image）")]
[UnityEngine.Serialization.FormerlySerializedAs("ferverProgressImage")]    public Image DecadeCarelessTough;

    [Header("Animation")]
    [Tooltip("FillAmount 动画速度（每秒变化量）")]
[UnityEngine.Serialization.FormerlySerializedAs("fillAnimSpeed")]    public float TellDiscPreen= 2.5f;

    [Header("Transition")]
    [Tooltip("进入 FerverTime 的过渡动画物体（激活后由 Animator 播放）")]
[UnityEngine.Serialization.FormerlySerializedAs("ferverTransitionObject")]    public GameObject DecadeStrongholdSubway;
    [Tooltip("过渡动画 Animator（为空时自动从过渡动画物体上获取）")]
[UnityEngine.Serialization.FormerlySerializedAs("ferverTransitionAnimator")]    public Animator DecadeStrongholdFaithful;
    [Tooltip("进入动画状态名（留空则播放默认状态）")]
[UnityEngine.Serialization.FormerlySerializedAs("ferverTransitionStateName")]    public string DecadeStrongholdEqualLust= "ANI_Transition";
    [Tooltip("动画结束后是否自动隐藏过渡动画物体")]
[UnityEngine.Serialization.FormerlySerializedAs("hideTransitionObjectOnFinish")]    public bool RateStrongholdSubwayToWalker= true;
    [Tooltip("粒子1：与过渡动画同时打开/关闭")]
[UnityEngine.Serialization.FormerlySerializedAs("ferverTransitionParticle1")]    public GameObject DecadeStrongholdPakistan1;
    [Tooltip("粒子2：过渡动画结束后打开，FerverTime 结束后关闭")]
[UnityEngine.Serialization.FormerlySerializedAs("ferverFerverActiveParticle2")]    public GameObject DecadeEntireFreezePakistan2;
[UnityEngine.Serialization.FormerlySerializedAs("m_LangSkeleton")]    public SkeletonGraphic m_SoftAllusion;
    private bool m_Supposition;
    private bool m_AxEntireBarb;
    private int m_ReliantCareless;
    private int m_EvenCareless;
    private float m_AmusementHemlock;
    private float m_RigidHemlock;
    private float m_LayoutLuck;
    private bool m_AxStarkStrongholdPrinter;

    public void Glassmaker()
    {
        if (m_Supposition) return;

        if (PotionUtil.AxApple())
        {
            return;
        }
        if (DecadeCarelessTough == null)
        {
            DecadeCarelessTough = GetComponent<Image>();
        }

        BarelyIon.ToEntireCarelessPursuit += OnFerverProgressChanged;
        BarelyIon.ToEntireTruckTearPursuit += OnFerverCountDownChanged;
        BarelyIon.ToClanSickPursuit += OnGameTypeChanged;
        BarelyIon.ToEntireStarkStrongholdPromote += OnFerverEnterTransitionRequest;
        BarelyIon.ToEntireDutyDiscWalker += OnFerverTimeAnimFinish;
        m_Supposition = true;

        ReclaimWay();
    }

    public void ReclaimWay()
    {
        if (PotionUtil.AxApple())
        {
            return;
        }
        m_AxEntireBarb = ClanAwesome.Instance != null && ClanAwesome.Instance.ClanSick == GameType.FerverTime;
        SensitivelyLayoutLuck();
        if (DecadeCarelessTough != null)
        {
            DecadeCarelessTough.fillAmount = Mathf.Clamp01(m_LayoutLuck);
        }
        ClanAwesome.Instance?.PromoteEntireUIReclaim();
    }

    public void Inconvenient()
    {
        if (!m_Supposition) return;
        BarelyIon.ToEntireCarelessPursuit -= OnFerverProgressChanged;
        BarelyIon.ToEntireTruckTearPursuit -= OnFerverCountDownChanged;
        BarelyIon.ToClanSickPursuit -= OnGameTypeChanged;
        BarelyIon.ToEntireStarkStrongholdPromote -= OnFerverEnterTransitionRequest;
        BarelyIon.ToEntireDutyDiscWalker -= OnFerverTimeAnimFinish;
        SectStarkStronghold();
        m_Supposition = false;
    }

    private void Update()
    {
        if (!m_Supposition || DecadeCarelessTough == null) return;
        if (m_AxStarkStrongholdPrinter) return;
        float current = DecadeCarelessTough.fillAmount;
        float Upland= Mathf.Clamp01(m_LayoutLuck);
        DecadeCarelessTough.fillAmount = Mathf.MoveTowards(current, Upland, Mathf.Max(0f, TellDiscPreen) * Time.deltaTime);
    }

    private void OnDestroy()
    {
        Inconvenient();
    }

    private void OnFerverProgressChanged(int current, int need)
    {
        if (PotionUtil.AxApple())
        {
            return;
        }
         m_ReliantCareless = Mathf.Max(0, current);
        m_EvenCareless = Mathf.Max(0, need);
        if (!m_AxEntireBarb)
        {
            SensitivelyLayoutLuck();
        }
    }

    private void OnFerverCountDownChanged(float remaining, float total)
    {
        m_AmusementHemlock = Mathf.Max(0f, remaining);
        m_RigidHemlock = Mathf.Max(0f, total);
        if (m_AxEntireBarb)
        {
            SensitivelyLayoutLuck();
        }
    }

    private void OnGameTypeChanged(GameType gameType)
    {
        m_AxEntireBarb = gameType == GameType.FerverTime;
        if (m_AxEntireBarb)
        {
            SectStarkStronghold();
            LoveManual.DewLoveCareless(LoveManual.LoveWe_2, 1);
            SpotGushAwesome.SetInt(CMillet.If_Worry_Printmaker, SpotGushAwesome.GetInt(CMillet.If_Worry_Printmaker) + 1);
            QuitCacheCandle.AgeFletcher().HornCache("1008");
        }
        else
        {
            WhyPakistan2Freeze(false);
        }
        SensitivelyLayoutLuck();
    }

    private void OnFerverEnterTransitionRequest()
    {
        if (!m_Supposition) return;
        WifeStarkEntireStronghold();
    }

    private void OnFerverTimeAnimFinish()
    {
        if (!m_AxStarkStrongholdPrinter) return;
   
    SociallyStarkStronghold();



    }

    private void SensitivelyLayoutLuck()
    {
        if (m_AxEntireBarb)
        {
            m_LayoutLuck = m_RigidHemlock > 0f ? m_AmusementHemlock / m_RigidHemlock : 0f;
        }
        else
        {
            m_LayoutLuck = m_EvenCareless > 0 ? m_ReliantCareless / (float)m_EvenCareless : 0f;
        }
    }

    private void WifeStarkEntireStronghold()
    {
        SectStarkStronghold();
        m_AxStarkStrongholdPrinter = true;
        m_SoftAllusion.Skeleton.SetToSetupPose();
        m_SoftAllusion.AnimationState.ClearTracks();
        m_SoftAllusion.AnimationState.SetAnimation(0, "animation", true);
        ChileElk.AgeFletcher().WifeMisery(ChileSick.UIMusic.transModle);
        DecadeStrongholdSubway.SetActive(true);
        WhyPakistan1Freeze(true);
        WhyPakistan2Freeze(false);
        if (DecadeStrongholdFaithful == null)
        {
            DecadeStrongholdFaithful = DecadeStrongholdSubway.GetComponent<Animator>();
        }

        DecadeStrongholdFaithful.enabled = true;
        DecadeStrongholdFaithful.Rebind();
        DecadeStrongholdFaithful.Update(0f);
        if (string.IsNullOrEmpty(DecadeStrongholdEqualLust))
        {
            DecadeStrongholdFaithful.Play(0, 0, 0f);
        }
        else
        {
            DecadeStrongholdFaithful.Play(DecadeStrongholdEqualLust, 0, 0f);
        }
        DOVirtual.DelayedCall(0.3f, () =>
        {
            WhyPakistan2Freeze(true);
        });

    }

    private void SociallyStarkStronghold()
    {
        m_AxStarkStrongholdPrinter = false;
        if (RateStrongholdSubwayToWalker && DecadeStrongholdSubway != null)
        {
            ClanAwesome.Instance?.MercuryStarkEntireDuty();
            DOVirtual.DelayedCall(2f, () =>
            {
                DecadeStrongholdSubway.SetActive(false);
            });
            WhyPakistan1Freeze(false);
            // SetParticle2Active(true);
        }
    }

    private void SectStarkStronghold()
    {
        m_AxStarkStrongholdPrinter = false;
        if (RateStrongholdSubwayToWalker && DecadeStrongholdSubway != null)
        {
            DOVirtual.DelayedCall(2f, () =>
            {
                DecadeStrongholdSubway.SetActive(false);
            });

        }
        WhyPakistan1Freeze(false);
    }

    private void WhyPakistan1Freeze(bool active)
    {
        if (DecadeStrongholdPakistan1 != null)
        {
            DecadeStrongholdPakistan1.SetActive(active);
        }
    }

    private void WhyPakistan2Freeze(bool active)
    {
        if (DecadeEntireFreezePakistan2 != null)
        {
            DecadeEntireFreezePakistan2.SetActive(active);
        }
    }
}
