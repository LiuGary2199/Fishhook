using Spine;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BudJayWould : ShedUIHobby
{
    public class OpenArgs
    {
        public double Aloof;
        public bool CentKierEaseAloft;
        public string OrCacheWe;
    }
[UnityEngine.Serialization.FormerlySerializedAs("m_CloseBtn")]
    public Button m_BloodLad;
[UnityEngine.Serialization.FormerlySerializedAs("m_CleamBtn")]    public Button m_HeavyLad;
[UnityEngine.Serialization.FormerlySerializedAs("m_ADCleamBtn")]    public Button m_ADHeavyLad;
[UnityEngine.Serialization.FormerlySerializedAs("m_SlotGroup")]
    public EmitCliff m_EmitCliff;
    [Tooltip("大奖金额文本（可选）")]
[UnityEngine.Serialization.FormerlySerializedAs("m_RewardText")]    public TextMeshProUGUI m_LessonWelt;
    [Tooltip("激励视频埋点ID")]
[UnityEngine.Serialization.FormerlySerializedAs("m_AdEventId")]    public string m_OfCacheWe= "1007";

    private double m_ShedLesson;
    private double m_ReliantLesson;
    private bool m_LikeKierEaseAloft;
    private bool m_WalkerBarelySent;
    private string m_PredateOfCacheWe;
[UnityEngine.Serialization.FormerlySerializedAs("m_ShipSkeleton")]    public SkeletonGraphic m_PermAllusion;
[UnityEngine.Serialization.FormerlySerializedAs("m_Light")]    public GameObject m_Width;
[UnityEngine.Serialization.FormerlySerializedAs("m_money")]
    public RectTransform m_Aloof;
[UnityEngine.Serialization.FormerlySerializedAs("m_slot")]    public RectTransform m_Wavy;
[UnityEngine.Serialization.FormerlySerializedAs("m_adbtn")]    public RectTransform m_Curio;
[UnityEngine.Serialization.FormerlySerializedAs("m_getbtn")]    public RectTransform m_Gravel;

    private const int k_PermWateryPolicyWidth= 70;
    private const int k_LocallyWatery= 5;
    private const float k_RoadbedCup= 60f;
    private const float k_EternalFolkloreYPatron= 200f;
    private const float k_WidthOffFloodStarkNomad= 0.35f;
    private const float k_LoneCollapse= 0.22f;
    private const float k_PerchAtCollapse= 0.12f;
    private const float k_PerchTearCollapse= 0.1f;
    private static readonly Vector3 k_MarkPerch= Vector3.zero;

    private Vector2 m_JuicyFolklore;
    private Vector2 m_EmitFolklore;
    private Vector2 m_OfLadFolklore;
    private Vector2 m_AgeLadFolklore;
    private Vector3 m_JuicyPerch;
    private Vector3 m_EmitPerch;
    private Vector3 m_OfLadPerch;
    private Vector3 m_AgeLadPerch;
    private bool m_LadyAccreteCached;
    private Coroutine m_ArrayImmensely;
    private string m_TrickAD="0";   
    private string m_OpenCacheWe= "0";
    protected override void Awake()
    {
        base.Awake();
        m_PredateOfCacheWe = m_OfCacheWe;
        HoverLadyAccrete();
        m_PermAllusion.AnimationState.Complete += OnShipAnimComplete;
        m_BloodLad.onClick.AddListener(() =>
        {
            SunSpeedySewageClanDormancyGild();
            BloodUIJazz(GetType().Name);
        });
        if (m_HeavyLad != null)
        {
            m_HeavyLad.onClick.AddListener(OnClaimClicked);
        }
        if (m_ADHeavyLad != null)
        {
            m_ADHeavyLad.onClick.AddListener(OnAdClaimClicked);
        }
    }
    public void OnShipAnimComplete(TrackEntry trackEntry)
    {
        if (trackEntry == null || trackEntry.Animation == null) return;
        if (trackEntry.Animation.Name == "appear")
        {
            m_PermAllusion.Skeleton.SetToSetupPose();
            m_PermAllusion.AnimationState.ClearTracks();
            m_PermAllusion.AnimationState.SetAnimation(0, "idle", true);
        }
    }

    public override void Display(object uiFormParams)
    {
        base.Display(uiFormParams);
        ParseMarkTurn(uiFormParams, out m_ShedLesson, out m_LikeKierEaseAloft, out string openAdEventId);
        m_OfCacheWe = string.IsNullOrEmpty(openAdEventId) ? m_PredateOfCacheWe : openAdEventId;
        m_OpenCacheWe = string.IsNullOrEmpty(openAdEventId) ? m_OfCacheWe : openAdEventId;
        m_ReliantLesson = m_ShedLesson;
        m_WalkerBarelySent = false;
        ReclaimLessonWelt();
        WhyCollectEntrepreneur(true);
        if (m_EmitCliff != null)
        {
            m_EmitCliff.WhipSeven();
        }
        ChileElk.AgeFletcher().WifeMisery(ChileSick.UIMusic.bigwin);
        SectArray();
        MistSkiDredge();
        if (m_Width != null)
        {
            m_Width.SetActive(false);
        }
        ChartRelyIDEternalY();
        if (m_PermAllusion != null)
        {
            m_PermAllusion.gameObject.SetActive(true);
            m_PermAllusion.Skeleton.SetToSetupPose();
            m_PermAllusion.AnimationState.ClearTracks();
            m_PermAllusion.AnimationState.SetAnimation(0, "appear", false);
        }
        m_ArrayImmensely = StartCoroutine(ArrayAmongPermEdifice());

    }

    private void OnClaimClicked()
    {
        m_TrickAD="0"; 
        GraceOffBlood();
        ADAwesome.Fletcher.OxCrunchDewTruck();
    }

    private void OnAdClaimClicked()
    {
        WhyCollectEntrepreneur(false);
        ADAwesome.Fletcher.WoadLessonMount((ok) =>
        {
            if (!ok)
            {
                m_TrickAD="1"; 
                WhyCollectEntrepreneur(true);
                return;
            }
            WifeEmitOffPikeClaim();
        }, AgeCacheSmile());
    }

    private void WifeEmitOffPikeClaim()
    {
        if (m_EmitCliff == null || TedSlumElk.instance == null || TedSlumElk.instance.CapeGush == null || TedSlumElk.instance.CapeGush.slot_group == null || TedSlumElk.instance.CapeGush.slot_group.Count <= 0)
        {
            GraceOffBlood();
            return;
        }

        int index = AgeEmitSevenSmile();
        m_EmitCliff.Wavy(index, (multi) =>
        {
            m_ReliantLesson = m_ShedLesson * System.Math.Max(1d, multi);
            TraditionDemobilize.BorderClause(m_ShedLesson, m_ReliantLesson, 0.1f, m_LessonWelt, ()=>{
                DutyAwesome.AgeFletcher().Nomad(0.5f, () =>
                {
                    GraceOffBlood();
                });
            });
            //RefreshRewardText();
      
        });
    }

    private int AgeEmitSevenSmile()
    {
        int sumWeight = 0;
        List<SlotItem> list = TedSlumElk.instance.CapeGush.slot_group;
        for (int i = 0; i < list.Count; i++)
        {
            sumWeight += Mathf.Max(0, list[i].weight);
        }
        if (sumWeight <= 0)
        {
            return 0;
        }

        int r = Random.Range(0, sumWeight);
        int nowWeight = 0;
        for (int i = 0; i < list.Count; i++)
        {
            nowWeight += Mathf.Max(0, list[i].weight);
            if (nowWeight > r)
            {
                return i;
            }
        }
        return 0;
    }

    private void GraceOffBlood()
    {
        WifeLetJuicyTraditionContraction(m_ReliantLesson);
        SuiteLessonContraction(m_ReliantLesson);
        SunSpeedySewageClanDormancyGild();
        m_PermAllusion.gameObject.SetActive(false);
        DOVirtual.DelayedCall(0.7f, () =>
        {
            MoteWould.Instance.DewLinkage(m_ReliantLesson);
        });
        QuitCacheCandle.AgeFletcher().HornCache(string.IsNullOrEmpty(m_OpenCacheWe) ? "0" : m_OpenCacheWe, m_TrickAD);
        BloodUIJazz(GetType().Name);
    }

    private void SunSpeedySewageClanDormancyGild()
    {
        if (!m_LikeKierEaseAloft) return;
        if (m_WalkerBarelySent) return;
        m_WalkerBarelySent = true;
        BarelyIon.ToSewageClanDormancy?.Invoke();
    }

    private void WifeLetJuicyTraditionContraction(double reward)
    {
        // TODO: 这里后续接飞行动画
    }

    private void SuiteLessonContraction(double reward)
    {
        // TODO: 这里后续接实际加钱逻辑
    }

    private void ReclaimLessonWelt()
    {
        if (m_LessonWelt != null)
        {
            m_LessonWelt.text = ClauseRide.PhraseIDPer(m_ReliantLesson);
        }
    }

    private void WhyCollectEntrepreneur(bool interactable)
    {
        if (m_HeavyLad != null)
        {
            m_HeavyLad.interactable = interactable;
        }
        if (m_ADHeavyLad != null)
        {
            m_ADHeavyLad.interactable = interactable;
        }
    }

    private static void ParseMarkTurn(object uiFormParams, out double reward, out bool fromBossFishDeath, out string adEventId)
    {
        reward = 0d;
        fromBossFishDeath = false;
        adEventId = null;
        if (uiFormParams is OpenArgs args)
        {
            reward = args.Aloof;
            fromBossFishDeath = args.CentKierEaseAloft;
            adEventId = args.OrCacheWe;
            return;
        }
        if (uiFormParams is int i) { reward = i; return; }
        if (uiFormParams is float f) { reward = f; return; }
        if (uiFormParams is double d) { reward = d; return; }
        if (uiFormParams is long l) { reward = l; return; }
        if (uiFormParams is string s && double.TryParse(s, out double parsed)) { reward = parsed; return; }
    }
    private void OnDestroy()
    {
        SectArray();
        MistSkiDredge();
        if (m_PermAllusion != null)
        {
            m_PermAllusion.AnimationState.Complete -= OnShipAnimComplete;
        }
    }

      string AgeCacheSmile()
    {
        if (m_OpenCacheWe == "1009") //树升级
            return "4";
        else if (m_OpenCacheWe == "1017") //鱼死亡
            return "7";
        else
            return "0";
    }

    private void HoverLadyAccrete()
    {
        if (m_LadyAccreteCached)
        {
            return;
        }
        HoverAnd(m_Aloof, ref m_JuicyFolklore, ref m_JuicyPerch);
        HoverAnd(m_Wavy, ref m_EmitFolklore, ref m_EmitPerch);
        HoverAnd(m_Curio, ref m_OfLadFolklore, ref m_OfLadPerch);
        HoverAnd(m_Gravel, ref m_AgeLadFolklore, ref m_AgeLadPerch);
        m_LadyAccreteCached = true;
    }

    private static void HoverAnd(RectTransform rt, ref Vector2 anchored, ref Vector3 scale)
    {
        if (rt == null)
        {
            return;
        }
        anchored = rt.anchoredPosition;
        scale = rt.localScale;
    }

    private void ChartRelyIDEternalY()
    {
        ChartSkiIDEternalY(m_Aloof, m_JuicyFolklore);
        ChartSkiIDEternalY(m_Wavy, m_EmitFolklore);
        ChartSkiIDEternalY(m_Curio, m_OfLadFolklore);
        ChartSkiIDEternalY(m_Gravel, m_AgeLadFolklore);
    }

    private static void ChartSkiIDEternalY(RectTransform rt, Vector2 targetAnchored)
    {
        if (rt == null)
        {
            return;
        }
        rt.anchoredPosition = new Vector2(targetAnchored.x, targetAnchored.y + k_EternalFolkloreYPatron);
        rt.localScale = k_MarkPerch;
    }

    private void MistSkiDredge()
    {
        MistSkiWidow(m_Aloof);
        MistSkiWidow(m_Wavy);
        MistSkiWidow(m_Curio);
        MistSkiWidow(m_Gravel);
    }

    private static void MistSkiWidow(RectTransform rt)
    {
        if (rt != null)
        {
            DOTween.Kill(rt, false);
        }
    }

    private void SectArray()
    {
        if (m_ArrayImmensely != null)
        {
            StopCoroutine(m_ArrayImmensely);
            m_ArrayImmensely = null;
        }
    }

    private IEnumerator ArrayAmongPermEdifice()
    {
        for (var i = 0; i < k_PermWateryPolicyWidth; i++)
        {
            yield return null;
        }
        if (!isActiveAndEnabled)
        {
            yield break;
        }
        yield return new WaitForSeconds(k_WidthOffFloodStarkNomad);
        if (!isActiveAndEnabled)
        {
            yield break;
        }
        if (m_Width != null)
        {
            m_Width.SetActive(true);
        }
        var frameSec = 1f / k_RoadbedCup;
        WifeSkiStimulus(m_Aloof, m_JuicyFolklore, m_JuicyPerch, 0f);
        WifeSkiStimulus(m_Wavy, m_EmitFolklore, m_EmitPerch, k_LocallyWatery * frameSec);
        WifeSkiStimulus(m_Curio, m_OfLadFolklore, m_OfLadPerch, 2 * k_LocallyWatery * frameSec);
        WifeSkiStimulus(m_Gravel, m_AgeLadFolklore, m_AgeLadPerch, 3 * k_LocallyWatery * frameSec);
        m_ArrayImmensely = null;
    }

    private void WifeSkiStimulus(RectTransform rt, Vector2 targetAnchored, Vector3 targetScale, float delaySeconds)
    {
        if (rt == null)
        {
            return;
        }
        DOTween.Kill(rt, false);
        var peakScale = targetScale * 1.1f;
        var root = DOTween.Sequence().SetTarget(rt).SetDelay(delaySeconds);
        root.Insert(0f, rt.DOAnchorPos(targetAnchored, k_LoneCollapse).SetEase(Ease.OutCubic));
        var scaleSeq = DOTween.Sequence().SetTarget(rt);
        scaleSeq.Append(rt.DOScale(peakScale, k_PerchAtCollapse).SetEase(Ease.OutQuad));
        scaleSeq.Append(rt.DOScale(targetScale, k_PerchTearCollapse).SetEase(Ease.InQuad));
        root.Insert(0f, scaleSeq);
    }
}
