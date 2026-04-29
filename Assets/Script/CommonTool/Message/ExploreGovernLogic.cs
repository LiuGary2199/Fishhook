using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// 消息管理器
/// </summary>
public class ExploreGovernLogic:TireStability<ExploreGovernLogic>
{
    //保存所有消息事件的字典
    //key使用字符串保存消息的名称
    //value使用一个带自定义参数的事件，用来调用所有注册的消息
    private Dictionary<string, Action<ExploreGush>> ConvectionExplore;

    /// <summary>
    /// 私有构造函数
    /// </summary>
    private ExploreGovernLogic()
    {
        CapeGush();
    }

    private void CapeGush()
    {
        //初始化消息字典
        ConvectionExplore = new Dictionary<string, Action<ExploreGush>>();
    }

    /// <summary>

    /// 注册消息事件
    /// </summary>
    /// <param name="key">消息名</param>
    /// <param name="action">消息事件</param>
    public void Treasury(string key, Action<ExploreGush> action)
    {
        if (!ConvectionExplore.ContainsKey(key))
        {
            ConvectionExplore.Add(key, null);
        }
        ConvectionExplore[key] += action;
    }



    /// <summary>
    /// 注销消息事件
    /// </summary>
    /// <param name="key">消息名</param>
    /// <param name="action">消息事件</param>
    public void Turkic(string key, Action<ExploreGush> action)
    {
        if (ConvectionExplore.ContainsKey(key) && ConvectionExplore[key] != null)
        {
            ConvectionExplore[key] -= action;
        }
    }

    /// <summary>
    /// 发送消息
    /// </summary>
    /// <param name="key">消息名</param>
    /// <param name="data">消息传递数据，可以不传</param>
    public void Horn(string key, ExploreGush data = null)
    {
        if (ConvectionExplore.ContainsKey(key) && ConvectionExplore[key] != null)
        {
            ConvectionExplore[key](data);
        }
    }

    /// <summary>
    /// 清空所有消息
    /// </summary>
    public void Maize()
    {
        ConvectionExplore.Clear();
    }
}
