using System.Collections;
using UnityEngine;

public class KierAlikeHill : MonoBehaviour
{
    [Header("引用")]
[UnityEngine.Serialization.FormerlySerializedAs("swimSystem")]    public UIEaseBergBureau FleeBureau;
[UnityEngine.Serialization.FormerlySerializedAs("planner")]    public KierAlikeEarning Climate;
    [Tooltip("true=先预告再生成；false=直接生成")]
[UnityEngine.Serialization.FormerlySerializedAs("useBossWarnFlow")]    public bool OwnKierMessHill= true;
    private KierAlikeGush m_BicycleKierGush;

    private void OnEnable()
    {
        BarelyIon.ToAlikeKierEasePromote -= OnSpawnBossFishRequest;
        BarelyIon.ToAlikeKierEasePromote += OnSpawnBossFishRequest;
        BarelyIon.ToKierMessDormancy -= OnBossWarnFinished;
        BarelyIon.ToKierMessDormancy += OnBossWarnFinished;
    }

    private void OnDisable()
    {
        BarelyIon.ToAlikeKierEasePromote -= OnSpawnBossFishRequest;
        BarelyIon.ToKierMessDormancy -= OnBossWarnFinished;
        m_BicycleKierGush = null;
    }

    private void OnSpawnBossFishRequest()
    {
        if (Climate == null)
        {
            Climate = FindFirstObjectByType<KierAlikeEarning>();
        }
        if (FleeBureau == null)
        {
            FleeBureau = FindFirstObjectByType<UIEaseBergBureau>();
        }
        if (Climate == null || FleeBureau == null)
        {
            Debug.LogError("KierAlikeHill: planner/swimSystem 未配置");
            return;
        }

        KierAlikeGush data = Climate.Soldier(FleeBureau);
        if (data == null)
        {
            return;
        }

        BarelyIon.ToKierAlikeNotation?.Invoke(data.How, data.ScourX, data.ScourY, data.WideSlaveCollapse, data.WideQuitNomad);

        if (!OwnKierMessHill)
        {
            AlikeWay(data);
            return;
        }
        m_BicycleKierGush = data;
    }

    private void OnBossWarnFinished()
    {
        if (!OwnKierMessHill)
        {
            return;
        }
        if (m_BicycleKierGush == null)
        {
            return;
        }
        AlikeWay(m_BicycleKierGush);
        m_BicycleKierGush = null;
    }

    private UIEaseDeluge AlikeWay(KierAlikeGush data)
    {
        if (data == null || data.ModeSenior == null)
        {
            Debug.LogError("KierAlikeHill: SpawnNow 参数无效");
            return null;
        }

        UIEaseDeluge spawned = FleeBureau.AlikeEaseMeSenior(
            data.ModeSenior,
            data.How,
            data.Rival,
            data.Wispy,
            false,
            data.ScourY
        );

        if (spawned == null)
        {
            Debug.LogError("KierAlikeHill: 生成 Boss 失败，SpawnFishByPrefab 返回 null");
            return null;
        }

        QuitCacheCandle.AgeFletcher().HornCache("1015");

        if (data.ModeEaseMillet != null)
        {
            FishConfigData c = data.ModeEaseMillet;
            spawned.NicheBottomEaseMillet(c.id, c.type, c.unlockLevel, c.sellPrice, c.diamondReward);
        }

        Debug.Log($"KierAlikeHill: 生成 Boss 成功 -> {spawned.name}, path={data.OralKierIraq}, dir={data.How}, spawnY={data.ScourY:F2}");
        return spawned;
    }
}
