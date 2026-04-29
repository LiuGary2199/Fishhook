/***
 * 
 * AudioSource组件管理(音效，背景音乐除外)
 * 
 * **/
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InnerInventApart 
{
    //音乐的管理者
    private GameObject InnerMgr;
    //音乐组件管理队列
    private List<AudioSource> InnerMacdonaldApart;
    //音乐组件默认容器最大值  
    private int RoeTruck= 25;
    public InnerInventApart(ChileElk audioMgr)
    {
        InnerMgr = audioMgr.gameObject;
        CapeInnerInventApart();
    }
  
    /// <summary>
    /// 初始化队列
    /// </summary>
    private void CapeInnerInventApart()
    {
        InnerMacdonaldApart = new List<AudioSource>();
        for(int i = 0; i < RoeTruck; i++)
        {
            DewInnerInventFitEdgeElk();
        }
    }
    /// <summary>
    /// 给音乐的管理者添加音频组件，同时组件加入队列
    /// </summary>
    private AudioSource DewInnerInventFitEdgeElk()
    {
        AudioSource audio = InnerMgr.AddComponent<AudioSource>();
        InnerMacdonaldApart.Add(audio);
        return audio;
    }
    /// <summary>
    /// 获取一个音频组件
    /// </summary>
    /// <param name="audioMgr"></param>
    /// <returns></returns>
    public AudioSource AgeInnerMacdonald()
    {
        if (InnerMacdonaldApart.Count > 0)
        {
            AudioSource audio = InnerMacdonaldApart.Find(t => !t.isPlaying);
            if (audio)
            {
                InnerMacdonaldApart.Remove(audio);
                return audio;
            }
            //队列中没有了，需额外添加
            return DewInnerInventFitEdgeElk();
            //直接返回队列中存在的组件
            //return AudioComponentQueue.Dequeue();
        }
        else
        {
            //队列中没有了，需额外添加
            return  DewInnerInventFitEdgeElk();
        }
    }
    /// <summary>
    /// 没有被使用的音频组件返回给队列
    /// </summary>
    /// <param name="audio"></param>
    public void DyHueInnerMacdonald(AudioSource audio)
    {
        if (InnerMacdonaldApart.Contains(audio)) return;
        if (InnerMacdonaldApart.Count >= RoeTruck)
        {
            GameObject.Destroy(audio);
            //Debug.Log("删除组件");
        }
        else
        {
            audio.clip = null;
            InnerMacdonaldApart.Add(audio);
        }

        //Debug.Log("队列长度是" + AudioComponentQueue.Count);
    }
    
}
