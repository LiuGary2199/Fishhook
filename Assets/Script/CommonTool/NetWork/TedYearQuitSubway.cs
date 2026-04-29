/**
 * 
 * 网络请求的post对象
 * 
 * ***/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class TedYearQuitSubway 
{
    //post请求地址
    public string URL;
    //post的数据表单
    public WWWForm Jazz;
    //post成功回调
    public Action<UnityWebRequest> QuitImpetus;
    //post失败回调
    public Action QuitRend;
    public TedYearQuitSubway(string url,WWWForm  form,Action<UnityWebRequest> success,Action fail)
    {
        URL = url;
        Jazz = form;
        QuitImpetus = success;
        QuitRend = fail;
    }
}
