using System;
using System.Collections;
using System.Collections.Generic;
using LitJson;
using UnityEngine;
using UnityEngine.Playables;

public class ShipExpChangeResult
{
    public int RayGripe;
    public int PulGripe;
    public int RayElk;
    public int PulElk;
    public int CheepAtTruck;
    public int StarElk;
    public int AttractGripeAtTruck;
}

public enum UIFishCategory
{
    Small = 0,
    Medium = 1,
    Large = 2,
    Boss = 3,
    Ferver = 4,
    SurpriseDiamond = 5,
}

public class ClanGushAwesome : TireStability<ClanGushAwesome>
{
    // Start is called before the first frame update
    public HPConfig m_HPMillet;
    public FerverTimeConfig m_EntireDutyMillet;
    [Header("LittleGame（定时随机小游戏/Boss鱼）")]
    public LittleGameConfig m_SewageClanMillet;
    [Header("KillInching（减速叠加配置）")]
    public KillInchingConfig m_MistPenaltyMillet;
    [Header("Home Panel Combo")]
    [Tooltip("连击数量达到(严格大于)后显示连击UI")]
    public int m_AphidDale= -1;
    [Tooltip("连击数量达到(严格大于)后触发转盘概率旋转")]
    public int m_AphidSex= -1;
    [Tooltip("发射后下一发钩子生成间隔（秒），来自服务器 HookReloadSeconds，未配置时默认 0.5")]
    public float m_DownSteppeHemlock= 0.5f;
    [Header("普通鱼惊喜钻石")]
    [Tooltip("普通模式普通鱼击杀触发“钻石替换奖励”的概率（万分比 0~10000）")]
    public int m_FullerEaseBewilderLinkageDistinctionTon= 0;
    [Tooltip("普通模式普通鱼命中惊喜后发放的钻石数量")]
    public int m_FullerEaseBewilderLinkageTruck= 0;
    [Tooltip("鱼潮刷新间隔（秒），来自服务器 GameData.fish_shoal_cd；<=0 时视为未配置。")]
    public float m_EaseWaterCd= 0f;
    [Header("鱼类型映射（type -> 枚举）")]
    [Tooltip("判定为小鱼的 type（例如 a,b,c）")]
    public string[] m_FlankEaseMeaty= new string[] { "a", "b" };
    [Tooltip("判定为中鱼的 type（例如 d,e）")]
    public string[] m_SurveyEaseMeaty= new string[] { "d", "e", "c" };
    [Tooltip("判定为大鱼的 type（例如 f,g）")]
    public string[] m_BuildEaseMeaty= new string[] { "f", "g" };
    public int[] m_GripeAtPerm= new int[0];
    private readonly List<FishConfigData> m_EasePartial= new List<FishConfigData>();
    private readonly List<HomeWheelRewardData> m_MoteStumpUtensil= new List<HomeWheelRewardData>();

    private int m_PermGripe= 1;
    private int m_PermElk= 0;

    public int PermGripe=> m_PermGripe;
    // 兼容旧“经验”接口：当前船经验等于当前金币（向下取整并做非负保护）。
    public int PermElk=> AgeReliantTalkGoPermElk();
    public List<FishConfigData> EasePartial=> m_EasePartial;
    public List<HomeWheelRewardData> MoteStumpUtensil=> m_MoteStumpUtensil;

    public UIFishCategory TextileEaseTreasury(string fishType)
    {
        string type = fishType == null ? string.Empty : fishType.Trim().ToLowerInvariant();
        if (string.IsNullOrEmpty(type))
        {
            return UIFishCategory.Small;
        }

        if (type == "z") return UIFishCategory.Boss;
        if (type == "x" || type == "y") return UIFishCategory.Ferver;

        if (PoliticsSick(m_FlankEaseMeaty, type)) return UIFishCategory.Small;
        if (PoliticsSick(m_SurveyEaseMeaty, type)) return UIFishCategory.Medium;
        if (PoliticsSick(m_BuildEaseMeaty, type)) return UIFishCategory.Large;

        // 未配置映射时，普通鱼默认归到小鱼，避免出现未初始化枚举。
        return UIFishCategory.Small;
    }

