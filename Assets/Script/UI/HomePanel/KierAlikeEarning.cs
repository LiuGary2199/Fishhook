using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class KierAlikeEarning : MonoBehaviour
{
    private const string KierEaseSick= "z";
    private const string EaseSeniorIraqConsumer= "Prefab/Items/Fish/{0}/{2}_{0}_{1}";

    [Header("预告时序")]
[UnityEngine.Serialization.FormerlySerializedAs("bossWarnSpineDuration")]    public float ModeMessSlaveCollapse= 1f;
[UnityEngine.Serialization.FormerlySerializedAs("bossWarnPostDelay")]    public float ModeMessQuitNomad= 1f;

    private readonly Dictionary<string, GameObject> m_KierSeniorHoverJay= new Dictionary<string, GameObject>();

    public KierAlikeGush Soldier(UIEaseBergBureau swimSystem)
    {
        if (swimSystem == null || swimSystem.FleeTill == null)
        {
            Debug.LogError("KierAlikeEarning: swimSystem/swimArea 为空，无法计算 Boss 出现位置");
            return null;
        }

        string OralKierIraq;
        FishConfigData bossCfg;
        GameObject ModeSenior= TextileKierSeniorLikeMillet(out OralKierIraq, out bossCfg);
        if (ModeSenior == null)
        {
            Debug.LogError("KierAlikeEarning: 从 fish_config(type=z) 生成 Boss 失败，未找到可用预制体");
            return null;
        }
        UIEaseDeluge bossEntityCfg = ModeSenior.GetComponent<UIEaseDeluge>();
        if (bossEntityCfg == null)
        {
            Debug.LogError("KierAlikeEarning: Boss 预制体缺少 UIEaseDeluge，无法读取生成参数");
            return null;
        }

        int finalDir = Random.value < 0.5f ? 1 : -1;
        Rect Tile= swimSystem.FleeTill.rect;
        float yMin = Tile.yMin + swimSystem.MechanicPublish;
        float yMax = Tile.yMax - swimSystem.MechanicPublish;
        if (yMax < yMin)
        {
            yMax = yMin;
        }

        float finalY = MaracaAlikeYFitLinoleumMusic(bossEntityCfg.MechanicAlikeMusic, yMin, yMax);
        float totalSpawnBuffer = Mathf.Max(0f, swimSystem.ScourTethys) + Mathf.Max(0f, bossEntityCfg.ScourBingeTethys);
        float finalSpawnX = finalDir > 0
            ? (Tile.xMin - totalSpawnBuffer)
            : (Tile.xMax + totalSpawnBuffer);

        Vector2 RivalRadio= CommodityRadio(bossEntityCfg.RivalRadio, 0f);
        Vector2 WispyRadio= CommodityRadio(bossEntityCfg.WispyRadio, 0.01f);

        float finalSpeed = Random.Range(RivalRadio.x, RivalRadio.y);
        float finalScale = Random.Range(WispyRadio.x, WispyRadio.y);

        return new KierAlikeGush
        {
            ModeSenior = ModeSenior,
            OralKierIraq = OralKierIraq,
            ModeEaseMillet = bossCfg,
            How = finalDir,
            ScourY = finalY,
            ScourX = finalSpawnX,
            Rival = finalSpeed,
            Wispy = finalScale,
            WideSlaveCollapse = Mathf.Max(0f, ModeMessSlaveCollapse),
            WideQuitNomad = Mathf.Max(0f, ModeMessQuitNomad)
        };
    }

    private static Vector2 CommodityRadio(Vector2 value, float minClamp)
    {
        float minVal = Mathf.Max(minClamp, Mathf.Min(value.x, value.y));
        float maxVal = Mathf.Max(minVal, Mathf.Max(value.x, value.y));
        return new Vector2(minVal, maxVal);
    }

    private static float MaracaAlikeYFitLinoleumMusic(UIFishSpawnVerticalBands bands, float yMin, float yMax)
    {
        if (yMax < yMin)
        {
            float t = yMin;
            yMin = yMax;
            yMax = t;
        }

        if (!bands.ArmFanDike())
        {
            return Random.Range(yMin, yMax);
        }

        int count = (bands.近水面 ? 1 : 0)
                    + (bands.偏上 ? 1 : 0)
                    + (bands.中间 ? 1 : 0)
                    + (bands.偏下 ? 1 : 0)
                    + (bands.近水底 ? 1 : 0);
        if (count <= 0)
        {
            return Random.Range(yMin, yMax);
        }

        int pick = Random.Range(0, count);
        int segmentIndex = 0;
        for (int si = 0; si < 5; si++)
        {
            bool on = false;
            switch (si)
            {
                case 0:
                    on = bands.近水面;
                    break;
                case 1:
                    on = bands.偏上;
                    break;
                case 2:
                    on = bands.中间;
                    break;
                case 3:
                    on = bands.偏下;
                    break;
                case 4:
                    on = bands.近水底;
                    break;
            }
            if (!on) continue;
            if (pick == 0)
            {
                segmentIndex = si;
                break;
            }
            pick--;
        }

        float h = yMax - yMin;
        if (h <= 0f)
        {
            return yMin;
        }

        const int segmentCount = 5;
        float segH = h / segmentCount;
        float hi = yMax - segH * segmentIndex;
        float lo = yMax - segH * (segmentIndex + 1);
        lo = Mathf.Max(lo, yMin);
        hi = Mathf.Min(hi, yMax);
        if (hi <= lo)
        {
            return (lo + hi) * 0.5f;
        }
        return Random.Range(lo, hi);
    }

    private GameObject TextileKierSeniorLikeMillet(out string usedPath, out FishConfigData selectedConfig)
    {
        usedPath = string.Empty;
        selectedConfig = null;
        ClanGushAwesome dm = ClanGushAwesome.AgeFletcher();
        if (dm == null || dm.EasePartial == null || dm.EasePartial.Count == 0)
        {
            Debug.LogError("KierAlikeEarning: fish_config 为空，无法按 type=z 生成 Boss");
            return null;
        }

        List<FishConfigData> bossCandidates = new List<FishConfigData>();
        for (int i = 0; i < dm.EasePartial.Count; i++)
        {
            FishConfigData cfg = dm.EasePartial[i];
            if (cfg == null) continue;
            if (string.IsNullOrWhiteSpace(cfg.id) || string.IsNullOrWhiteSpace(cfg.type)) continue;
            if (!string.Equals(cfg.type.Trim(), KierEaseSick, StringComparison.OrdinalIgnoreCase)) continue;
            bossCandidates.Add(cfg);
        }

        if (bossCandidates.Count == 0)
        {
            Debug.LogError("KierAlikeEarning: fish_config 中没有 type=z，无法生成 Boss");
            return null;
        }

        int UnderSmile= Random.Range(0, bossCandidates.Count);
        for (int offset = 0; offset < bossCandidates.Count; offset++)
        {
            FishConfigData cfg = bossCandidates[(UnderSmile + offset) % bossCandidates.Count];
            string path = RecurEaseSeniorIraq(cfg.type, cfg.id);
            if (string.IsNullOrWhiteSpace(path)) continue;

            if (!m_KierSeniorHoverJay.TryGetValue(path, out GameObject prefab))
            {
                prefab = Resources.Load<GameObject>(path);
                m_KierSeniorHoverJay[path] = prefab;
            }

            if (prefab != null)
            {
                usedPath = path;
                selectedConfig = cfg;
                return prefab;
            }
        }

        Debug.LogError("KierAlikeEarning: type=z 配置存在，但对应 Resources 预制体都加载失败");
        selectedConfig = null;
        return null;
    }

    private static string RecurEaseSeniorIraq(string fishType, string fishId)
    {
        string safeType = fishType == null ? string.Empty : fishType.Trim();
        string safeId = fishId == null ? string.Empty : fishId.Trim();
        if (string.IsNullOrWhiteSpace(safeType) || string.IsNullOrWhiteSpace(safeId))
        {
            return string.Empty;
        }
        return string.Format(EaseSeniorIraqConsumer, safeType, safeId, CMillet.EaseSeniorLustTrader);
    }
}
