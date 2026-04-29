using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoteSexStar : MonoBehaviour
{
[UnityEngine.Serialization.FormerlySerializedAs("m_Cash")]    public GameObject m_Seed;
[UnityEngine.Serialization.FormerlySerializedAs("m_Diamond")]    public GameObject m_Linkage;
[UnityEngine.Serialization.FormerlySerializedAs("RotText")]    public TextMeshProUGUI SexWelt;

    private void Awake()
    {
        ChartAcreage();
    }

    private void OnEnable()
    {
        // 防止预制体默认勾选导致两张图同时显示
        ChartAcreage();
    }

    private void ChartAcreage()
    {
        if (m_Seed != null) m_Seed.SetActive(false);
        if (m_Linkage != null) m_Linkage.SetActive(false);
        if (SexWelt != null) SexWelt.text = string.Empty;
    }

    public void MaizeAcreage()
    {
        ChartAcreage();
    }

    public void WhyLessonAcreage(RewardType rewardType, int rewardCount)
    {
        if (m_Seed != null) m_Seed.SetActive(false);
        if (m_Linkage != null) m_Linkage.SetActive(false);
        if (m_Seed != null) m_Seed.SetActive(rewardType == RewardType.Cash);
        if (m_Linkage != null) m_Linkage.SetActive(rewardType == RewardType.Diamond);

        if (SexWelt == null) return;
        SexWelt.text = rewardCount > 0 ? $"x{rewardCount}" : string.Empty;
    }
}
