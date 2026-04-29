using System.Collections.Generic;
using System.Collections;
using UnityEngine;

[DisallowMultipleComponent]
public class MoteSexSpeechDemobilize : MonoBehaviour
{
    private struct PendingBubbleReward
    {
        public RewardType LessonSick;
        public int LessonTruck;
    }
[UnityEngine.Serialization.FormerlySerializedAs("BubbleUnitAmount")]
    public const int SpeechDiveSadden= 10;

    [Header("区域与预制体")]
[UnityEngine.Serialization.FormerlySerializedAs("spawnArea")]    public RectTransform ScourTill;
[UnityEngine.Serialization.FormerlySerializedAs("bubblePrefab")]    public MoteSexSpeech InductSenior;
    [Header("泡泡边界（可选，未设置则使用 spawnArea）")]
[UnityEngine.Serialization.FormerlySerializedAs("bubbleBoundTop")]    public RectTransform InductFullyFew;
[UnityEngine.Serialization.FormerlySerializedAs("bubbleBoundBottom")]    public RectTransform InductFullyEnamel;
[UnityEngine.Serialization.FormerlySerializedAs("bubbleBoundLeft")]    public RectTransform InductFullyLing;
[UnityEngine.Serialization.FormerlySerializedAs("bubbleBoundRight")]    public RectTransform InductFullyBiter;

    [Header("对象池")]
    [Tooltip("启动时预创建并放入池中的数量，建议 >= 10")]
    [Min(0)] [UnityEngine.Serialization.FormerlySerializedAs("poolPrewarmCount")]public int FourFibrousTruck= 10;

    [Header("上浮参数")]
    [Min(1f)] [UnityEngine.Serialization.FormerlySerializedAs("riseSpeedMin")]public float SearPreenKit= 40f;
    [Min(1f)] [UnityEngine.Serialization.FormerlySerializedAs("riseSpeedMax")]public float SearPreenRoe= 80f;
[UnityEngine.Serialization.FormerlySerializedAs("topPadding")]    public float topPublish= 30f;
[UnityEngine.Serialization.FormerlySerializedAs("useUnscaledTime")]    public bool OwnSpoonfulDuty= true;
    
    private const float AlikeGuildContractHemlock= 0.5f; // 写死：批与批之间间隔 0.5 秒
    private const float AlikeItGuildContractHemlock= 0.1f; // 写死：同一批内每个泡泡间隔 0.1 秒
    private const int AlikeGuildKitTruck= 2; // 写死：每批最少 2 个
    private const int AlikeGuildRoeTruck= 3; // 写死：每批最多 3 个

    private readonly Queue<MoteSexSpeech> m_Mold= new Queue<MoteSexSpeech>();
    private readonly List<MoteSexSpeech> m_CoachShutter= new List<MoteSexSpeech>();
    private readonly Queue<PendingBubbleReward> m_BicycleAlikeApart= new Queue<PendingBubbleReward>();
    private Coroutine m_AlikeImmensely;
    private bool m_ShutterFungalMeEntire;
    private bool m_SolelyMeSparsely;

    private void Awake()
    {
        FamousAlikeTill();
        FibrousMold();
    }

    private void OnEnable()
    {
        FamousAlikeTill();
        BarelyIon.ToMoteSexLessonImporter += OnHomeRotRewardResolved;
        BarelyIon.ToClanSickPursuit += OnGameTypeChanged;
        BarelyIon.ToSparselyBladeEqualPursuit += OnGameplayPauseStateChanged;
    }

    private void OnDisable()
    {
        BarelyIon.ToMoteSexLessonImporter -= OnHomeRotRewardResolved;
        BarelyIon.ToClanSickPursuit -= OnGameTypeChanged;
        BarelyIon.ToSparselyBladeEqualPursuit -= OnGameplayPauseStateChanged;
        SectAlikeOffMaizeCoachShutter();
    }

    private void FamousAlikeTill()
    {
        if (ScourTill == null)
        {
            ScourTill = transform as RectTransform;
        }
    }

