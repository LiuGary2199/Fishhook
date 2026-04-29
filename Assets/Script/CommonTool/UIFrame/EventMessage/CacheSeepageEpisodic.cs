/*
 *     主题： 事件触发监听      
 *    Description: 
 *           功能： 实现对于任何对象的监听处理。
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CacheSeepageEpisodic : UnityEngine.EventSystems.EventTrigger
{
    public delegate void VoidDelegate(GameObject go);
    public VoidDelegate NoThese;
    public VoidDelegate NoTear;
    public VoidDelegate NoStark;
    public VoidDelegate NoMint;
    public VoidDelegate NoAt;
    public VoidDelegate NoTundra;
    public VoidDelegate NoJobberTundra;

    /// <summary>
    /// 得到监听器组件
    /// </summary>
    /// <param name="go">监听的游戏对象</param>
    /// <returns></returns>
    public static CacheSeepageEpisodic Age(GameObject go)
    {
        CacheSeepageEpisodic listener = go.GetComponent<CacheSeepageEpisodic>();
        if (listener == null)
        {
            listener = go.AddComponent<CacheSeepageEpisodic>();
        }
        return listener;
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        if (NoThese != null)
        {
            NoThese(gameObject);
        }
    }
    public override void OnPointerDown(PointerEventData eventData)
    {
        if (NoTear != null)
        {
            NoTear(gameObject);
        }
    }
    public override void OnPointerEnter(PointerEventData eventData)
    {
        if (NoStark != null)
        {
            NoStark(gameObject);
        }
    }
    public override void OnPointerExit(PointerEventData eventData)
    {
        if (NoMint != null)
        {
            NoMint(gameObject);
        }
    }
    public override void OnPointerUp(PointerEventData eventData)
    {
        if (NoAt != null)
        {
            NoAt(gameObject);
        }
    }
    public override void OnSelect(BaseEventData eventData)
    {
        if (NoTundra != null)
        {
            NoTundra(gameObject);
        }
    }
    public override void OnUpdateSelected(BaseEventData eventData)
    {
        if (NoJobberTundra != null)
        {
            NoJobberTundra(gameObject);
        }
    }
}
