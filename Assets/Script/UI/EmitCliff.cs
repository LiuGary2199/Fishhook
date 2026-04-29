using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EmitCliff : MonoBehaviour
{
[UnityEngine.Serialization.FormerlySerializedAs("InitGroup")]    public GameObject CapeCliff;

    private GameObject SmoothlySevenSubway;
    private float NameEnure= 120f; // 两个item的position.x之差

    // Start is called before the first frame update
    void Start()
    {
        SmoothlySevenSubway = CapeCliff.transform.Find("SlotCard_1").gameObject;
        float x = NameEnure * 3;
        int multiCount = TedSlumElk.instance.CapeGush.slot_group.Count;
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < multiCount; j++)
            {
                GameObject fangkuai = Instantiate(SmoothlySevenSubway, CapeCliff.transform);
                fangkuai.transform.localPosition = new Vector3(x + NameEnure * multiCount * i + NameEnure * j, SmoothlySevenSubway.transform.localPosition.y, 0);
                double multi = TedSlumElk.instance.CapeGush.slot_group[j].multi;
                fangkuai.transform.Find("Text").GetComponent<Text>().text = "×" + multi.ToString("0.##");
            }
        }
    }

    public void WhipSeven()
    {
        CapeCliff.GetComponent<RectTransform>().localPosition = new Vector3(0, -14, 0);
    }

    public void Wavy(int index, Action<double> finish)
    {
        ChileElk.AgeFletcher().WifeMisery(ChileSick.UIMusic.Sound_OneArmBandit);
        TraditionDemobilize.EcologicalScroll(CapeCliff, -(NameEnure * 2 + NameEnure * TedSlumElk.instance.CapeGush.slot_group.Count * 3 + NameEnure * (index + 1)), () =>
        {
            finish?.Invoke(TedSlumElk.instance.CapeGush.slot_group[index].multi);
        });
    }
}
