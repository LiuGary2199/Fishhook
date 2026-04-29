/***
 * 
 *    
 *           主题： 资源加载管理器      
 *    Description: 
 *           功能： 本功能是在Unity的Resources类的基础之上，增加了“缓存”的处理。
 *                  
 *   
 *    
 *   
 */
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class ReachableElk : MonoBehaviour
{
    /* 字段 */
    private static ReachableElk _Fletcher;              //本脚本私有单例实例
    private Hashtable We= null;                        //容器键值对集合




    /// <summary>
    /// 得到实例(单例)
    /// </summary>
    /// <returns></returns>
    public static ReachableElk AgeFletcher()
    {
        if (_Fletcher == null)
        {
            _Fletcher = new GameObject("_ResourceMgr").AddComponent<ReachableElk>();
        }
        return _Fletcher;
    }

    void Awake()
    {
        We = new Hashtable();
    }

    /// <summary>
    /// 调用资源（带对象缓冲技术）
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="path"></param>
    /// <param name="isCatch"></param>
    /// <returns></returns>
    public T LoadResource<T>(string path, bool isCatch) where T : UnityEngine.Object
    {
        if (We.Contains(path))
        {
            return We[path] as T;
        }

        T TResource = Resources.Load<T>(path);
        if (TResource == null)
        {
            Debug.LogError(GetType() + "/GetInstance()/TResource 提取的资源找不到，请检查。 path=" + path);
        }
        else if (isCatch)
        {
            We.Add(path, TResource);
        }

        return TResource;
    }

    /// <summary>
    /// 调用资源（带对象缓冲技术）
    /// </summary>
    /// <param name="path"></param>
    /// <param name="isCatch"></param>
    /// <returns></returns>
    public GameObject FrogHairy(string path, bool isCatch)
    {
        GameObject goObj = LoadResource<GameObject>(path, isCatch);
        GameObject goObjClone = GameObject.Instantiate<GameObject>(goObj);
        if (goObjClone == null)
        {
            Debug.LogError(GetType() + "/LoadAsset()/克隆资源不成功，请检查。 path=" + path);
        }
        return goObjClone;
    }
}