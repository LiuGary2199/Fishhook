/***
 * 
 * 网络请求的get对象
 * 
 * **/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class TedYearAgeSubway 
{
    //get的url
    public string Peg;
    //get成功的回调
    public Action<UnityWebRequest> AgeImpetus;
    //get失败的回调
    public Action AgeFail;
    public TedYearAgeSubway(string url,Action<UnityWebRequest> success,Action fail)
    {
        Peg = url;
        AgeImpetus = success;
        AgeFail = fail;
    }
   
}
