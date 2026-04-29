using DG.Tweening;
using Spine;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


/// <summary> 大奖面板 树升级和疯狂模式结束使用  基本逻辑和RewardPanel相同
/// </summary>
public class LessonWould : ShedUIHobby
{
[UnityEngine.Serialization.FormerlySerializedAs("Gold")]
    public GameObject Talk;
[UnityEngine.Serialization.FormerlySerializedAs("GoldText")]    public TextMeshProUGUI TalkWelt;
    double TalkLesson;
[UnityEngine.Serialization.FormerlySerializedAs("Cash")]    public GameObject Seed;
[UnityEngine.Serialization.FormerlySerializedAs("CashText")]    public TextMeshProUGUI SeedWelt;
    double SeedLesson;
[UnityEngine.Serialization.FormerlySerializedAs("AdGetBtn")]    public Button OfAgeLad;
[UnityEngine.Serialization.FormerlySerializedAs("GetBtn")]    public Button AgeLad;
[UnityEngine.Serialization.FormerlySerializedAs("FinishEvent")]    public UnityAction WalkerCache;
[UnityEngine.Serialization.FormerlySerializedAs("RewardShowList")]    public List<GameObject> LessonDalePloy;
    Coroutine NomadDaleAgeLad;
[UnityEngine.Serialization.FormerlySerializedAs("m_ShipSkeleton")]    public SkeletonGraphic m_PermAllusion;
[UnityEngine.Serialization.FormerlySerializedAs("rewaobj")]    public GameObject Primary;
    string AxCompanyAD;
    string CacheID;


    void Start()
    {
        m_PermAllusion.AnimationState.Complete += OnShipAnimComplete;
        OfAgeLad.onClick.AddListener(() =>
        {
            ADAwesome.Fletcher.WoadLessonMount((ok) =>
            {
                if (ok)
                {
                    DutyAwesome.AgeFletcher().SectNomad(NomadDaleAgeLad);
                    OfAgeLad.gameObject.SetActive(false);
                    AgeLad.gameObject.SetActive(false);
                    AxCompanyAD = "1";
                    TraditionDemobilize.BorderClause(TalkLesson, TalkLesson * 2, 0.1f, TalkWelt, null);
                    TraditionDemobilize.BorderClause(SeedLesson, SeedLesson * 2, 0.1f, SeedWelt, null);
                    TalkLesson = TalkLesson * 2;
                    SeedLesson = SeedLesson * 2;
                    DutyAwesome.AgeFletcher().Nomad(1f, () =>
                              {

                                  AgeLessonOffSendCacheOffBlood();
                              });
                }
            }, AgeCacheSmile());
        });
        AgeLad.onClick.AddListener(() =>
        {
            AxCompanyAD = "0";
            TalkLesson = TalkLesson;
            SeedLesson = SeedLesson;
            DutyAwesome.AgeFletcher().Nomad(0.5f, () =>
             {

                 AgeLessonOffSendCacheOffBlood();
             });
            ADAwesome.Fletcher.OxCrunchDewTruck();
        });
    }
    public void OnShipAnimComplete(TrackEntry trackEntry)
    {
        if (trackEntry == null || trackEntry.Animation == null) return;
        if (trackEntry.Animation.Name == "start")
        {
            m_PermAllusion.AnimationState.SetAnimation(0, "idle", true);
        }
    }
    public void Cape(RewardData ColdDate, RewardData CashDate, UnityAction FinishEvent, string EventID = "")
    {
        ChileElk.AgeFletcher().WifeMisery(ChileSick.UIMusic.rewardPanel);
        Primary.gameObject.SetActive(false);
        OfAgeLad.gameObject.SetActive(false);
        m_PermAllusion.gameObject.SetActive(true);
        m_PermAllusion.Skeleton.SetToSetupPose();
        m_PermAllusion.AnimationState.ClearTracks();
        m_PermAllusion.AnimationState.SetAnimation(0, "start", false);
        DutyAwesome.AgeFletcher().Nomad(0.6f, () =>
        {
            OfAgeLad.gameObject.SetActive(true);
            Primary.gameObject.SetActive(true);
        });

        this.WalkerCache = FinishEvent;
        this.CacheID = EventID;
        if (PotionUtil.AxApple() && CashDate != null) // 现金换成金币
        {
            SeedLesson = 0;
            if (ColdDate == null)
                ColdDate = new RewardData() { rewardNum = CashDate.rewardNum, type = RewardType.Cash };
            else
                ColdDate.rewardNum += CashDate.rewardNum;
            CashDate = null;
        }
        TalkLesson = ColdDate != null ? ColdDate.rewardNum : 0;
        SeedLesson = CashDate != null ? CashDate.rewardNum : 0;
        Talk.SetActive(false);
        Seed.SetActive(false);
        if (ColdDate != null && ColdDate.rewardNum > 0)
        {
            Talk.SetActive(true);
            TraditionDemobilize.BorderClause(0, ColdDate.rewardNum, 0.1f, TalkWelt, null);
        }
        if (CashDate != null && CashDate.rewardNum > 0)
        {
            Seed.SetActive(true);
            TraditionDemobilize.BorderClause(0, CashDate.rewardNum, 0.1f, SeedWelt, null);
        }
        //AdGetBtn.gameObject.SetActive(true);
        AgeLad.gameObject.SetActive(false);
        NomadDaleAgeLad = DutyAwesome.AgeFletcher().Nomad(2, () =>
        {
            AgeLad.gameObject.SetActive(true);
            AgeLad.transform.localScale = new Vector3(0, 0, 0);
            AgeLad.transform.DOScale(1, 0.2f).SetEase(Ease.OutBack);
        });
        //  ChileElk.GetInstance().PlayEffect(ChileSick.UIMusic.SFX_BigWin);

    }

