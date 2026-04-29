using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConcertWould : MonoBehaviour
{
[UnityEngine.Serialization.FormerlySerializedAs("sliderImage")]    public Image VacuumTough;
[UnityEngine.Serialization.FormerlySerializedAs("progressText")]    public Text SculptorWelt;
    private bool ToStarkClan= false;

    void Start()
    {
        VacuumTough.fillAmount = 0;
        SculptorWelt.text = "0%";
        ToStarkClan = false;
        ChileElk.AgeFletcher().FibrousStarkClanInner();
        ZJT_Manager.AgeFletcher().RecordStartTime();
        BarelyIon.ToStarkClan += OnEnterGame;
    }
    public void OnEnterGame()
    {
        Destroy(transform.parent.gameObject);
        ThaiAwesome.instance.PairCape();
    }

    // Update is called once per frame
    void Update()
    {
        if (VacuumTough.fillAmount <= 0.8f || (TedSlumElk.instance.Habit && CashOutManager.AgeFletcher().Ready))
        //if (sliderImage.fillAmount <= 0.8f || (TedSlumElk.instance.ready))
        {
            VacuumTough.fillAmount += Time.deltaTime * 0.2f;
            SculptorWelt.text = (int)(VacuumTough.fillAmount * 100) + "%";
            if (VacuumTough.fillAmount >= 1 && !ToStarkClan)
            {
                ToStarkClan = true;
                // 安卓平台特殊屏蔽规则 被屏蔽玩家显示提示 阻止进入
                if (PotionUtil.SeveralDrapePhase())
                    return;
                //主动调用一次IsApple 判断是否符合屏蔽规则
                PotionUtil.AxApple();
                UIAwesome.AgeFletcher().DaleUIHobby(nameof(ConcertBowl));

                //Destroy(transform.parent.gameObject);
                //  ThaiAwesome.instance.gameInit();
                ZJT_Manager.AgeFletcher().ReportEvent_LoadingTime();
            }
        }
    }
    void OnDestroy()
    {
        BarelyIon.ToStarkClan -= OnEnterGame;
    }
}
