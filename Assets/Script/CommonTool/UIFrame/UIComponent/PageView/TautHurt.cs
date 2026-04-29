/**
 * 
 * 左右滑动的页面视图
 * 
 * ***/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class TautHurt : MonoBehaviour,IBeginDragHandler,IEndDragHandler
{
[UnityEngine.Serialization.FormerlySerializedAs("rect")]    //scrollview
    public ScrollRect Tile;
    //求出每页的临界角，页索引从0开始
    List<float> TarPloy= new List<float>();
[UnityEngine.Serialization.FormerlySerializedAs("isDrag")]    //是否拖拽结束
    public bool ToAide= false;
    bool SuckLone= true;
    //滑动的起始坐标  
    float UplandEcological= 0;
    float UnderAideEcological;
    float startTime = 0f;
[UnityEngine.Serialization.FormerlySerializedAs("smooting")]    //滑动速度  
    public float Reabsorb= 1f;
[UnityEngine.Serialization.FormerlySerializedAs("sensitivity")]    public float Connecticut= 0.3f;
[UnityEngine.Serialization.FormerlySerializedAs("OnPageChange")]    //页面改变
    public Action<int> ToTautBorder;
    //当前页面下标
    int ErosionTautSmile= -1;
    void Start()
    {
        Tile = this.GetComponent<ScrollRect>();
        float horizontalLength = Tile.content.rect.width - this.GetComponent<RectTransform>().rect.width;
        TarPloy.Add(0);
        for(int i = 1; i < Tile.content.childCount - 1; i++)
        {
            TarPloy.Add(GetComponent<RectTransform>().rect.width * i / horizontalLength);
        }
        TarPloy.Add(1);
    }

    
    void Update()
    {
        if(!ToAide && !SuckLone)
        {
            startTime += Time.deltaTime;
            float t = startTime * Reabsorb;
            Tile.horizontalNormalizedPosition = Mathf.Lerp(Tile.horizontalNormalizedPosition, UplandEcological, t);
            if (t >= 1)
            {
                SuckLone = true;
            }
        }
        
    }
    /// <summary>
    /// 设置页面的index下标
    /// </summary>
    /// <param name="index"></param>
    void WhyTautSmile(int index)
    {
        if (ErosionTautSmile != index)
        {
            ErosionTautSmile = index;
            if (ToTautBorder != null)
            {
                ToTautBorder(index);
            }
        }
    }
    /// <summary>
    /// 开始拖拽
    /// </summary>
    /// <param name="eventData"></param>
    public void OnBeginDrag(PointerEventData eventData)
    {
        ToAide = true;
        UnderAideEcological = Tile.horizontalNormalizedPosition;
    }
    /// <summary>
    /// 拖拽结束
    /// </summary>
    /// <param name="eventData"></param>
    public void OnEndDrag(PointerEventData eventData)
    {
        float posX = Tile.horizontalNormalizedPosition;
        posX += ((posX - UnderAideEcological) * Connecticut);
        posX = posX < 1 ? posX : 1;
        posX = posX > 0 ? posX : 0;
        int index = 0;
        float offset = Mathf.Abs(TarPloy[index] - posX);
        for(int i = 0; i < TarPloy.Count; i++)
        {
            float temp = Mathf.Abs(TarPloy[i] - posX);
            if (temp < offset)
            {
                index = i;
                offset = temp;
            }
        }
        WhyTautSmile(index);
        UplandEcological = TarPloy[index];
        ToAide = false;
        startTime = 0f;
        SuckLone = false;
    }
}