    void AgeLessonOffSendCacheOffBlood()
    {
        if (TalkLesson > 0)
        {
            BarelyIon.ToDewJuicy?.Invoke(null, (int)TalkLesson);
        }
        if (SeedLesson > 0)
        {
            GameObject Icon = MoteWould.Instance.m_LinkageTough.gameObject;
            DOVirtual.DelayedCall(0.7f, () =>
            {
                MoteWould.Instance.DewLinkage(SeedLesson);
            });

        }

        WalkerCache?.Invoke();
        WalkerCache = null;
        m_PermAllusion.gameObject.SetActive(false);
        QuitCacheCandle.AgeFletcher().HornCache(AgeClanCacheSmile(),AxCompanyAD);
        BloodUIJazz(nameof(LessonWould));
    }

    string AgeCacheSmile()
    {
        if (CacheID == "1004") //树升级
            return "1";
        else if (CacheID == "1006") //fever
            return "2";
        else if (CacheID == "1009") //转盘
            return "3";
        else if (CacheID == "1010") //刮刮卡
            return "4";
        else if (CacheID == "1017") //飞行气泡
            return "11";
        else
            return "0";
    }
     string AgeClanCacheSmile()
    {
        if (CacheID == "1004") //树升级
            return "1005";
        else if (CacheID == "1006") //fever
            return "1007";
        else if (CacheID == "1009") //转盘
            return "3";
        else if (CacheID == "1010") //刮刮卡
            return "4";
        else if (CacheID == "1017") //飞行气泡
            return "11";
        else
            return "0";
    }

    private void OnEnable()
    {
        float Delaytime = 1.5f;
        for (int i = 0; i < LessonDalePloy.Count; i++)
        {
            GameObject item = LessonDalePloy[i];
            item.transform.localScale = Vector3.zero;
            item.transform.DOScale(1, 0.3f).SetEase(Ease.OutBack).SetDelay(Delaytime);
            Delaytime += 0.1f;
        }
    }
}