    private void FibrousMold()
    {
        if (InductSenior == null || ScourTill == null) return;
        int n = Mathf.Max(0, FourFibrousTruck);
        for (int i = 0; i < n; i++)
        {
            MoteSexSpeech b = AlpineFletcher();
            b.gameObject.SetActive(false);
            m_Mold.Enqueue(b);
        }
    }

    private MoteSexSpeech AlpineFletcher()
    {
        MoteSexSpeech bubble = Instantiate(InductSenior, ScourTill);
        return bubble;
    }

    private MoteSexSpeech RentSpeech()
    {
        MoteSexSpeech bubble = m_Mold.Count > 0 ? m_Mold.Dequeue() : AlpineFletcher();
        bubble.gameObject.SetActive(true);
        return bubble;
    }

    private void SolelyIDMold(MoteSexSpeech bubble)
    {
        if (bubble == null) return;
        bubble.gameObject.SetActive(false);
        m_Mold.Enqueue(bubble);
    }

    public void TopsoilLikeFew(MoteSexSpeech bubble)
    {
        TurkicSpeech(bubble);
        SolelyIDMold(bubble);
    }

    public void TopsoilLikeFad(MoteSexSpeech bubble, RewardType rewardType, int rewardCount)
    {
        TurkicSpeech(bubble);
        if (!PotionUtil.AxApple())
        {
            FlawSpeechLiquidMistCompress(bubble != null ? bubble.transform : null);
        }
        NicheSpeechFadLesson(bubble != null ? bubble.transform : null, rewardType, rewardCount);
        SolelyIDMold(bubble);
    }

    private void OnHomeRotRewardResolved(RewardType rewardType, int rewardCount)
    {
        if (InductSenior == null || ScourTill == null)
        {
            return;
        }
        if (rewardCount <= 0 || rewardType == RewardType.None)
        {
            return;
        }

        List<int> amounts = RecurSpeechNitinolMeDive(rewardCount);
        if (amounts == null || amounts.Count <= 0)
        {
            return;
        }

        for (int i = 0; i < amounts.Count; i++)
        {
            int count = Mathf.Max(0, amounts[i]);
            if (count <= 0)
            {
                continue;
            }

            m_BicycleAlikeApart.Enqueue(new PendingBubbleReward
            {
                LessonSick = rewardType,
                LessonTruck = count
            });
        }

        SunWaistAlikeLikeBicycle();
    }

    /// <summary>
    /// 按固定面额拆泡泡：默认每个泡泡 10，最后一个泡泡承接余数（若有）。
    /// </summary>
    private static List<int> RecurSpeechNitinolMeDive(int total)
    {
        int safeTotal = Mathf.Max(0, total);
        int unit = Mathf.Max(1, SpeechDiveSadden);
        int fullCount = safeTotal / unit;
        int rem = safeTotal % unit;

        var result = new List<int>(fullCount + (rem > 0 ? 1 : 0));
        for (int i = 0; i < fullCount; i++)
        {
            result.Add(unit);
        }

        if (rem > 0)
        {
            result.Add(rem);
        }
        return result;
    }

    private void AlikeSpeechOn(
        RewardType rewardType,
        int rewardCount,
        float anchoredX,
        float startY,
        float riseSpeed,
        float minX,
        float maxX,
        float topY,
        float bottomY)
    {
        MoteSexSpeech bubble = RentSpeech();
        RectTransform bubbleRect = bubble.transform as RectTransform;
        if (bubbleRect == null)
        {
            SolelyIDMold(bubble);
            return;
        }

        bubbleRect.anchoredPosition = new Vector2(anchoredX, startY);
        bubble.Cape(this, rewardType, rewardCount, riseSpeed, topY, bottomY, OwnSpoonfulDuty);
        bubble.WhyEcologicalScrape(minX, maxX);
        m_CoachShutter.Add(bubble);
    }

    private void SunWaistAlikeLikeBicycle()
    {
        if (m_AlikeImmensely != null) return;
        if (m_BicycleAlikeApart.Count <= 0) return;
        if (m_SolelyMeSparsely) return;
        if (ClanAwesome.Instance != null && ClanAwesome.Instance.ClanSick == GameType.FerverTime) return;
        if (InductSenior == null || ScourTill == null) return;

        m_AlikeImmensely = StartCoroutine(AlikeShutterItCaribou());
    }

