using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TraditionExercise : MonoBehaviour
{
[UnityEngine.Serialization.FormerlySerializedAs("ferverTransitionAnimator")]    public Animator DecadeStrongholdFaithful;
[UnityEngine.Serialization.FormerlySerializedAs("OnLoadingAniFinish")]    public Action ToConcertAgoWalker;
    public void OnFercverAniOver()
    {
        Debug.Log("ferver animation over");
        BarelyIon.ToEntireDutyDiscWalker?.Invoke();
    }

    // 供 Ferver 过渡动画中段关键帧调用：提前清场
    public void OnFerverPreClear()
    {
        Debug.Log("ferver pre-clear");
        BarelyIon.ToEntirePreMaizePromote?.Invoke();
    }
    public void OnLoadingAniOver()
    {
        Debug.Log("loading animation over");
        ToConcertAgoWalker?.Invoke();
    }
    public void WifeWifeConcertAgoWalker()
    {
        DecadeStrongholdFaithful.enabled = true;
        DecadeStrongholdFaithful.Rebind();
        DecadeStrongholdFaithful.Update(0f);
        if (string.IsNullOrEmpty("LoadingAni"))
        {
            DecadeStrongholdFaithful.Play(0, 0, 0f);
        }
        else
        {
            DecadeStrongholdFaithful.Play("LoadingAni", 0, 0f);
        }
    }
     public void OnEnterGame()
    {
        Debug.Log("loading OnEnterGame");
        BarelyIon.ToStarkClan?.Invoke();
    }    
}
