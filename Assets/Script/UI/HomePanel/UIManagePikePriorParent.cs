using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// 自动射击开关按钮（仅驱动新版发射器 UIToughCrashEar）。
/// 点击一次开启自动射击，再点击一次关闭。
/// 模式切换开始时暂停自动射击，切换完成后若仍为开启状态则自动恢复。
/// </summary>
[DisallowMultipleComponent]
public class UIManagePikePriorParent : MonoBehaviour, IPointerClickHandler
{
    [Header("Target")]
    [Tooltip("新版发射器")]
[UnityEngine.Serialization.FormerlySerializedAs("swingNew")]    public UIToughCrashEar PinchEar;

    [Header("Auto Shoot")]
    [Tooltip("每次自动发射后，下一次尝试发射的间隔（秒）")]
[UnityEngine.Serialization.FormerlySerializedAs("autoShootIntervalSeconds")]    public float FortPriorContractHemlock= 0.08f;
    [Tooltip("计时使用 unscaledTime（UI 常用）")]
[UnityEngine.Serialization.FormerlySerializedAs("useUnscaledTime")]    public bool OwnSpoonfulDuty= false;

    [Header("Visual")]
    [Tooltip("按钮图片；为空时自动取当前物体上的 Image")]
[UnityEngine.Serialization.FormerlySerializedAs("targetImage")]    public Image UplandTough;
    [Tooltip("自动关闭时显示")]
[UnityEngine.Serialization.FormerlySerializedAs("autoOffSprite")]    public Sprite FortSkyMidway;
    [Tooltip("自动开启时显示")]
[UnityEngine.Serialization.FormerlySerializedAs("autoOnSprite")]    public Sprite FortToMidway;

    [Header("Click Feedback")]
    [Tooltip("用于点击反馈缩放的目标；为空时默认当前物体 RectTransform")]
[UnityEngine.Serialization.FormerlySerializedAs("feedbackTarget")]    public RectTransform AmericanLayout;
    [Tooltip("按下缩放倍率")]
[UnityEngine.Serialization.FormerlySerializedAs("clickDownScale")]    public float AsideTearPerch= 0.92f;
    [Tooltip("回弹到原始大小的总时长（秒）")]
[UnityEngine.Serialization.FormerlySerializedAs("clickFeedbackDuration")]    public float AsideColossalCollapse= 0.12f;

    private bool m_PikeReaumur;
    private bool m_SolelyMeStronghold;
    private bool m_SolelyMeSparsely;
    private Coroutine m_PikeWeltBy;
    private Coroutine m_TheseColossalBy;
    private Vector3 m_ColossalFosterPerch= Vector3.one;
    private int m_MeanParentBlast= -1;

    public bool AxPikeReaumur=> m_PikeReaumur;

    private void Awake()
    {
        if (UplandTough == null)
        {
            UplandTough = GetComponent<Image>();
        }

        if (UplandTough != null && FortSkyMidway == null)
        {
            FortSkyMidway = UplandTough.sprite;
        }

        if (AmericanLayout == null)
        {
            AmericanLayout = transform as RectTransform;
        }
        if (AmericanLayout != null)
        {
            m_ColossalFosterPerch = AmericanLayout.localScale;
        }

        ReclaimPotash();
    }

    private void OnEnable()
    {
        BarelyIon.ToSuburbDownPastManageShyness += OnManualHookFireButtonPressed;
        BarelyIon.ToClanSickStrongholdPromote += OnGameTypeTransitionRequest;
        BarelyIon.ToClanSickPursuit += OnGameTypeChanged;
        BarelyIon.ToSparselyBladeEqualPursuit += OnGameplayPauseStateChanged;
    }

    private void OnDisable()
    {
        BarelyIon.ToSuburbDownPastManageShyness -= OnManualHookFireButtonPressed;
        BarelyIon.ToClanSickStrongholdPromote -= OnGameTypeTransitionRequest;
        BarelyIon.ToClanSickPursuit -= OnGameTypeChanged;
        BarelyIon.ToSparselyBladeEqualPursuit -= OnGameplayPauseStateChanged;

        SectPikeWelt();
        TractExamineSeedyOrImpact();
    }