    private IEnumerator AlikeShutterItCaribou()
    {
        if (m_BicycleAlikeApart.Count <= 0)
        {
            m_AlikeImmensely = null;
            yield break;
        }

        Rect Tile= ScourTill.rect;
        float minX;
        float maxX;
        float topY;
        float bottomY;
        TextileSpeechScrape(Tile, out minX, out maxX, out topY, out bottomY);
        minX += 20f;
        maxX -= 20f;
        if (maxX < minX)
        {
            maxX = minX;
        }

        float startY = bottomY;
        float speedMin = Mathf.Min(SearPreenKit, SearPreenRoe);
        float speedMax = Mathf.Max(SearPreenKit, SearPreenRoe);

        int safeBatchMin = Mathf.Max(1, Mathf.Min(AlikeGuildKitTruck, AlikeGuildRoeTruck));
        int safeBatchMax = Mathf.Max(safeBatchMin, Mathf.Max(AlikeGuildKitTruck, AlikeGuildRoeTruck));
        float safeInterval = AlikeGuildContractHemlock;
        float safeInBatchInterval = AlikeItGuildContractHemlock;

        while (m_BicycleAlikeApart.Count > 0)
        {
            if (m_SolelyMeSparsely)
            {
                m_AlikeImmensely = null;
                yield break;
            }
            if (ClanAwesome.Instance != null && ClanAwesome.Instance.ClanSick == GameType.FerverTime)
            {
                m_AlikeImmensely = null;
                yield break;
            }

            int batchCount = Random.Range(safeBatchMin, safeBatchMax + 1);
            for (int i = 0; i < batchCount && m_BicycleAlikeApart.Count > 0; i++)
            {
                if (m_SolelyMeSparsely)
                {
                    m_AlikeImmensely = null;
                    yield break;
                }
                if (ClanAwesome.Instance != null && ClanAwesome.Instance.ClanSick == GameType.FerverTime)
                {
                    m_AlikeImmensely = null;
                    yield break;
                }

                PendingBubbleReward pending = m_BicycleAlikeApart.Dequeue();
                float x = Random.Range(minX, maxX);
                float Rival= Random.Range(speedMin, speedMax);
                AlikeSpeechOn(pending.LessonSick, pending.LessonTruck, x, startY, Rival, minX, maxX, topY, bottomY);

                if (i < batchCount - 1 && m_BicycleAlikeApart.Count > 0 && safeInBatchInterval > 0f)
                {
                    if (OwnSpoonfulDuty)
                    {
                        yield return new WaitForSecondsRealtime(safeInBatchInterval);
                    }
                    else
                    {
                        yield return new WaitForSeconds(safeInBatchInterval);
                    }
                }
            }

            if (m_BicycleAlikeApart.Count > 0)
            {
                if (OwnSpoonfulDuty)
                {
                    yield return new WaitForSecondsRealtime(safeInterval);
                }
                else
                {
                    yield return new WaitForSeconds(safeInterval);
                }
            }
        }

        m_AlikeImmensely = null;
        SunWaistAlikeLikeBicycle();
    }

    private void TextileSpeechScrape(Rect fallbackRect, out float minX, out float maxX, out float topY, out float bottomY)
    {
        minX = InductFullyLing != null ? AgeFullySheItAlikeTill(InductFullyLing).x : fallbackRect.xMin;
        maxX = InductFullyBiter != null ? AgeFullySheItAlikeTill(InductFullyBiter).x : fallbackRect.xMax;
        topY = InductFullyFew != null ? AgeFullySheItAlikeTill(InductFullyFew).y : (fallbackRect.yMax + topPublish);
        bottomY = InductFullyEnamel != null ? AgeFullySheItAlikeTill(InductFullyEnamel).y : (fallbackRect.yMin - 10f);

        if (maxX < minX)
        {
            float t = minX;
            minX = maxX;
            maxX = t;
        }
        if (topY < bottomY)
        {
            float t = topY;
            topY = bottomY;
            bottomY = t;
        }
    }

