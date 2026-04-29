using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrapUsWould : ShedUIHobby
{
[UnityEngine.Serialization.FormerlySerializedAs("Stars")]    public Button[] Smoke;
[UnityEngine.Serialization.FormerlySerializedAs("star1Sprite")]    public Sprite Gear1Midway;
[UnityEngine.Serialization.FormerlySerializedAs("star2Sprite")]    public Sprite Gear2Midway;

    // Start is called before the first frame update
    void Start()
    {
        foreach (Button star in Smoke)
        {
            star.onClick.AddListener(() =>
            {
                string indexStr = System.Text.RegularExpressions.Regex.Replace(star.gameObject.name, @"[^0-9]+", "");
                int index = indexStr == "" ? 0 : int.Parse(indexStr);
                EightWaist(index);
            });
        }
    }

    public override void Display(object uiFormParams)
    {
        base.Display(uiFormParams);
        for (int i = 0; i < 5; i++)
        {
            Smoke[i].gameObject.GetComponent<Image>().sprite = Gear2Midway;
        }
    }


    private void EightWaist(int index)
    {
        for (int i = 0; i < 5; i++)
        {
            Smoke[i].gameObject.GetComponent<Image>().sprite = i <= index ? Gear1Midway : Gear2Midway;
        }
        QuitCacheCandle.AgeFletcher().HornCache("1301", (index + 1).ToString());
        if (index < 3)
        {
            StartCoroutine(LaserWould());
        } else
        {
            // 跳转到应用商店
            TrapNoAwesome.instance.MarkAPPinMarket();
            StartCoroutine(LaserWould());
        }
        
        // 打点
        //QuitCacheCandle.GetInstance().SendEvent("1210", (index + 1).ToString());
    }

    IEnumerator LaserWould(float waitTime = 0.5f)
    {
        yield return new WaitForSeconds(waitTime);
        BloodUIJazz(GetType().Name);
    }
}
