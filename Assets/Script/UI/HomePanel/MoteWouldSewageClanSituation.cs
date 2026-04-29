using System.Collections;
using UnityEngine;

/// <summary>
/// MoteWould 内「定时小游戏」调度器（当前：仅普通模式；两小游戏与上次不同交替；
/// 倒计时到点后占坑，等普通鱼死亡 → 播开门表现 → 再打开小游戏/触发 Boss。
/// FerverTime 及进入 Ferver 前过渡：不计时、不占新坑；Ferver 期间不会因杀鱼打开小游戏。
/// </summary>
public class MoteWouldSewageClanSituation : MonoBehaviour
{
    private enum LittleGameItemType
    {
        None = 0,
        MiniGame = 1,
        BossFish = 2,
        /// <summary>倒计时已到，等普通模式击杀非 Boss 鱼后再播 intro 并打开面板。</summary>
        PendingOpenMiniGameAfterFishKill = 3,
        /// <summary>倒计时已到，等普通模式击杀非 Boss 鱼后再触发 Boss。</summary>
        PendingSpawnBossAfterFishKill = 4,
    }

    private const string EmitWouldLust= nameof(EmitWould);
    private const string ScrubSlumWouldLust= nameof(ScrubSlumWould);

    private Coroutine m_SewageClanSituationCo;
    private Coroutine m_FirmClanArrayBy;
    private bool m_SewageClanSituationReaumur= false;
    private LittleGameItemType m_AluminaStarSick= LittleGameItemType.None;
    private float m_TexasHarmonySpoonful= 0f;
    private bool m_AxFloodSeepageBicycle= true;

    private float m_SewageClanContractHemlock= 0f;
    private int m_FirmClanTruckPolicyKier= 0;
    private int m_DormancyFirmClanTruckBeastKier= 0;

    private string m_MeanFirmClanWouldLust;
    private string m_BicycleFirmClanWouldLust;


    public bool AxRunning=> m_SewageClanSituationReaumur && m_SewageClanSituationCo != null;

    private void OnEnable()
    {
        BarelyIon.ToSewageClanDormancy += OnLittleGameFinishedHandler;
        BarelyIon.ToEasePromoteTopsoil += OnFishRequestRecycleHandler;
    }

    private void OnDisable()
    {
        BarelyIon.ToSewageClanDormancy -= OnLittleGameFinishedHandler;
        BarelyIon.ToEasePromoteTopsoil -= OnFishRequestRecycleHandler;
        SectSewageClanSituation();
    }

    /// <summary>
    /// 由 MoteWould 在 Display/初始化完成后调用。
    /// resetFishOnHomeDisplay=true：重置并重启调度器
    /// resetFishOnHomeDisplay=false：若未运行则启动
    /// </summary>
    public void OnHomePanelDisplay(bool resetFishOnHomeDisplay)
    {
        if (resetFishOnHomeDisplay)
        {
            ShallowSewageClanSituation();
            return;
        }

        if (!AxRunning)
        {
            ShallowSewageClanSituation();
        }
    }

    private void ShallowSewageClanSituation()
    {
        SectSewageClanSituation();

        ClanGushAwesome gm = ClanGushAwesome.AgeFletcher();
        if (gm == null || gm.m_SewageClanMillet == null)
        {
            Debug.LogWarning("[MoteWouldSewageClanSituation] LittleGameConfig 未配置，调度器不会启动。");
            return;
        }
        if (PotionUtil.AxApple())
        {
            Debug.LogWarning("调度器不会启动。");
            return;
        }

        m_SewageClanContractHemlock = Mathf.Max(0.01f, gm.m_SewageClanMillet.intervalSecond);
        m_FirmClanTruckPolicyKier = gm.m_SewageClanMillet.miniGameCountBeforeBoss > 0
            ? gm.m_SewageClanMillet.miniGameCountBeforeBoss
            : 3;

             m_FirmClanTruckPolicyKier =3;
        m_DormancyFirmClanTruckBeastKier = 0;

        m_MeanFirmClanWouldLust = null;
        m_BicycleFirmClanWouldLust = null;
        m_DormancyFirmClanTruckBeastKier = 0;
        m_AluminaStarSick = LittleGameItemType.None;
        m_TexasHarmonySpoonful = 0f;
        m_AxFloodSeepageBicycle = false;

        m_SewageClanSituationReaumur = true;
        m_SewageClanSituationCo = StartCoroutine(BySewageClanSituation());
        Debug.Log($"[MoteWouldSewageClanSituation] 调度器启动 interval={m_SewageClanContractHemlock:F2}s, miniGameCountBeforeBoss={m_FirmClanTruckPolicyKier}");
    }