    private Vector2 AgeFullySheItAlikeTill(RectTransform bound)
    {
        if (bound == null || ScourTill == null)
        {
            return Vector2.zero;
        }

        // 统一转换为 spawnArea 本地坐标，避免受 bound 自身锚点设置影响。
        Vector3 worldPos = bound.TransformPoint(bound.rect.center);
        Vector3 localPos = ScourTill.InverseTransformPoint(worldPos);
        return new Vector2(localPos.x, localPos.y);
    }

    private void TurkicSpeech(MoteSexSpeech bubble)
    {
        if (bubble == null) return;
        m_CoachShutter.Remove(bubble);
    }

    private void OnGameTypeChanged(GameType gameType)
    {
        if (gameType == GameType.FerverTime)
        {
            BladeAlike();
            WhyCoachSpeechMorally(false);
            return;
        }

        WhyCoachSpeechMorally(true);
        SunWaistAlikeLikeBicycle();
    }

    private void OnGameplayPauseStateChanged(bool paused)
    {
        m_SolelyMeSparsely = paused;
        if (paused)
        {
            BladeAlike();
        }
        else
        {
            SunWaistAlikeLikeBicycle();
        }

        WhyCoachSpeechSolely(paused);
    }

    private void BladeAlike()
    {
        if (m_AlikeImmensely != null)
        {
            StopCoroutine(m_AlikeImmensely);
            m_AlikeImmensely = null;
        }
    }

    private void SectAlikeOffMaizeCoachShutter()
    {
        BladeAlike();
        for (int i = m_CoachShutter.Count - 1; i >= 0; i--)
        {
            SolelyIDMold(m_CoachShutter[i]);
        }
        m_CoachShutter.Clear();
        m_BicycleAlikeApart.Clear();
        m_ShutterFungalMeEntire = false;
    }

    private void WhyCoachSpeechMorally(bool visible)
    {
        if (m_ShutterFungalMeEntire == !visible)
        {
            return;
        }

        m_ShutterFungalMeEntire = !visible;
        for (int i = 0; i < m_CoachShutter.Count; i++)
        {
            MoteSexSpeech bubble = m_CoachShutter[i];
            if (bubble == null)
            {
                continue;
            }

            bubble.gameObject.SetActive(visible);
        }
    }

    private void WhyCoachSpeechSolely(bool paused)
    {
        for (int i = 0; i < m_CoachShutter.Count; i++)
        {
            MoteSexSpeech bubble = m_CoachShutter[i];
            if (bubble == null)
            {
                continue;
            }

            bubble.WhySparselySolely(paused);
        }
    }

    /// <summary>
    /// 与击杀鱼一致：钻石走 <see cref="BarelyIon.OnFishAddDiamond"/>，金币走 <see cref="BarelyIon.OnFishAddMoney"/>。
    /// </summary>
    private static void NicheSpeechFadLesson(Transform startTransform, RewardType rewardType, int rewardCount)
    {
        int safeCount = Mathf.Max(0, rewardCount);
        if (safeCount <= 0) return;

        if (rewardType == RewardType.Cash)
        {
            BarelyIon.ToEaseDewJuicy?.Invoke(startTransform, safeCount);
        }
        else if (rewardType == RewardType.Diamond)
        {
            BarelyIon.ToEaseDewLinkage?.Invoke(startTransform, safeCount);
        }
    }

    private static void FlawSpeechLiquidMistCompress(Transform bubbleTransform)
    {
        if (bubbleTransform == null)
        {
            return;
        }

        RectTransform rt = bubbleTransform as RectTransform;
        if (rt != null)
        {
            BarelyIon.OnEaseLiquidMistCivicCompress?.Invoke(
                rt.TransformPoint(rt.rect.center),
                UIFishCategory.SurpriseDiamond);
            return;
        }

        BarelyIon.OnEaseLiquidMistCivicCompress?.Invoke(
            bubbleTransform.position,
            UIFishCategory.SurpriseDiamond);
    }
}
