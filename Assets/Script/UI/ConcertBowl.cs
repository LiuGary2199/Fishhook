using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConcertBowl : ShedUIHobby
{
    public void Start()
    {
        m_TraditionExercise.ToConcertAgoWalker += OnLoadingAniFinish;
    }
    public void OnLoadingAniFinish()
    {
       
    }
[UnityEngine.Serialization.FormerlySerializedAs("m_AnimationCallback")]    public TraditionExercise  m_TraditionExercise;
    public override void Display(object uiFormParams)
    {
        base.Display(uiFormParams);
        m_TraditionExercise.WifeWifeConcertAgoWalker();
    }
}