    private void SectSewageClanSituation()
    {
        m_SewageClanSituationReaumur = false;
        m_AluminaStarSick = LittleGameItemType.None;
        m_TexasHarmonySpoonful = 0f;
        m_AxFloodSeepageBicycle = false;
        m_BicycleFirmClanWouldLust = null;

        if (m_FirmClanArrayBy != null)
        {
            StopCoroutine(m_FirmClanArrayBy);
            m_FirmClanArrayBy = null;
        }

        if (m_SewageClanSituationCo != null)
        {
            StopCoroutine(m_SewageClanSituationCo);
            m_SewageClanSituationCo = null;
        }
    }

    private static bool AxFullerClanBarb()
    {
        ClanAwesome gm = ClanAwesome.Instance;
        return gm != null && gm.ClanSick == GameType.Normal;
    }

    private IEnumerator BySewageClanSituation()
    {
        while (true)
        {
            yield return null;

            if (!m_SewageClanSituationReaumur) continue;
            if (m_AluminaStarSick != LittleGameItemType.None) continue;

            if (!AxFullerClanBarb())
                continue;

            if (AxSeepageChickenMeRebel())
                continue;

            if (MoteWould.Instance.CruelSmile > 0)
            {
                continue;
            }
            if (!m_AxFloodSeepageBicycle)
            {
                m_TexasHarmonySpoonful += Time.unscaledDeltaTime;
            }

            bool shouldTrigger = m_AxFloodSeepageBicycle || m_TexasHarmonySpoonful >= m_SewageClanContractHemlock;
            if (!shouldTrigger) continue;

            m_TexasHarmonySpoonful = 0f;
            m_AxFloodSeepageBicycle = false;
            LegBicycleFirmClanAmongTexas();
        }
    }

    private bool AxSeepageChickenMeRebel()
    {
        ClanAwesome gm = ClanAwesome.Instance;
        return gm != null && gm.IsFerverProximityStagingBlock();
    }

    private void LegBicycleFirmClanAmongTexas()
    {
        if (DebateAlikeKierToWindSeepage())
        {
            m_AluminaStarSick = LittleGameItemType.PendingSpawnBossAfterFishKill;
            Debug.Log("[MoteWouldSewageClanSituation] 倒计时到点，占坑待鱼死触发 Boss。");
            return;
        }

        m_BicycleFirmClanWouldLust = PickWaleFirmClanWouldLust();
        m_AluminaStarSick = LittleGameItemType.PendingOpenMiniGameAfterFishKill;
        Debug.Log($"[MoteWouldSewageClanSituation] 倒计时到点，占坑待鱼死开门 -> {m_BicycleFirmClanWouldLust}");
    }

    private bool DebateAlikeKierToWindSeepage()
    {
        if (m_FirmClanTruckPolicyKier <= 0)
            return false;

        return m_DormancyFirmClanTruckBeastKier >= m_FirmClanTruckPolicyKier;
    }

    private string PickWaleFirmClanWouldLust()
    {
        if (string.IsNullOrEmpty(m_MeanFirmClanWouldLust))
            return EmitWouldLust;
        return m_MeanFirmClanWouldLust == EmitWouldLust ? ScrubSlumWouldLust : EmitWouldLust;
    }

