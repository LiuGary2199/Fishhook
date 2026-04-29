using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

using UnityEngine;
using UnityEngine.UI;

/// <summary>  </summary>
public class EmitWould : ShedUIHobby
{
[UnityEngine.Serialization.FormerlySerializedAs("SlotMachine")]    public GameObject EmitTrainer;
[UnityEngine.Serialization.FormerlySerializedAs("winparticle1")]    public GameObject Overbalance1;
[UnityEngine.Serialization.FormerlySerializedAs("winparticle2")]    public GameObject Overbalance2;
[UnityEngine.Serialization.FormerlySerializedAs("SlotSprites")]    public Sprite[] EmitDevelop;
[UnityEngine.Serialization.FormerlySerializedAs("RealItemParent")]    public Transform RageStarFemale;
    private List<List<Transform>> RagePeach;
    int[] RageLoopTruck= new int[3];
[UnityEngine.Serialization.FormerlySerializedAs("FakeItemsParent")]    public Transform YorkPeachFemale;
    List<List<Transform>> YorkPeach;
    int[] YorkWeltTruck= new int[3];
    int YorkWeltRoe= 15;
    float EraY; //假slotitem之间的间距
    float Few; //假slot最上面一行高度
    float Enamel; //假slot最底下一行高度
    bool AxDiffuse;
    bool AxEmitWin;
    int JaySmile;
    RewardData Lesson;

    void Start()
    {
        Cape();
    }

    public override void Display(object uiFormParams)
    {
        base.Display(uiFormParams);
        SpotGushAwesome.SetInt(CMillet.If_Worry_Pheromone, SpotGushAwesome.GetInt(CMillet.If_Worry_Pheromone) + 1);
        QuitCacheCandle.AgeFletcher().HornCache("1006");
        DutyAwesome.AgeFletcher().Nomad(1, () => { Emit(); });
    }

    void Cape()
    {
        RagePeach = new List<List<Transform>>();
        for (int i = 0; i < 3; i++)
        {
            RagePeach.Add(new List<Transform>());
            for (int j = 0; j < 1; j++)
            {
                Transform Item = RageStarFemale.GetChild(i * 1 + j);
                Item.gameObject.SetActive(false);
                Item.name = $"第{i + 1}列  第{j + 1}行";
                RagePeach[i].Add(Item);
            }
        }
        YorkPeach = new List<List<Transform>>();
        for (int i = 0; i < 3; i++)
        {
            YorkPeach.Add(new List<Transform>());
            for (int j = 0; j < 5; j++)
            {
                Transform Item = YorkPeachFemale.GetChild(i * 5 + j);
                int Index = UnityEngine.Random.Range(0, EmitDevelop.Length);
                WhyEmitTwin(Item, Index);
                Item.name = $"第{i + 1}列  第{j + 1}行";
                YorkPeach[i].Add(Item);
            }
        }

        EraY = YorkPeach[0][0].GetChild(0).GetComponent<RectTransform>().sizeDelta.y + YorkPeachFemale.GetComponent<GridLayoutGroup>().spacing.y;
        Few = YorkPeach[0][0].transform.localPosition.y;
        Enamel = YorkPeach[0][^1].transform.localPosition.y;
    }

    public void Emit()
    {
        if (RagePeach == null || RagePeach.Count == 0)
            Cape();

        AxDiffuse = true;
        AxEmitWin = true;//Random.value < .7f;
        int[] RealItemIndex = new int[3];
        if (AxEmitWin)
        {
            // WinIndex = GetWinIndexByWeight();
            Lesson = WildRide.AgeLessonGushMeBounceOffRadio(TedSlumElk.instance.ClanGush.slots_data_list);
            if (Lesson.rewardNum > 999f)
                Lesson.rewardNum = 999;
            print("老虎机中奖：" + Lesson.type + "  数值： " + Lesson.rewardNum);
            // 将小数转换为整数（乘以100）
            int totalValue = (int)Lesson.rewardNum;
            if (Lesson.rewardNum < 10)
                totalValue = (int)(Lesson.rewardNum * 100);
            else
                totalValue = (int)Lesson.rewardNum;
            Lesson.rewardNum = totalValue;
            // 拆分三个数字
            int[] Indexs = new int[3];
            Indexs[0] = totalValue / 100;         // 十位
            Indexs[1] = (totalValue / 10) % 10;   // 个位
            Indexs[2] = totalValue % 10;          // 十分位
            for (int i = 0; i < 3; i++)
                RealItemIndex[i] = Indexs[i];
        }
        else
        {
            int[] temp = new int[EmitDevelop.Length];
            for (int i = 0; i < EmitDevelop.Length; i++)
                temp[i] = i;
            for (int i = 0; i < 3; i++)
            {
                int tempIndex = UnityEngine.Random.Range(0, EmitDevelop.Length - i);
                int tempValue = temp[tempIndex];
                temp[tempIndex] = temp[EmitDevelop.Length - i];
                temp[EmitDevelop.Length - i] = tempValue;
            }
            for (int i = 0; i < 3; i++)
                RealItemIndex[i] = temp[i];
        }
        for (int i = 0; i < 3; i++)
        {
            Transform Item = RagePeach[i][0];
            WhyEmitTwin(Item, RealItemIndex[i]);
            Item.gameObject.SetActive(false);
            Item.transform.localPosition = new Vector2(Item.transform.localPosition.x, Item.transform.localPosition.y + EraY);
        }
        for (int i = 0; i < 3; i++)
        {
            RageLoopTruck[i] = 0;
            YorkWeltTruck[i] = 0;
            for (int j = 0; j < 5; j++)
                YorkPeach[i][j].Find("Icon").gameObject.SetActive(true);
            int Index = i;
            DutyAwesome.AgeFletcher().Nomad(Index * .4f, () =>
            {
                YorkUrnLiquidDisc(Index, "开始");
            });
        }
         ChileElk.AgeFletcher().WifeMisery(ChileSick.UIMusic.slotsrot);
        // ChileElk.GetInstance().PlayEffect(ChileSick.UIMusic.SFX_SlotsRotate);
    }
    int AgeJaySmileMeBounce()
    {
        // // 0:钱 1钻石
        // int[] WinIndex = new int[] { 0, 1 };
        // int[] Weights = new int[] { 10, 10 };
        // int Sum = 0;
        // for (int i = 0; i < WinIndex.Length; i++)
        //     Sum += Weights[i];
        // int RandomrewardNum = UnityEngine.Random.Range(0, Sum);
        // for (int i = 0; i < WinIndex.Length; i++)
        // {
        //     RandomrewardNum -= Weights[i];
        //     if (RandomrewardNum < 0)
        //         return WinIndex[i];
        // }
        return 0;
    }