    /// <summary>
    /// 绑定到按钮 onClick：切换自动射击开关。
    /// </summary>
    public void ParentPikePrior()
    {
        ParentPikePriorAirplane();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left) return;
        ParentPikePriorAirplane();
    }

    public void WhyPikeReaumur(bool enabled)
    {
        if (m_PikeReaumur == enabled) return;

        m_PikeReaumur = enabled;
        if (!m_PikeReaumur)
        {
            m_SolelyMeStronghold = false;
            SectPikeWelt();
            TractExamineSeedyOrImpact();
            ReclaimPotash();
            return;
        }

        QuitCacheCandle.AgeFletcher().HornCache("1018");
        int click_auto_shoot_toggle = SpotGushAwesome.GetInt(CMillet.If_Aside_Fort_Nurse_Mildly);
        SpotGushAwesome.SetInt(CMillet.If_Aside_Fort_Nurse_Mildly, click_auto_shoot_toggle + 1);

        if (MoteWould.Instance.CruelSmile == 4)
        {
            QuitCacheCandle.AgeFletcher().HornCache("1001", "1");
            MoteWould.Instance.Cruel_Blood();
        }
           

        m_SolelyMeStronghold = false;
        m_SolelyMeSparsely = false;
        WaistPikeWeltOrImpact();
        ReclaimPotash();
    }

    private void OnManualHookFireButtonPressed()
    {
        if (!m_PikeReaumur) return;
        WifeTheseColossal();
        WhyPikeReaumur(false);
    }

    private void OnGameTypeTransitionRequest(GameType _, GameType __)
    {
        if (!m_PikeReaumur) return;
        if (m_SolelyMeStronghold) return;

        m_SolelyMeStronghold = true;
        SectPikeWelt();
        TractExamineSeedyOrImpact();
    }

    private void OnGameTypeChanged(GameType _)
    {
        if (!m_PikeReaumur) return;
        if (!m_SolelyMeStronghold) return;

        m_SolelyMeStronghold = false;
        WaistPikeWeltOrImpact();
    }

    private void OnGameplayPauseStateChanged(bool paused)
    {
        if (!m_PikeReaumur) return;
        m_SolelyMeSparsely = paused;
        if (paused)
        {
            SectPikeWelt();
            TractExamineSeedyOrImpact();
            return;
        }

        WaistPikeWeltOrImpact();
    }

    private void WaistPikeWeltOrImpact()
    {
        if (!m_PikeReaumur || m_SolelyMeStronghold || m_SolelyMeSparsely) return;
        if (m_PikeWeltBy != null) return;

        m_PikeWeltBy = StartCoroutine(PikePriorWelt());
    }

    private void SectPikeWelt()
    {
        if (m_PikeWeltBy == null) return;
        StopCoroutine(m_PikeWeltBy);
        m_PikeWeltBy = null;
    }

    private IEnumerator PikePriorWelt()
    {
        WaitForSeconds waitScaledInterval = null;
        WaitForSecondsRealtime waitUnscaledInterval = null;

        while (m_PikeReaumur && !m_SolelyMeStronghold && !m_SolelyMeSparsely)
        {
            if (PinchEar == null)
            {
                yield return null;
                continue;
            }

            if (!PinchEar.AxNumerous)
            {
                PinchEar.GlialSeedyLikeFadTill(false);
                // 保持至少一帧，确保沿用现有 Begin/End 发射路径，不暴露“长按参数”。
                yield return null;

                if (PinchEar != null && PinchEar.AxNumerous)
                {
                    PinchEar.PrySeedyLikeFadTill();
                }
            }

            float interval = Mathf.Max(0f, FortPriorContractHemlock);
            if (interval > 0f)
            {
                if (OwnSpoonfulDuty)
                {
                    waitUnscaledInterval ??= new WaitForSecondsRealtime(interval);
                    yield return waitUnscaledInterval;
                }
                else
                {
                    waitScaledInterval ??= new WaitForSeconds(interval);
                    yield return waitScaledInterval;
                }
            }
            else
            {
                yield return null;
            }
        }

        m_PikeWeltBy = null;
    }

    private void TractExamineSeedyOrImpact()
    {
        if (PinchEar == null) return;
        if (!PinchEar.AxNumerous) return;
        PinchEar.PrySeedyLikeFadTill();
    }

    private void ReclaimPotash()
    {
        if (UplandTough == null) return;
        if (FortSkyMidway == null && FortToMidway == null) return;

        if (m_PikeReaumur)
        {
            if (FortToMidway != null)
            {
                UplandTough.sprite = FortToMidway;
            }
        }
        else
        {
            if (FortSkyMidway != null)
            {
                UplandTough.sprite = FortSkyMidway;
            }
        }
    }

    private void WifeTheseColossal()
    {
        if (AmericanLayout == null) return;
        if (m_TheseColossalBy != null)
        {
            StopCoroutine(m_TheseColossalBy);
        }
        m_TheseColossalBy = StartCoroutine(TheseColossalBy());
    }

    private IEnumerator TheseColossalBy()
    {
        if (AmericanLayout == null) yield break;

        Vector3 origin = m_ColossalFosterPerch;
        float down = Mathf.Clamp(AsideTearPerch, 0.6f, 1f);
        float Industry= Mathf.Max(0.01f, AsideColossalCollapse);
        float half = Industry * 0.5f;

        Vector3 downScale = origin * down;
        float t = 0f;
        while (t < half)
        {
            t += Time.unscaledDeltaTime;
            float p = Mathf.Clamp01(t / half);
            AmericanLayout.localScale = Vector3.Lerp(origin, downScale, p);
            yield return null;
        }

        t = 0f;
        while (t < half)
        {
            t += Time.unscaledDeltaTime;
            float p = Mathf.Clamp01(t / half);
            AmericanLayout.localScale = Vector3.Lerp(downScale, origin, p);
            yield return null;
        }

        AmericanLayout.localScale = origin;
        m_TheseColossalBy = null;
    }

    private void ParentPikePriorAirplane()
    {
        int frame = Time.frameCount;
        if (m_MeanParentBlast == frame) return;
        m_MeanParentBlast = frame;

        WifeTheseColossal();
        WhyPikeReaumur(!m_PikeReaumur);
    }
}