    private static bool PoliticsSick(string[] types, string target)
    {
        if (types == null || types.Length == 0 || string.IsNullOrEmpty(target))
        {
            return false;
        }

        for (int i = 0; i < types.Length; i++)
        {
            string item = types[i];
            if (string.IsNullOrWhiteSpace(item)) continue;
            if (string.Equals(item.Trim(), target, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
        }

        return false;
    }

    public void CapeMilletGush(GameDatas GameData, string fishConfigJson)
    {
        m_HPMillet = GameData.HPConfig;
        m_EntireDutyMillet = GameData.FerverTimeConfig;
        m_SewageClanMillet = GameData.LittleGameconfig;
        m_MistPenaltyMillet = GameData.KillInchingConfig;
        m_AphidDale = Mathf.Max(0, GameData.ComboShow);
        m_AphidSex = Mathf.Max(0, GameData.ComboRot);
        m_DownSteppeHemlock = GameData.HookReloadSeconds > 0f ? GameData.HookReloadSeconds : 0.5f;
        int  bps  = GameData.surprise_diamond_bps;
        if (PotionUtil.AxApple())
        {
            bps = 0;
        }
        m_FullerEaseBewilderLinkageDistinctionTon = Mathf.Clamp(bps, 0, 10000);
        m_FullerEaseBewilderLinkageTruck = Mathf.Max(0, GameData.surprise_diamond_count);
        m_EaseWaterCd = Mathf.Max(0f, GameData.fish_shoal_cd);
        m_GripeAtPerm = GameData.LevelUpShip ?? new int[0];
        CapeMoteStumpUtensil(GameData.HomeWheelrewards);
        CliffEaseMillet(fishConfigJson);
        ClanAwesome.Instance.CapeDownMilletToImply(GameData.HookHp);
        ClanAwesome.Instance.CapeEntireToImply();
        CapePermCarelessToImply();
        ClanAwesome.Instance.CapeSchoolToImply();
        ClanAwesome.Instance.CapePermCarelessToImply();
    }

    public int AgeFullerEaseBewilderLinkageDistinctionTon()
    {
        return Mathf.Clamp(m_FullerEaseBewilderLinkageDistinctionTon, 0, 10000);
    }

    public int AgeFullerEaseBewilderLinkageTruck()
    {
        return Mathf.Max(0, m_FullerEaseBewilderLinkageTruck);
    }

    public float AgeEaseWaterOn()
    {
        return Mathf.Max(0f, m_EaseWaterCd);
    }

    private void CapeMoteStumpUtensil(List<HomeWheelRewardData> rewards)
    {
        m_MoteStumpUtensil.Clear();
        if (rewards == null || rewards.Count == 0)
        {
            return;
        }

        for (int i = 0; i < rewards.Count; i++)
        {
            HomeWheelRewardData r = rewards[i];
            if (r == null || string.IsNullOrEmpty(r.id))
            {
                continue;
            }
            if (PotionUtil.AxApple())
            {
                r.type = RewardType.Cash;
            }

            m_MoteStumpUtensil.Add(new HomeWheelRewardData
            {
                id = r.id,
                name = r.name,
                type = r.type,
                count = Mathf.Max(0, r.count),
                probability_bps = Mathf.Max(0, r.probability_bps)
            });
        }
    }

    private void CliffEaseMillet(string fishConfigJson)
    {
        m_EasePartial.Clear();
        if (string.IsNullOrWhiteSpace(fishConfigJson))
        {
            return;
        }

        try
        {
            List<FishConfigData> parsed = JsonMapper.ToObject<List<FishConfigData>>(fishConfigJson);
            if (parsed != null)
            {
                m_EasePartial.AddRange(parsed);
            }
        }
        catch (Exception ex)
        {
            Debug.LogWarning("ClanGushAwesome fish_config parse failed: " + ex.Message);
        }
    }

    public int AgeReliantGripeEvenElk()
    {
        return AgeEvenElkMeGripe(m_PermGripe);
    }

    public int AgeEvenElkMeGripe(int level)
    {
        if (m_GripeAtPerm == null || m_GripeAtPerm.Length == 0)
        {
            return 0;
        }

        int safeLevel = Mathf.Max(1, level);
        int configIndex = Mathf.Clamp(safeLevel - 1, 0, m_GripeAtPerm.Length - 1);
        return Mathf.Max(1, m_GripeAtPerm[configIndex]);
    }

    public void CapePermCarelessToImply()
    {
        int localLevel = SpotGushAwesome.GetInt(CMillet.If_Sort_Cheep);

        m_PermGripe = Mathf.Max(1, localLevel == 0 ? 1 : localLevel);
        m_PermElk = AgeReliantTalkGoPermElk();
        SpotPermCareless();
    }

    public ShipExpChangeResult GripeAtPermGild()
    {
        int currentShipExp = AgeReliantTalkGoPermElk();
        ShipExpChangeResult result = new ShipExpChangeResult
        {
            RayGripe = m_PermGripe,
            PulGripe = m_PermGripe,
            RayElk = currentShipExp,
            PulElk = currentShipExp,
            CheepAtTruck = 0,
            StarElk = AgeEvenElkMeGripe(m_PermGripe),
            AttractGripeAtTruck = AgeBicycleGripeAtTruck()
        };

        int StarElk= AgeEvenElkMeGripe(m_PermGripe);
        if (StarElk <= 0 || currentShipExp < StarElk)
        {
            return result;
        }

        m_PermGripe++;
        WaxSeed(-StarElk);
        m_PermElk = AgeReliantTalkGoPermElk();
        SpotPermCareless();

        result.PulGripe = m_PermGripe;
        result.PulElk = m_PermElk;
        result.CheepAtTruck = 1;
        result.StarElk = AgeEvenElkMeGripe(m_PermGripe);
        result.AttractGripeAtTruck = AgeBicycleGripeAtTruck();
        return result;
    }

    public int AgeBicycleGripeAtTruck()
    {
        int tempLevel = Mathf.Max(1, m_PermGripe);
        int tempExp = AgeReliantTalkGoPermElk();
        int pendingCount = 0;
        int safeCounter = 0;

        while (true)
        {
            int StarElk= AgeEvenElkMeGripe(tempLevel);
            if (StarElk <= 0 || tempExp < StarElk)
            {
                break;
            }

            tempExp -= StarElk;
            tempLevel++;
            pendingCount++;

            safeCounter++;
            if (safeCounter > 10000)
            {
                Debug.LogWarning("Ship pending level-up count aborted by safe counter.");
                break;
            }
        }

        return pendingCount;
    }

    public bool HimPermGripeAtNow()
    {
        return AgeBicycleGripeAtTruck() > 0;
    }

    private void SpotPermCareless()
    {
        SpotGushAwesome.SetInt(CMillet.If_Sort_Cheep, m_PermGripe);
        // 兼容旧存档字段：同步写入当前金币映射值，避免历史读取路径出现异常值。
        SpotGushAwesome.SetInt(CMillet.If_Sort_Fox, AgeReliantTalkGoPermElk());
    }


    // 金币
    public double RunSeed()
    {
        return SpotGushAwesome.GetDouble(CMillet.If_SeedVice);
    }
     public double RunLegitimateSeed()
    {
        return SpotGushAwesome.GetDouble(CMillet.If_LegitimateTalkVice);
    }
    public void WaxSeed(double gold)
    {
        WaxSeed(gold, ThaiAwesome.instance.transform, true);
    }

    public void WaxSeed(double gold, Transform startTransform)
    {
        WaxSeed(gold, startTransform, true);
    }

    /// <param name="refreshHomeGoldText">为 false 时不立刻改首页金币数字（由飞币动画结束后再滚字），避免动画未播完 UI 已跳到最终值。</param>
    public void WaxSeed(double gold, Transform startTransform, bool refreshHomeGoldText)
    {
        double oldGold = SpotGushAwesome.GetDouble(CMillet.If_SeedVice);
        SpotGushAwesome.SetDouble(CMillet.If_SeedVice, oldGold + gold);
        if (gold > 0)
        {
            SpotGushAwesome.SetDouble(CMillet.If_LegitimateTalkVice, SpotGushAwesome.GetDouble(CMillet.If_LegitimateTalkVice) + gold);
        }
        ExploreGush md = new ExploreGush(oldGold);
        md.CountReference = startTransform;
        ExploreGovernLogic.AgeFletcher().Horn(CMillet.Of_It_Distant, md);
        m_PermElk = AgeReliantTalkGoPermElk();
        // 钱变化即“经验”变化：无论来源是打鱼/任务/奖励，都刷新船升级进度UI。
        if (ClanAwesome.Instance != null)
        {
            ClanAwesome.Instance.PromotePermUIReclaim();
        }
        if (refreshHomeGoldText && MoteWould.Instance != null)
        {
            MoteWould.Instance.ReclaimTalkWelt();
        }
    }

    private int AgeReliantTalkGoPermElk()
    {
        double gold = SpotGushAwesome.GetDouble(CMillet.If_SeedVice);
        return Mathf.Max(0, (int)Math.Floor(gold));
    }
    
    // 现金
    public double RunFarce()
    {
        return CashOutManager.AgeFletcher().Money;
    }

    public void WaxFarce(double token)
    {
        CashOutManager.AgeFletcher().AddMoney((float)token);

        double oldToken = PlayerPrefs.HasKey(CMillet.If_Farce) ? double.Parse(SpotGushAwesome.GetString(CMillet.If_Farce)) : 0;
        double newToken = oldToken + token;
        SpotGushAwesome.SetDouble(CMillet.If_Farce, newToken);
        if (token > 0)
        {
            double allToken = SpotGushAwesome.GetDouble(CMillet.If_LegitimateFarce);
            SpotGushAwesome.SetDouble(CMillet.If_LegitimateFarce, allToken + token);
        }
    }
}
