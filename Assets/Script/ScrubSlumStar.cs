using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LuckyCardController : MonoBehaviour
{
[UnityEngine.Serialization.FormerlySerializedAs("DaimImg")]    public GameObject KnotMud;
[UnityEngine.Serialization.FormerlySerializedAs("CashImg")]    public GameObject SeedMud;
[UnityEngine.Serialization.FormerlySerializedAs("overImg")]    public GameObject CampMud;
[UnityEngine.Serialization.FormerlySerializedAs("rewardText")]    public TextMeshProUGUI PoorlyWelt;
[UnityEngine.Serialization.FormerlySerializedAs("rewardType")]    public RewardType PoorlySick;
[UnityEngine.Serialization.FormerlySerializedAs("rewardNum")]    public double PoorlyCud;
[UnityEngine.Serialization.FormerlySerializedAs("m_FlipAnimator")]    public Animator m_SoarFaithful;
[UnityEngine.Serialization.FormerlySerializedAs("cardBtn")]    public Button LensLad;
[UnityEngine.Serialization.FormerlySerializedAs("isThanksCard")]    public bool ToCrunchSlum;
    public void BloodLap()
    {
        HoneLessonThorn();
        if (PoorlyWelt != null)
            PoorlyWelt.text = "";
        BloodMud();
    }

    private void HoneLessonThorn()
    {
        if (SeedMud != null) SeedMud.SetActive(false);
        if (KnotMud != null) KnotMud.SetActive(false);
        if (CampMud != null) CampMud.SetActive(false);
    }

    public void CapeLessonLapGush(LuckyObjData luckyObjData, bool resetAnimToIdle = true)
    {
        ToCrunchSlum = false;
        LensLad.onClick.RemoveAllListeners();
        LensLad.onClick.AddListener(OnCardButtonClick);
        LensLad.interactable = true;
        PoorlySick = luckyObjData.LuckyObjType;
        PoorlyCud = luckyObjData.RewardNum;

        HoneLessonThorn();

        if (resetAnimToIdle)
        {
            BloodMud();
            if (PoorlyWelt != null)
                PoorlyWelt.text = "";
        }
        else
        {
            if (PoorlyWelt != null)
                PoorlyWelt.text = PoorlyCud + "";
            switch (PoorlySick)
            {
                case RewardType.Cash:
                    if (SeedMud != null) SeedMud.SetActive(true);
                    break;
                case RewardType.Diamond:
                    if (KnotMud != null) KnotMud.SetActive(true);
                    break;
                default:
                    break;
            }
        }
    }

    public void CapeCrunchLapGush()
    {
        ToCrunchSlum = true;
        LensLad.onClick.RemoveAllListeners();
        LensLad.onClick.AddListener(OnCardButtonClick);
        LensLad.interactable = true;
        HoneLessonThorn();
        if (PoorlyWelt != null)
            PoorlyWelt.text = "";
        BloodMud();
    }

    public void DaleCrunchVest()
    {
        HoneLessonThorn();
        if (CampMud != null)
            CampMud.SetActive(true);
    }

    public void DaleReliantVest()
    {
        HoneLessonThorn();
        if (ToCrunchSlum)
        {
            if (CampMud != null) CampMud.SetActive(true);
            if (PoorlyWelt != null) PoorlyWelt.text = "";
            return;
        }

        if (PoorlyWelt != null)
            PoorlyWelt.text = PoorlyCud .ToString();

        switch (PoorlySick)
        {
            case RewardType.Cash:
                if (SeedMud != null) SeedMud.SetActive(true);
                break;
            case RewardType.Diamond:
                if (KnotMud != null) KnotMud.SetActive(true);
                break;
        }
    }

    private void OnCardButtonClick()
    {
        var panel = ScrubSlumWould.Instance;
        if (panel == null) return;
        // 对齐旧版：解锁前不允许点击翻牌
        if (panel.ToBath) return;
        if (panel.AgencyLapPloy.Contains(gameObject)) return;

        ChileElk.AgeFletcher().WifeMisery(ChileSick.UIMusic.Sound_PopShow);
        panel.DewTundraPloy(gameObject);
    }

    private void BloodMud()
    {
        m_SoarFaithful.Play("idleCard", 0, 0f);
    }
    public void WifeDisc()
    {
        m_SoarFaithful.Play("FlipTheCard", 0, 0f);
    }

    public void WhyEntrepreneur(bool value)
    {
        if (LensLad != null)
            LensLad.interactable = value;
    }
}