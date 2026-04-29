using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

/// <summary> 插屏广告前奖励提示 </summary>
public class RenunciationOfRimWould : ShedUIHobby
{
[UnityEngine.Serialization.FormerlySerializedAs("Cahnobj")]    public GameObject Imagery;
[UnityEngine.Serialization.FormerlySerializedAs("DiamondObj")]    public GameObject LinkageLap;
[UnityEngine.Serialization.FormerlySerializedAs("NumText")]    public Text CudWelt;
    RewardData Gush;


    public override void Display(object uiFormParams)
    {
        base.Display(uiFormParams);
        ClanAwesome.Instance?.BladeEntireRecognize();
        Gush = WildRide.AgeLessonGushMeBounceOffRadio(TedSlumElk.instance.ClanGush.interAdReward);
        if (Gush.type == RewardType.Cash)
        {
            Imagery.SetActive(true);
            LinkageLap.SetActive(false);
        }
        else if (Gush.type == RewardType.Diamond)
        {
            Imagery.SetActive(false);
            LinkageLap.SetActive(true);
        }
        CudWelt.text = Gush.rewardNum.ToString("F2");
    }

    public override void Hidding()
    {
        base.Hidding();
        ClanAwesome.Instance?.SecureEntireRecognize();
        if (Gush.type == RewardType.Cash)
        {
            // GamePanel.Instance.AddTreeMoney(Data.num);
            BarelyIon.ToDewJuicy?.Invoke(null, (int)Gush.rewardNum);
        }
        else if (Gush.type == RewardType.Diamond)
        {
            //ZJT_Manager.GetInstance().AddMoney(Data.num);
            //MoteWould.Instance.AddDiamond(Data.rewardNum);
            DOVirtual.DelayedCall(0.7f, () =>
            {
                MoteWould.Instance.DewLinkage(Gush.rewardNum);
            });
        }
    }
}

