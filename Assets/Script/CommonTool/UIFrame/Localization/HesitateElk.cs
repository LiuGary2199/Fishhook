/*
 * 
 * 多语言
 * 
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HesitateElk 
{
    public static HesitateElk _Instatnce;
    //语言翻译的缓存集合
    private Dictionary<string, string> _HopHesitateHover;

    private HesitateElk()
    {
        _HopHesitateHover = new Dictionary<string, string>();
        //初始化语言缓存集合
        CapeHesitateHover();
    }

    /// <summary>
    /// 获取实例
    /// </summary>
    /// <returns></returns>
    public static HesitateElk AgeFletcher()
    {
        if (_Instatnce == null)
        {
            _Instatnce = new HesitateElk();
        }
        return _Instatnce;
    }

    /// <summary>
    /// 得到显示文本信息
    /// </summary>
    /// <param name="lauguageId">语言id</param>
    /// <returns></returns>
    public string DaleWelt(string lauguageId)
    {
        string strQueryResult = string.Empty;
        if (string.IsNullOrEmpty(lauguageId)) return null;
        //查询处理
        if(_HopHesitateHover!=null && _HopHesitateHover.Count >= 1)
        {
            _HopHesitateHover.TryGetValue(lauguageId, out strQueryResult);
            if (!string.IsNullOrEmpty(strQueryResult))
            {
                return strQueryResult;
            }
        }
        Debug.Log(GetType() + "/ShowText()/ Query is Null!  Parameter lauguageID: " + lauguageId);
        return null;
    }

    /// <summary>
    /// 初始化语言缓存集合
    /// </summary>
    private void CapeHesitateHover()
    {
        //LauguageJSONConfig_En
        //LauguageJSONConfig
        IMilletAwesome config = new MilletAwesomeMeMode("LauguageJSONConfig");
        if (config != null)
        {
            _HopHesitateHover = config.AppCentral;
        }
    }
}
