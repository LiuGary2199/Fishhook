/**
 * 
 * 支持上下滑动的scroll view
 * 
 * **/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LiquidHurt : MonoBehaviour
{
[UnityEngine.Serialization.FormerlySerializedAs("itemCell")]    //预支单体
    public LiquidHurtStar NameTern;
[UnityEngine.Serialization.FormerlySerializedAs("scrollRect")]    //scrollview
    public ScrollRect BenignLady;
[UnityEngine.Serialization.FormerlySerializedAs("content")]
    //content
    public RectTransform Garland;
[UnityEngine.Serialization.FormerlySerializedAs("spacing")]    //间隔
    public float Upriver= 10;
[UnityEngine.Serialization.FormerlySerializedAs("totalWidth")]    //总的宽
    public float WorryEnure;
[UnityEngine.Serialization.FormerlySerializedAs("totalHeight")]    //总的高
    public float WorryCanopy;
[UnityEngine.Serialization.FormerlySerializedAs("visibleCount")]    //可见的数量
    public int InsightTruck;
[UnityEngine.Serialization.FormerlySerializedAs("isClac")]    //初始数据完成是否检测计算
    public bool ToClac= false;
[UnityEngine.Serialization.FormerlySerializedAs("startIndex")]    //开始的索引
    public int UnderSmile;
[UnityEngine.Serialization.FormerlySerializedAs("lastIndex")]    //结尾的索引
    public int RendSmile;
[UnityEngine.Serialization.FormerlySerializedAs("itemHeight")]    //item的高
    public float NameCanopy= 50;
[UnityEngine.Serialization.FormerlySerializedAs("itemList")]
    //缓存的itemlist
    public List<LiquidHurtStar> NamePloy;
[UnityEngine.Serialization.FormerlySerializedAs("visibleList")]    //可见的itemList
    public List<LiquidHurtStar> InsightPloy;
[UnityEngine.Serialization.FormerlySerializedAs("allList")]    //总共的dataList
    public List<int> TapPloy;

    void Start()
    {
        WorryCanopy = this.GetComponent<RectTransform>().sizeDelta.y;
        WorryEnure = this.GetComponent<RectTransform>().sizeDelta.x;
        Garland = BenignLady.content;
        CapeGush();

    }
    //初始化
    public void CapeGush()
    {
        InsightTruck = Mathf.CeilToInt(WorryCanopy / ZealCanopy) + 1;
        for (int i = 0; i < InsightTruck; i++)
        {
            this.DewStar();
        }
        UnderSmile = 0;
        RendSmile = 0;
        List<int> numberList = new List<int>();
        //数据长度
        int dataLength = 20;
        for (int i = 0; i < dataLength; i++)
        {
            numberList.Add(i);
        }
        WhyGush(numberList);
    }
    //设置数据
    void WhyGush(List<int> list)
    {
        TapPloy = list;
        UnderSmile = 0;
        if (GushTruck <= InsightTruck)
        {
            RendSmile = GushTruck;
        }
        else
        {
            RendSmile = InsightTruck - 1;
        }
        //Debug.Log("ooooooooo"+lastIndex);
        for (int i = UnderSmile; i < RendSmile; i++)
        {
            LiquidHurtStar obj = SeeStar();
            if (obj == null)
            {
                Debug.Log("获取item为空");
            }
            else
            {
                obj.gameObject.name = i.ToString();

                obj.gameObject.SetActive(true);
                obj.transform.localPosition = new Vector3(0, -i * ZealCanopy, 0);
                InsightPloy.Add(obj);
                JobberStar(i, obj);
            }

        }
        Garland.sizeDelta = new Vector2(WorryEnure, GushTruck * ZealCanopy - Upriver);
        ToClac = true;
    }
    //更新item
    public void JobberStar(int index, LiquidHurtStar obj)
    {
        int d = TapPloy[index];
        string str = d.ToString();
        obj.name = str;
        //更新数据 todo
    }
    //从itemlist中取出item
    public LiquidHurtStar SeeStar()
    {
        LiquidHurtStar obj = null;
        if (NamePloy.Count > 0)
        {
            obj = NamePloy[0];
            obj.gameObject.SetActive(true);
            NamePloy.RemoveAt(0);
        }
        else
        {
            Debug.Log("从缓存中取出的是空");
        }
        return obj;
    }
    //item进入itemlist
    public void NewsStar(LiquidHurtStar obj)
    {
        NamePloy.Add(obj);
        obj.gameObject.SetActive(false);
    }
    public int GushTruck    {
        get
        {
            return TapPloy.Count;
        }
    }
    //每一行的高
    public float ZealCanopy    {
        get
        {
            return NameCanopy + Upriver;
        }
    }
    //添加item到缓存列表中
    public void DewStar()
    {
        GameObject obj = Instantiate(NameTern.gameObject);
        obj.transform.SetParent(Garland);
        RectTransform Tile= obj.GetComponent<RectTransform>();
        Tile.anchorMin = new Vector2(0.5f, 1);
        Tile.anchorMax = new Vector2(0.5f, 1);
        Tile.pivot = new Vector2(0.5f, 1);
        obj.SetActive(false);
        obj.transform.localScale = Vector3.one;
        LiquidHurtStar o = obj.GetComponent<LiquidHurtStar>();
        NamePloy.Add(o);
    }



    void Update()
    {
        if (ToClac)
        {
            Liquid();
        }
    }
    /// <summary>
    /// 计算滑动支持上下滑动
    /// </summary>
    void Liquid()
    {
        float vy = Garland.anchoredPosition.y;
        float rollUpTop = (UnderSmile + 1) * ZealCanopy;
        float rollUnderTop = UnderSmile * ZealCanopy;

        if (vy > rollUpTop && RendSmile < GushTruck)
        {
            //上边界移除
            if (InsightPloy.Count > 0)
            {
                LiquidHurtStar obj = InsightPloy[0];
                InsightPloy.RemoveAt(0);
                NewsStar(obj);
            }
            UnderSmile++;
        }
        float rollUpBottom = (RendSmile - 1) * ZealCanopy - Upriver;
        if (vy < rollUpBottom - WorryCanopy && UnderSmile > 0)
        {
            //下边界减少
            RendSmile--;
            if (InsightPloy.Count > 0)
            {
                LiquidHurtStar obj = InsightPloy[InsightPloy.Count - 1];
                InsightPloy.RemoveAt(InsightPloy.Count - 1);
                NewsStar(obj);
            }

        }
        float rollUnderBottom = RendSmile * ZealCanopy - Upriver;
        if (vy > rollUnderBottom - WorryCanopy && RendSmile < GushTruck)
        {
            //Debug.Log("下边界增加"+vy);
            //下边界增加
            LiquidHurtStar go = SeeStar();
            InsightPloy.Add(go);
            go.transform.localPosition = new Vector3(0, -RendSmile * ZealCanopy);
            JobberStar(RendSmile, go);
            RendSmile++;
        }


        if (vy < rollUnderTop && UnderSmile > 0)
        {
            //Debug.Log("上边界增加"+vy);
            //上边界增加
            UnderSmile--;
            LiquidHurtStar go = SeeStar();
            InsightPloy.Insert(0, go);
            JobberStar(UnderSmile, go);
            go.transform.localPosition = new Vector3(0, -UnderSmile * ZealCanopy);
        }

    }
}
