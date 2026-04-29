using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class LoveStar : MonoBehaviour
{
[UnityEngine.Serialization.FormerlySerializedAs("mCompleted")]    //已完成
    public GameObject mConfident;
[UnityEngine.Serialization.FormerlySerializedAs("mReady")]    //可领取
    public GameObject mFrost;
[UnityEngine.Serialization.FormerlySerializedAs("mIncomplete")]    //未达成
    public GameObject mEarthquake;
[UnityEngine.Serialization.FormerlySerializedAs("mFinish")]    //已领取
    public GameObject mWalker;
[UnityEngine.Serialization.FormerlySerializedAs("BtnCompleted")]    public Button LadConfident;
[UnityEngine.Serialization.FormerlySerializedAs("BtnReady")]    public Button LadFrost;
[UnityEngine.Serialization.FormerlySerializedAs("TaskText")]    public Text LoveWelt;
[UnityEngine.Serialization.FormerlySerializedAs("GoldText")]    public Text TalkWelt;
[UnityEngine.Serialization.FormerlySerializedAs("ProgressText")]    public Text CarelessWelt;
[UnityEngine.Serialization.FormerlySerializedAs("ProgressImage")]    public Image CarelessTough;
[UnityEngine.Serialization.FormerlySerializedAs("ProgressNum")]    public int CarelessCud;
[UnityEngine.Serialization.FormerlySerializedAs("Total")]    public int Rigid;
[UnityEngine.Serialization.FormerlySerializedAs("Gold")]    public int Talk;
[UnityEngine.Serialization.FormerlySerializedAs("TaskId")]    public int LoveWe;

    public GameObject cashImage;
    public GameObject DiamondImage;

    private TaskStatus _Hairdo;
    public TaskStatus Impose    {
        get => _Hairdo;
        set
        {
            _Hairdo = value;
            mConfident.SetActive(value == TaskStatus.Completed);
            mFrost.SetActive(value == TaskStatus.Ready);
            mEarthquake.SetActive(value == TaskStatus.Incomplete);
            if (mWalker != null)
                mWalker.SetActive(value == TaskStatus.Get);
            LoveManual.WhyLoveImpose(LoveWe, _Hairdo);
        }
    }

    private void Awake()
    {
        LadConfident.onClick.AddListener(() =>
        {ChileElk.AgeFletcher().WifeMisery(ChileSick.UIMusic.Sound_UIButton);
            if (Impose != TaskStatus.Completed) return;
            ADAwesome.Fletcher.WoadLessonMount((ok) =>
            {
                if (ok)
                {
                         QuitCacheCandle.AgeFletcher().HornCache("1014", "1");
                    Impose = TaskStatus.Ready;
                }
            }, "6");
        });
        
        LadFrost.onClick.AddListener((() =>
        {ChileElk.AgeFletcher().WifeMisery(ChileSick.UIMusic.Sound_UIButton);
            if (Impose != TaskStatus.Ready) return;

            DOVirtual.DelayedCall(0.7f, () =>
            {
                if (PotionUtil.AxApple())
                {
                    BarelyIon.ToDewJuicy?.Invoke(null, (int)Talk);
                }
                else
                {
                    MoteWould.Instance.DewLinkage(Talk, this.transform);
                }

            
            });
            Impose = TaskStatus.Get;
            BarelyIon.ToJobberLoveAge.Invoke();
            //AEventModule.Send(AEventType.TaskGet_B);
        }));
        
    }

    public void Cape(int taskId, int progressNum, int total, int gold)
    {
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
        LoveWe = taskId;
        CarelessCud = progressNum;
        Rigid = total;
        //ProgressText.text = $"{ProgressNum}/{Total}";
        CarelessTough.fillAmount = (float)CarelessCud / Rigid;
        Talk = gold;
        TalkWelt.text = $"X{Talk}";
        switch (taskId)
        {
            case LoveManual.LoveWe_1:
                LoveWelt.text = $"Complete Daily Check-in ({CarelessCud}/{Rigid})";
                break;
            case LoveManual.LoveWe_2:
                LoveWelt.text = $"Defeat Boss ({CarelessCud}/{Rigid}) Time";
                break;
            case LoveManual.LoveWe_3:
                LoveWelt.text = $"Watch ({CarelessCud}/{Rigid}) Ads in Total";
                break;
            case LoveManual.LoveWe_4:
                LoveWelt.text = $"Fire ({CarelessCud}/{Rigid}) Times in Total";
                break;
        }
        JobberEqual();
    }

    public void JobberEqual()
    {
        var cacheState = LoveManual.AgeLoveImpose(LoveWe);
        if (cacheState == TaskStatus.Incomplete)
        {
            Impose = CarelessCud >= Rigid ? TaskStatus.Completed : TaskStatus.Incomplete;
            
        }
        else
        {
            Impose = cacheState;
        }
    }
    
}

public enum TaskStatus
{
    Completed,
    Ready,
    Incomplete,
    Get,
}
