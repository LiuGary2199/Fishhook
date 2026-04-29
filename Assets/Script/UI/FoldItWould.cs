using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class FoldItWould : ShedUIHobby
{
    // 常量定义
    private const int MAX_SIGN_DAYS= 7;
    private const float REWARD_DELAY= 0.5f;

    [Header("UI Components")]
[UnityEngine.Serialization.FormerlySerializedAs("list_Reward")]    public List<FoldLessonStar> Genu_Lesson;
[UnityEngine.Serialization.FormerlySerializedAs("claimButton")]    public Button claimManage;

    [Header("Private Fields")]
    /// <summary>
    /// 已经签到的天数
    /// </summary>
    private int BlinkExpo= 0;
    private int CryPhaseSmile= 0;
    private bool ToPhase= false;
    
    [Header("Settings")]
[UnityEngine.Serialization.FormerlySerializedAs("hideUnlockedRewards")]    /// <summary>
    /// 是否隐藏未解锁的奖励金额（显示为"？？？"）
    /// </summary>
    public bool RateImitatorUtensil= true;
[UnityEngine.Serialization.FormerlySerializedAs("closebtn")]
    public Button Severity;
    private Tween Fatal;

    public override void Display(object uiFormParams)
    {
        base.Display(uiFormParams);

        try
        {
            CapeSwampAxHimPhase();
            LessonWouldCape();
            CapeFoldItWould();
        }
        catch (Exception e)
        {
            Debug.LogError($"FoldItWould Display error: {e.Message}");
        }
    }

    void Start()
    {
        if (claimManage != null)
        {
            claimManage.onClick.AddListener(OnClaimClick);
        }
        else
        {
            Debug.LogError("FoldItWould: claimButton is null!");
        }
        Severity.onClick.AddListener(() =>
        {
            Fatal?.Kill();
            UIAwesome.AgeFletcher().BloodSoSolelyUIHobby(this.GetType().Name);
        });
    }

    /// <summary>
    /// 签到按钮点击事件
    /// </summary>
    private void OnClaimClick()
    {
        if (!HimGrace())
            return;

        try
        {
           ADAwesome.Fletcher.WoadLessonMount((success) =>
        {
            if (success)
            {
                QuitCacheCandle.AgeFletcher().HornCache("1013", "1");
                // 播放音效
                // ChileElk.GetInstance().PlayEffect(ChileSick.UIMusic.Sound_Progress_Box);
                BlinkExpo++;
            Debug.Log($"After checkNums++: checkNums={BlinkExpo}");
            LoveManual.DewLoveCareless(LoveManual.LoveWe_1, 1);
            // 先保存签到数据
            WhyPhaseGush();
            Debug.Log($"After SetCheckData: checkNums={BlinkExpo}");

            // 重新检查今天是否可签到（确保状态一致）
            CapeSwampAxHimPhase();
            Debug.Log($"After InitTodayIsCanCheck: checkNums={BlinkExpo}, isCheck={ToPhase}");

            // 更新UI状态
            LessonWouldCape();
            ChileElk.AgeFletcher().WifeMisery(ChileSick.UIMusic.Sound_UIButton);
            // 获取奖励
            AgeGraceLesson();
               
            }
        }, "5");           
        }
        catch (Exception e)
        {
            Debug.LogError($"OnClaimClick error: {e.Message}");
        }
    }

    /// <summary>
    /// 检查是否可以领取奖励
    /// </summary>
    private bool HimGrace()
    {
        if (BlinkExpo >= MAX_SIGN_DAYS)
        {
            Debug.Log("已达到最大签到天数");
            return false;
        }

        if (!ToPhase)
        {
            Debug.Log("今天已经签到过了");
            UIAwesome.AgeFletcher().DaleUIHobby(nameof(Briny),"Reward Claimed");
            return false;
        }

        return true;
    }

    /// <summary>
    /// 修改奖励领取状态
    /// </summary>
    private void LessonWouldCape()
    {
        if (TedSlumElk.instance == null)
        {
            Debug.LogError("TedSlumElk.instance is null!");
            return;
        }

        TedSlumElk.instance.CapeFoldItGush();
        List<List<RewardData>> list = TedSlumElk.instance.Ploy_FoldItGush;

        if (list == null || list.Count == 0)
        {
            Debug.LogWarning("SignIn data list is empty");
            return;
        }

        for (int i = 0; i < Genu_Lesson.Count && i < list.Count; i++)
        {
            if (Genu_Lesson[i] != null)
            {
                bool isClaimed = i < BlinkExpo;           // 是否已领取
                bool isAvailable = false;                 // 是否可领取
                bool isLastDay = i == MAX_SIGN_DAYS - 1;  // 是否是最后一天
                bool shouldHideReward = false;            // 是否应该隐藏奖励金额

                // 只有当前可签到的一天才能领取
                if (i == BlinkExpo && ToPhase)
                {
                    isAvailable = true;
                }

                // 判断是否应该隐藏奖励金额
                if (RateImitatorUtensil)
                {
                    // 隐藏条件：不是已领取的 && 不是当前可领取的 && 不是最后一天
                    shouldHideReward = !isClaimed && !isAvailable && !isLastDay;
                }

                // 添加调试信息
                Debug.Log($"Day {i}: checkNums={BlinkExpo}, isCheck={ToPhase}, isClaimed={isClaimed}, isAvailable={isAvailable}, isLastDay={isLastDay}, shouldHideReward={shouldHideReward}");

                Genu_Lesson[i].WhyClaimedEqual(isClaimed);
                Genu_Lesson[i].WhyLessonGush(list[i], isAvailable, shouldHideReward);
            }
        }
    }

    private void AgeGraceLesson()
    {
        // 获取奖励之后刷新界面
        // InitTodayIsCanCheck(); // 已经在OnClaimClick中调用了，这里不需要重复调用
        StartCoroutine(AgeLesson());
    }

    IEnumerator AgeLesson()
    {
        yield return new WaitForSeconds(REWARD_DELAY);

        List<List<RewardData>> list = TedSlumElk.instance.Ploy_FoldItGush;
        RewardData panelData = new RewardData();
        RewardData rewardData = list[CryPhaseSmile - 1][0];
        double rewardValue = rewardData.rewardNum;
        DOVirtual.DelayedCall(0.7f, () =>
        {
            if (PotionUtil.AxApple())
            {
                BarelyIon.ToDewJuicy?.Invoke(null, (int)rewardValue);
            }
            else
            {
                MoteWould.Instance.DewLinkage(rewardValue, this.transform);
            }
        });

        Fatal = DOVirtual.DelayedCall(0.3f, () =>
        {
            Fatal?.Kill();
            UIAwesome.AgeFletcher().BloodSoSolelyUIHobby(this.GetType().Name);
        });
    }

    /// <summary>
    /// 检查今天是否可签到
    /// </summary>
    public void CapeSwampAxHimPhase()
    {
        try
        {
            int[] checkDay = SpotGushAwesome.GetIntArray("CheckDay");
            BlinkExpo = SpotGushAwesome.GetInt("CheckNum");
            CryPhaseSmile = BlinkExpo;

            DateTime dateTime = DateTime.Now;
            int[] time = new int[3] { dateTime.Year, dateTime.Month, dateTime.Day };

            if (checkDay == null || checkDay.Length == 0)
            {
                ToPhase = true;
                Debug.Log("First time signing in, isCheck = true");
            }
            else
            {
                DateTime dt1 = new DateTime(checkDay[0], checkDay[1], checkDay[2]);
                DateTime dt2 = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day);
                TimeSpan span = dt2.Subtract(dt1);

                ToPhase = span.Days > 0;

                Debug.Log($"Last check: {dt1}, Today: {dt2}, Days difference: {span.Days}, isCheck: {ToPhase}");

                // 如果超过7天，重置签到计数
                if (ToPhase && BlinkExpo >= MAX_SIGN_DAYS)
                {
                    BlinkExpo = 0;
                    SpotGushAwesome.SetInt("CheckNum", BlinkExpo);
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"InitTodayIsCanCheck error: {e.Message}");
            ToPhase = false;
        }
    }

    /// <summary>
    /// 保存签到数据
    /// </summary>
    public void WhyPhaseGush()
    {
        try
        {
            SpotGushAwesome.SetInt("CheckNum", BlinkExpo);
            DateTime dateTime = DateTime.Now;
            int[] time = new int[3] { dateTime.Year, dateTime.Month, dateTime.Day };
            SpotGushAwesome.SetIntArray("CheckDay", time);
        }
        catch (Exception e)
        {
            Debug.LogError($"SetCheckData error: {e.Message}");
        }
    }

    public void CapeFoldItWould()
    {
        if (TedSlumElk.instance == null || TedSlumElk.instance.Ploy_FoldItGush == null)
        {
            Debug.LogError("SignIn data not available");
            return;
        }

        List<List<RewardData>> list = TedSlumElk.instance.Ploy_FoldItGush;

        for (int i = 0; i < list.Count && i < Genu_Lesson.Count; i++)
        {
            if (Genu_Lesson[i] != null)
            {
                bool isClaimed = i < BlinkExpo;           // 是否已领取
                bool isAvailable = i == BlinkExpo && ToPhase; // 是否可领取
                bool isLastDay = i == MAX_SIGN_DAYS - 1;  // 是否是最后一天
                bool shouldHideReward = false;            // 是否应该隐藏奖励金额

                // 判断是否应该隐藏奖励金额
                if (RateImitatorUtensil)
                {
                    // 隐藏条件：不是已领取的 && 不是当前可领取的 && 不是最后一天
                    shouldHideReward = !isClaimed && !isAvailable && !isLastDay;
                }

                var textlist = Genu_Lesson[i].Genu_Welt;
                if (textlist != null)
                {
                    for (int j = 0; j < textlist.Count && j < list[i].Count; j++)
                    {
                        if (textlist[j] != null)
                        {
                            if (shouldHideReward)
                            {
                                textlist[j].text = "xxx";
                            }
                            else
                            {
                                textlist[j].text = list[i][j].rewardNum.ToString();
                            }
                        }
                    }
                }
            }
        }
    }
}