    void WhyEmitTwin(Transform Item, int Index)
    {
        Item.Find("Icon").GetComponent<Image>().sprite = EmitDevelop[Index];
        Item.Find("Icon").gameObject.SetActive(true);
    }

    void RageUrnLiquidDisc(int Index)
    {
        float AnimTime = 0.2f;
        Ease AnimEase = Ease.OutBack;
        for (int i = 0; i < 1; i++)
        {
            Transform Item = RagePeach[Index][i];
            Item.gameObject.SetActive(true);
            Item.transform.DOLocalMoveY(Item.transform.localPosition.y - EraY, AnimTime).SetEase(AnimEase).OnUpdate(() =>
            {
                for (int j = 0; j < 5; j++)
                {
                    Transform FakeItem = YorkPeach[Index][j];
                    if (Mathf.Abs(Item.transform.position.y - FakeItem.transform.position.y) < .5f)
                        FakeItem.Find("Icon").gameObject.SetActive(false);
                }
            });
        }
        DutyAwesome.AgeFletcher().Nomad(AnimTime, () =>
  {
      // if (Index == 0)
      //     ChileElk.GetInstance().PlayEffect(ChileSick.UIMusic.Sound_High_Rate_3Slot);
      // else if (Index == 1)
      //     ChileElk.GetInstance().PlayEffect(ChileSick.UIMusic.Sound_High_Rate_4Slot);
      // else if (Index == 2)
      // ChileElk.GetInstance().PlayEffect(ChileSick.UIMusic.SFX_ScratchGetReward);
      if (Index == 2)
      {
          Overbalance1.SetActive(true);
          Overbalance2.SetActive(true);
          EmitWalker();
      }
  });
    }
    void YorkUrnLiquidDisc(int Index, string AnimType)
    {
       
        float AnimTime = 0;
        Ease AnimEase = Ease.Linear;
        if (AnimType == "开始")
        {
            AnimTime = 0.2f;
            AnimEase = Ease.InSine;
        }
        else if (AnimType == "持续")
        {
            AnimTime = 0.05f;
            AnimEase = Ease.Linear;
        }
        else if (AnimType == "结束")
        {
            AnimTime = 0.2f;
            AnimEase = Ease.OutBack;
        }
        for (int i = 0; i < 5; i++)
        {
            Transform Item = YorkPeach[Index][i];
            Item.transform.DOLocalMoveY(Item.transform.localPosition.y - EraY, AnimTime).SetEase(AnimEase);
        }
        DutyAwesome.AgeFletcher().Nomad(AnimTime, () =>
 {
     for (int i = 0; i < 5; i++)
     {
         Transform Item = YorkPeach[Index][i];
         if (Item.transform.localPosition.y < Enamel)
         {
             Item.transform.localPosition = new Vector2(Item.transform.localPosition.x, Few);
             WhyEmitTwin(Item, UnityEngine.Random.Range(0, EmitDevelop.Length));
             break;
         }
     }

     if (AnimType == "开始")
         YorkUrnLiquidDisc(Index, "持续");
     else if (AnimType == "持续")
     {
         YorkWeltTruck[Index]++;
         if (YorkWeltTruck[Index] < YorkWeltRoe)
             YorkUrnLiquidDisc(Index, "持续");
         else
             YorkUrnLiquidDisc(Index, "结束");

         if (YorkWeltTruck[Index] == YorkWeltRoe)
             RageUrnLiquidDisc(Index);
     }
 });
    }

    void EmitWalker()
    {
        AxDiffuse = false;
        //AllGetBtn.gameObject.SetActive(true);
        //AllGetBtn.transform.localPosition = new Vector2(0, -422);
        DutyAwesome.AgeFletcher().Nomad(1.5f, () =>
        {
            //GetBtn.gameObject.SetActive(true);
            // GetBtn.transform.localPosition = new Vector2(185, -422);
            // AllGetBtn.transform.localPosition = new Vector2(-185, -422);
            Overbalance1.SetActive(false);
            Overbalance2.SetActive(false);
            BloodUIJazz(nameof(EmitWould));
            UIAwesome.AgeFletcher().DaleUIHobby(nameof(LessonWould)).GetComponent<LessonWould>().Cape(null, Lesson, 
            ()=>{
                        BarelyIon.ToSewageClanDormancy?.Invoke();
            }, "1006");
        });
    }

}
