using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using Spine.Unity;

public class ScrubSlumWould : ShedUIHobby
{
    public static ScrubSlumWould Instance;
[UnityEngine.Serialization.FormerlySerializedAs("luckyCardList")]    public List<GameObject> ExistSlumPloy;
[UnityEngine.Serialization.FormerlySerializedAs("selectObjList")]    public List<GameObject> AgencyLapPloy;
[UnityEngine.Serialization.FormerlySerializedAs("rewardMap")]    public Dictionary<RewardType, double> PoorlyJay;
[UnityEngine.Serialization.FormerlySerializedAs("luckyObjDataList")]    public List<LuckyObjData> ExistLapGushPloy;
[UnityEngine.Serialization.FormerlySerializedAs("isLock")]    public bool ToBath;
    private bool ToMelt;
[UnityEngine.Serialization.FormerlySerializedAs("onThanksWeight")]    public int NoCrunchBounce;

    private int SillTruck;
    private int DamRoeTruck;
    RewardData Lesson;

    protected override void Awake()
    {
        base.Awake();
        Instance = this;
        DamRoeTruck = TedSlumElk.instance.ClanGush.lucky_card_win_max_count;
    }

    public override void Display(object uiFormParams)
    {
        base.Display(uiFormParams);
        SpotGushAwesome.SetInt(CMillet.If_Worry_Pheromone, SpotGushAwesome.GetInt(CMillet.If_Worry_Pheromone) + 1);
        QuitCacheCandle.AgeFletcher().HornCache("1004");
        ADAwesome.Fletcher.BladeDutyRenunciation();
        CapeScrubSlum();
       // ChileElk.GetInstance().PlayEffect(ChileSick.UIMusic.sound_littlegame_show);
    }
    public override void Hidding()
    {
        base.Hidding();
        ADAwesome.Fletcher.SecureDutyRenunciation();
    }

    private void BloodScrubSlumWould()
    {
        if (!gameObject.activeInHierarchy) return;
        BloodUIJazz(GetType().Name);
    }

    public void CapeScrubSlum()
    {
        CancelInvoke(nameof(DoJet));
        CancelInvoke(nameof(MarkBath));

        int maxExclusive = Mathf.Max(3, DamRoeTruck);
        SillTruck = Mathf.Max(2, Random.Range(2, maxExclusive) + 1);
        ExistLapGushPloy = new List<LuckyObjData>();
        ToBath = true;
        ToMelt = false;
        for (int i = 0; i < ExistSlumPloy.Count; i++)
        {
            GameObject obj = ExistSlumPloy[i].gameObject;
            if (i == 4)
            {
                obj.GetComponent<LuckyCardController>().CapeCrunchLapGush();
            }
            else
            {
                LuckyObjData objData = GameUtil.GetLuckyCardObjData();
                ExistLapGushPloy.Add(objData);
                obj.GetComponent<LuckyCardController>().CapeLessonLapGush(objData);
            }
        }

        AgencyLapPloy = new List<GameObject>();
        PoorlyJay = new Dictionary<RewardType, double>();

        Invoke(nameof(DoJet), 0.5f);
    }


    private void DoJet()
    {
        for (int i = 0; i < ExistSlumPloy.Count; i++)
        {
            GameObject obj = ExistSlumPloy[i].gameObject;
            // 避免玩家在 DoAct 执行前已翻开的卡被强制复位翻回去
            if (AgencyLapPloy != null && AgencyLapPloy.Contains(obj))
                continue;
            obj.GetComponent<LuckyCardController>().BloodLap();
        }
        Invoke(nameof(MarkBath), 0.5f);
    }

    private void MarkBath()
    {
        ToBath = false;
    }

    private void DewLessonJay(LuckyObjData rewardObj)
    {
        RewardType PoorlySick= rewardObj.LuckyObjType;
        if (PoorlyJay.ContainsKey(PoorlySick))
        {
            PoorlyJay[PoorlySick] =
                PoorlyJay[PoorlySick] + rewardObj.RewardNum;
        }
        else
        {
            PoorlyJay.Add(PoorlySick, rewardObj.RewardNum);
        }
    }

    private void DaleHorseWould()
    {
        PoorlyJay.TryGetValue(RewardType.Diamond, out double diamondTotal);
        Lesson = diamondTotal > 0
            ? new RewardData { type = RewardType.Diamond, rewardNum = diamondTotal }
            : null;

       BloodUIJazz(nameof(ScrubSlumWould));
            UIAwesome.AgeFletcher().DaleUIHobby(nameof(LessonWould)).GetComponent<LessonWould>().Cape(null, Lesson, 
            ()=>{
                        BarelyIon.ToSewageClanDormancy?.Invoke();
            }, "1004");
    }

    public void DewTundraPloy(GameObject obj)
    {
        if (ToBath) return;
        if (ToMelt) return;
        if (AgencyLapPloy.Contains(obj)) return;

        AgencyLapPloy.Add(obj);
        var ctrl = obj.GetComponent<LuckyCardController>();
        if (AgencyLapPloy.Count < SillTruck && !ToMelt)
        {
            if (ExistLapGushPloy.Count == 0)
            {
                ctrl.WifeDisc();
                ctrl.WhyEntrepreneur(false);
            }
            else
            {
                int num = Random.Range(0, ExistLapGushPloy.Count);
                LuckyObjData objData = ExistLapGushPloy[num];
                ctrl.WifeDisc();
                ctrl.CapeLessonLapGush(objData, resetAnimToIdle: false);
                DewLessonJay(objData);
                ExistLapGushPloy.Remove(objData);
                ctrl.WhyEntrepreneur(false);
            }
        }
        else
        {
            ToMelt = true;
            ToBath = true;
            Debug.Log($"[LuckyCard] 游戏结束: 已翻牌数={AgencyLapPloy.Count}, 目标次数={SillTruck}, 奖励种类数={PoorlyJay.Count}");
            ctrl.WifeDisc();
            ctrl.DaleCrunchVest();
            ctrl.WhyEntrepreneur(false);
            Invoke(nameof(DaleHorseWould), 2f);
        }
    }
}