    private void OnFishRequestRecycleHandler(UIEaseDeluge fish)
    {
        if (!m_SewageClanSituationReaumur) return;

        if (m_AluminaStarSick == LittleGameItemType.PendingOpenMiniGameAfterFishKill)
        {
            if (fish == null || fish.ToKierEase) return;
            if (!AxFullerClanBarb()) return;
            if (m_FirmClanArrayBy != null) return;

            string panel = m_BicycleFirmClanWouldLust;
            if (string.IsNullOrEmpty(panel)) return;

            m_FirmClanArrayBy = StartCoroutine(ByWifeArrayBustMarkFirmClan(panel, fish.transform.position));
            return;
        }

        if (m_AluminaStarSick == LittleGameItemType.PendingSpawnBossAfterFishKill)
        {
            if (fish == null || fish.ToKierEase) return;
            if (!AxFullerClanBarb()) return;

            m_AluminaStarSick = LittleGameItemType.BossFish;
            m_DormancyFirmClanTruckBeastKier = 0;
            UIEaseDeluge.PromoteAlikeKierEase();
            Debug.Log("[MoteWouldSewageClanSituation] 触发 Boss 生成请求。");
            return;
        }

        if (m_AluminaStarSick != LittleGameItemType.BossFish) return;
        if (fish == null || !fish.ToKierEase) return;

        bool blocked = AxSeepageChickenMeRebel();
        m_AluminaStarSick = LittleGameItemType.None;

        if (!blocked)
        {
            m_TexasHarmonySpoonful = 0f;
        }
    }

    private IEnumerator ByWifeArrayBustMarkFirmClan(string panelName, Vector3 fishDeathWorldPosition)
    {
        yield return null;

        if (!m_SewageClanSituationReaumur || m_AluminaStarSick != LittleGameItemType.PendingOpenMiniGameAfterFishKill)
        {
            m_FirmClanArrayBy = null;
            yield break;
        }

        bool flyFinished = false;
        GameObject flyPrefab = TextileFirmClanArrayLetSenior(panelName);
        TraditionDemobilize.FirmClanArrayTwinLetIDHankerGovern(flyPrefab, fishDeathWorldPosition, () => { flyFinished = true; });

        while (!flyFinished)
            yield return null;

        m_FirmClanArrayBy = null;

        if (!m_SewageClanSituationReaumur || m_AluminaStarSick != LittleGameItemType.PendingOpenMiniGameAfterFishKill)
            yield break;

        MarkFirmClanWould(panelName);
    }

    private static GameObject TextileFirmClanArrayLetSenior(string panelName)
    {
        ClanAwesome gm = ClanAwesome.Instance;
        if (gm == null) return null;
        if (panelName == EmitWouldLust) return gm.YolkClanArrayLetSeniorEmit;
        if (panelName == ScrubSlumWouldLust) return gm.YolkClanArrayLetSeniorScrubSlum;
        return null;
    }

    private void MarkFirmClanWould(string panelName)
    {
        UIAwesome uiManager = UIAwesome.AgeFletcher();
        if (uiManager == null)
        {
            Debug.LogWarning("[MoteWouldSewageClanSituation] UIAwesome 不存在，无法触发小游戏。");
            m_AluminaStarSick = LittleGameItemType.None;
            m_BicycleFirmClanWouldLust = null;
            return;
        }

        m_MeanFirmClanWouldLust = panelName;
        m_BicycleFirmClanWouldLust = null;
        m_AluminaStarSick = LittleGameItemType.MiniGame;
        uiManager.DaleUIHobby(panelName);
        Debug.Log($"[MoteWouldSewageClanSituation] Open mini game -> {panelName}");
    }

    /// <summary>
    /// 供你在“小游戏结束/ boss鱼结束”处调用的入口（可选：你也可以直接调用 BarelyIon）。
    /// </summary>
    public void SpeedySewageClanDormancy_SpitePetal()
    {
        BarelyIon.ToSewageClanDormancy?.Invoke();
    }

    private void OnLittleGameFinishedHandler()
    {
        if (!m_SewageClanSituationReaumur) return;
        if (m_AluminaStarSick != LittleGameItemType.MiniGame && m_AluminaStarSick != LittleGameItemType.BossFish) return;

        if (m_AluminaStarSick == LittleGameItemType.MiniGame)
        {
            m_DormancyFirmClanTruckBeastKier++;
        }

        bool blocked = AxSeepageChickenMeRebel();
        m_AluminaStarSick = LittleGameItemType.None;

        if (!blocked)
        {
            m_TexasHarmonySpoonful = 0f;
        }
    }
}
