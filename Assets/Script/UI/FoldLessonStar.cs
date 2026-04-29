using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoldLessonStar : MonoBehaviour
{
    [Header("UI Components")]
[UnityEngine.Serialization.FormerlySerializedAs("dayText")]    public Text SkiWelt;
[UnityEngine.Serialization.FormerlySerializedAs("claimedImage")]    public GameObject GeneticTough;
[UnityEngine.Serialization.FormerlySerializedAs("reward")]
    public GameObject Poorly;
[UnityEngine.Serialization.FormerlySerializedAs("fx_Parent")]    public GameObject By_Female;

    [Header("Reward Lists")]
[UnityEngine.Serialization.FormerlySerializedAs("list_Reward")]    public List<Image> Genu_Lesson= new List<Image>();
[UnityEngine.Serialization.FormerlySerializedAs("list_Text")]    public List<Text> Genu_Welt= new List<Text>();

    public GameObject cashImage;
    public GameObject DiamondImage;
    /// <summary>
    /// 设置天数文本
    /// </summary>
    /// <param name="str">天数文本</param>
    public void WhyGetWelt(string str)
    {
        if (SkiWelt != null)
        {
            SkiWelt.text = str;
        }
        else
        {
            Debug.LogWarning("FoldLessonStar: dayText is null!");
        }
    }

    /// <summary>
    /// 设置领取状态
    /// </summary>
    /// <param name="isCla">true表示已领取，false表示未领取</param>
    public void WhyClaimedEqual(bool isCla)
    {
        if (GeneticTough != null)
        {
            GeneticTough.SetActive(isCla);
        }
        else
        {
            Debug.LogWarning("FoldLessonStar: claimedImage is null!");
        }
    }

    /// <summary>
    /// 设置奖励状态和数据显示
    /// </summary>
    /// <param name="list">奖励数据列表</param>
    /// <param name="isReward">true表示可领取，false表示不可领取</param>
    /// <param name="hideRewardAmount">true表示隐藏奖励金额（显示为"？？？"），false表示显示实际金额</param>
    public void WhyLessonGush(List<RewardData> list, bool isReward, bool hideRewardAmount = false)
    {
        if (list == null)
        {
            Debug.LogWarning("FoldLessonStar: RewardData list is null!");
            return;
        }

        // 设置奖励对象状态（可领取时显示）
        if (Poorly != null)
        {
            Poorly.SetActive(isReward);
        }

        // 设置奖励数据显示
        WhyLessonAcreage(list, hideRewardAmount);
    }

    /// <summary>
    /// 设置奖励显示数据
    /// </summary>
    /// <param name="list">奖励数据列表</param>
    /// <param name="hideRewardAmount">true表示隐藏奖励金额（显示为"？？？"），false表示显示实际金额</param>
    private void WhyLessonAcreage(List<RewardData> list, bool hideRewardAmount = false)
    {
        if (list == null || list.Count == 0)
        {
            Debug.LogWarning("FoldLessonStar: RewardData list is empty!");
            return;
        }
        if (PotionUtil.AxApple())
        {
            cashImage.SetActive(true);
            DiamondImage.SetActive(false);
        }
        else
        {
            cashImage.SetActive(false);
            DiamondImage.SetActive(true);
        }

        // 设置奖励数量文本
        for (int i = 0; i < Genu_Welt.Count && i < list.Count; i++)
        {

            if (Genu_Welt[i] != null)
            {
                if (hideRewardAmount)
                {
                    Genu_Welt[i].text = "xxx";
                }
                else
                {
                    Genu_Welt[i].text = list[i].rewardNum.ToString();
                }
            }
        }



        // 设置奖励图标
        //  for (int i = 0; i < list_Reward.Count && i < list.Count; i++)
        //  {
        // if (list_Reward[i] != null)
        //   {
        //     RewardType type = list[i].type;
        // 使用实际的奖励类型，而不是强制转换
        // list_Reward[i].sprite = LGNUtils.GetInstance().GetRewardImagePath(type);

        // TODO: 实现根据奖励类型设置图标的逻辑
        //    SetRewardIcon(list_Reward[i], type);
        // }
        // }
    }

    /// <summary>
    /// 根据奖励类型设置图标
    /// </summary>
    /// <param name="image">目标图片组件</param>
    /// <param name="rewardType">奖励类型</param>
    private void WhyLessonTwin(Image image, RewardType rewardType)
    {
        if (image == null) return;

        // TODO: 实现根据奖励类型获取图标的逻辑
        // 这里可以根据实际需求实现
        switch (rewardType)
        {
            case RewardType.Diamond:
                // 设置金币图标
                break;
            case RewardType.Cash:
                // 设置现金图标
                break;

                break;
            default:
                // 设置默认图标
                break;
        }
    }
}

