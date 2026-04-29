using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUtil
{
    /// <summary>
    /// 获取multi系数
    /// </summary>
    /// <returns></returns>
    private static double GetMulti(RewardType type, double cumulative, MultiGroup[] multiGroup)
    {
        foreach (MultiGroup item in multiGroup)
        {
            if (item.max > cumulative)
            {
                if (type == RewardType.Cash)
                {
                    float random =  UnityEngine.Random.Range((float)TedSlumElk.instance.CapeGush.cash_random[0], (float)TedSlumElk.instance.CapeGush.cash_random[1]);
                    return item.multi * (1 + random);
                }
                else
                {
                    return item.multi;
                }
            }
        }
        return 1;
    }

    public static double GetGoldMulti()
    {
        return GetMulti(RewardType.Cash, SpotGushAwesome.GetDouble(CMillet.If_LegitimateTalkVice), TedSlumElk.instance.CapeGush.gold_group);
    }

    public static double GetCashMulti()
    {
        return GetMulti(RewardType.Diamond, SpotGushAwesome.GetDouble(CMillet.If_LegitimateFarce), TedSlumElk.instance.CapeGush.cash_group);
    }
    /// <summary>
    /// 获取毫秒级时间戳（跨平台统一）
    /// </summary>
    public static long GetCurrentTimestamp()
    {
        return DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
    }


    public static LuckyObjData GetLuckyCardObjData()
    {
        LuckyObjData luckyObjData = new LuckyObjData();
        List<RewardData> luckyDataListSource = TedSlumElk.instance.ClanGush.lucky_card_data_list;
        List<RewardData> luckyDataList = new List<RewardData>();

        foreach (RewardData item in luckyDataListSource)
        {
            RewardData obj = new RewardData();
            obj.rewardNum = item.rewardNum;
            obj.type = item.type;
            obj.weight = item.weight;
            luckyDataList.Add(obj);
        }

        double maxWeight = 0;
        foreach (RewardData item in luckyDataList)
        {
            double weight = item.weight;
            maxWeight += weight;
        }
        float randomNum = UnityEngine.Random.Range(0, (float)maxWeight);
        double tempNum = 0;
        foreach (RewardData item in luckyDataList)
        {
            tempNum += item.weight;
            if (tempNum >= randomNum)
            {
                luckyObjData.LuckyObjType =item.type;
                luckyObjData.RewardNum = item.rewardNum;
                break;
            }
        }
        switch (luckyObjData.LuckyObjType)
        {
            case RewardType.Diamond:
                luckyObjData.RewardNum = luckyObjData.RewardNum;
                break;
            case RewardType.Cash:
                double cashReward = luckyObjData.RewardNum;
                luckyObjData.RewardNum = Math.Round(cashReward, 2);
                break;
        }
        return luckyObjData;
    }


}



/// <summary>
/// 奖励类型
/// </summary>
public enum RewardType { None, Cash, Diamond }
